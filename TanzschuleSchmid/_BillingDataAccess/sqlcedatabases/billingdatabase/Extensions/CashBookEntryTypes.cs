// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.Extensions
{
	/// <summary>Describes the <see cref="CashBookEntry" /> type.</summary>
	[Serializable]
	public enum CashBookEntryTypes : byte
	{
		/// <summary>The type is not known by this enumeration.</summary>
		[Description("Unbekannt")] Unknown = 0,
		/// <summary>Money exchanged cash.</summary>
		[Description("Bar")] BarUmsatz = 11,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Bankomat")] BankomatUmsatz = 12,
		/// <summary>Beleg reversal.</summary>
		[Description("Storno")] Storno = 100,
	}
}