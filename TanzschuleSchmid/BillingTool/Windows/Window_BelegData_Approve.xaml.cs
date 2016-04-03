// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingOutput.btOutputScope;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Approve.xaml</summary>
	public partial class Window_BelegData_Approve : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (BelegData), typeof (Window_BelegData_Approve), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((Window_BelegData_Approve) o).ItemChanged(args.NewValue as BelegData)});

#pragma warning restore 1591
		#endregion


		private readonly ProcessLock _managedClosing = new ProcessLock();

		/// <summary>ctor</summary>
		public Window_BelegData_Approve()
		{
			InitializeComponent();
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
			using (_managedClosing.Activate())
			{
				Bt.DataFunctions.Save_NewBelegData(Item);
				Bt.Functions.SetExitCode(ExitCodes.NewBelegData_Created);

				var outputWindow = new Window_BelegData_ProcessOutput(Item) {Owner = this};
				outputWindow.ShowDialog();

				Close();
			}
		}

		private void Canceled()
		{
			using (_managedClosing.Activate())
			{
				var i = Item;
				Item = null;
				Bt.DataFunctions.Cancle_NewBelegData(i);
				Bt.Functions.SetExitCode(ExitCodes.BelegDataCreation_Aborted);
				Close();
			}
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
			if (_managedClosing.Active)
				return;
			e.Cancel = true;
		}

		private void ApprovedClick(object sender, RoutedEventArgs e)
		{
			Item.Typ = (BelegDataTypes) ((FrameworkElement) sender).Tag;
			Approved();
		}

		private void CancleClick(object sender, RoutedEventArgs e)
		{
			Canceled();
		}

		private void NewMailClicked(object sender, RoutedEventArgs e)
		{
			Bt.DataFunctions.CreateNewMailBeleg(Item, "");
		}
		private void NewPrintClicked(object sender, RoutedEventArgs e)
		{
			Bt.DataFunctions.CreateNewPrintBeleg(Item);
		}

		private void DeleteMailClicked(object sender, RoutedEventArgs e)
		{
			var beleg = (MailedBeleg)((FrameworkElement)sender).DataContext;
			beleg.Delete();
		}

		private void DeletePrintClicked(object sender, RoutedEventArgs e)
		{
			var beleg = (PrintedBeleg)((FrameworkElement)sender).DataContext;
			beleg.Delete();
		}
	}
}