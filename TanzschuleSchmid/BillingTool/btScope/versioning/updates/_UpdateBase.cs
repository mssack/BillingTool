// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using BillingDataAccess.sqlcedatabases.Router;
using BillingTool.btScope.configuration;
using BillingTool.Exceptions;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.btScope.versioning.updates
{
	internal abstract class UpdateBase : Base
	{
		public static UpdateBase GetUpdateForBuildVersion(BuildVersion rc)
		{
			if (rc.ActiveDevelopment <= 66)
				return new RC66_To_Next_Updater();


			return null;
		}

		private readonly HashSet<string> _backupFiles = new HashSet<string>();
		private readonly DirectoryInfo _backupDirectory;
		private SqlCeRouter _router;

		protected UpdateBase()
		{
			_backupDirectory = new DirectoryInfo(Path.Combine(CsGlobal.Storage.Private.Directory.FullName, "Update-Backups", GetType().Name));
		}


		#region Abstract
		protected abstract void RunUpdate();
		protected abstract string TargetDataVersion { get; }
		#endregion


		/// <summary>Gets or sets the Router.</summary>
		public SqlCeRouter Router
		{
			get
			{
				if (_router == null)
				{
					Bt.Config.LocalSettings.Load();
					if (!File.Exists(Bt.Config.LocalSettings.BillingDatabaseFilePath))
						return null;
					AddBackupFile(Bt.Config.LocalSettings.BillingDatabaseFilePath);
					_router = new SqlCeRouter(Bt.Config.LocalSettings.BillingDatabaseFilePath);
					_router.Open();
				}
				return _router;
			}
		}

		public void Run()
		{
			var result = CsGlobal.Message.Push("Sie führen ein Datenupdate aus. Nachdem Sie dieses Update ausgeführt haben, können Sie das Programm nicht mehr mit der alten Version benutzen. Wollen Sie fortfahren?", CsMessage.Types.Warning, "Datenupdate steht bevor", CsMessage.MessageButtons.YesNo);

			if (result == CsMessage.MessageResults.No)
				throw new BillingToolException(BillingToolException.Types.Invalid_DataVersion, "Der Updatevorgang wurde abgebrochen das Programm muss beendet werden.");

			try
			{
				RunUpdate();
				Update_Parameter_DataVersion();
				Bt.Config.LocalSettings.Load();
			}
			finally
			{
				_router?.Close();
			}
		}

		protected void AddBackupFile(string file)
		{
			if (_backupFiles.Contains(file))
				return;

			_backupDirectory.Create_If_NotExists();
			var fi = new FileInfo(file);
			fi.CopyTo(Path.Combine(_backupDirectory.FullName, fi.Name), true);
			_backupFiles.Add(file);
		}

		protected void Rename_File(string oldFileName, string newFileName)
		{
			var fi = new FileInfo(oldFileName);

			if (!fi.Exists)
				return;

			AddBackupFile(oldFileName);
			fi.MoveTo(newFileName);
		}

		protected void Rename_ParameterField_InFile(string file, string oldParamName, string newParamName)
		{
			var fi = new FileInfo(file);

			if (!fi.Exists)
				return;

			AddBackupFile(file);
			Regex.Replace(fi.LoadAs_UTF8String(), $"{oldParamName}.*?=", $"{newParamName} =").SaveAs_Utf8String(fi);
		}

		protected void Add_ParameterField_InFile(string file, string paramName, string value)
		{
			var fi = new FileInfo(file);

			if (!fi.Exists)
				return;

			AddBackupFile(file);
			(fi.LoadAs_UTF8String() + $"\r\n{paramName} = {value}").SaveAs_Utf8String(fi);
		}

		protected void Update_Parameter_DataVersion()
		{
			var fi = ConfigFile_LocalSettings.FileName;

			if (!fi.Exists)
				return;

			Regex.Replace(fi.LoadAs_UTF8String(), $"{nameof(ConfigFile_LocalSettings.DataVersion)}\\s*?=.*", $"{nameof(ConfigFile_LocalSettings.DataVersion)} = {TargetDataVersion ?? Bt.Versioning.Build.Version.Name}").SaveAs_Utf8String(fi);
		}
		protected void Add_Column(CsDbNativeDataColumnAttribute attribute, string modifier = "")
		{
			Router?.ExecuteNonQuery($"ALTER TABLE [{attribute.Table}] ADD COLUMN  [{attribute.Name}] {attribute.Type} {modifier}");
		}
	}
}