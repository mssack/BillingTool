// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.IO;
using BillingTool.btScope.configuration;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.tables;
using CsWpfBase.Global;






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
			Parameter.Rename(KasseneinstellungenFilePath, "Default_PrinterName", "DefaultPrinter");
			Parameter.Add(KasseneinstellungenFilePath, "DataVersion", Bt.Versioning.Build.Version.Name);
			File.Rename(KasseneinstellungenFilePath, ConfigFile_LocalSettings.FileName.FullName);
			File.Remove(Path.Combine(CsGlobal.Storage.Private.Directory.FullName, "pc.txt"));
			File.Remove(Path.Combine(CsGlobal.Storage.Private.Directory.FullName, "NewBelegData.txt"));
			Database.Column_Add(OutputFormatsTable.Cols.ImageQuality, "NOT NULL DEFAULT(100)");
			Database.Column_Add(OutputFormatsTable.Cols.ImageScaling, "NOT NULL DEFAULT(4)");
			Database.Column_Add(MailedBelegeTable.Cols.Bcc, null);
		}
		#endregion
	}
}