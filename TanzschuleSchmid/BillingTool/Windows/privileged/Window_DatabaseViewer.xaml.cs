// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-30</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Db.models;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows.privileged
{
	/// <summary>Interaction logic for Window_DatabaseViewer.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_DatabaseViewer : CsWindow
	{

		/// <summary>ctor</summary>
		public Window_DatabaseViewer()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "Window_DatabaseViewer");
			Closing += DatabaseWindow_Closing;
			FromToSelector.From = DateTime.Now.AddDays(-7);
			FromToSelector.To = DateTime.Now;
			FromToSelector.SelectionChanged += FromToSelectorControl_OnSelectionChanged;
		}

		/// <summary>The filtered list with the <see cref="Log" />'s between two dates.</summary>
		public ContractCollection<Log> FilteredLogs
		{
			get { return (ContractCollection<Log>) GetValue(FilteredLogsProperty); }
			set { SetValue(FilteredLogsProperty, value); }
		}

		/// <summary>The filtered list with the <see cref="BelegData" /> between two dates.</summary>
		public ContractCollection<BelegData> FilteredBelegDaten
		{
			get { return (ContractCollection<BelegData>) GetValue(FilteredBelegDatenProperty); }
			set { SetValue(FilteredBelegDatenProperty, value); }
		}



		private void DatabaseWindow_Closing(object sender, CancelEventArgs e)
		{
			Bt.Db.Billing.SaveUnspecific();
		}


		private void TabItemChanged(object sender, SelectionChangedEventArgs e)
		{
			Refilter();
		}

		/// <summary>Will be invoked whenever From or To date changes</summary>
		private void Refilter()
		{
			if (BelegDataTab == null)
				return;


			Bt.EnsureInitialization();
			if (BelegDataTab.IsSelected && (FilteredBelegDaten == null || !Equals(FilteredBelegDaten.Tag, $"{FromToSelector.From.Date}{FromToSelector.To.Date}")))
			{
				FilteredBelegDaten = Bt.Db.Billing.BelegDaten.Get_Between(FromToSelector.From, FromToSelector.To);
				FilteredBelegDaten.Tag = $"{FromToSelector.From.Date}{FromToSelector.To.Date}";
			}
			else if (LogsTab.IsSelected && (FilteredLogs == null || !Equals(FilteredLogs.Tag, $"{FromToSelector.From.Date}{FromToSelector.To.Date}")))
			{
				FilteredLogs = Bt.Db.Billing.Logs.Get_Between(FromToSelector.From, FromToSelector.To);
				FilteredLogs.Tag = $"{FromToSelector.From.Date}{FromToSelector.To.Date}";
			}
			else if (ConfigurationTab.IsSelected && !Bt.Db.Billing.Configurations.HasBeenLoaded)
			{
				Bt.Db.Billing.Configurations.DownloadRows();
			}
			else if (PostenTab.IsSelected && !Bt.Db.Billing.Postens.HasBeenLoaded)
			{
				Bt.Db.Billing.Postens.DownloadRows();
			}
		}


		private void ÄnderungenVerwerfenClicked(object sender, RoutedEventArgs e)
		{
			var table = (TabControl1.SelectedContent as TabItem)?.Tag as CsDbTable;
			if (table == null)
				return;

			table.RejectChanges();
		}

		private void FromToSelectorControl_OnSelectionChanged()
		{
			Refilter();
		}


#pragma warning disable 1591


		#region DP Keys
		public static readonly DependencyProperty FilteredBelegDatenProperty = DependencyProperty.Register("FilteredBelegDaten", typeof(ContractCollection<BelegData>), typeof(Window_DatabaseViewer), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<BelegData>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FilteredLogsProperty = DependencyProperty.Register("FilteredLogs", typeof(ContractCollection<Log>), typeof(Window_DatabaseViewer), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<Log>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


#pragma warning restore 1591
	}
}