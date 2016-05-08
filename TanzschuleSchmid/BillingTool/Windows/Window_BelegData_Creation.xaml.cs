// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_BelegData_Creation.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_Creation : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(Window_BelegData_Creation), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>Ctor</summary>
		public Window_BelegData_Creation()
		{
			Item = Bt.DataFunctions.New_BelegData_From_Configuration();
			InitializeComponent();

		}

		/// <summary>The newly created Item</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}


		private void Cancle()
		{
			Bt.DataFunctions.Cancle_New_BelegData(Item);
		}
	}
}