// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.Modes.newCashBookEntry;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.UiFunctions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class BtUiFunctions : Base
	{
		private static BtUiFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BtUiFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BtUiFunctions());
				}
			}
		}

		private BtUiFunctions()
		{
		}


		/// <summary>Opens a window for the user, using the <see cref="Bt.Config" />, to allow a creation of an new <see cref="CashBookEntry" />.</summary>
		public NewCashBookEntryWindow NewCashBookEntry()
		{
			Bt.Db.EnsureConnectivity();
			var window = new NewCashBookEntryWindow(Bt.Db.Billing.CashBook.NewRow());
			window.Show();
			return window;
		}
	}
}