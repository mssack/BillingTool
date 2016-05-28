// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.ComponentModel;


namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>Describes the possible types for a BelegData.</summary>
	[Serializable]
	public enum BelegDataTypes
	{
		/// <summary>The type is not known by this enumeration. This is not a valid type.</summary>
		[Description("Unbekannt")] Unknown = 0,
		/// <summary>The type is currently not defined. The type definition is pending.</summary>
		[Description("Undefined")] Undefined = 10,


		/// <summary>Money exchanged cash.</summary>
		[Description("Bar")] Bar = 11,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Bankomat")] Bankomat = 12,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Kreditkarte")] Kreditkarte = 13,


		/// <summary>Beleg reversal.</summary>
		[Description("Storno")] Storno = 100,


		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Tagesbon")] TagesBon = 1001,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Monatsbon")] MonatsBon = 1002,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[Description("Jahresbon")] JahresBon = 1003,
	}
}