﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class Log
	{
		[DependsOn("TypeName")]
		public LogTypes Type
		{
			get
			{
				LogTypes result;
				return Enum.TryParse(TypeName, true, out result) ? result : LogTypes.Error;
			}
			set { TypeName = value.ToString(); }
		}
	}
}