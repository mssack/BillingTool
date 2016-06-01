// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;





namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>Contains different properties to apply logic to a text box.</summary>
	public class ATextBox
	{
#pragma warning disable 1591
		public static readonly DependencyProperty AutoSelectionProperty = DependencyProperty.RegisterAttached("AutoSelection", typeof (bool), typeof (ATextBox), new FrameworkPropertyMetadata
																																								{
																																									DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) =>
																																									{
																																										if (o is TextBox)
																																											HandleAutoSelection(o as TextBox, args);
																																									}
																																								});
		public static bool GetAutoSelection(DependencyObject obj)
		{
			return (bool) obj.GetValue(AutoSelectionProperty);
		}
		public static void SetAutoSelection(DependencyObject obj, bool value)
		{
			obj.SetValue(AutoSelectionProperty, value);
		}
#pragma warning restore 1591


		private static void HandleAutoSelection(TextBox textBox, DependencyPropertyChangedEventArgs args)
		{
			if ((bool) args.NewValue)
			{
				textBox.PreviewMouseLeftButtonDown += SelectAllTextBoxPreviewMouseDown;
				textBox.GotKeyboardFocus += SelectAllTextBox;
				textBox.MouseDoubleClick += SelectAllTextBox;
			}
			else
			{
				textBox.PreviewMouseLeftButtonDown -= SelectAllTextBoxPreviewMouseDown;
				textBox.GotKeyboardFocus -= SelectAllTextBox;
				textBox.MouseDoubleClick -= SelectAllTextBox;
			}
		}
		private static void SelectAllTextBoxPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			DependencyObject parent = e.OriginalSource as UIElement;
			while (parent != null && !(parent is TextBox))
				parent = VisualTreeHelper.GetParent(parent);

			if (parent != null)
			{
				var textBox = (TextBox) parent;
				if (!textBox.IsKeyboardFocusWithin)
				{
					// If the text box is not yet focused, give it the focus and
					// stop further processing of this click event.
					textBox.Focus();
					e.Handled = true;
				}
			}
		}
		private static void SelectAllTextBox(object sender, RoutedEventArgs e)
		{
			var textBox = e.OriginalSource as TextBox;
			if (textBox != null)
				textBox.SelectAll();
		}
	}
}