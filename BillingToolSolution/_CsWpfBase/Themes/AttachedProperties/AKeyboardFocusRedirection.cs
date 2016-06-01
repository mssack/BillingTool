// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CsWpfBase.Global;





namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>
	///     Redirects the keyboard focus of the actual element to another element. If the target is not focusable the
	///     first child will be taken which is focusable.
	/// </summary>
	public class AKeyboardFocusRedirection
	{
#pragma warning disable 1591
		public static readonly DependencyProperty TargetProperty = DependencyProperty.RegisterAttached("Target", typeof (FrameworkElement), typeof (AKeyboardFocusRedirection), new FrameworkPropertyMetadata
																																												{
																																													DefaultValue = default(FrameworkElement), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) =>
																																													{
																																														if (o is FrameworkElement)
																																															HandleRedirectMouse(o as FrameworkElement, args);
																																													}
																																												});

		public static FrameworkElement GetTarget(DependencyObject obj)
		{
			return (FrameworkElement) obj.GetValue(TargetProperty);
		}
		public static void SetTarget(DependencyObject obj, UIElement value)
		{
			obj.SetValue(TargetProperty, value);
		}
#pragma warning restore 1591


		private static void HandleRedirectMouse(UIElement uiElement, DependencyPropertyChangedEventArgs args)
		{
			if (args.NewValue is UIElement)
			{
				uiElement.MouseDown += uiElement_MouseDown;
			}
			else if (args.NewValue == null && args.OldValue != null)
			{
				uiElement.MouseDown -= uiElement_MouseDown;
			}
		}

		private static void uiElement_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var senderElement = sender as FrameworkElement;
			FrameworkElement redirectElement = GetTarget(senderElement);
			if (redirectElement.Focusable)
			{
				redirectElement.Focus();
				Keyboard.Focus(redirectElement);
			}
			else
			{
				var focusableElement = CsGlobal.Wpf.VisualTree.FindChild(redirectElement, a =>
				{
					if (a != null && a.Focusable)
						return true;
					return false;
				});
				if (focusableElement != null)
				{
					Keyboard.Focus(focusableElement);
				}
			}
		}
	}
}