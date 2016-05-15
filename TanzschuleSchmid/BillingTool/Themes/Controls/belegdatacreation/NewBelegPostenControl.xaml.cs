// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;






#pragma warning disable 1591

namespace BillingTool.Themes.Controls.belegdatacreation
{
	/// <summary>Interaction logic for NewBelegPostenControl.xaml</summary>
	public partial class NewBelegPostenControl : UserControl
	{
		#region DP Keys
		public static readonly DependencyProperty AnzahlProperty = DependencyProperty.Register("Anzahl", typeof(int), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(int), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PostenProperty = DependencyProperty.Register("Posten", typeof(Posten), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(Posten), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SteuersatzProperty = DependencyProperty.Register("Steuersatz", typeof(Steuersatz), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(Steuersatz), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		/// <summary>ctor</summary>
		public NewBelegPostenControl()
		{
			InitializeComponent();
			Loaded += NewBelegPostenControl_Loaded;
			Reset();
		}

		private void NewBelegPostenControl_Loaded(object sender, RoutedEventArgs e)
		{
			Reset();
		}

		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		public int Anzahl
		{
			get { return (int) GetValue(AnzahlProperty); }
			set { SetValue(AnzahlProperty, value); }
		}
		public Posten Posten
		{
			get { return (Posten) GetValue(PostenProperty); }
			set { SetValue(PostenProperty, value); }
		}
		public Steuersatz Steuersatz
		{
			get { return (Steuersatz) GetValue(SteuersatzProperty); }
			set { SetValue(SteuersatzProperty, value); }
		}

		private void Reset()
		{
			Bt.EnsureInitialization();
			if (!Bt.Db.Billing.Postens.HasBeenLoaded)
				Bt.Db.Billing.Postens.DownloadRows();
			if (!Bt.Db.Billing.Steuersätze.HasBeenLoaded)
				Bt.Db.Billing.Steuersätze.DownloadRows();


			Anzahl = 1;
			Posten = Bt.Db.Billing.Postens.OrderByDescending(x=>x.LastUsedDate).FirstOrDefault();
			Steuersatz = Bt.Db.Billing.Steuersätze.OrderByDescending(x => x.LastUsedDate).FirstOrDefault();
		}

		private void ErstellenClick(object sender, RoutedEventArgs e)
		{
			Bt.Data.BelegPosten.New(Item, Anzahl, Posten, Steuersatz);
			Reset();
		}
	}
}