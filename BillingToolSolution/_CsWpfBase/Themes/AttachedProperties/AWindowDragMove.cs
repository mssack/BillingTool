// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;





namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>Allows to apply the <see cref="Window.DragMove" /> method to any UIElement contained in a window.</summary>
	public class AWindowDragMove
	{
#pragma warning disable 1591
		public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached("IsActive", typeof (bool), typeof (AWindowDragMove), new UIPropertyMetadata {DefaultValue = default(bool), PropertyChangedCallback = (o, args) => IsActiveChanged(((FrameworkElement) o), args)});
		public static readonly DependencyProperty DestinationWindowProperty = DependencyProperty.RegisterAttached("DestinationWindow", typeof (Window), typeof (AWindowDragMove), new FrameworkPropertyMetadata {DefaultValue = default(Window), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		private static readonly DependencyPropertyKey IsCurrentlyDraggingPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsCurrentlyDragging", typeof (bool), typeof (AWindowDragMove), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty IsCurrentlyDraggingProperty = IsCurrentlyDraggingPropertyKey.DependencyProperty;
		public static readonly DependencyProperty AutoNormalizeOnDragProperty = DependencyProperty.RegisterAttached("AutoNormalizeOnDrag", typeof (bool), typeof (AWindowDragMove), new FrameworkPropertyMetadata {DefaultValue = true, BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});

		public static bool GetAutoNormalizeOnDrag(DependencyObject obj)
		{
			return (bool) obj.GetValue(AutoNormalizeOnDragProperty);
		}
		public static void SetAutoNormalizeOnDrag(DependencyObject obj, bool value)
		{
			obj.SetValue(AutoNormalizeOnDragProperty, value);
		}
		public static bool GetIsActive(DependencyObject obj)
		{
			return (bool) obj.GetValue(IsActiveProperty);
		}
		public static void SetIsActive(DependencyObject obj, bool value)
		{
			obj.SetValue(IsActiveProperty, value);
		}

		public static Window GetDestinationWindow(DependencyObject obj)
		{
			return (Window) obj.GetValue(DestinationWindowProperty);
		}
		public static void SetDestinationWindow(DependencyObject obj, Window value)
		{
			obj.SetValue(DestinationWindowProperty, value);
		}
#pragma warning restore 1591


		private static void IsActiveChanged(FrameworkElement o, DependencyPropertyChangedEventArgs args)
		{
			if ((bool) args.NewValue)
			{
				o.MouseDown += MouseDown;
				o.MouseUp += MouseUp;
			}
			else
			{
				o.MouseDown -= MouseDown;
				o.MouseUp -= MouseUp;
			}
		}

		private static void MouseUp(object sender, MouseButtonEventArgs e)
		{
		}

		private static void MouseDown(object sender, MouseButtonEventArgs args)
		{
			var casetedSender = sender as DependencyObject;
			if (casetedSender == null)
				return;
			if (args.ChangedButton != MouseButton.Left || args.ButtonState != MouseButtonState.Pressed)
				return;

			InternalDragMove(sender as DependencyObject);
			args.Handled = true;
		}


		private static void InternalDragMove(DependencyObject sender)
		{
			Window destinationWindow = GetDestinationWindow(sender);
			if (destinationWindow == null)
			{
				destinationWindow = Window.GetWindow(sender);
				if (destinationWindow == null)
					return;
				SetDestinationWindow(sender, destinationWindow);
			}
			bool automimize = GetAutoNormalizeOnDrag(sender);

			if (automimize && destinationWindow.WindowState == WindowState.Maximized)
			{
				Point pointToScreen = destinationWindow.PointToScreen(Mouse.GetPosition(destinationWindow));



				double relX = (pointToScreen.X)/(destinationWindow.ActualWidth);
				double relY = (pointToScreen.Y)/(destinationWindow.ActualHeight);



				destinationWindow.Left = pointToScreen.X - (destinationWindow.Width*relX);
				destinationWindow.Top = pointToScreen.Y - (destinationWindow.Height*relY);


				destinationWindow.WindowState = WindowState.Normal;
			}

			destinationWindow.DragMove();
		}
	}
}