// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace CsWpfBase.Themes.Controls.Containers
{
	/// <summary>
	///     The window button provides a control which is able to embed a window directly into xaml code. The opened window style will be replaced if a
	///     window style is found in the resources of this control.
	/// </summary>
	public class WindowButton : Control
	{
		private readonly RelativeLocationMaintainer _locationMaintainer = new RelativeLocationMaintainer();
		private Window _ownerWindow;

		private Window _win;

		static WindowButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (WindowButton), new FrameworkPropertyMetadata(typeof (WindowButton)));
		}

		/// <summary>Init a new instance.</summary>
		public WindowButton()
		{
			MouseLeftButtonUp += OnMouseLeftButtonUp;
			Unloaded += OnUnloaded;
			Loaded += OnLoaded;
		}

		/// <summary>Gets or sets the content of the button.</summary>
		public object Content
		{
			get { return GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
		/// <summary>Gets or sets the style which is applied to the opened Window.</summary>
		public Style WindowStyle
		{
			get { return (Style) GetValue(WindowStyleProperty); }
			set { SetValue(WindowStyleProperty, value); }
		}
		/// <summary>Gets or sets the content of the window.</summary>
		public object WindowContent
		{
			get { return GetValue(WindowContentProperty); }
			set { SetValue(WindowContentProperty, value); }
		}
		/// <summary>Gets or sets the template for the content of the button.</summary>
		public DataTemplate ContentTemplate
		{
			get { return (DataTemplate) GetValue(ContentTemplateProperty); }
			set { SetValue(ContentTemplateProperty, value); }
		}
		/// <summary>Gets or sets the template for the window content.</summary>
		public DataTemplate WindowContentTemplate
		{
			get { return (DataTemplate) GetValue(WindowContentTemplateProperty); }
			set { SetValue(WindowContentTemplateProperty, value); }
		}
		/// <summary>Defines if the window should be opened as dialog. If this option is activated a new instance of the window will be created each time opened.</summary>
		public bool ShowDialog
		{
			get { return (bool) GetValue(ShowDialogProperty); }
			set { SetValue(ShowDialogProperty, value); }
		}
		/// <summary>Determines if the window is currently opened.</summary>
		public bool IsOpened
		{
			get { return (bool) GetValue(IsOpenedProperty); }
			set { SetValue(IsOpenedProperty, value); }
		}
		/// <summary>Determine whether the opened window should hold the relative position to the owned window.</summary>
		public bool HoldRelativePosition
		{
			get { return (bool) GetValue(HoldRelativePositionProperty); }
			set { SetValue(HoldRelativePositionProperty, value); }
		}
		/// <summary>The window which is opened on click.</summary>
		private Window Win
		{
			get { return _win; }
			set
			{
				var old = _win;
				if (!_win.Equals(value))
				{
					_win = value;
					WindowChanged(old);
				}
			}
		}

		/// <summary>The owning window this property is set whenever control gets loaded.</summary>
		private Window OwnerWindow
		{
			get { return _ownerWindow; }
			set
			{
				var old = _ownerWindow;
				if (!value.Equals(_ownerWindow))
				{
					_ownerWindow = value;
					OwnerWindowChanged(old);
				}
			}
		}
		/// <summary>Occurs whenever a new window is initialized.</summary>
		public event Action<Window> WindowInitialized;

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Loaded -= OnLoaded;
			OwnerWindow = Window.GetWindow(this);
			if (IsOpened)
				IsOpenedChanged();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (Win != null)
			{
				Win.Close();
				Win = null;
			}
		}

		private void WinOnClosing(object sender, CancelEventArgs e)
		{
			if (ShowDialog)
				return;
			IsOpened = false;
			e.Cancel = true;
		}

		private void WinOnClosed(object sender, EventArgs eventArgs)
		{
			Win = null;
			IsOpened = false;
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			IsOpened = !IsOpened;
		}

		private void HoldRelativePositionChanged()
		{
			_locationMaintainer.IsEnabled = HoldRelativePosition;
			_locationMaintainer.SaveRelativePosition();
		}

		private void IsOpenedChanged()
		{
			if (!IsLoaded)
				return;
			if (Win == null && IsOpened)
			{
				Win = new Window();
				if (ShowDialog)
					Application.Current.Dispatcher.BeginInvoke(new Action(() => Win.ShowDialog()), DispatcherPriority.DataBind);
				else
					Win.Show();
			}
			else if (IsOpened)
			{
				if (Win == null)
					Win = new Window();
				Win.Visibility = Visibility.Visible;
			}
			else if (!IsOpened && Win != null)
			{
				if (ShowDialog)
					Win.Close();
				else
					Win.Visibility = Visibility.Collapsed;
			}
		}

		private void WindowChanged(Window old)
		{
			if (old != null)
			{
				BindingOperations.ClearAllBindings(old);
				_locationMaintainer.Target = null;
				old.Closed -= WinOnClosed;
				old.Closing -= WinOnClosing;
			}
			if (Win != null)
			{
				_win.SetBinding(ContentControl.ContentProperty, new Binding("WindowContent") {Source = this});
				_win.SetBinding(ContentControl.ContentTemplateProperty, new Binding("WindowContentTemplate") {Source = this});
				_win.ShowInTaskbar = false;
				_win.Owner = OwnerWindow;


				var targetPoints = OwnerWindow.GetAbsoluteScreenCoordinates();

				_win.Left = targetPoints.X + OwnerWindow.ActualWidth;
				_win.Top = targetPoints.Y;
				_locationMaintainer.Target = _win;

				_win.Closed += WinOnClosed;
				_win.Closing += WinOnClosing;

				if (WindowStyle != null)
					_win.Style = WindowStyle;

				if (WindowInitialized != null)
					WindowInitialized(_win);
			}
		}

		// ReSharper disable once UnusedParameter.Local
		private void OwnerWindowChanged(Window old)
		{
			if (WindowContent is FrameworkElement)
				NameScope.SetNameScope((WindowContent as FrameworkElement), NameScope.GetNameScope(OwnerWindow));
			_locationMaintainer.Owner = OwnerWindow;
		}

		private void WindowContentChanged()
		{
			if (WindowContent is FrameworkElement)
			{
				(WindowContent as FrameworkElement).SetBinding(DataContextProperty, new Binding("DataContext") {Source = this});
				if (OwnerWindow != null)
					NameScope.SetNameScope((WindowContent as FrameworkElement), NameScope.GetNameScope(OwnerWindow));
			}
		}





		private class RelativeLocationMaintainer
		{
			private bool _isadjusting;
			private Window _owner;
			private Point _relPos;
			private bool _suppressEvents;
			private Window _target;
			public Window Owner
			{
				get { return _owner; }
				set
				{
					if (_owner != null)
					{
						_owner.LocationChanged -= OwnerLocationChanged;
						_owner.SizeChanged -= OwnerSizeChanged;
					}
					_owner = value;
					if (_owner != null)
					{
						_owner.LocationChanged += OwnerLocationChanged;
						_owner.SizeChanged += OwnerSizeChanged;
					}
				}
			}
			public Window Target
			{
				get { return _target; }
				set
				{
					if (_target != null)
					{
						_target.LocationChanged -= TargetOnLocationChanged;
						_target.IsVisibleChanged -= TargetIsVisibleChanged;
					}
					_target = value;
					if (_target != null)
					{
						_target.LocationChanged += TargetOnLocationChanged;
						_target.IsVisibleChanged += TargetIsVisibleChanged;
					}
				}
			}
			public bool IsEnabled { get; set; }


			public void SaveRelativePosition()
			{
				if (Owner == null || Target == null)
					return;
				_relPos = new Point(Target.Left - Owner.Left, Target.Top - Owner.Top);
			}

			public void AdjustTargetPosition()
			{
				if (Owner == null || Target == null)
					return;
				if (_isadjusting)
					return;
				_isadjusting = true;
				var fittingRectangle = CsGlobal.Wpf.Storage.Window.GetFittingRectangle(Owner.Left + _relPos.X, Owner.Top + _relPos.Y, Target.Width, Target.Height);
				Target.Left = fittingRectangle.Left;
				Target.Top = fittingRectangle.Top;
				_isadjusting = false;
			}

			private void TargetIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
			{
				SaveRelativePosition();
			}


			private void TargetOnLocationChanged(object sender, EventArgs eventArgs)
			{
				if (_suppressEvents)
					return;
				SaveRelativePosition();
			}

			private void OwnerLocationChanged(object sender, EventArgs args)
			{
				if (!IsEnabled)
					return;
				if (_suppressEvents)
					return;
				_suppressEvents = true;
				AdjustTargetPosition();
				_suppressEvents = false;
			}

			private void OwnerSizeChanged(object sender, SizeChangedEventArgs args)
			{
				if (!IsEnabled)
					return;
				if (_suppressEvents)
					return;
				if (!args.WidthChanged)
					return;

				_suppressEvents = true;

				_relPos = new Point(_relPos.X + args.NewSize.Width - args.PreviousSize.Width, _relPos.Y);
				AdjustTargetPosition();

				_suppressEvents = false;
			}
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ShowDialogProperty = DependencyProperty.Register("ShowDialog", typeof (bool), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register("IsOpened", typeof (bool), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((WindowButton) o).IsOpenedChanged()});
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof (object), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(object), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty WindowContentProperty = DependencyProperty.Register("WindowContent", typeof (object), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(object), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((WindowButton) o).WindowContentChanged()});
		public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof (DataTemplate), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty WindowContentTemplateProperty = DependencyProperty.Register("WindowContentTemplate", typeof (DataTemplate), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HoldRelativePositionProperty = DependencyProperty.Register("HoldRelativePosition", typeof (bool), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((WindowButton) o).HoldRelativePositionChanged()});
		public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.Register("WindowStyle", typeof (Style), typeof (WindowButton), new FrameworkPropertyMetadata {DefaultValue = default(Style), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}