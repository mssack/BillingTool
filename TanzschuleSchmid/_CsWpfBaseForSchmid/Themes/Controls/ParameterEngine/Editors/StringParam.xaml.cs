// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

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
		#region DP Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (string), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register("AcceptsReturn", typeof (bool), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof (TextWrapping), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(TextWrapping), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MinHeightTextBoxProperty = DependencyProperty.Register("MinHeightTextBox", typeof (double), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty VerticalContentAlignmentTextBoxProperty = DependencyProperty.Register("VerticalContentAlignmentTextBox", typeof (VerticalAlignment), typeof (StringParam), new FrameworkPropertyMetadata {DefaultValue = default(VerticalAlignment), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
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

		public double MinHeightTextBox
		{
			get { return (double) GetValue(MinHeightTextBoxProperty); }
			set { SetValue(MinHeightTextBoxProperty, value); }
		}


		public VerticalAlignment VerticalContentAlignmentTextBox
		{
			get { return (VerticalAlignment) GetValue(VerticalContentAlignmentTextBoxProperty); }
			set { SetValue(VerticalContentAlignmentTextBoxProperty, value); }
		}
	}
}