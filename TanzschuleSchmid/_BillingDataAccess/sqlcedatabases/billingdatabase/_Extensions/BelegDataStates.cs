// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The possible states of the row <see cref="BelegData" />.</summary>
	public enum BelegDataStates
	{
		/// <summary>No state is applied.</summary>
		[EnumDescription("Unbekannt")] Unknown = 0,
		/// <summary>The <see cref="BelegData" /> was approved by an the user.</summary>
		[EnumDescription("Bestätigt", "Dieser Beleg wurde durch den User bestätigt.")] Approved = 1,
		/// <summary>The <see cref="BelegData" /> was storniert.</summary>
		[EnumDescription("Storniert", "Der Beleg wurde storniert")] Storniert = 2
	}



}