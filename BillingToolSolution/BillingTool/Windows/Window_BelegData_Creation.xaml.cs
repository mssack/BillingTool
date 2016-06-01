// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingTool.btScope;
using BillingTool.Themes.Controls.belegdatacreation;
using BillingTool._SharedEnumerations;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Creation.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_Creation : CsWindow
	{


		private readonly ProcessLock _managedClosingLock = new ProcessLock();
		private readonly ProcessLock _selectionChangedLock = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_BelegData_Creation(bool isApproval)
		{
			IsApproval = isApproval;
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, nameof(Window_BelegData_Creation));
			CsGlobal.Wpf.Storage.ListView.Handle(PostenListView, nameof(PostenListView));
			CsGlobal.Wpf.Storage.ListView.Handle(MailedBelegeListView, nameof(MailedBelegeListView));
			CsGlobal.Wpf.Storage.ListView.Handle(PrintedBelegeListView, nameof(PrintedBelegeListView));
			Closing += WindowClosing;
		}

		/// <summary>If true this window will only be used to approve data not to modify or create data.</summary>
		public bool IsApproval
		{
			get { return (bool) GetValue(IsApprovalProperty); }
			set { SetValue(IsApprovalProperty, value); }
		}

		/// <summary>The item which needs to be approved.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		private void Approved()
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				Bt.Data.BelegData.Finalize(Item);
				Bt.Data.SyncAnabolicChanges();

				Bt.AppOutput.Include_ExitCode(ExitCodes.BelegData_Created);

				Bt.Ui.ProcessNonProcessedOutputs(Item, true);
				Bt.Data.SyncAnabolicChanges();

				using (_managedClosingLock.Activate())
					Close();
			}
		}

		private void Canceled()
		{
			Bt.Data.RejectAllChanges();

			Bt.AppOutput.Include_ExitCode(ExitCodes.BelegData_Creation_Aborted);

			using (_managedClosingLock.Activate())
				Close();
		}

		private void SomeOutputFormatChanged()
		{
			BonPreviewControl.ReloadSelectablePreviewFormats();
		}


		private void ItemChanged(BelegData belegData)
		{
			if (belegData == null)
				return;
			if (belegData.State != BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {belegData} is already fixed it has currently state {belegData.State}.");
		}

		private void WindowClosing(object sender, CancelEventArgs e)
		{
			if (_managedClosingLock.Active)
				return;
			e.Cancel = true;
		}

		private void ApprovedClick(object sender, RoutedEventArgs e)
		{
			Approved();
		}

		private void CancleClick(object sender, RoutedEventArgs e)
		{
			Canceled();
		}

		private void NewMailClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.MailedBeleg.New(Item, "");

			BonPreviewControl.ReloadSelectablePreviewFormats();
			((FrameworkElement) sender).GetParentByCondition<Expander>(ex => true).IsExpanded = true;
		}

		private void NewPrintClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.PrintedBeleg.New(Item);

			BonPreviewControl.ReloadSelectablePreviewFormats();
			((FrameworkElement) sender).GetParentByCondition<Expander>(ex => true).IsExpanded = true;
		}

		private void DeleteMailClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.MailedBeleg.Delete((MailedBeleg) ((FrameworkElement) sender).DataContext);

			BonPreviewControl.ReloadSelectablePreviewFormats();

			if (Item.MailedBelege.Count == 0)
				((FrameworkElement) sender).GetParentByCondition<Expander>(ex => true).IsExpanded = false;
		}

		private void DeletePrintClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.PrintedBeleg.Delete((PrintedBeleg) ((FrameworkElement) sender).DataContext);

			BonPreviewControl.ReloadSelectablePreviewFormats();
			if (Item.PrintedBelege.Count == 0)
				((FrameworkElement) sender).GetParentByCondition<Expander>(ex => true).IsExpanded = false;
		}

		private void DeleteBelegPostenClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.BelegPosten.Delete((BelegPosten) ((FrameworkElement) sender).DataContext);
		}

		private void ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_selectionChangedLock.Active)
				return;


			using (_selectionChangedLock.Activate())
			{
				if (Equals(sender, MailedBelegeListView))
					PrintedBelegeListView.SelectedItem = null;
				else if (Equals(sender, PrintedBelegeListView))
					MailedBelegeListView.SelectedItem = null;

				var outputFormat = ((sender as ListView)?.SelectedItem as IOutputBeleg)?.OutputFormat;
				if (outputFormat != null)
					BonPreviewControl.SelectedPreviewFormat = outputFormat;
			}
		}

		private void NewArticleClicked(object sender, RoutedEventArgs e)
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var control = new NewBelegPostenControl {Item = Item};
				var messageWindow = CsGlobal.Message.GetWindow(control, CsMessage.Types.Information, null, CsMessage.MessageButtons.NoButtons);
				
				control.Completed += c =>
				{
					messageWindow.Close();
				};
				messageWindow.ShowDialog();
			}
		}
		
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(Window_BelegData_Creation), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((Window_BelegData_Creation) o).ItemChanged(args.NewValue as BelegData)});
		public static readonly DependencyProperty IsApprovalProperty = DependencyProperty.Register("IsApproval", typeof(bool), typeof(Window_BelegData_Creation), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}