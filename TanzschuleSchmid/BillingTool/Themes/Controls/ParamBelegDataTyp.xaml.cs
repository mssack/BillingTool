// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;






namespace BillingTool.Themes.Controls
{
	/// <summary>Used for column <see cref="BelegData.Typ" />.</summary>
	public class ParamBelegDataTyp : ParameterEngineBase
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (BelegDataTypes), typeof (ParamBelegDataTyp), new FrameworkPropertyMetadata {DefaultValue = default(BelegDataTypes), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		static ParamBelegDataTyp()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ParamBelegDataTyp), new FrameworkPropertyMetadata(typeof (ParamBelegDataTyp)));
		}

		/// <summary>The current selected type.</summary>
		public BelegDataTypes Value
		{
			get { return (BelegDataTypes) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}