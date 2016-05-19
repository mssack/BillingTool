// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>Describes the <see cref="BelegData" /> types.</summary>
	[Serializable]
	public enum BelegDataTypes
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


		/// <summary>Money exchanged with cash dispenser.</summary>
		[EnumDescription("Tagesbon")] TagesBon = 1001,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[EnumDescription("Monatsbon")] MonatsBon = 1002,
		/// <summary>Money exchanged with cash dispenser.</summary>
		[EnumDescription("Jahresbon")] JahresBon = 1003,
	}



	/// <summary>Contains enumeration extensions.</summary>
	public static class BelegDataTypesExtensions
	{
		/// <summary>returns true if this type is can be storniert.</summary>
		public static bool CanBeStorniert(this BelegDataTypes type) => type == BelegDataTypes.Bar || type == BelegDataTypes.Bankomat || type == BelegDataTypes.Kreditkarte;

		/// <summary>
		///     returns true if this type is one of the following: <see cref="BelegDataTypes.TagesBon" />, <see cref="BelegDataTypes.MonatsBon" />,
		///     <see cref="BelegDataTypes.JahresBon" />.
		/// </summary>
		public static bool IsZeitBon(this BelegDataTypes type) => type == BelegDataTypes.TagesBon || type == BelegDataTypes.MonatsBon || type == BelegDataTypes.JahresBon;
		

	}
}