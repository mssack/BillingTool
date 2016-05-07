// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Themes.Controls.Containers;






namespace CsWpfBase.Global.wpf.Storage
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public class CsgWpfStorageWindow : Base
	{
		private static CsgWpfStorageWindow _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpfStorageWindow I
		{
			get { return _instance ?? (_instance = CsGlobal.Storage.Private.Handle("Configuration-Windows", () => new CsgWpfStorageWindow())); }
		}
		private List<WindowHandle> _handles;

		private CsgWpfStorageWindow()
		{
		}

		private List<WindowHandle> Handles
		{
			get { return _handles ?? (_handles = new List<WindowHandle>()); }
		}

		/// <summary>Get a rectangle which is sure on the screen. Use this method to avoid hidden windows after unplugging a screen.</summary>
		public Rect GetFittingRectangle(double left, double top, double width, double height)
		{
			if (left < SystemParameters.VirtualScreenLeft)
				left = SystemParameters.VirtualScreenLeft;
			if (top < SystemParameters.VirtualScreenTop)
				top = SystemParameters.VirtualScreenTop;

			if (left + width > SystemParameters.VirtualScreenWidth)
			{
				if (SystemParameters.VirtualScreenLeft + width > SystemParameters.VirtualScreenWidth)
				{
					left = SystemParameters.VirtualScreenLeft;
					width = SystemParameters.VirtualScreenWidth;
				}
				else
				{
					left = left - ((left + width) - SystemParameters.VirtualScreenWidth);
				}
			}
			if (top + height > SystemParameters.VirtualScreenHeight)
			{
				if (SystemParameters.VirtualScreenTop + height > SystemParameters.VirtualScreenHeight)
				{
					top = SystemParameters.VirtualScreenTop;
					height = SystemParameters.VirtualScreenHeight;
				}
				else
				{
					top = top - ((top + height) - SystemParameters.VirtualScreenHeight);
				}
			}
			return new Rect(left, top, width, height);
		}

		/// <summary>Adds a new handle</summary>
		public void Handle(Window w, string name, bool includeScaling = false)
		{
			var handle = Handles.FirstOrDefault(x => x.Name == name);
			if (handle != null)
			{
				handle.ApplyWindow(w);
				if (w.IsLoaded == false)
				{
					var fittingRectangle = GetFittingRectangle(handle.Left, handle.Top, handle.Width, handle.Height);

					w.WindowStartupLocation = WindowStartupLocation.Manual;
					w.Width = fittingRectangle.Width;
					w.Height = fittingRectangle.Height;
					w.Left = fittingRectangle.Left;
					w.Top = fittingRectangle.Top;
					w.Topmost = handle.Topmost;
					if (w is CsWindow && includeScaling)
						((CsWindow) w).Scale = handle.Scale;
				}
			}
			else
			{
				handle = new WindowHandle(w, name);
				Handles.Add(handle);
			}
		}



		/// <summary>Handles the automated position save. Also provides functionality the avoid invisible positions of the window.</summary>
		[Serializable]
		private class WindowHandle : Base
		{
			private readonly string _name;
			private double _height;
			private double _left;
			private double _scale = 1;
			private double _top;
			private bool _topmost;
			private double _width;
			[field: NonSerialized] private Window _window;

			internal WindowHandle(Window window, string name)
			{
				_name = name;
				ApplyWindow(window);
			}

			/// <summary>Associated window name.</summary>
			public string Name
			{
				get { return _name; }
			}
			/// <summary>The X position of the window.</summary>
			public double Left
			{
				get { return _left; }
				private set { SetProperty(ref _left, value); }
			}
			/// <summary>The Y position of the window.</summary>
			public double Top
			{
				get { return _top; }
				private set { SetProperty(ref _top, value); }
			}
			/// <summary>The width of the window.</summary>
			public double Width
			{
				get { return _width; }
				private set { SetProperty(ref _width, value); }
			}
			/// <summary>The height of the window.</summary>
			public double Height
			{
				get { return _height; }
				private set { SetProperty(ref _height, value); }
			}
			public bool Topmost
			{
				get { return _topmost; }
				set { SetProperty(ref _topmost, value); }
			}
			/// <summary>The zoom of the window.</summary>
			public double Scale
			{
				get { return _scale; }
				private set { SetProperty(ref _scale, value); }
			}
			private Window Window
			{
				get { return _window; }
				set { SetProperty(ref _window, value); }
			}

			internal void ApplyWindow(Window window)
			{
				if (Window != null)
					Window.Closed -= Closing;
				Window = window;
				if (Window != null)
					Window.Closed += Closing;
			}

			private void Closing(object sender, EventArgs e)
			{
				Left = Window.Left;
				Top = Window.Top;
				Width = Window.Width;
				Height = Window.Height;
				Topmost = Window.Topmost;
				if (Window is CsWindow)
					Scale = ((CsWindow) Window).Scale;
			}
		}
	}
}