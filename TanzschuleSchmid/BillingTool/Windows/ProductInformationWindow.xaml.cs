// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for ProductInformationWindow.xaml</summary>
	public partial class ProductInformationWindow : CsWindow
	{
		/// <summary>ctor</summary>
		public ProductInformationWindow()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "ProductInformationWindow");
		}

		private void OpenConfigurationClicked(object sender, RoutedEventArgs e)
		{
			Bt.UiFunctions.OpenConfiguration(true);
		}
	}
}