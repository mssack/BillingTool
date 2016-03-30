// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration._interfaces
{
	/// <summary>Describes all <see cref="CashBookEntry" /> configurations which can be done either by command line or configuration file.</summary>
	public interface IConfigNewCashBookEntrySettings : ICashBookEntry
	{
		#region Abstract
		/// <summary>see <see cref="Base.PropertyChanged" /> class for further information.</summary>
		event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}