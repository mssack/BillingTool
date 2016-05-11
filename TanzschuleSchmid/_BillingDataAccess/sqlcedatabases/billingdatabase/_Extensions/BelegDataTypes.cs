// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>Describes the <see cref="BelegData" /> types.</summary>
	[Serializable]
	public enum BelegDataTypes : byte
	{
		/// <summary>The type is not known by this enumeration. This is not a valid type.</summary>
		[EnumDescription("Unbekannt")] Unknown = 0,
		/// <summary>The type is currently not defined. The type definition is pending.</summary>
		[EnumDescription("Undefined")] Undefined = 10,
		/// <summary>Money exchanged cash.</summary>
		[EnumDescription("Bar")] Bar = 11,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[EnumDescription("Bankomat")] Bankomat = 12,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[EnumDescription("Kreditkarte")] Kreditkarte = 13,
		/// <summary>Beleg reversal.</summary>
		[EnumDescription("Storno Beleg")] Storno = 100,
	}
}