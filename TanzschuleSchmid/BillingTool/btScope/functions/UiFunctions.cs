// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
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


		/// <summary>Opens a window for the user, using the <see cref="Bt.Config" />, to allow a creation of an new <see cref="BelegData" />.</summary>
		public void New_BelegData(bool showdialog = false)
		{
			Bt.Db.EnsureConnectivity();
			var entry = Bt.Functions.New_BelegData_FromConfiguration();
			


			var window = new BelegDataApproveWindow(entry);

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

		/// <summary>Opens a window with the product informations.</summary>
		public void OpenProductInformation(bool showdialog = false)
		{
			var window = new ProductInformationWindow();

			if (showdialog)
				window.ShowDialog();
			else
				window.Show();
		}

		/// <summary>Opens a window with the logs.</summary>
		public void OpenLogs(bool showdialog = false)
		{
			var window = new DatabaseWindow();

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
			else if (mode == StartupModes.NewBelegData)
			{
				New_BelegData();
			}
			else if (mode == StartupModes.Configuration)
			{
				OpenConfiguration();
			}
			else if (mode == StartupModes.ProductInformation)
			{
				OpenProductInformation();
			}
			else if (mode == StartupModes.Database)
			{
				OpenLogs();
			}

			if (Application.Current.MainWindow == null)
				Application.Current.MainWindow = Application.Current.Windows[0];
		}

		/// <summary>
		///     Opens a window with an <paramref name="title" /> and a specific <paramref name="text" />. This window ensures that the user knows what he does.
		///     The method returns true if the user passed all verification mechanism.
		/// </summary>
		public bool CheckOperatorsTrustAbility(string title, string text)
		{
			var wind = new CheckTrustAbilityWindow(title, text);
			wind.ShowDialog();
			return wind.HasBeenValidated;
		}
	}
}