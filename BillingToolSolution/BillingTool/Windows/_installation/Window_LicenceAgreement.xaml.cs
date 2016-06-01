// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Windows;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






// ReSharper disable InconsistentNaming

namespace BillingTool.Windows._installation
{
	/// <summary>Interaction logic for Window_LicenceAgreement.xaml</summary>
	public partial class Window_LicenceAgreement : CsWindow
	{
		readonly ProcessLock _managedClosing = new ProcessLock();
		/// <summary>ctor</summary>
		public Window_LicenceAgreement()
		{
			InitializeComponent();
			Loaded += (sender, args) => DialogResult = false;
			Closing += Window_LicenceAgreement_Closing;
		}

		private void Window_LicenceAgreement_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!_managedClosing.Active)
			{
				e.Cancel = true;
				return;
			}
		}

		private void CancleClicked(object sender, RoutedEventArgs e)
		{
			using (_managedClosing.Activate())
				Close();
		}

		private void AcceptClicked(object sender, RoutedEventArgs e)
		{
			using (_managedClosing.Activate())
			{
				DialogResult = true;
				Close();
			}
		}
	}
}