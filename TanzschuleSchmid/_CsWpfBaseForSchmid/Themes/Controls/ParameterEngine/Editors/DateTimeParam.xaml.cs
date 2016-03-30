// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors
{
#pragma warning disable 1591
	public class DateTimeParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (DateTime?), typeof (DateTimeParam), new FrameworkPropertyMetadata {DefaultValue = default(DateTime?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (DateTimeParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static DateTimeParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DateTimeParam), new FrameworkPropertyMetadata(typeof (DateTimeParam)));
		}
		public DateTime? Value
		{
			get { return (DateTime?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
	}
}