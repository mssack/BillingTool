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
	public class UInt64Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = UInt64Editor.ValueNumberProperty.AddOwner(typeof (UInt64Param));
		public static readonly DependencyProperty MinimumProperty = UInt64Editor.MinimumProperty.AddOwner(typeof (UInt64Param));
		public static readonly DependencyProperty MaximumProperty = UInt64Editor.MaximumProperty.AddOwner(typeof (UInt64Param));
		public static readonly DependencyProperty AllowNullProperty = NumberEditor.AllowNullProperty.AddOwner(typeof (UInt64Param));
		#endregion


		static UInt64Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (UInt64Param), new FrameworkPropertyMetadata(typeof (UInt64Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public UInt64 Maximum
		{
			get { return (UInt64) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public UInt64 Minimum
		{
			get { return (UInt64) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public UInt64? Value
		{
			get { return (UInt64?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}