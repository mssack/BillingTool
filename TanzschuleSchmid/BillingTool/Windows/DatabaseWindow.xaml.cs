// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for DatabaseWindow.xaml</summary>
	public partial class DatabaseWindow : CsWindow
	{

		/// <summary>ctor</summary>
		public DatabaseWindow()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "DatabaseWindow");
			Loaded += LogsWindow_Loaded;
			Closing += LogsWindow_Closing;
		}

		/// <summary>All logs from <see cref="From" /> to <see cref="To" /> date.</summary>
		public DateTime From
		{
			get { return (DateTime) GetValue(FromProperty); }
			set { SetValue(FromProperty, value); }
		}
		/// <summary>All logs from <see cref="From" /> to <see cref="To" /> date.</summary>
		public DateTime To
		{
			get { return (DateTime) GetValue(ToProperty); }
			set { SetValue(ToProperty, value); }
		}
		/// <summary>The filtered list with the <see cref="Log" />'s between <see cref="From" /> and <see cref="To" />.</summary>
		public ContractCollection<Log> FilteredLogs
		{
			get { return (ContractCollection<Log>) GetValue(FilteredLogsProperty); }
			set { SetValue(FilteredLogsProperty, value); }
		}

		/// <summary>The filtered list with the <see cref="BelegData" /> between <see cref="From" /> and <see cref="To" />.</summary>
		public ContractCollection<BelegData> FilteredBelegDaten
		{
			get { return (ContractCollection<BelegData>) GetValue(FilteredBelegDatenProperty); }
			set { SetValue(FilteredBelegDatenProperty, value); }
		}

		private void LogsWindow_Closing(object sender, CancelEventArgs e)
		{
			Bt.Db.Billing.SaveUnspecific();
		}

		private void LogsWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Refilter();
		}

		private void TabItemChanged(object sender, SelectionChangedEventArgs e)
		{
			if (BelegDataTab.IsSelected && (FilteredBelegDaten == null || !Equals(FilteredBelegDaten.Tag, $"{From}{To}")))
				Refilter();
			else if (LogsTab.IsSelected && (FilteredLogs == null || !Equals(FilteredLogs.Tag, $"{From}{To}")))
				Refilter();
		}

		/// <summary>Will be invoked whenever From or To date changes</summary>
		private void Refilter()
		{
			Bt.EnsureInitialization();
			if (BelegDataTab == null || BelegDataTab.IsSelected)
			{
				FilteredBelegDaten = Bt.Db.Billing.BelegDaten.Get_Between(From, To);
				FilteredBelegDaten.Tag = $"{From}{To}";
			}
			else if (LogsTab.IsSelected)
			{
				FilteredLogs = Bt.Db.Billing.Logs.Get_Between(From, To);
				FilteredLogs.Tag = $"{From}{To}";
			}
		}


		private void ÄnderungenVerwerfenClicked(object sender, RoutedEventArgs e)
		{
			if (BelegDataTab.IsSelected)
				Bt.Db.Billing.BelegDaten.RejectChanges();
			else if (LogsTab.IsSelected)
				Bt.Db.Billing.Logs.RejectChanges();
		}


#pragma warning disable 1591


		#region DP Keys
		public static readonly DependencyProperty FilteredBelegDatenProperty = DependencyProperty.Register("FilteredBelegDaten", typeof(ContractCollection<BelegData>), typeof(DatabaseWindow), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<BelegData>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FilteredLogsProperty = DependencyProperty.Register("FilteredLogs", typeof(ContractCollection<Log>), typeof(DatabaseWindow), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<Log>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(DateTime), typeof(DatabaseWindow), new FrameworkPropertyMetadata
		{
			DefaultValue = DateTime.Now - TimeSpan.FromDays(14), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((DatabaseWindow) o).Refilter()
		});
		public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(DateTime), typeof(DatabaseWindow), new FrameworkPropertyMetadata
		{
			DefaultValue = DateTime.Now,
			BindsTwoWayByDefault = true,
			DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((DatabaseWindow) o).Refilter()
		});
		#endregion


#pragma warning restore 1591
	}
}