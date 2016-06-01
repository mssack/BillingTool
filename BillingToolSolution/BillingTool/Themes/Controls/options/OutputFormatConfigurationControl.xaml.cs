// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingTool.btScope;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Global;






namespace BillingTool.Themes.Controls.options
{
	/// <summary>Interaction logic for OutputFormatConfigurationControl.xaml</summary>
	public partial class OutputFormatConfigurationControl : UserControl
	{


		/// <summary>ctor</summary>
		public OutputFormatConfigurationControl()
		{
			InitializeComponent();
			Loaded += Control_Loaded;
		}


		/// <summary>The current selected item.</summary>
		public OutputFormat SelectedItem
		{
			get { return (OutputFormat) GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}
		/// <summary>A <see cref="BelegData" /> which can be used to preview the layout.</summary>
		public BelegData SampleBelegData
		{
			get { return (BelegData) GetValue(SampleBelegDataProperty); }
			set { SetValue(SampleBelegDataProperty, value); }
		}

		private void Control_Loaded(object sender, RoutedEventArgs e)
		{
			if (!Bt.Db.Billing.OutputFormats.HasBeenLoaded)
				Bt.Db.Billing.OutputFormats.DownloadRows();
		}

		private void SelectedItemChanged()
		{
			if (SelectedItem == null)
				SampleBelegData = null;
			else if (SelectedItem.BonLayoutType == BonLayoutTypes.Print || SelectedItem.BonLayoutType == BonLayoutTypes.Mail)
				SampleBelegData = SelectedItem.DataSet.BelegDaten.SampleFor.PrintOrMail;
			else if (SelectedItem.BonLayoutType == BonLayoutTypes.Storno)
				SampleBelegData = SelectedItem.DataSet.BelegDaten.SampleFor.Storno;
			else if (SelectedItem.BonLayoutType == BonLayoutTypes.TagesBon)
				SampleBelegData = SelectedItem.DataSet.BelegDaten.SampleFor.TagesBon;
			else if (SelectedItem.BonLayoutType == BonLayoutTypes.MonatsBon)
				SampleBelegData = SelectedItem.DataSet.BelegDaten.SampleFor.MonatsBon;
			else if (SelectedItem.BonLayoutType == BonLayoutTypes.JahresBon)
				SampleBelegData = SelectedItem.DataSet.BelegDaten.SampleFor.JahresBon;
		}


		private void LöschenClicked(object sender, RoutedEventArgs e)
		{
			if (SelectedItem.HasBeenUsed)
			{
				CsGlobal.Message.Push("Sie können dieses Layout nicht löschen da es bereits benutzt wird.");
				return;
			}
			SelectedItem.Delete();
		}

		private void HinzufügenClicked(object sender, RoutedEventArgs e)
		{
			var format = Bt.Data.OutputFormat.New();
			Bt.Data.OutputFormat.Finalize(format);
			SelectedItem = format;
		}

		private void SetAsStandardButtonClicked(object sender, RoutedEventArgs e)
		{
			SelectedItem.SetAsDbStandard();
		}

		private void ResetScalingClicked(object sender, RoutedEventArgs e)
		{
			SelectedItem.Scaling = 1;
		}
#pragma warning disable 1591
		public static readonly DependencyProperty SampleBelegDataProperty = DependencyProperty.Register("SampleBelegData", typeof(BelegData), typeof(OutputFormatConfigurationControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(OutputFormat), typeof(OutputFormatConfigurationControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((OutputFormatConfigurationControl) o).SelectedItemChanged()});
#pragma warning restore 1591

	}
}