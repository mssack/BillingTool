// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Markup;
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
			UmsatzZähler = letzterUmsatzZähler == null || letzterUmsatzZähler.Rows.Count==0 || letzterUmsatzZähler.Columns.Count ==0 ||  letzterUmsatzZähler.Rows[0][0] == DBNull.Value ?0:(decimal)letzterUmsatzZähler.Rows[0][0];
		}

		/// <summary>Returns an identifier for the database row.</summary>
		public override string ToString()
		{
			return $"[Finanzbucheintrag, Beleg Nr.={BelegNummer}]";
		}
		#endregion


		[DependsOn(nameof(BelegAussteller))]
		[DependsOn(nameof(LeistungsBeschreibung))]
		public bool IsValid => !string.IsNullOrEmpty(BelegAussteller)  &&!string.IsNullOrEmpty(LeistungsBeschreibung);

		[DependsOn(nameof(BetragBrutto))]
		[DependsOn(nameof(Steuersatz))]
		public decimal BetragNetto
		{
			get { return BetragBrutto/(1 + Steuersatz/100); }
			set { BetragBrutto = value*(1 + Steuersatz/100); }
		}
	}
}