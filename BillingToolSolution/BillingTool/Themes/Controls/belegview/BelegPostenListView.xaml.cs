// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Themes.Controls.belegview
{
	/// <summary>Interaction logic for MailedBelegeListView.xaml</summary>
	public partial class BelegPostenListView : ListView
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BelegPostenListView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public BelegPostenListView()
		{
			SetBinding(ItemsSourceProperty, new Binding($"{nameof(Item)}.{nameof(Item.Postens)}") {Source = this});
			InitializeComponent();
		}

		/// <summary>The source item.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
	}
}