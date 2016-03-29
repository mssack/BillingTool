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
	public class ByteParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof (Byte), typeof (ByteParam), new FrameworkPropertyMetadata {DefaultValue = default(Byte), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof (Byte), typeof (ByteParam), new FrameworkPropertyMetadata {DefaultValue = default(Byte), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (ByteParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (Byte?), typeof (ByteParam), new FrameworkPropertyMetadata {DefaultValue = default(Byte?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static ByteParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ByteParam), new FrameworkPropertyMetadata(typeof (ByteParam)));
		}
		public Byte Minimum
		{
			get { return (Byte) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public Byte Maximum
		{
			get { return (Byte) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public Byte? Value
		{
			get { return (Byte?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}