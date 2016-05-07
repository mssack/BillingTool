// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using CsWpfBase.Ev.Attributes;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The available output types.</summary>
	[Serializable]
	public enum BonLayouts
	{
		/// <summary>the undefined PDF type, this is usually a programming failure.</summary>
		[EnumDescription("Unbekannt")] Unknown = 0,
		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("Mailbon Version 1")] V1MailBon = 1001,
		/// <summary>Bon layout version 1.</summary>
		[EnumDescription("Druckbon Version 1")] V1PrintBon = 2001,
		/// <summary>Storno layout version 1.</summary>
		[EnumDescription("Storno Version 1")] V1StornoBon = 10001,
		
	}
}