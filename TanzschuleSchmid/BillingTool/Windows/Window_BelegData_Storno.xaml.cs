// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-30</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Storno.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_Storno : CsWindow
	{


		/// <summary>ctor</summary>
		public Window_BelegData_Storno()
		{
			InitializeComponent();
			FromToSelector.From = DateTime.Now.AddDays(-1);
			FromToSelector.To = DateTime.Now;
			FromToSelector.SelectionChanged += FromToSelector_SelectionChanged;
			Loaded += Window_BelegData_Storno_Loaded;
		}

		/// <summary>The filtered <see cref="BelegData" /> list for the selection.</summary>
		public ContractCollection FilteredBelegDataList
		{
			get { return (ContractCollection) GetValue(FilteredBelegDataListProperty); }
			set { SetValue(FilteredBelegDataListProperty, value); }
		}
		/// <summary>The currently selected <see cref="BelegData" />. This is meant to be the item which needs to be stornod.</summary>
		public BelegData SelectedBelegData
		{
			get { return (BelegData) GetValue(SelectedBelegDataProperty); }
			set { SetValue(SelectedBelegDataProperty, value); }
		}

		private void Window_BelegData_Storno_Loaded(object sender, RoutedEventArgs e)
		{
			Refilter();
		}


		private void Refilter()
		{
			if (FilteredBelegDataList != null && Equals(FilteredBelegDataList.Tag, $"{FromToSelector.From}{FromToSelector.To}"))
				return;
			Bt.Db.EnsureConnectivity();
			FilteredBelegDataList = Bt.Db.Billing.BelegDaten.Get_Between(FromToSelector.From, FromToSelector.To, data => data.State != BelegDataStates.Storno);
		}

		private void FromToSelector_SelectionChanged()
		{
			Refilter();
		}
#pragma warning disable 1591
		public static readonly DependencyProperty FilteredBelegDataListProperty = DependencyProperty.Register("FilteredBelegDataList", typeof(ContractCollection), typeof(Window_BelegData_Storno), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SelectedBelegDataProperty = DependencyProperty.Register("SelectedBelegData", typeof(BelegData), typeof(Window_BelegData_Storno), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}