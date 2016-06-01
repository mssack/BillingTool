// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using CsWpfBase.Themes.Controls.Editors.Base;






namespace CsWpfBase.Themes.Controls.Basics
{
	/// <summary>
	///     Can be used like a text box. It offers functionality to present a alternating text while <see cref="AlternatingTextBox.AlternatingText" /> is
	///     null or empty.
	/// </summary>
	public class AlternatingTextBox : EditorBase
	{
		#region DP Keys
		/// <summary></summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (AlternatingTextBox), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty AlternatingTextProperty = DependencyProperty.Register("AlternatingText", typeof (string), typeof (AlternatingTextBox), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty AlternatingVisibleProperty = DependencyProperty.Register("AlternatingVisible", typeof (bool), typeof (AlternatingTextBox), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty AlternatingBrushProperty = DependencyProperty.Register("AlternatingBrush", typeof (Brush), typeof (AlternatingTextBox), new FrameworkPropertyMetadata {DefaultValue = default(Brush), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty AcceptReturnProperty = TextBoxBase.AcceptsReturnProperty.AddOwner(typeof (AlternatingTextBox));
		/// <summary></summary>
		public static readonly DependencyProperty TextWrappingProperty = TextBox.TextWrappingProperty.AddOwner(typeof (AlternatingTextBox));
		#endregion


		static AlternatingTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AlternatingTextBox), new FrameworkPropertyMetadata(typeof (AlternatingTextBox)));
		}

		/// <summary>The text which is displayed. If null the <see cref="AlternatingText" /> will be displayed.</summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		/// <summary>Look at <see cref="TextBoxBase.AcceptsReturn" />.</summary>
		public bool AcceptReturn
		{
			get { return (bool) GetValue(AcceptReturnProperty); }
			set { SetValue(AcceptReturnProperty, value); }
		}
		/// <summary>Look at <see cref="TextBox.TextWrapping" />.</summary>
		public TextWrapping TextWrapping
		{
			get { return (TextWrapping) GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}
		/// <summary>The text which will be displayed if <see cref="Text" /> is null or empty.</summary>
		public string AlternatingText
		{
			get { return (string) GetValue(AlternatingTextProperty); }
			set { SetValue(AlternatingTextProperty, value); }
		}
		/// <summary>Indicates if alternating text is visible.</summary>
		public bool AlternatingVisible
		{
			get { return (bool) GetValue(AlternatingVisibleProperty); }
			set { SetValue(AlternatingVisibleProperty, value); }
		}
		/// <summary>The brush which is used if alternating text is visible.</summary>
		public Brush AlternatingBrush
		{
			get { return (Brush) GetValue(AlternatingBrushProperty); }
			set { SetValue(AlternatingBrushProperty, value); }
		}
	}
}