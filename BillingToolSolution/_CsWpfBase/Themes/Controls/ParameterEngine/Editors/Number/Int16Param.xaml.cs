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
	public class Int16Param : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = Int16Editor.ValueNumberProperty.AddOwner(typeof (Int16Param));
		public static readonly DependencyProperty MinimumProperty = Int16Editor.MinimumProperty.AddOwner(typeof (Int16Param));
		public static readonly DependencyProperty MaximumProperty = Int16Editor.MaximumProperty.AddOwner(typeof (Int16Param));
		public static readonly DependencyProperty AllowNullProperty = NumberEditor.AllowNullProperty.AddOwner(typeof (Int16Param));
		#endregion


		static Int16Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Int16Param), new FrameworkPropertyMetadata(typeof (Int16Param)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public Int16 Maximum
		{
			get { return (Int16) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public Int16 Minimum
		{
			get { return (Int16) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public Int16? Value
		{
			get { return (Int16?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}