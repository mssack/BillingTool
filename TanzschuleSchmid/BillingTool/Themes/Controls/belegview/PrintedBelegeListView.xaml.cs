﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;






namespace BillingTool.Themes.Controls.belegview
{
	/// <summary>Interaction logic for PrintedBelegeListView.xaml</summary>
	public partial class PrintedBelegeListView : ListView
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(PrintedBelegeListView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public PrintedBelegeListView()
		{
			InitializeComponent();
		}

		/// <summary>The source item.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>Occurs whenever the user presses on a <see cref="PrintedBeleg" />.</summary>
		public event Action<OutputFormat> OutputFormatSelected;

		private void ListViewItemClicked(object sender, MouseButtonEventArgs e)
		{
			OutputFormatSelected?.Invoke(((IOutputBeleg) ((ListViewItem) sender).DataContext).OutputFormat);
		}
	}
}