// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.dataanalysis;






namespace BillingToolOutput.Controls.RecapBelege
{
	/// <summary>Interaction logic for V1MonatsBonVisual.xaml</summary>
	public partial class V1MonatsBonVisual : UserControl
	{
		/// <summary>ctor</summary>
		public V1MonatsBonVisual()
		{
			InitializeComponent();
		}

		/// <summary>The <see cref="Steuersatz" /> informations.</summary>
		public BelegDataAnalysis BelegDataAnalysis
		{
			get { return (BelegDataAnalysis) GetValue(BelegDataAnalysisProperty); }
			set { SetValue(BelegDataAnalysisProperty, value); }
		}
		/// <summary>Item to display.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>The output format to use.</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}

		private void UpdateData()
		{
			BelegDataAnalysis?.Dispose();
			BelegDataAnalysis = Item?.VonBis_BelegData == null ? null : new BelegDataAnalysis(Item.VonBis_BelegData);
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(V1MonatsBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((V1MonatsBonVisual) o).UpdateData()});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(V1MonatsBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty BelegDataAnalysisProperty = DependencyProperty.Register("BelegDataAnalysis", typeof(BelegDataAnalysis), typeof(V1MonatsBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegDataAnalysis), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}