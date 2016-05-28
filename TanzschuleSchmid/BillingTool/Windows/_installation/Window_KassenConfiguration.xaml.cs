// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.ComponentModel;
using System.Windows;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows._installation
{
	/// <summary>Interaction logic for Window_KassenConfiguration.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_KassenConfiguration : CsWindow
	{
		readonly ProcessLock _managedClose = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_KassenConfiguration()
		{
			InitializeComponent();
			Closing += KassenConfiguratorWindow_Closing;
		}

		private void KassenConfiguratorWindow_Closing(object sender, CancelEventArgs e)
		{
			if (_managedClose.Active)
				return;

			e.Cancel = true;
		}

		private void NextClick(object sender, RoutedEventArgs e)
		{
			if (Bt.Config.Local.IsValid)
				Bt.Config.Local.Save();

			using (_managedClose.Activate())
			{
				Close();
			}
		}
	}
}