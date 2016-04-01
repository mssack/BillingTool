// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows

{
	partial class CashBookEntry
	{
		#region Overrides/Interfaces
		/// <summary>On row creation this method will be executed. So a new row will always have the highest reference number</summary>
		public override void ApplyExtendedDefaults()
		{
			KassenId = 1;
			var highestBelegNummer = DbProxy.ExecuteCommand($"SELECT MAX({CashBookTable.BelegNummerCol}) FROM {CashBookTable.NativeName}").Rows[0][0];
			var letzterUmsatzZähler = DbProxy.ExecuteCommand($"SELECT [{CashBookTable.UmsatzZählerCol}] FROM {CashBookTable.NativeName} WHERE [{CashBookTable.BelegNummerCol}] = '{highestBelegNummer}'");
			BelegNummer = (highestBelegNummer == DBNull.Value ? 0 : (int) highestBelegNummer) + 1;
			UmsatzZähler = letzterUmsatzZähler == null || letzterUmsatzZähler.Rows.Count == 0 || letzterUmsatzZähler.Columns.Count == 0 || letzterUmsatzZähler.Rows[0][0] == DBNull.Value ? 0 : (decimal) letzterUmsatzZähler.Rows[0][0];
		}

		/// <summary>Returns an identifier for the database row.</summary>
		public override string ToString()
		{
			return $"[Finanzbucheintrag, Beleg Nr.={BelegNummer}]";
		}
		#endregion


		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(KassenOperator))]
		[DependsOn(nameof(LeistungsBeschreibung))]
		[DependsOn(nameof(TypName))]
		public bool IsValid => !string.IsNullOrEmpty(KassenOperator) && !string.IsNullOrEmpty(LeistungsBeschreibung) && !string.IsNullOrEmpty(TypName) && Typ != CashBookEntryTypes.Unknown;


		/// <summary>A calculated property consisting of column properties <see cref="BetragBrutto" /> and <see cref="Steuersatz" />.</summary>
		[DependsOn(nameof(BetragBrutto))]
		[DependsOn(nameof(Steuersatz))]
		public decimal BetragNetto
		{
			get { return BetragBrutto/(1 + Steuersatz/100); }
			set { BetragBrutto = value*(1 + Steuersatz/100); }
		}

		/// <summary>The wrapper property for column property <see cref="TypName" />.</summary>
		[DependsOn(nameof(TypName))]
		public CashBookEntryTypes Typ
		{
			get
			{
				CashBookEntryTypes val;
				if (Enum.TryParse(TypName, true, out val))
					return val;
				return CashBookEntryTypes.Unknown;
			}
			set { TypName = value.ToString(); }
		}
	}
}