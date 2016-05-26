// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingOutput.Controls._shared
{
	/// <summary>Interaction logic for AnyBonVisual.xaml</summary>
	public partial class AnyBonVisual : UserControl
	{


		/// <summary>ctor</summary>
		public AnyBonVisual()
		{
			InitializeComponent();
		}

		/// <summary>The item to display.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>The output format.</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
		/// <summary>The output format which will be displayed</summary>
		public OutputFormat DisplayFormat
		{
			get { return (OutputFormat) GetValue(DisplayFormatProperty); }
			set { SetValue(DisplayFormatProperty, value); }
		}


		private void SomethingChanged()
		{
			if (Item == null)
			{
				DisplayFormat = null;
				return;
			}


			if (OutputFormat != null)
				DisplayFormat = OutputFormat;

			else if (Item.Typ.IsNormalBon())
				DisplayFormat = Item.DataSet.OutputFormats.Default_PrintFormat;
			else if (Item.Typ == BelegDataTypes.Storno)
				DisplayFormat = Item?.DataSet.OutputFormats.Default_StornoFormat;
			else if (Item.Typ == BelegDataTypes.TagesBon)
				DisplayFormat = Item?.DataSet.OutputFormats.Default_TagesBonFormat;
			else if (Item.Typ == BelegDataTypes.MonatsBon)
				DisplayFormat = Item?.DataSet.OutputFormats.Default_MonatsBonFormat;
			else if (Item.Typ == BelegDataTypes.JahresBon)
				DisplayFormat = Item?.DataSet.OutputFormats.Default_MonatsBonFormat;
		}


#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(AnyBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((AnyBonVisual) o).SomethingChanged()});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(AnyBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((AnyBonVisual) o).SomethingChanged()});
		public static readonly DependencyProperty DisplayFormatProperty = DependencyProperty.Register("DisplayFormat", typeof(OutputFormat), typeof(AnyBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}