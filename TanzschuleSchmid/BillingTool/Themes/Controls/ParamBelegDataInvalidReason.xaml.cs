// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;






namespace BillingTool.Themes.Controls
{
	/// <summary>Used to show the current <see cref="BelegDataInvalidReasons" /> to the user.</summary>
	public class ParamBelegDataInvalidReason : ParameterEngineBase
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(BelegData), typeof(ParamBelegDataInvalidReason), new FrameworkPropertyMetadata {DefaultValue = default(BelegData),  DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		static ParamBelegDataInvalidReason()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ParamBelegDataInvalidReason), new FrameworkPropertyMetadata(typeof(ParamBelegDataInvalidReason)));
		}

		/// <summary>The <see cref="BelegData" /> the <see cref="BelegData.InvalidReason" /> should be shown for.</summary>
		public BelegData Value
		{
			get { return (BelegData) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}