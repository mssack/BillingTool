// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Themes.Controls.belegapproval
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

		/// <summary>The item with the <see cref="PrintedBeleg" /> list.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>Occurs whenever the user selects a new <see cref="OutputFormat" />.</summary>
		public event Action SomeOutputFormatChanged;

		private void NewFormatsAvailable(object sender, SelectionChangedEventArgs e)
		{
			SomeOutputFormatChanged?.Invoke();
		}
	}
}