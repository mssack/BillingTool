// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using System.Windows.Controls;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_KassenConfiguration.xaml</summary>
	public partial class Window_KassenConfiguration : CsWindow
	{
		readonly ProcessLock _managedClose = new ProcessLock();
		/// <summary>ctor</summary>
		public Window_KassenConfiguration()
		{
			InitializeComponent();
			Closing += KassenConfiguratorWindow_Closing;
		}

		private void KassenConfiguratorWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_managedClose.Active)
				return;

			e.Cancel = true;
		}

		private void ChoosePrinterClicked(object sender, RoutedEventArgs e)
		{
			var dialog = new PrintDialog();
			var b = dialog.ShowDialog();

			if (b != true)
				return;

			Bt.Config.File.KassenEinstellung.PrinterName = dialog.PrintQueue.FullName;
		}

		private void NextClick(object sender, RoutedEventArgs e)
		{
			if (Bt.Config.File.KassenEinstellung.IsValid)
				Bt.Config.File.KassenEinstellung.Save();

			using (_managedClose.Activate())
			{
				Close();
			}
		}
	}
}