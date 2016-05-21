// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-17</date>

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingTool.btScope;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows.tools
{
	/// <summary>Interaction logic for Window_BelegData_ProcessNonProcessedOutputs.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_ProcessNonProcessedOutputs : CsWindow
	{
		private readonly bool _forceReprintIfFailed;
		private readonly ProcessLock _saveClosing = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_BelegData_ProcessNonProcessedOutputs(BelegData item, bool forceReprintIfFailed)
		{
			_forceReprintIfFailed = forceReprintIfFailed;
			Item = item;
			IsFinished = false;
			InitializeComponent();
			Loaded += Window_BelegData_ProcessNonProcessedOutputs_Loaded;
			Closing += Window_BelegData_ProcessNonProcessedOutputs_Closing;
		}

		private void Window_BelegData_ProcessNonProcessedOutputs_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!_saveClosing.Active)
				e.Cancel = true;
		}

		private void Window_BelegData_ProcessNonProcessedOutputs_Loaded(object sender, RoutedEventArgs e)
		{
			ReprintOutputFormat = Bt.Db.Billing.OutputFormats.Default_PrintFormat;
			ReprintPrinterDevice = Bt.Config.File.KassenEinstellung.Default_PrinterName;
			OpenedItems = new ObservableCollection<DataRow>(Item.MailedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed).OfType<DataRow>().Union(Item.PrintedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed)));
			Bt.Output.DoOpenedExportsAsync(Item).ContinueWith(TaskCompleted, TaskScheduler.FromCurrentSynchronizationContext());
		}

		/// <summary>The item</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>All the items which are currently being processed.</summary>
		public ObservableCollection<DataRow> OpenedItems
		{
			get { return (ObservableCollection<DataRow>) GetValue(OpenedItemsProperty); }
			set { SetValue(OpenedItemsProperty, value); }
		}
		/// <summary>Is true if all mails have failed, an no print were actually done.</summary>
		public bool IsReprintNecessary
		{
			get { return (bool) GetValue(IsReprintNecessaryProperty); }
			set { SetValue(IsReprintNecessaryProperty, value); }
		}
		/// <summary>All operations finished.</summary>
		public bool IsFinished
		{
			get { return (bool) GetValue(IsFinishedProperty); }
			set { SetValue(IsFinishedProperty, value); }
		}
		/// <summary>The format which can be selected if the Bon have to be reprinted.</summary>
		public OutputFormat ReprintOutputFormat
		{
			get { return (OutputFormat) GetValue(ReprintOutputFormatProperty); }
			set { SetValue(ReprintOutputFormatProperty, value); }
		}

		/// <summary>The device which can be selected if the Bon have to be reprinted.</summary>
		public string ReprintPrinterDevice
		{
			get { return (string) GetValue(ReprintPrinterDeviceProperty); }
			set { SetValue(ReprintPrinterDeviceProperty, value); }
		}

		private void TaskCompleted(Task<Task[]> task)
		{
			IsFinished = true;
			if (!task.Result.Any(x => x.IsFaulted))
			{
				using (_saveClosing.Activate())
					Close();
				return;
			}

			if (_forceReprintIfFailed)
			{
				var anyPrints = false;
				var allMailsFailed = true;
				task.Result.ForEach(t =>
				{
					if (t is Task<PrintedBeleg>)
						anyPrints = true;
					else if (t is Task<MailedBeleg> && !t.IsFaulted)
						allMailsFailed = false;
				});
				if (allMailsFailed && !anyPrints)
					IsReprintNecessary = true;
			}
		}

		private void PrintAndCloseClicked(object sender, RoutedEventArgs e)
		{
			var printedBeleg = Bt.Data.PrintedBeleg.New(Item);
			printedBeleg.OutputFormat = ReprintOutputFormat;
			printedBeleg.PrinterDevice = ReprintPrinterDevice;
			Bt.Data.PrintedBeleg.Finalize(printedBeleg);
			OpenedItems.Add(printedBeleg);
			IsFinished = false;
			Bt.Output.DoOpenedExportsAsync(Item).ContinueWith(TaskCompleted, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private void CloseClicked(object sender, RoutedEventArgs e)
		{
			using (_saveClosing.Activate())
				Close();
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ReprintOutputFormatProperty = DependencyProperty.Register("ReprintOutputFormat", typeof(OutputFormat), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ReprintPrinterDeviceProperty = DependencyProperty.Register("ReprintPrinterDevice", typeof(string), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata { DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
		public static readonly DependencyProperty IsReprintNecessaryProperty = DependencyProperty.Register("IsReprintNecessary", typeof(bool), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsFinishedProperty = DependencyProperty.Register("IsFinished", typeof(bool), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty OpenedItemsProperty = DependencyProperty.Register("OpenedItems", typeof(ObservableCollection<DataRow>), typeof(Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(ObservableCollection<DataRow>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}