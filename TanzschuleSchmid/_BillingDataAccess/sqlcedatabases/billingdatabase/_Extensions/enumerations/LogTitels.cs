// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>log titles.</summary>
	public enum LogTitels
	{
		/// <summary>An unhandled <see cref="Exception" /> occured.</summary>
		[Description("Unhandled exception")] UnhandledException,
		/// <summary>A new database was created.</summary>
		[Description("Datenbank erstellt")] DatenbankErstellt,
		/// <summary>A new <see cref="BelegData" /> was created and saved to <see cref="BillingDatabase" />.</summary>
		[Description("Neuer Beleg, erstellt")] NewBelegData_Saved,
		/// <summary>A new <see cref="BelegData" /> was cancelled.</summary>
		[Description("Neuer Beleg, verworfen")] NewBelegData_Canceled,

	}
}