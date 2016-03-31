// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-24</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;





namespace CsWpfBase.Global.debug
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgDebug : Base
	{
		#region SINGLETON CLASS
		private static CsgDebug _instance;
		private static readonly object SingletonLock = new object();
		private CsgDebug()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgDebug I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgDebug());
				}
			}
		}
		#endregion


		private readonly Dictionary<string, int> _trackcounter = new Dictionary<string, int>();


		private IInputElement _currentFocused;
		private Dispatcher _uiDispatcher;
		private Dispatcher UiDispatcher
		{
			get { return _uiDispatcher ?? (_uiDispatcher = Application.Current.Dispatcher); }
		}
		/// <summary>Writes a debug message to output window</summary>
		[Conditional("DEBUG")]
		public void Write(object msg, string typ = "DBG", [CallerMemberName] string method = null, [CallerFilePath] string filepath = null, [CallerLineNumber] int line = 0)
		{
			var threadID = UiDispatcher.Thread == Thread.CurrentThread ? "[ UI ]" : $"[{Thread.CurrentThread.ManagedThreadId,4}]";
			string codeIdentifier = new CodePosition(method, filepath, line).GetIdentifier(24).Expand(24);


			typ = typ.Cut(3, "").Expand(3);
			var prefix = $"{typ} {DateTime.Now:mm:ss.fff} {threadID}-> {codeIdentifier} :: ";


			string message = msg == null ? "null" : msg is string ? msg as string : msg.ToString();

			var lines = message.Split("\r\n").ToList();
			var firstline = lines[0];
			lines.RemoveAt(0);
			if (lines.Count == 0)
			{
				Debug.WriteLine(prefix + firstline);
			}
			else
			{
				var restlines = lines.Select(x => " ".Expand(prefix.Length) + x).Join("\r\n");
				Debug.WriteLine(prefix + firstline + "\r\n" + restlines);
			}
		}

		/// <summary>
		///     Track the current line of code. Writes a message to the output window. Every time the line of code is hit an
		///     index is incremented by one.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="method"></param>
		/// <param name="filepath"></param>
		/// <param name="line"></param>
		[Conditional("DEBUG")]
		public void Track(string name = null, [CallerMemberName] string method = null, [CallerFilePath] string filepath = null, [CallerLineNumber] int line = 0)
		{
			lock (_trackcounter)
			{
				var threadID = UiDispatcher.Thread == Thread.CurrentThread ? "-UI-" : ($"{Thread.CurrentThread.ManagedThreadId,4}");
				var identifier = new CodePosition(filepath, method, line).GetIdentifier(24).Expand(24);
				var count = 1;
				if (_trackcounter.ContainsKey(identifier))
				{
					count = _trackcounter[identifier] + 1;
					_trackcounter[identifier] = count;
				}
				else
				{
					_trackcounter.Add(identifier, count);
				}

				Debug.WriteLine((name ?? identifier) + "  =>  Count:" + count.ToString(CultureInfo.InvariantCulture).Expand(5, " ", true) + "   Thread: " + threadID);
			}
		}
		/// <summary>
		///     Tracks the Keyboard focus in WPF. When activated the focused element is written to the output window and is
		///     outlined with an border.
		/// </summary>
		[Conditional("DEBUG")]
		public void TrackKeyboardFocus()
		{
			EventManager.RegisterClassHandler(typeof (UIElement), Keyboard.GotKeyboardFocusEvent, new KeyboardFocusChangedEventHandler((sender, args) =>
			{
				if (_currentFocused == args.NewFocus)
					return;
				_currentFocused = args.NewFocus;
				if (_currentFocused is FrameworkElement)
				{
					new FocusAdorner(_currentFocused as FrameworkElement);
				}
				Write((_currentFocused is FrameworkElement ? (_currentFocused as FrameworkElement).Name : "") + "<" + _currentFocused.GetType().Name + ">");
			}), true);
		}





		private class FocusAdorner : Adorner
		{
			private static Adorner _currentAdorner = null;
			private static AdornerLayer _currentLayer = null;
			public FocusAdorner(FrameworkElement adornedElement) : base(adornedElement)
			{
				if (_currentLayer != null)
				{
					_currentLayer.Remove(_currentAdorner);
				}
				_currentLayer = AdornerLayer.GetAdornerLayer(adornedElement);
				if (_currentLayer == null)
					return;
				_currentAdorner = this;
				_currentLayer.Add(_currentAdorner);
				IsHitTestVisible = false;
				Opacity = 0.5;
			}


			#region Overrides
			protected override void OnRender(DrawingContext drawingContext)
			{
				base.OnRender(drawingContext);

				var fre = (FrameworkElement) AdornedElement;

				drawingContext.DrawRectangle(null, new Pen(new SolidColorBrush(Colors.Red), 5), new Rect(-2.5, -2.5, fre.ActualWidth + 5, fre.ActualHeight + 5));
			}
			#endregion


		}
	}
}