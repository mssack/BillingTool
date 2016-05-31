// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.IO;
using BillingTool.btScope.configuration;






// ReSharper disable InconsistentNaming

namespace BillingTool.btScope.versioning.updates
{
	internal class RC66_To_Next_Updater : UpdateBase
	{
		public static string KasseneinstellungenFilePath = Path.Combine(ConfigFile_LocalSettings.FileName.Directory.FullName, "Kasseneinstellungen.txt");


		#region Overrides/Interfaces
		protected override string TargetDataVersion => null;

		protected override void RunBackup()
		{
			AddBackupFile(KasseneinstellungenFilePath);
			Rename_File(Path.Combine(KasseneinstellungenFilePath), ConfigFile_LocalSettings.FileName.FullName);
			Rename_ParameterField_InFile(ConfigFile_LocalSettings.FileName.FullName, "Default_PrinterName", "DefaultPrinter");

		}
		#endregion
	}
}