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
	public class DecimalParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DecimalEditor.ValueNumberProperty.AddOwner(typeof (DecimalParam));
		public static readonly DependencyProperty MinimumProperty = DecimalEditor.MinimumProperty.AddOwner(typeof (DecimalParam));
		public static readonly DependencyProperty MaximumProperty = DecimalEditor.MaximumProperty.AddOwner(typeof (DecimalParam));
		public static readonly DependencyProperty AllowNullProperty = NumberEditor.AllowNullProperty.AddOwner(typeof (DecimalParam));
		#endregion


		static DecimalParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DecimalParam), new FrameworkPropertyMetadata(typeof (DecimalParam)));
		}

		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public Decimal Maximum
		{
			get { return (Decimal) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public Decimal Minimum
		{
			get { return (Decimal) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public Decimal? Value
		{
			get { return (Decimal?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}