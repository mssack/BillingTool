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




#pragma warning disable 1591


namespace BillingOutput.Controls.BonVisuals
{
	/// <summary>Interaction logic for V1StornoBonVisual.xaml</summary>
	public partial class V1StornoBonVisual : UserControl
	{
		#region DP Keys
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(V1StornoBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(V1StornoBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion
		public V1StornoBonVisual()
		{
			InitializeComponent();
		}

		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
	}
}