// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Attributes;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	//!!!!!!!!!!!!!!!!!!!!!!!
	//Important each Print layout have to be in Range between 10000 - 19999
	//Important each E-Mail layout have to be in Range between 20000 - 29999
	//Important each Storno layout have to be in Range between 30000 - 39999
	//!!!!!!!!!!!!!!!!!!!!!!!

	/// <summary>The possible <see cref="BonLayouts" /> types.</summary>
	public enum BonLayoutTypes
	{
		/// <summary>unknown type.</summary>
		[EnumDescription("Unbekannt")] Unknown = 1 << 30,
		/// <summary>druck layout type.</summary>
		[EnumDescription("Druck")] Print = 1 << 29,
		/// <summary>mail layout type.</summary>
		[EnumDescription("Mail")] Mail = 1 << 28,
		/// <summary>Storno layout type.</summary>
		[EnumDescription("Storno")] Storno = 1 << 27,
		/// <summary>Tagesbon layout type.</summary>
		[EnumDescription("Tagesbon")] TagesBon = 1 << 26,
		/// <summary>Monatsbon layout type.</summary>
		[EnumDescription("Monatsbon")] MonatsBon = 1 << 25,
		/// <summary>Jahresbon layout type.</summary>
		[EnumDescription("Jahresbon")] JahresBon = 1 << 24,
	}



	/// <summary>The available output types.</summary>
	[Serializable]
	public enum BonLayouts
	{
		/// <summary>the undefined PDF type, this is usually a programming failure.</summary>
		[EnumDescription("Unbekannt")] Unknown = BonLayoutTypes.Unknown + 1,


		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("Druck Bon V1")] V1PrintBon = BonLayoutTypes.Print + 1,

		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("E-Mail Bon V1")] V1MailBon = BonLayoutTypes.Mail + 1,

		/// <summary>Storno layout version 1.</summary>
		[EnumDescription("Storno Bon V1")] V1StornoBon = BonLayoutTypes.Storno + 1,



		/// <summary>Tagesbon layout version 1.</summary>
		[EnumDescription("Tages Bon V1")] V1TagesBon = BonLayoutTypes.TagesBon + 1,

		/// <summary>Monatsbon layout version 1.</summary>
		[EnumDescription("Monats Bon V1")] V1MonatsBon = BonLayoutTypes.MonatsBon + 1,

		/// <summary>Jahresbon layout version 1.</summary>
		/// s
		[EnumDescription("Jahres Bon V1")] V1JahresBon = BonLayoutTypes.JahresBon + 1,


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
		public static bool IsPrintLayout(this BonLayouts layout) => ((int)layout& (int)BonLayoutTypes.Print) == (int)BonLayoutTypes.Print;

		/// <summary>returns true if this type is one of the mail layout types.</summary>
		public static bool IsMailLayout(this BonLayouts layout) => ((int)layout & (int)BonLayoutTypes.Mail) == (int)BonLayoutTypes.Mail;

		/// <summary>returns true if this type is one of the Storno layout types.</summary>
		public static bool IsStornoLayout(this BonLayouts layout) => ((int)layout & (int)BonLayoutTypes.Storno) == (int)BonLayoutTypes.Storno;

		/// <summary>returns true if this type is one of the Tagesbon layout types.</summary>
		public static bool IsTagesBonLayout(this BonLayouts layout) => ((int)layout & (int)BonLayoutTypes.TagesBon) == (int)BonLayoutTypes.TagesBon;

		/// <summary>returns true if this type is one of the Monatsbon layout types.</summary>
		public static bool IsMonatsBonLayout(this BonLayouts layout) => ((int)layout & (int)BonLayoutTypes.MonatsBon) == (int)BonLayoutTypes.MonatsBon;

		/// <summary>returns true if this type is one of the Jahresbon layout types.</summary>
		public static bool IsJahresBonLayout(this BonLayouts layout) => ((int)layout & (int)BonLayoutTypes.JahresBon) == (int)BonLayoutTypes.JahresBon;




		/// <summary>returns true if this type is one of the Jahresbon layout types.</summary>
		public static BonLayoutTypes GetBonLayoutType(this BonLayouts layout)
		{
			var allOnes = Enum.GetValues(typeof(BonLayoutTypes)).OfType<BonLayoutTypes>().Select(x=>(int)x).Aggregate((l1, l2) => l1|l2);
			var lay = (int) layout;
			return (BonLayoutTypes) (lay & allOnes);
		}
	}
}