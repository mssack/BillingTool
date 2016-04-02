// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>Describes the <see cref="BelegData" /> types.</summary>
	[Serializable]
	public enum BelegDataTypes : byte
	{
		/// <summary>The type is not known by this enumeration. This is not a valid typ.</summary>
		[Description("Unbekannt")] Unknown = 0,
		/// <summary>Money exchanged cash.</summary>
		[Description("Bar")] Bar = 11,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Bankomat")] Bankomat = 12,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Kreditkarte")] Kreditkarte = 12,
		/// <summary>Beleg reversal.</summary>
		[Description("Storno")] Storno = 100,
	}
}