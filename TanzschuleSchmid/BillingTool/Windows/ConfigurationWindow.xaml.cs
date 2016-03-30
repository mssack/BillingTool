// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for ConfigurationWindow.xaml</summary>
	public partial class ConfigurationWindow : CsWindow
	{
		/// <summary>ctor</summary>
		public ConfigurationWindow()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "ConfigurationWindow");
		}

		private void CreateDatabaseClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				Bt.Db.CreateDatabase();
				CsGlobal.Message.Push("Die Datenbank wurde erfolgreich erstellt.");
			}
			catch (Exception excp)
			{
				CsGlobal.Message.Push(excp, CsMessage.Types.Error);
			}
		}
	}
}