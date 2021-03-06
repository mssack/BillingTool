﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>The possible log types.</summary>
	public enum LogTypes
	{
		/// <summary>Used whenever the database contains a type which is not known by the enum.</summary>
		Undefined,
		/// <summary>A fatal error occurred. The application has to be closed.</summary>
		Fatal,
		/// <summary>An normal error occurred but the application is still alive.</summary>
		Error,
	}
}