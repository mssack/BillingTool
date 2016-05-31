// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class BelegDataFunctions : DataFunctionsBase<BelegData>
	{
		private static BelegDataFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BelegDataFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BelegDataFunctions());
				}
			}
		}

		private BelegDataFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid.</summary>
		protected override void FinalizeAction(BelegData item)
		{
			foreach (var posten in item.Postens)
			{
				posten.Steuersatz.LastUsedDate = DateTime.Now;
				posten.Posten.LastUsedDate = DateTime.Now;
				Bt.Data.BelegPosten.TryFinalize(posten);

			}
			foreach (var mailedBeleg in item.MailedBelege)
			{
				Bt.Data.MailedBeleg.TryFinalize(mailedBeleg);
			}
			foreach (var printedBeleg in item.PrintedBelege)
			{
				Bt.Data.PrintedBeleg.TryFinalize(printedBeleg);
			}


			item.State = BelegDataStates.Approved;


			var di = item.DataSet.Configurations.DataIntegrity;
			item.Recalculate_BetragBrutto();
			item.Recalculate_BetragNetto();



			if (!item.Typ.IsRecapBon())
			{
				item.UmsatzZähler = di.Umsatzzähler + item.BetragBrutto;
				di.Umsatzzähler = item.UmsatzZähler;
			}
			if (item.Typ == BelegDataTypes.MonatsBon)
			{
				di.MonatsBon_LastUsedBelegDataNumber = item.BonNummerBis;
				di.MonatsBon_LastTimeCreated = DateTime.Now;
			}
			if (item.Typ == BelegDataTypes.Storno)
				item.StornoBeleg.State = BelegDataStates.Storniert;


			item.Nummer = di.LastBelegNummer + 1;
			di.LastBelegNummer = item.Nummer;
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(BelegData item)
		{
			if (item.State != BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {item} state is not {nameof(BelegDataStates.Unknown)}.");
			if (!item.IsValid)
				throw new InvalidOperationException($"The {item} is invalid and can not be saved.");
			if (item.Typ == BelegDataTypes.Storno && !item.StornoBeleg.CanBeStorniert)
				throw new InvalidOperationException($"The {item.StornoBeleg} cannot be stornod.");
		}
		#endregion


		/// <summary>Creates a new <see cref="BelegData" />, using the <see cref="Bt.Config" />.</summary>
		public BelegData New_FromConfiguration()
		{
			Bt.EnsureInitialization();
			var db = Bt.Db.Billing;

			if (HasNonFinalizedRows)
				throw new NotFinalizedInstanceException();

			var newItem = db.BelegDaten.NewRow();

			newItem.Copy_From_But_Ignore(Bt.Config.Control.NewBelegData,
				BelegDatenTable.IdCol,
				BelegDatenTable.KassenIdCol,
				BelegDatenTable.DatumCol,
				BelegDatenTable.UmsatzZählerCol,
				BelegDatenTable.StornoBelegIdCol,
				BelegDatenTable.BonNummerVonCol,
				BelegDatenTable.BonNummerBisCol,
				BelegDatenTable.StateNumberCol,
				BelegDatenTable.NummerCol,
				BelegDatenTable.BetragBruttoCol,
				BelegDatenTable.BetragNettoCol,
				BelegDatenTable.CommentLastChangedCol,
				BelegDatenTable.PrintCountCol,
				BelegDatenTable.MailCountCol);

			newItem.KassenId = Bt.Config.LocalSettings.KassenId;
			newItem.UmsatzZähler = 0;
			db.BelegDaten.Add(newItem);

			if (Bt.Config.Control.NewBelegData.Postens != null)
				foreach (var template in Bt.Config.Control.NewBelegData.Postens)
				{
					var posten = Bt.Data.Posten.GetOrNew_FromTemplate(template);
					var steuersatz = Bt.Data.Steuersatz.GetOrNew_FromTemplate(template);

					Bt.Data.Steuersatz.TryFinalize(steuersatz);
					Bt.Data.Posten.TryFinalize(posten);

					Bt.Data.BelegPosten.New(newItem, template.Anzahl, posten, steuersatz);
				}
			newItem.Recalculate_BetragBrutto();
			newItem.Recalculate_BetragNetto();


			if (Bt.Config.Control.NewBelegData.PrintBeleg)
				Bt.Data.PrintedBeleg.New(newItem);
			if (Bt.Config.Control.NewBelegData.SendBelegTargets != null && Bt.Config.Control.NewBelegData.SendBelegTargets.Length != 0)
			{
				foreach (var mailTargets in Bt.Config.Control.NewBelegData.SendBelegTargets)
				{
					Bt.Data.MailedBeleg.New(newItem, mailTargets);
				}
			}


			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>Creates a new <see cref="BelegData" /> for storno the <paramref name="data" />.</summary>
		public BelegData New_Storno(BelegData data)
		{
			if (HasNonFinalizedRows)
				throw new NotFinalizedInstanceException();
			if (!data.CanBeStorniert)
				throw new InvalidOperationException($"The {data} cannot be stornod.");


			var newItem = data.DataSet.BelegDaten.NewRow();

			newItem.Typ = BelegDataTypes.Storno;
			newItem.KassenId = Bt.Config.LocalSettings.KassenId;
			newItem.KassenOperator = Bt.Config.Control.NewBelegData.KassenOperator;
			newItem.StornoBeleg = data;
			newItem.UmsatzZähler = 0;
			newItem.Table.Add(newItem);

			foreach (var belegPosten in data.Postens)
			{
				Bt.Data.BelegPosten.New(newItem, -belegPosten.Anzahl, belegPosten.Posten, belegPosten.Steuersatz);
			}

			newItem.Recalculate_BetragBrutto();
			newItem.Recalculate_BetragNetto();

			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>Creates a new <see cref="BelegData" /> for as an <see cref="BelegDataTypes.MonatsBon" />.</summary>
		public BelegData New_MonatsBeleg()
		{
			if (HasNonFinalizedRows)
				throw new NotFinalizedInstanceException();


			var newItem = Bt.Db.Billing.BelegDaten.NewRow();

			newItem.Typ = BelegDataTypes.MonatsBon;
			newItem.KassenId = Bt.Config.LocalSettings.KassenId;
			newItem.KassenOperator = Bt.Config.Control.NewBelegData.KassenOperator;
			newItem.UmsatzZähler = 0;
			newItem.BonNummerVon = Bt.Db.Billing.Configurations.DataIntegrity.MonatsBon_LastUsedBelegDataNumber == null ? 1 : Bt.Db.Billing.Configurations.DataIntegrity.MonatsBon_LastUsedBelegDataNumber + 1;
			newItem.BonNummerBis = Bt.Db.Billing.Configurations.DataIntegrity.LastBelegNummer;
			newItem.Table.Add(newItem);

			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>Updates the <see cref="BelegData.BetragBrutto" /> field.</summary>
		public void UpdateBetrag_Of_BelegData(BelegData data)
		{
			if (!data.Recalculate_BetragBrutto())
				data.RaisePropertyChanged(nameof(BelegData.BetragBrutto));
			if (!data.Recalculate_BetragNetto())
				data.RaisePropertyChanged(nameof(BelegData.BetragNetto));

			data.RaisePropertyChanged(nameof(BelegData.InvalidReason));
		}
	}
}