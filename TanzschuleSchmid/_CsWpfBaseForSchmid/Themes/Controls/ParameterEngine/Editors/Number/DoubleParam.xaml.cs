// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-10</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors.Number
{
#pragma warning disable 1591
	public class DoubleParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (Double?), typeof (DoubleParam), new FrameworkPropertyMetadata {DefaultValue = default(Double?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof (Double), typeof (DoubleParam), new FrameworkPropertyMetadata {DefaultValue = default(Double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof (Double), typeof (DoubleParam), new FrameworkPropertyMetadata {DefaultValue = default(Double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (DoubleParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static DoubleParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DoubleParam), new FrameworkPropertyMetadata(typeof (DoubleParam)));
		}
		public Double? Value
		{
			get { return (Double?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public Double Maximum
		{
			get { return (Double) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public Double Minimum
		{
			get { return (Double) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
	}
}