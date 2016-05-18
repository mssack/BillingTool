// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using CsWpfBase.Ev.Attributes;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	//!!!!!!!!!!!!!!!!!!!!!!!
	//Important each Print layout have to be in Range between 10000 - 19999
	//Important each E-Mail layout have to be in Range between 20000 - 29999
	//Important each Storno layout have to be in Range between 30000 - 39999
	//!!!!!!!!!!!!!!!!!!!!!!!


	/// <summary>The available output types.</summary>
	[Serializable]
	public enum BonLayouts
	{
		/// <summary>the undefined PDF type, this is usually a programming failure.</summary>
		[EnumDescription("Unbekannt")] Unknown = 0,


		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("Druck Bon V1")] V1PrintBon = 10001,

		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("E-Mail Bon V1")] V1MailBon = 20001,

		/// <summary>Storno layout version 1.</summary>
		[EnumDescription("Storno Bon V1")] V1StornoBon = 30001,



		/// <summary>Tagesbon layout version 1.</summary>
		[EnumDescription("Tages Bon V1")] V1TagesBon = 41001,

		/// <summary>Monatsbon layout version 1.</summary>
		[EnumDescription("Monats Bon V1")] V1MonatsBon = 42001,

		/// <summary>Jahresbon layout version 1.</summary>
		/// s
		[EnumDescription("Jahres Bon V1")] V1JahresBon = 43001,

		
}



	/// <summary>Contains enumeration extensions.</summary>
	public static class BonLayoutsExtensions
	{
		/// <summary>returns the default print Bon layout.</summary>
		public static BonLayouts Default_Print(this BonLayouts layout) => BonLayouts.V1PrintBon;

		/// <summary>returns the default mail Bon layout.</summary>
		public static BonLayouts Default_Mail(this BonLayouts layout) => BonLayouts.V1MailBon;

		/// <summary>returns the default Storno-Bon layout.</summary>
		public static BonLayouts Default_Storno(this BonLayouts layout) => BonLayouts.V1StornoBon;

		/// <summary>returns the default Tages-Bon layout.</summary>
		public static BonLayouts Default_Tag(this BonLayouts layout) => BonLayouts.V1TagesBon;

		/// <summary>returns the default Monats-Bon layout.</summary>
		public static BonLayouts Default_Monat(this BonLayouts layout) => BonLayouts.V1MonatsBon;

		/// <summary>returns the default Jahres-Bon layout.</summary>
		public static BonLayouts Default_Jahr(this BonLayouts layout) => BonLayouts.V1JahresBon;

		/// <summary>returns true if this type is one of the print layout types.</summary>
		public static bool IsPrintLayout(this BonLayouts layout) => (int) layout >= 10000 && (int) layout <= 19999;

		/// <summary>returns true if this type is one of the mail layout types.</summary>
		public static bool IsMailLayout(this BonLayouts layout) => (int) layout >= 20000 && (int) layout <= 29999;

		/// <summary>returns true if this type is one of the Storno layout types.</summary>
		public static bool IsStornoLayout(this BonLayouts layout) => (int) layout >= 30000 && (int) layout <= 39999;

		/// <summary>returns true if this type is one of the Tagesbon layout types.</summary>
		public static bool IsTagesBonLayout(this BonLayouts layout) => (int) layout >= 41000 && (int) layout <= 41999;

		/// <summary>returns true if this type is one of the Monatsbon layout types.</summary>
		public static bool IsMonatsBonLayout(this BonLayouts layout) => (int) layout >= 42000 && (int) layout <= 42999;

		/// <summary>returns true if this type is one of the Jahresbon layout types.</summary>
		public static bool IsJahresBonLayout(this BonLayouts layout) => (int) layout >= 43000 && (int) layout <= 43999;
	}
}