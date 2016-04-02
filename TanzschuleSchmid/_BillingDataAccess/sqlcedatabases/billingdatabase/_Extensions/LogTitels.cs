// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.ComponentModel;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	public enum LogTitels
	{
		[Description("Unhandled exception")]
		UnhandledException,
		[Description("Datenbank erstellt")]
		DatenbankErstellt,
		[Description("Finanzbucheintrag erstellt")]
		BelegDatenErstellt,
		[Description("Finanzbucheintrag Erstellung abgebrochen")]
		FinanzbucheintragAbgebrochen,

	}
}