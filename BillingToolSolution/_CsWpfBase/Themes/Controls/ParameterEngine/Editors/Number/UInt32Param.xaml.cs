// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using CsWpfBase.Themes.Controls.Editors.Base;
using CsWpfBase.Themes.Controls.Editors.Number;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors.Number
{
#pragma warning disable 1591
	public class UInt32Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = UInt32Editor.ValueNumberProperty.AddOwner(typeof (UInt32Param));
		public static readonly DependencyProperty MinimumProperty = UInt32Editor.MinimumProperty.AddOwner(typeof (UInt32Param));
		public static readonly DependencyProperty MaximumProperty = UInt32Editor.MaximumProperty.AddOwner(typeof (UInt32Param));
		public static readonly DependencyProperty AllowNullProperty = NumberEditor.AllowNullProperty.AddOwner(typeof (UInt32Param));
		#endregion


		static UInt32Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (UInt32Param), new FrameworkPropertyMetadata(typeof (UInt32Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public UInt32 Maximum
		{
			get { return (UInt32) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public UInt32 Minimum
		{
			get { return (UInt32) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public UInt32? Value
		{
			get { return (UInt32?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}