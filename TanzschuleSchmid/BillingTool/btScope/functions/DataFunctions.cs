// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-17</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope.configuration.commandLine;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.DataFunctions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class DataFunctions
	{
		private static DataFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static DataFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new DataFunctions());
				}
			}
		}

		private DataFunctions()
		{
		}

		/// <summary>Appends a new <see cref="MailedBeleg" /> to the <paramref name="data" />.</summary>
		public MailedBeleg CreateNewMailBeleg(BelegData data, string targetMailAddress)
		{
			var mail = data.DataSet.MailedBelege.NewRow();
			mail.BelegData = data;
			mail.TargetMailAddress = targetMailAddress;
			data.DataSet.MailedBelege.Add(mail);
			return mail;
		}

		/// <summary>Appends a new <see cref="PrintedBeleg" /> to the <paramref name="data" />.</summary>
		public PrintedBeleg CreateNewPrintBeleg(BelegData data)
		{
			var printedBeleg = data.DataSet.PrintedBelege.NewRow();
			printedBeleg.BelegData = data;
			data.DataSet.PrintedBelege.Add(printedBeleg);
			return printedBeleg;
		}

		/// <summary>Opens a window for the user, using the <see cref="Bt.Config" />, to allow a creation of an new <see cref="BelegData" />.</summary>
		public BelegData New_BelegData_FromConfiguration()
		{
			Bt.EnsureInitialization();
			var db = Bt.Db.Billing;

			var belegData = db.BelegDaten.NewRow();

			belegData.Copy_From_But_Ignore(Bt.Config.Merged.NewBelegData,
				BelegDatenTable.IdCol,
				BelegDatenTable.KassenIdCol,
				BelegDatenTable.DatumCol,
				BelegDatenTable.UmsatzZählerCol,
				BelegDatenTable.StornoBelegIdCol,
				BelegDatenTable.StateNameCol,
				BelegDatenTable.NummerCol,
				BelegDatenTable.BetragBruttoCol,
				BelegDatenTable.BetragNettoCol,
				BelegDatenTable.ZuletztGeändertCol,
				BelegDatenTable.PrintCountCol,
				BelegDatenTable.MailCountCol);

			belegData.KassenId = Bt.Config.File.KassenEinstellung.KassenId;
			belegData.UmsatzZähler = 0;
			db.BelegDaten.Add(belegData);


			foreach (var template in Bt.Config.CommandLine.NewBelegData.Postens)
			{
				var posten = GetPostenFromTemplate(db, template);

				var steuersatz = GetSteuersatzFromTemplate(db, template);

				var belegPosten = db.BelegPostens.NewRow();
				belegPosten.Steuersatz = steuersatz;
				belegPosten.Data = belegData;
				belegPosten.Posten = posten;
				belegPosten.Anzahl = template.Anzahl;
				db.BelegPostens.Add(belegPosten);
			}
			belegData.Recalculate_BetragBrutto();
			belegData.Recalculate_BetragNetto();


			if (Bt.Config.Merged.NewBelegData.PrintBeleg)
				CreateNewPrintBeleg(belegData);
			if (Bt.Config.Merged.NewBelegData.SendBeleg)
				CreateNewMailBeleg(belegData, Bt.Config.CommandLine.NewBelegData.SendBelegTarget);

			return belegData;
		}

		/// <summary>Finalizes the <see cref="BelegData" /> and then saves it to the database.</summary>
		public void Save_NewBelegData(BelegData item, bool approvedByUser = true)
		{
			if (item.State != BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {item} is already fixed it actual state {item.State} is.");
			if (!item.IsValid)
				throw new InvalidOperationException($"The {item} is invalid and can not be saved.");


			item.Recalculate_BetragBrutto();
			item.Recalculate_BetragNetto();

			foreach (var posten in item.Postens)
			{
				posten.Posten.AnzahlGekauft++;
			}

			item.ZuletztGeändert = item.Datum = DateTime.Now;
			item.Nummer = item.DataSet.Configurations.LastBelegNummer + 1;
			item.UmsatzZähler = item.DataSet.Configurations.Umsatzzähler + item.BetragBrutto;
			item.State = approvedByUser ? BelegDataStates.Approved_ByUser : BelegDataStates.Approved_ByApplication;



			item.DataSet.Configurations.Umsatzzähler = item.UmsatzZähler;
			item.DataSet.Configurations.LastBelegNummer = item.Nummer;

			item.DataSet.SaveAnabolic();
			item.DataSet.AcceptChanges();

			Bt.Logging.New(LogTitels.NewBelegData_Saved, $"The {item} has been created and stored to the {nameof(BillingDatabase)}.");
		}

		/// <summary>Cancels a new <see cref="BelegData" /> <paramref name="item" />.</summary>
		public void Cancle_NewBelegData(BelegData item)
		{
			var canceledItem = item.ToString();
			item.DataSet.RejectChanges();
			Bt.Logging.New(LogTitels.NewBelegData_Canceled, $"The {canceledItem} is canceled and is not created.");
		}

		private Posten GetPostenFromTemplate(BillingDatabase db, CommandLine_BelegPostenTemplate template)
		{
			var posten = db.Postens.FindOrLoad_By_NameAndPreis(template.Name, template.BetragBrutto);
			if (posten == null)
			{
				posten = db.Postens.NewRow();
				posten.Name = template.Name;
				posten.PreisBrutto = template.BetragBrutto;
				db.Postens.Add(posten);
			}
			return posten;
		}

		private Steuersatz GetSteuersatzFromTemplate(BillingDatabase db, CommandLine_BelegPostenTemplate template)
		{
			var steuersatz = db.Steuersätze.FindOrLoad_By_Percent(template.Steuer);
			if (steuersatz == null)
			{
				steuersatz = db.Steuersätze.NewRow();
				steuersatz.Percent = template.Steuer;
				db.Configurations.LastSteuersatzKürzel = (char) (db.Configurations.LastSteuersatzKürzel + 1);
				steuersatz.Kürzel = db.Configurations.LastSteuersatzKürzel.ToString();
				db.Steuersätze.Add(steuersatz);
			}
			return steuersatz;
		}
	}
}