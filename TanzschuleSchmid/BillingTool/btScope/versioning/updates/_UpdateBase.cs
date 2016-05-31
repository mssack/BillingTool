// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using System.IO;
using System.Text.RegularExpressions;
using BillingTool.btScope.configuration;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace BillingTool.btScope.versioning.updates
{
	internal abstract class UpdateBase : Base
	{
		public static UpdateBase GetUpdateForRc(ReleaseCandidate rc)
		{
			if (rc.ActiveDevelopment <= 66)
				return new RC66_To_Next_Updater();


			return null;
		}

		private readonly DirectoryInfo _backupDirectory;

		protected UpdateBase()
		{
			_backupDirectory = new DirectoryInfo(Path.Combine(CsGlobal.Storage.Private.Directory.FullName, "Update-Backups", GetType().Name));
		}


		#region Abstract
		protected abstract void RunBackup();
		protected abstract string TargetDataVersion { get; }
		#endregion


		public void Run()
		{
			RunBackup();
			Update_Parameter_DataVersion();
		}

		protected void AddBackupFile(string file)
		{
			_backupDirectory.Create_If_NotExists();
			File.Copy(file, Path.Combine(_backupDirectory.FullName, file), true);
		}

		protected void Rename_File(string oldFileName, string newFileName)
		{
			AddBackupFile(oldFileName);
			File.Move(oldFileName, newFileName);
		}

		protected void Rename_ParameterField_InFile(string file, string oldParamName, string newParamName)
		{
			var fi = new FileInfo(file);
			Regex.Replace(fi.LoadAs_UTF8String(), $"{oldParamName}.*?=", $"{newParamName} =").SaveAs_Utf8String(fi);
		}
		protected void Update_Parameter_DataVersion()
		{
			var fi = ConfigFile_LocalSettings.FileName;
			Regex.Replace(fi.LoadAs_UTF8String(), $"{nameof(ConfigFile_LocalSettings.DataVersion)}\\s*?=.*", $"{nameof(ConfigFile_LocalSettings.DataVersion)} = {(TargetDataVersion??Bt.Versioning.BuildDetails.Name)}").SaveAs_Utf8String(fi);
		}
	}
}