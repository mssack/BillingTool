// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using CsWpfBase.Themes.Controls.ParameterEngine;






namespace BillingTool.Themes.Controls.belegapproval
{
	/// <summary>Interaction logic for BelegDataTypParam.xaml</summary>
	public partial class BelegDataTypParam : Param
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(BelegDataTypes), typeof(BelegDataTypParam), new FrameworkPropertyMetadata {DefaultValue = default(BelegDataTypes), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public BelegDataTypParam()
		{
			InitializeComponent();
		}

		/// <summary>param value.</summary>
		public BelegDataTypes Value
		{
			get { return (BelegDataTypes) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}