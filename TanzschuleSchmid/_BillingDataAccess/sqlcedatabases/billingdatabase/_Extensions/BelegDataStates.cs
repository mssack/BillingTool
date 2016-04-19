// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The possible states of the row <see cref="BelegData" />.</summary>
	public enum BelegDataStates
	{
		/// <summary>No state is applied.</summary>
		Unknown = 0,
		/// <summary>The <see cref="BelegData" /> was approved by an external application. The application didn't asked the user for approval.</summary>
		Approved_ByApplication = 1,
		/// <summary>The <see cref="BelegData" /> was approved by an the user.</summary>
		Approved_ByUser = 2,
		/// <summary>The <see cref="BelegData" /> was Storno.</summary>
		Storno = 3
	}



}