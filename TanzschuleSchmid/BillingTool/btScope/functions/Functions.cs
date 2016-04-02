// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Collections.Generic;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope.configuration.commandLine;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.Functions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Functions : Base
	{
		private static Functions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Functions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Functions());
				}
			}
		}

		private Functions()
		{
		}


		/// <summary>Sets the application exit code by setting property <see cref="Environment.ExitCode" /> according.</summary>
		public void SetExitCode(ExitCodes code)
		{
			Environment.ExitCode = (int) code;
		}

		/// <summary>Opens a window for the user, using the <see cref="Bt.Config" />, to allow a creation of an new <see cref="BelegData" />.</summary>
		public BelegData New_BelegData_FromConfiguration()
		{
			Bt.Db.EnsureConnectivity();
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

			belegData.UmsatzZähler = 0;
			db.BelegDaten.Add(belegData);


			var linkedPosted = new List<BelegPosten>();
			foreach (var template in Bt.Config.CommandLine.NewBelegData.Postens)
			{
				var posten = GetPostenFromTemplate(db, template);

				var steuersatz = GetSteuersatzFromTemplate(db, template);

				var belegPosten = db.BelegPostens.NewRow();
				belegPosten.Steuersatz = steuersatz;
				belegPosten.Data = belegData;
				belegPosten.Posten = posten;
				belegPosten.Anzahl = template.Anzahl;
				linkedPosted.Add(belegPosten);
				db.BelegPostens.Add(belegPosten);
			}
			belegData.Recalculate_BetragBrutto();
			belegData.Recalculate_BetragNetto();

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