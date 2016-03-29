// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors
{
#pragma warning disable 1591
	/// <summary>An editor for strings.</summary>
	public class StringParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (string), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register("AcceptsReturn", typeof (bool), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof (TextWrapping), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(TextWrapping), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static StringParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (StringParam), new FrameworkPropertyMetadata(typeof (StringParam)));
		}

		/// <summary>The value which needs to be edited.</summary>
		public string Value
		{
			get { return (string) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		/// <summary>Specifies if returns are allowed.</summary>
		public bool AcceptsReturn
		{
			get { return (bool) GetValue(AcceptsReturnProperty); }
			set { SetValue(AcceptsReturnProperty, value); }
		}

		public TextWrapping TextWrapping
		{
			get { return (TextWrapping) GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}
	}
}