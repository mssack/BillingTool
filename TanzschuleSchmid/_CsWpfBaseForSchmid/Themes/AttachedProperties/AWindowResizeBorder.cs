// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;





namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>Extensions for the manual implementation of resize borders to a window.</summary>
	public class AWindowResizeBorder
	{
		/// <summary>Defines the target resize direction.</summary>
		public enum Target
		{
			/// <summary>corner</summary>
			TopLeft = 13,
			/// <summary>border</summary>
			Top = 12,
			/// <summary>corner</summary>
			TopRight = 14,
			/// <summary>border</summary>
			Right = 11,
			/// <summary>corner</summary>
			BottomRight = 17,
			/// <summary>border</summary>
			Bottom = 15,
			/// <summary>corner</summary>
			BottomLeft = 16,
			/// <summary>border</summary>
			Left = 10,
		}


#pragma warning disable 1591
		public static readonly DependencyProperty TypeProperty = DependencyProperty.RegisterAttached("Type", typeof (Target), typeof (AWindowResizeBorder), new FrameworkPropertyMetadata {DefaultValue = default(Target), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { TypeSetted((FrameworkElement) o, args); }});

		public static Target GetType(DependencyObject obj)
		{
			return (Target) obj.GetValue(TypeProperty);
		}
		public static void SetType(DependencyObject obj, Target value)
		{
			obj.SetValue(TypeProperty, value);
		}
#pragma warning restore 1591

		private static void TypeSetted(FrameworkElement fe, DependencyPropertyChangedEventArgs args)
		{
			if (args.NewValue != null)
			{
				Apply(fe, (Target) args.NewValue);
			}
			else
			{
				UnApply(fe);
			}
		}


		private static void fe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var fe = (FrameworkElement) sender;
			var tag = (Tuple<Window, Target>) fe.Tag;
			if (tag.Item1.WindowState == WindowState.Maximized)
			{
				tag.Item1.Left = 0;
				tag.Item1.Top = 0;
				double width = SystemParameters.WorkArea.Width;
				double height = SystemParameters.WorkArea.Height;

				//TODO Better State Change with holding width and height
				tag.Item1.WindowState = WindowState.Normal;
				tag.Item1.Width = width;
				tag.Item1.Height = height;
			}
			InteropResizer.Resize(tag.Item1, tag.Item2);
			e.Handled = true;
		}
		private static void fe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}
		private static void border_LostMouseCapture(object sender, MouseEventArgs e)
		{
		}
		private static void UnApply(FrameworkElement border)
		{
			border.Tag = null;

			border.MouseLeftButtonDown -= fe_MouseLeftButtonDown;
			border.MouseLeftButtonUp -= fe_MouseLeftButtonUp;
			border.LostMouseCapture -= border_LostMouseCapture;
		}
		private static void Apply(FrameworkElement fe, Target target)
		{
			Window window = Window.GetWindow(fe);
			fe.Tag = new Tuple<Window, Target>(window, target);

			fe.MouseLeftButtonDown += fe_MouseLeftButtonDown;
			fe.MouseLeftButtonUp += fe_MouseLeftButtonUp;
			fe.LostMouseCapture += border_LostMouseCapture;
		}


		#region HelperClasses
		private static class InteropResizer
		{
			private const int WmNclbuttondown = 0x00A1;
			[DllImport("user32.dll")]
			private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);
			[DllImport("User32.dll")]
			private static extern bool ReleaseCapture();

			public static void Resize(Window w, Target ha)
			{
				IntPtr windowHandle = new WindowInteropHelper(w).Handle;
				ReleaseCapture();
				SendMessage(windowHandle, WmNclbuttondown, (int) ha, 0);
			}
		}
		#endregion
	}
}