// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingToolOutput.Controls._shared
{
	/// <summary>Interaction logic for V1Header.xaml</summary>
	public partial class V1Header : UserControl
	{


		/// <summary>ctor</summary>
		public V1Header()
		{
			InitializeComponent();
		}

		/// <summary>the item</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>the <see cref="OutputFormat" /> .</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(V1Header), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(V1Header), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}