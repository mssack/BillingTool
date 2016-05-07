// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingDataAccess.DatabaseCreation;
using BillingTool.btScope;
using BillingTool.btScope.configuration._enums;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.Windows.privileged
{
	/// <summary>
	///     The Developer window provides administrative functionality. TAKE CARE whenever it will be used. This window should be opened in
	///     <see cref="StartupModes.Developer" /> Mode.
	/// </summary>
	public partial class DeveloperWindow
	{
		/// <summary>ctor</summary>
		public DeveloperWindow()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "DeveloperWindow");
		}

		private void ReinstallDatabase_Click(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push("Wollen Sie wirklich die bestehende Datenbank löschen und eine komplett neue Datenbank erstellen?", CsMessage.Types.Warning, "Neue Datenbank installieren?", CsMessage.MessageButtons.YesNo))
				return;
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push("Die bestehende Datenbank wird somit gelöscht!! SIND SIE SICH WIRKLICH SICHER?", CsMessage.Types.Warning, "Neue Datenbank installieren? VORSICHT!", CsMessage.MessageButtons.YesNo))
				return;

			try
			{
				using (var installer = new DatabaseInstaller(Bt.Config.File.KassenEinstellung.BillingDatabaseFilePath))
				{
					installer.Install(true);
				}
				CsGlobal.Message.Push("Eine neue Datenbank wurde erstellt.", CsMessage.Types.Information, "Erfolgreich");
			}
			catch (Exception exp)
			{
				CsGlobal.Message.Push(exp, CsMessage.Types.Error, "Installation error");
			}
		}

		private void NewCashBookEntry_Click(object sender, RoutedEventArgs e)
		{
			//Bt.UiFunctions.New_BelegData();
			e.Handled = true;
		}

		private void ConfigurationWindow_Click(object sender, RoutedEventArgs e)
		{
			var w = new Window_KassenConfiguration();
			w.Show();
		}

		private void Logs_Click(object sender, RoutedEventArgs e)
		{
			var w = new Window_DatabaseViewer();
			w.Show();
		}
	}
}