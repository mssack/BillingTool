﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Approve.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_Approve : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(Window_BelegData_Approve), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((Window_BelegData_Approve) o).ItemChanged(args.NewValue as BelegData)});
#pragma warning restore 1591
		#endregion


		private readonly ProcessLock _managedClosingLock = new ProcessLock();
		private readonly ProcessLock _selectionChangedLock = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_BelegData_Approve()
		{
			Bt.EnsureInitialization();

			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "Window_BelegData_Approve");
			Closing += WindowClosing;
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
				Bt.Ui.ProcessNonProcessedOutputs(Item);
				Bt.Data.SyncAnabolicChanges();

				Bt.AppOutput.SetExitCode(ExitCodes.NewBelegData_Created);

				using (_managedClosingLock.Activate())
					Close();
			}
		}

		private void Canceled()
		{
			Bt.Data.RejectAllChanges();
			Bt.AppOutput.SetExitCode(ExitCodes.BelegDataCreation_Aborted);

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
		}

		private void NewPrintClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.PrintedBeleg.New(Item);

			BonPreviewControl.ReloadSelectablePreviewFormats();
		}

		private void DeleteMailClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.MailedBeleg.Delete((MailedBeleg) ((FrameworkElement) sender).DataContext);

			BonPreviewControl.ReloadSelectablePreviewFormats();
		}

		private void DeletePrintClicked(object sender, RoutedEventArgs e)
		{
			Bt.Data.PrintedBeleg.Delete((PrintedBeleg) ((FrameworkElement) sender).DataContext);

			BonPreviewControl.ReloadSelectablePreviewFormats();
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
	}
}