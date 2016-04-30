// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The possible states of the row <see cref="BelegData" />.</summary>
	public enum BelegDataStates
	{
		/// <summary>No state is applied.</summary>
		[EnumDescription("Unbekannt")]
		Unknown = 0,
		/// <summary>The <see cref="BelegData" /> was approved by an external application. The application didn't asked the user for approval.</summary>
		[EnumDescription("Automatisch Bestätigt", "Dieser Beleg wurde durch die Applikation bestätigt.")]
		Approved_ByApplication = 1,
		/// <summary>The <see cref="BelegData" /> was approved by an the user.</summary>
		[EnumDescription("Bestätigt durch User", "Dieser Beleg wurde durch den User bestätigt.")]
		Approved_ByUser = 2,
		/// <summary>The <see cref="BelegData" /> was Storno.</summary>
		[EnumDescription("Storniert", "Der Beleg wurde bereits storniert")]
		Storno = 3
	}



}