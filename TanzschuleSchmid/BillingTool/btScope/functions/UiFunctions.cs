// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingTool.btScope.configuration._enums;
using BillingTool.Windows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.UiFunctions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class UiFunctions : Base
	{
		private static UiFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static UiFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new UiFunctions());
				}
			}
		}

		private UiFunctions()
		{
		}


		/// <summary>Opens a window for the user, using the <see cref="Bt.Config" />, to allow a creation of an new <see cref="CashBookEntry" />.</summary>
		public void NewCashBookEntry(bool showdialog = false)
		{
			Bt.Db.EnsureConnectivity();
			var entry = Bt.Db.Billing.CashBook.NewRow();

			entry.Copy_From(Bt.Config.Merged.NewCashBookEntry, CashBookTable.IdCol, CashBookTable.ReferenceNumberCol, CashBookTable.DateCol, CashBookTable.LastEditedCol);

			var window = new NewCashBookEntryWindow(entry);

			if (showdialog)
				window.ShowDialog();
			else
				window.Show();
		}
		/// <summary>Opens a window for the configuration of this software.</summary>
		public void OpenConfiguration(bool showdialog = false)
		{
			var window = new ConfigurationWindow();

			if (showdialog)
				window.ShowDialog();
			else
				window.Show();
		}
		/// <summary>
		/// Opens a window with the product informations.
		/// </summary>
		/// <param name="showdialog"></param>
		public void OpenProductInformation(bool showdialog = false)
		{
			var window = new ProductInformationWindow();

			if (showdialog)
				window.ShowDialog();
			else
				window.Show();
		}
		

		/// <summary>Executes the current configured mode.</summary>
		public void ExecuteConfiguration()
		{
			var mode = Bt.Config.Merged.General.StartupMode;
			if (mode == StartupModes.Developer)
			{
				var w = new DeveloperWindow();
				w.Show();
			}
			else if (mode == StartupModes.NewCashBookEntry)
			{
				NewCashBookEntry();
			}
			else if (mode == StartupModes.Configuration)
			{
				OpenConfiguration();
			}
			else if (mode == StartupModes.ProductInformation)
			{
				OpenProductInformation();
			}

			if (Application.Current.MainWindow == null)
				Application.Current.MainWindow = Application.Current.Windows[0];
		}
	}
}