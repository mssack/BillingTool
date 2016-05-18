// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingOutput.Controls.BonVisuals
{
	/// <summary>Interaction logic for BonVisual.xaml</summary>
	public partial class BonVisual : UserControl
	{


		/// <summary>ctor</summary>
		public BonVisual()
		{
			InitializeComponent();
		}

		/// <summary>The <see cref="BelegData" /> which holds all the data.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>The <see cref="OutputFormat" /> which holds all informations for the design.</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}

		/// <summary>The Format which is used for drawing.</summary>
		public OutputFormat DisplayFormat
		{
			get { return (OutputFormat) GetValue(DisplayFormatProperty); }
			set { SetValue(DisplayFormatProperty, value); }
		}


		private void SomethingChanged()
		{
			if (OutputFormat != null)
				DisplayFormat = OutputFormat;
			else if (Item?.Typ == BelegDataTypes.Storno)
				DisplayFormat = Item.DataSet.OutputFormats.Default_StornoFormat;
			else
				DisplayFormat = Item?.DataSet.OutputFormats.Default_PrintFormat;
		}
#pragma warning disable 1591
		public static readonly DependencyProperty DisplayFormatProperty = DependencyProperty.Register("DisplayFormat", typeof(OutputFormat), typeof(BonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true,DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BonVisual)o).SomethingChanged()});


		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(BonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BonVisual)o).SomethingChanged() });
#pragma warning restore 1591
	}
}