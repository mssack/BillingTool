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
	public class Int64Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = Int64Editor.ValueNumberProperty.AddOwner(typeof (Int64Param));
		public static readonly DependencyProperty MinimumProperty = Int64Editor.MinimumProperty.AddOwner(typeof (Int64Param));
		public static readonly DependencyProperty MaximumProperty = Int64Editor.MaximumProperty.AddOwner(typeof (Int64Param));
		public static readonly DependencyProperty AllowNullProperty = NumberEditor.AllowNullProperty.AddOwner(typeof (Int64Param));
		#endregion


		static Int64Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Int64Param), new FrameworkPropertyMetadata(typeof (Int64Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public Int64 Maximum
		{
			get { return (Int64) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public Int64 Minimum
		{
			get { return (Int64) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public Int64? Value
		{
			get { return (Int64?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}