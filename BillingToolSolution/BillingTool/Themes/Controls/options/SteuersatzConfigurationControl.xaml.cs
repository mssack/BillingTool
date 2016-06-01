// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingTool.btScope;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Global;






namespace BillingTool.Themes.Controls.options
{
	/// <summary>Interaction logic for SteuersatzConfigurationControl.xaml</summary>
	public partial class SteuersatzConfigurationControl : UserControl
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Steuersatz), typeof(SteuersatzConfigurationControl), new FrameworkPropertyMetadata {DefaultValue = default(Steuersatz), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public SteuersatzConfigurationControl()
		{
			InitializeComponent();
			Loaded += Control_Loaded;
		}

		private void Control_Loaded(object sender, RoutedEventArgs e)
		{
			if (!Bt.Db.Billing.Steuersätze.HasBeenLoaded)
				Bt.Db.Billing.Steuersätze.DownloadRows();
		}

		/// <summary>The current selected item.</summary>
		public Steuersatz SelectedItem
		{
			get { return (Steuersatz) GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		private void LöschenClicked(object sender, RoutedEventArgs e)
		{

			if (SelectedItem.HasBeenUsed)
			{
				CsGlobal.Message.Push("Sie können diesen Steuersatz nicht löschen da er bereits benutzt wird.");
				return;
			}
			SelectedItem.Delete();
		}

		private void HinzufügenClicked(object sender, RoutedEventArgs e)
		{
			var item = Bt.Data.Steuersatz.New();
			Bt.Data.Steuersatz.Finalize(item);
			SelectedItem = item;
		}
	}
}