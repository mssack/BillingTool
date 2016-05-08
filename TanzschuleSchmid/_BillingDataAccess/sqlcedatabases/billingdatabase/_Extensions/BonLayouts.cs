// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using CsWpfBase.Ev.Attributes;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
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
	}



	/// <summary>Contains enumeration extensions.</summary>
	public static class BonLayoutExtensions
	{
		/// <summary>returns true if this type is one of the print layout types.</summary>
		public static bool IsPrintLayout(this BonLayouts layout) => (int) layout >= 10000 && (int) layout <= 19999;

		/// <summary>returns true if this type is one of the mail layout types.</summary>
		public static bool IsMailLayout(this BonLayouts layout) => (int) layout >= 20000 && (int) layout <= 29999;

		/// <summary>returns true if this type is one of the Storno layout types.</summary>
		public static bool IsStornoLayout(this BonLayouts layout) => (int) layout >= 30000 && (int) layout <= 39999;
	}
}