// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-30</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Themes.Controls
{
	/// <summary>This control is used to represent a <see cref="BelegData" /> for viewing purpose. This helps to separate style changes of Bon controls.</summary>
	public class BelegDataView : Control
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		static BelegDataView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BelegDataView), new FrameworkPropertyMetadata(typeof(BelegDataView)));
		}

		/// <summary>The item to view.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
	}
}