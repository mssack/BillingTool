// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.Editors;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors
{
#pragma warning disable 1591
	/// <summary>An editor for a time only field.</summary>
	public class TimeParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = TimeEditor.ValueProperty.AddOwner(typeof (TimeParam), new FrameworkPropertyMetadata {DefaultValue = default(DateTime?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowNullProperty = TimeEditor.AllowNullProperty.AddOwner(typeof (TimeParam));
		#endregion


		static TimeParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TimeParam), new FrameworkPropertyMetadata(typeof (TimeParam)));
		}
		/// <summary>The value from which the date will be taken.</summary>
		public DateTime? Value
		{
			get { return (DateTime?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		/// <summary>Specify if the source value can be set to null.</summary>
		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
	}
}