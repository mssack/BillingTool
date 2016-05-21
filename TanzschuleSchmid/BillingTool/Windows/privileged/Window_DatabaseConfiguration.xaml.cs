// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-21</date>

using System;
using System.ComponentModel;
using System.Windows;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows.privileged
{
	/// <summary>Interaction logic for Window_DatabaseConfiguration.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_DatabaseConfiguration : CsWindow
	{
		readonly ProcessLock _managedClose = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_DatabaseConfiguration()
		{
			InitializeComponent();
			Closing += Window_Closing;
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (_managedClose.Active)
				return;

			Bt.Data.SyncChanges();
			e.Cancel = true;
		}


		private void NextClick(object sender, RoutedEventArgs e)
		{
			using (_managedClose.Activate())
			{
				Close();
			}
		}
	}
}