// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The available output types.</summary>
	[Serializable]
	public enum OutputFormats
	{
		/// <summary>the undefined PDF type, this is usually a programming failure.</summary>
		Unknown = 0,
		/// <summary>The default layout of this program version.</summary>
		Default,
		/// <summary>The standard layout of a Bon version 1.</summary>
		StandardBonV1 = 1001,
	}
}