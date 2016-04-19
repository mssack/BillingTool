// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Windows.Markup;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class BelegPosten
	{


		/// <summary>The calculated brutto amount. Calculation formula: (<see cref="Anzahl" />*<see cref="Posten" />)</summary>
		[DependsOn(nameof(Anzahl))]
		[DependsOn(nameof(Posten))]
		public decimal BetragBrutto => Anzahl*Posten.PreisBrutto;

		/// <summary>The calculated netto amount. Calculation formula: ((<see cref="Anzahl" />*<see cref="BetragBrutto" />)/(1+<see cref="Steuersatz" />/100))</summary>
		[DependsOn(nameof(Anzahl))]
		[DependsOn(nameof(Steuersatz))]
		public decimal BetragNetto => Anzahl*Posten.PreisBrutto/(1 + Steuersatz.Percent/100);
	}
}