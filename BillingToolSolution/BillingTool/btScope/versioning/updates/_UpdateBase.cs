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

		private readonly DirectoryInfo _backupDirectory;

		private readonly HashSet<string> _backupFiles = new HashSet<string>();
		private SqlCeRouter _router;

		protected UpdateBase()
		{
			Database = new WrapperDatabase(this);
			Parameter = new WrapperParameter(this);
			File = new WrapperFile(this);
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
					if (!System.IO.File.Exists(Bt.Config.LocalSettings.BillingDatabaseFilePath))
						return null;
					AddBackupFile(Bt.Config.LocalSettings.BillingDatabaseFilePath);
					_router = new SqlCeRouter(Bt.Config.LocalSettings.BillingDatabaseFilePath);
					_router.Open();
				}
				return _router;
			}
		}

		protected internal WrapperFile File { get; }
		protected internal WrapperParameter Parameter { get; }
		protected internal WrapperDatabase Database { get; }

		public void Run()
		{
			try
			{
				RunUpdate();
				Parameter.Update(ConfigFile_LocalSettings.FileName.FullName, nameof(ConfigFile_LocalSettings.DataVersion), TargetDataVersion ?? Bt.Versioning.Build.Version.Name);
				Bt.Config.LocalSettings.Load();
			}
			finally
			{
				_router?.Close();
			}
		}

		private void AddBackupFile(string file)
		{
			if (_backupFiles.Contains(file))
				return;

			_backupDirectory.Create_If_NotExists();
			var fi = new FileInfo(file);
			fi.CopyTo(Path.Combine(_backupDirectory.FullName, fi.Name), true);
			_backupFiles.Add(file);
		}



		protected internal class WrapperFile
		{
			private readonly UpdateBase _owner;

			public WrapperFile(UpdateBase owner)
			{
				_owner = owner;
			}

			public void Remove(string fileName)
			{
				var fi = new FileInfo(fileName);

				if (!fi.Exists)
					return;

				_owner.AddBackupFile(fileName);
				fi.Delete();
			}

			public void Rename(string oldFileName, string newFileName)
			{
				var fi = new FileInfo(oldFileName);

				if (!fi.Exists)
					return;

				_owner.AddBackupFile(oldFileName);
				fi.MoveTo(newFileName);
			}
		}



		protected internal class WrapperParameter
		{
			private readonly UpdateBase _owner;

			public WrapperParameter(UpdateBase owner)
			{
				_owner = owner;
			}

			public void Rename(string file, string oldParamName, string newParamName)
			{
				var fi = new FileInfo(file);

				if (!fi.Exists)
					return;

				_owner.AddBackupFile(file);
				Regex.Replace(fi.LoadAs_UTF8String(), $"{oldParamName}.*?=", $"{newParamName} =").SaveAs_Utf8String(fi);
			}

			public void Add(string file, string paramName, string value)
			{
				var fi = new FileInfo(file);

				if (!fi.Exists)
					return;

				_owner.AddBackupFile(file);
				(fi.LoadAs_UTF8String() + $"\r\n{paramName} = {value}").SaveAs_Utf8String(fi);

			}

			public void Update(string filename, string name, string value)
			{
				var fi = new FileInfo(filename);

				if (!fi.Exists)
					return;

				Regex.Replace(fi.LoadAs_UTF8String(), $"{name}\\s*?=.*", $"{name} = {value}").SaveAs_Utf8String(fi);
			}
		}



		protected internal class WrapperDatabase
		{
			private readonly UpdateBase _owner;

			public WrapperDatabase(UpdateBase owner)
			{
				_owner = owner;
			}

			public void Column_Add(CsDbNativeDataColumnAttribute attribute, string modifier = "")
			{
				_owner.Router?.ExecuteNonQuery($"ALTER TABLE [{attribute.Table}] ADD COLUMN  [{attribute.Name}] {attribute.Type} {modifier}");
			}
		}
	}
}