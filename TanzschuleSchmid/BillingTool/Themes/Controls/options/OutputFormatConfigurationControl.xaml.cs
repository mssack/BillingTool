﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Global;






namespace BillingTool.Themes.Controls.options
{
	/// <summary>Interaction logic for OutputFormatConfigurationControl.xaml</summary>
	public partial class OutputFormatConfigurationControl : UserControl
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(OutputFormat), typeof(OutputFormatConfigurationControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((OutputFormatConfigurationControl) o).SelectedItemChanged()});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public OutputFormatConfigurationControl()
		{
			InitializeComponent();
		}


		/// <summary>The current selected item.</summary>
		public OutputFormat SelectedItem
		{
			get { return (OutputFormat) GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}


		private void SelectedItemChanged()
		{

		}

		private void LöschenClicked(object sender, RoutedEventArgs e)
		{
			if (SelectedItem.HasBeenUsed)
			{
				CsGlobal.Message.Push("Sie können dieses Layout nicht löschen da es bereits benutzt wird.");
				return;
			}
			SelectedItem.Delete();
		}

		private void HinzufügenClicked(object sender, RoutedEventArgs e)
		{
			var format = Bt.Data.OutputFormat.New();
			SelectedItem = format;
		}
	}
}