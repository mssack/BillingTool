// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration._interfaces
{
	/// <summary>Describes all <see cref="BelegData" /> configurations which can be done either by command line or configuration file.</summary>
	// ReSharper disable once InconsistentNaming
	public interface IConfig_NewBelegData : IBelegData
	{
		#region Abstract
		/// <summary>see <see cref="Base.PropertyChanged" /> class for further information.</summary>
		event PropertyChangedEventHandler PropertyChanged;


		/// <summary>If true a new <see cref="PrintedBeleg" /> will be created and printed to a printer.</summary>
		bool PrintBeleg { get; set; }
		/// <summary>If true a new <see cref="MailedBeleg" /> will be created and sent per mail to the mail address specified in <see cref="SendBelegTarget" />.</summary>
		bool SendBeleg { get; set; }
		/// <summary>The mail target if <see cref="SendBeleg" /> is true.</summary>
		string SendBelegTarget { get; set; }
		#endregion
	}
}