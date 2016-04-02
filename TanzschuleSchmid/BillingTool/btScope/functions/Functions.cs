// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Collections.Generic;
using System.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
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
		public void FinalizeAndSave_NewBelegData(BelegData data)
		{
			if (!data.IsValid)
				throw new InvalidOperationException($"The {data} is invalid and can not be saved.");


			data.Recalculate_BetragBrutto();
			data.Recalculate_BetragNetto();

			foreach (var posten in data.Postens)
			{
				posten.Posten.AnzahlGekauft++;
			}

			data.ZuletztGeändert = data.Datum = DateTime.Now;
			data.Nummer = data.DataSet.Configurations.LastBelegNummer+1;
			data.UmsatzZähler = data.DataSet.Configurations.Umsatzzähler + data.BetragBrutto;


			data.DataSet.Configurations.Umsatzzähler = data.UmsatzZähler;
			data.DataSet.Configurations.LastBelegNummer = data.Nummer;

			data.DataSet.SaveAnabolic();
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