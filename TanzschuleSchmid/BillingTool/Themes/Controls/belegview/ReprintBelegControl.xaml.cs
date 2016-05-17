// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace BillingTool.Themes.Controls.belegview
{
	/// <summary>Interaction logic for ReprintBelegControl.xaml</summary>
	public partial class ReprintBelegControl : UserControl
	{
		#region DP Keys
		#endregion


		/// <summary>ctor</summary>
		public ReprintBelegControl()
		{
			InitializeComponent();
			Reset();
		}
		/// <summary>
		/// 
		/// </summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary></summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
		/// <summary></summary>
		public string Device
		{
			get { return (string) GetValue(DeviceProperty); }
			set { SetValue(DeviceProperty, value); }
		}
		/// <summary>Occurs whenever a <see cref="BelegData" /> has been successfully printed.</summary>
		public event Action BelegPrinted;

		private void Print()
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var printedBeleg = Bt.Data.PrintedBeleg.New(Item);
				printedBeleg.OutputFormat = OutputFormat;
				printedBeleg.PrinterDevice = Device;
				Bt.Data.PrintedBeleg.Finalize(printedBeleg);
				Bt.Ui.ProcessNonProcessedOutputs(Item);
				Bt.Data.SyncAnabolicChanges();


				Reset();
				BelegPrinted?.Invoke();
			}
		}

		private void Reset()
		{
			OutputFormat = (Item != null && Item.Typ == BelegDataTypes.Storno) ? Bt.Db.Billing.OutputFormats.Default_StornoFormat : Bt.Db.Billing.OutputFormats.Default_PrintFormat;

			Device = Bt.Config.File.KassenEinstellung.Default_PrinterName;
		}

		private void DruckenButtonClicked(object sender, RoutedEventArgs e)
		{
			Print();
			((FrameworkElement)sender).GetParentByCondition<Popup>(ex => true).IsOpen = false;
		}
		private void ItemChanged()
		{
			Reset();
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(ReprintBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ReprintBelegControl)o).ItemChanged()});
		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(ReprintBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty DeviceProperty = DependencyProperty.Register("Device", typeof(string), typeof(ReprintBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}