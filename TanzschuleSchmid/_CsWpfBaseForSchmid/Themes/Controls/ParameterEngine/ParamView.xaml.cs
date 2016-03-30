// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine
{
	/// <summary>Used to view the value of a parameter.</summary>
	public class ParamView : ParameterEngineBase
	{
#pragma warning disable 1591
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (object), typeof (ParamView), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AutoHideProperty = DependencyProperty.Register("AutoHide", typeof (bool), typeof (ParamView), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ValueStringFormatProperty = DependencyProperty.Register("ValueStringFormat", typeof (string), typeof (ParamView), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591


		static ParamView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ParamView), new FrameworkPropertyMetadata(typeof (ParamView)));
		}

		/// <summary>The Value to display.</summary>
		public object Value
		{
			get { return GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		/// <summary>The string format which applies to the value</summary>
		public string ValueStringFormat
		{
			get { return (string) GetValue(ValueStringFormatProperty); }
			set { SetValue(ValueStringFormatProperty, value); }
		}
		/// <summary>If active the parameter will be collapsed if the value is null or empty.</summary>
		public bool AutoHide
		{
			get { return (bool) GetValue(AutoHideProperty); }
			set { SetValue(AutoHideProperty, value); }
		}
	}
}