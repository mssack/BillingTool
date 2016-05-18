// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.dataanalysis;






namespace BillingOutput.Controls.BonVisuals
{
	/// <summary>Interaction logic for V1PrintBonVisual.xaml</summary>
	public partial class V1PrintBonVisual : UserControl
	{


		/// <summary>ctor</summary>
		public V1PrintBonVisual()
		{
			InitializeComponent();
			Loaded += (sender, args) => SteuersatzAufschlüsselungBorder.BringIntoView();
		}


		/// <summary>The item for which the <see cref="V1PrintBonVisual" /> should be drawn.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>The output format for the Bon.</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
		/// <summary>Contains information about the <see cref="Steuersatz" /> which were used in this <see cref="BelegData" />.</summary>
		public SteuersatzAufschlüsselung SteuersatzAufschlüsselung
		{
			get { return (SteuersatzAufschlüsselung) GetValue(SteuersatzAufschlüsselungProperty); }
			set { SetValue(SteuersatzAufschlüsselungProperty, value); }
		}


		private void ItemChanged(BelegData oldValue, BelegData newValue)
		{
			SteuersatzAufschlüsselung?.Dispose();
			SteuersatzAufschlüsselung = newValue==null?null: new SteuersatzAufschlüsselung(newValue);

			SteuersatzAufschlüsselungBorder.BringIntoView();
		}



#pragma warning disable 1591
		public static readonly DependencyProperty SteuersatzAufschlüsselungProperty = DependencyProperty.Register("SteuersatzAufschlüsselung", typeof(SteuersatzAufschlüsselung), typeof(V1PrintBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(SteuersatzAufschlüsselung), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(V1PrintBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((V1PrintBonVisual) o).ItemChanged(args.OldValue as BelegData, args.NewValue as BelegData)});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(V1PrintBonVisual), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}