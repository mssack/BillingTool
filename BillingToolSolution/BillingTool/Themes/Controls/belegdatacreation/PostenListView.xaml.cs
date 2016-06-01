// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Themes.Controls.belegdatacreation
{
	/// <summary>Interaction logic for PostenListView.xaml</summary>
	public partial class PostenListView : ListView
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(PostenListView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public PostenListView()
		{
			InitializeComponent();
		}

		/// <summary>The item with the <see cref="PrintedBeleg" /> list.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
	}
}