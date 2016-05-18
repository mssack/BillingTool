// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Viewer.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_Viewer : CsWindow
	{


		/// <summary>ctor</summary>
		public Window_BelegData_Viewer()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "Window_BelegData_Viewer");
			Loaded += WindowLoaded;
		}


		/// <summary>The currently selected item.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>The filtered Items which are currently selectable.</summary>
		public ContractCollection<BelegData> FilteredItems
		{
			get { return (ContractCollection<BelegData>) GetValue(FilteredItemsProperty); }
			set { SetValue(FilteredItemsProperty, value); }
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			var latestTen = Bt.Db.Billing.BelegDaten.LoadThenFind_Latest(10);
			FromToSelector.From = latestTen.Any() ? latestTen.Min(x => x.Datum) : DateTime.Now;
			FromToSelector.To = DateTime.Now;
			Refilter();
			FromToSelector.SelectionChanged += FromToSelector_SelectionChanged;
		}

		private void FromToSelector_SelectionChanged()
		{
			Refilter();
		}

		private void Refilter()
		{
			if (FilteredItems != null && Equals(FilteredItems.Tag, $"{FromToSelector.From.Date}{FromToSelector.To.Date}"))
				return;
			Bt.EnsureInitialization();
			var collection = Bt.Db.Billing.BelegDaten.LoadThenFind_Between(FromToSelector.From, FromToSelector.To);
			collection.Tag = $"{FromToSelector.From.Date}{FromToSelector.To.Date}";
			collection.SortDesc(x => x.Nummer);
			FilteredItems = collection;

			if (Item == null)
				Item = FilteredItems.Count == 0 ? null : FilteredItems[0];
		}

		private void NeuerBelegClicked(object sender, RoutedEventArgs e)
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var w = new Window_BelegData_Creation(false)
				{
					Item = Bt.Data.BelegData.New_FromConfiguration(), Owner = this,
					WindowStartupLocation = WindowStartupLocation.CenterOwner
				};
				w.ShowDialog();
			}
		}


#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(Window_BelegData_Viewer), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FilteredItemsProperty = DependencyProperty.Register("FilteredItems", typeof(ContractCollection<BelegData>), typeof(Window_BelegData_Viewer), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<BelegData>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}