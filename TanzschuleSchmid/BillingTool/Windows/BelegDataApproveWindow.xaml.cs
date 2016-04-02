// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for BelegDataApproveWindow.xaml</summary>
	public partial class BelegDataApproveWindow : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (BelegData), typeof (BelegDataApproveWindow), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BelegDataApproveWindow) o).ItemChanged(args.NewValue as BelegData)});

#pragma warning restore 1591
		#endregion


		readonly ProcessLock _managedClosing = new ProcessLock();

		/// <summary>ctor</summary>
		public BelegDataApproveWindow(BelegData item = null)
		{
			InitializeComponent();
			this.Closing += WindowClosing;
			Item = item ?? Bt.Functions.New_BelegData_FromConfiguration();
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
				Bt.Functions.Save_NewBelegData(Item);
				Bt.Functions.SetExitCode(ExitCodes.NewBelegData_Created);
				Close();
			}
		}
		private void Canceled()
		{
			using (_managedClosing.Activate())
			{
				var i = Item;
				Item = null;
				Bt.Functions.Cancle_NewBelegData(i);
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
		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
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
	}
}