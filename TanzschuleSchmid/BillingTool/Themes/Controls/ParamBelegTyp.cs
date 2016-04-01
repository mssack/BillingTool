// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;






namespace BillingTool.Themes.Controls
{
	/// <summary>Used for column <see cref="CashBookEntry.Typ" />.</summary>
	public class ParamBelegTyp : ParameterEngineBase
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (CashBookEntryTypes), typeof (ParamBelegTyp), new FrameworkPropertyMetadata {DefaultValue = default(CashBookEntryTypes), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		static ParamBelegTyp()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ParamBelegTyp), new FrameworkPropertyMetadata(typeof (ParamBelegTyp)));
		}

		/// <summary>The current selected type.</summary>
		public CashBookEntryTypes Value
		{
			get { return (CashBookEntryTypes) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}