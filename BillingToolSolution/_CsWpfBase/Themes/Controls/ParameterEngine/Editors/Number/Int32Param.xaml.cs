// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors.Number
{
#pragma warning disable 1591
	public class Int32Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof (Int32), typeof (Int32Param), new FrameworkPropertyMetadata {DefaultValue = default(Int32), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof (Int32), typeof (Int32Param), new FrameworkPropertyMetadata {DefaultValue = default(Int32), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (Int32Param), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (Int32?), typeof (Int32Param), new FrameworkPropertyMetadata {DefaultValue = default(Int32?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static Int32Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Int32Param), new FrameworkPropertyMetadata(typeof (Int32Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public Int32 Maximum
		{
			get { return (Int32) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public Int32 Minimum
		{
			get { return (Int32) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public Int32? Value
		{
			get { return (Int32?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}