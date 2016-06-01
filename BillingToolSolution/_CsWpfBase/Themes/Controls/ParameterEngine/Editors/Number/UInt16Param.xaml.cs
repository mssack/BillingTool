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
	public class UInt16Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof (UInt16), typeof (UInt16Param), new FrameworkPropertyMetadata {DefaultValue = default(UInt16), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof (UInt16), typeof (UInt16Param), new FrameworkPropertyMetadata {DefaultValue = default(UInt16), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (UInt16Param), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (UInt16?), typeof (UInt16Param), new FrameworkPropertyMetadata {DefaultValue = default(UInt16?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static UInt16Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (UInt16Param), new FrameworkPropertyMetadata(typeof (UInt16Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public UInt16 Maximum
		{
			get { return (UInt16) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public UInt16 Minimum
		{
			get { return (UInt16) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public UInt16? Value
		{
			get { return (UInt16?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}