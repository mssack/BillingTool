// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

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


		/// <summary>Creates a new <see cref="OutputFormat"/>.</summary>
		public OutputFormat New_OutputFormat()
		{
			var row = Bt.Db.Billing.OutputFormats.NewRow();
			row.Id = Guid.NewGuid();
			row.CreationDate = DateTime.Now;
			row.BonLayout = BonLayouts.V1MailBon;
			row.Name = "Neues Layout";
			row.Table.Add(row);
			return row;
		}

		/// <summary>Creates a new <see cref="BelegData"/>, using the <see cref="Bt.Config" />.</summary>
		public BelegData New_BelegData_From_Configuration()
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
				BelegDatenTable.CommentLastChangedCol,
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
				New_PrintBeleg_For_BelegData(belegData);
			if (Bt.Config.Merged.NewBelegData.SendBeleg)
				New_MailedBeleg_For_BelegData(belegData, Bt.Config.CommandLine.NewBelegData.SendBelegTarget);

			return belegData;
		}

		/// <summary>Creates a new <see cref="BelegData" /> for storno the <paramref name="data" />.</summary>
		public BelegData New_StornoBelegData_From_BelegData(BelegData data)
		{
			if (!data.CanBeStorniert)
				throw new InvalidOperationException($"The {data} cannot be stornod.");

			var stornoBeleg = data.DataSet.BelegDaten.NewRow();

			stornoBeleg.Typ = BelegDataTypes.Storno;
			stornoBeleg.KassenId = Bt.Config.File.KassenEinstellung.KassenId;
			stornoBeleg.KassenOperator = Bt.Config.Merged.NewBelegData.KassenOperator;
			stornoBeleg.StornoBeleg = data;
			stornoBeleg.UmsatzZähler = 0;

			stornoBeleg.Table.Add(stornoBeleg);

			foreach (var belegPosten in data.Postens)
			{
				var stornoPosten = belegPosten.Table.NewRow();

				stornoPosten.SteuersatzId = belegPosten.SteuersatzId;
				stornoPosten.Data = stornoBeleg;
				stornoPosten.Anzahl = -belegPosten.Anzahl;
				stornoPosten.Posten = belegPosten.Posten;

				stornoPosten.Table.Add(stornoPosten);
			}

			stornoBeleg.Recalculate_BetragBrutto();
			stornoBeleg.Recalculate_BetragNetto();

			return stornoBeleg;
		}

		/// <summary>Finalizes the <see cref="BelegData" /> and then saves it to the database.</summary>
		public void Save_New_BelegData(BelegData item, bool approvedByUser = true)
		{
			if (item.State != BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {item} is already fixed it actual state {item.State} is.");
			if (!item.IsValid)
				throw new InvalidOperationException($"The {item} is invalid and can not be saved.");
			if (item.Typ == BelegDataTypes.Storno && !item.StornoBeleg.CanBeStorniert)
				throw new InvalidOperationException($"The {item.StornoBeleg} cannot be stornod.");


			item.Recalculate_BetragBrutto();
			item.Recalculate_BetragNetto();

			foreach (var posten in item.Postens)
			{
				posten.Posten.AnzahlGekauft = posten.Posten.AnzahlGekauft + posten.Anzahl;
				if (posten.Anzahl < 0)
					posten.Posten.AnzahlStorniert = posten.Posten.AnzahlStorniert + -posten.Anzahl;

				posten.Posten.LastUsedDate = DateTime.Now;
				posten.Steuersatz.LastUsedDate = DateTime.Now;
			}

			item.Nummer = item.DataSet.Configurations.LastBelegNummer + 1;
			item.UmsatzZähler = item.DataSet.Configurations.Umsatzzähler + item.BetragBrutto;
			item.State = approvedByUser ? BelegDataStates.Approved_ByUser : BelegDataStates.Approved_ByApplication;

			item.DataSet.Configurations.Umsatzzähler = item.UmsatzZähler;
			item.DataSet.Configurations.LastBelegNummer = item.Nummer;

			if (item.Typ == BelegDataTypes.Storno)
			{
				item.StornoBeleg.State = BelegDataStates.Storno;
			}

			item.DataSet.SaveAnabolic();
			item.DataSet.AcceptChanges();

			Bt.Logging.New(LogTitels.NewBelegData_Saved, $"The {item} has been created and stored to the {nameof(BillingDatabase)}.");
		}

		/// <summary>Cancels a new <see cref="BelegData" /> <paramref name="item" />.</summary>
		public void Cancle_New_BelegData(BelegData item)
		{
			var canceledItem = item.ToString();
			item.DataSet.RejectChanges();
			Bt.Logging.New(LogTitels.NewBelegData_Canceled, $"The {canceledItem} is canceled and is not created.");
		}




		/// <summary>Appends a new <see cref="MailedBeleg" /> to the <paramref name="data" />.</summary>
		public MailedBeleg New_MailedBeleg_For_BelegData(BelegData data, string targetMailAddress)
		{
			var mail = data.DataSet.MailedBelege.NewRow();
			mail.BelegData = data;
			mail.TargetMailAddress = targetMailAddress;
			mail.Betreff = data.DataSet.Configurations.Default_MailBetreff;
			mail.Text = data.DataSet.Configurations.Default_MailText;
			mail.OutputFormat = mail.DataSet.OutputFormats.Default_MailFormat;
			data.DataSet.MailedBelege.Add(mail);
			return mail;
		}


		/// <summary>Appends a new <see cref="PrintedBeleg" /> to the <paramref name="data" />.</summary>
		public PrintedBeleg New_PrintBeleg_For_BelegData(BelegData data)
		{
			var printedBeleg = data.DataSet.PrintedBelege.NewRow();
			printedBeleg.BelegData = data;
			printedBeleg.PrinterDevice = Bt.Config.File.KassenEinstellung.PrinterName;
			printedBeleg.OutputFormat = printedBeleg.DataSet.OutputFormats.Default_PrintFormat;
			data.DataSet.PrintedBelege.Add(printedBeleg);
			return printedBeleg;
		}



		private Posten GetPostenFromTemplate(BillingDatabase db, CommandLine_BelegPostenTemplate template)
		{
			var posten = db.Postens.FindOrLoad_By_NameAndPreis(template.Name, template.BetragBrutto);
			if (posten == null)
			{
				posten = db.Postens.NewRow();
				posten.CreationDate = DateTime.Now;
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
				steuersatz.CreationDate = DateTime.Now;
				steuersatz.Percent = template.Steuer;
				db.Configurations.LastSteuersatzKürzel = (char) (db.Configurations.LastSteuersatzKürzel + 1);
				steuersatz.Kürzel = db.Configurations.LastSteuersatzKürzel.ToString();
				db.Steuersätze.Add(steuersatz);
			}
			return steuersatz;
		}
	}
}