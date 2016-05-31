// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.IO;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingTool.btScope.configuration;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingTool.btScope.versioning.updates
{
	internal class RC66_To_Next_Updater : UpdateBase
	{
		public static readonly string KasseneinstellungenFilePath = Path.Combine(ConfigFile_LocalSettings.FileName.Directory.FullName, "Kasseneinstellungen.txt");


		#region Overrides/Interfaces
		protected override string TargetDataVersion => null;

		protected override void RunUpdate()
		{



			Rename_ParameterField_InFile(KasseneinstellungenFilePath, "Default_PrinterName", "DefaultPrinter");
			Add_ParameterField_InFile(KasseneinstellungenFilePath, "DataVersion", Bt.Versioning.Build.Version.Name);
			Rename_File(Path.Combine(KasseneinstellungenFilePath), ConfigFile_LocalSettings.FileName.FullName);
			Add_Column(OutputFormatsTable.Cols.ImageQuality, "NOT NULL DEFAULT(100)");
			Add_Column(OutputFormatsTable.Cols.ImageScaling, "NOT NULL DEFAULT(4)");

		
			Router.ExecuteCommand($"SELECT * FROM {OutputFormatsTable.NativeName}").GetDiagnosticString().SaveAs_Utf8String(new FileInfo("test.txt").In_Desktop_Directory());
		}
		#endregion
	}
}