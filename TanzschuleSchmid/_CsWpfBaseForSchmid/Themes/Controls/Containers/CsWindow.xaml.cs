// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-30</date>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Themes.AttachedProperties;
using CsWpfBase.Utilitys.feedback;






namespace CsWpfBase.Themes.Controls.Containers
{
#pragma warning disable 1591
	[ContentProperty("Content")]
	public class CsWindow : Window
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ResizeThicknessProperty = DependencyProperty.Register("ResizeThickness", typeof (Thickness), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Thickness), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (object), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof (DataTemplate), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MaximizeButtonVisibilityProperty = DependencyProperty.Register("MaximizeButtonVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MinimizeButtonVisibilityProperty = DependencyProperty.Register("MinimizeButtonVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register("HeaderVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.Register("HeaderBackground", typeof (Brush), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Brush), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FooterProperty = DependencyProperty.Register("Footer", typeof (object), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FooterTemplateProperty = DependencyProperty.Register("FooterTemplate", typeof (DataTemplate), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FooterVisibilityProperty = DependencyProperty.Register("FooterVisibility", typeof (Visibility), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof (double), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ScaleActivatedProperty = DependencyProperty.Register("ScaleActivated", typeof (bool), typeof (CsWindow), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private RelayCommand _closeCommand;
		private RelayCommand _maximizeCommand;
		private RelayCommand _minimizeCommand;
		private RelayCommand _openWebPageCommand;
		static CsWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (CsWindow), new FrameworkPropertyMetadata(typeof (CsWindow)));
		}


		#region Overrides
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			(Template.FindName("PART_FirstElementInTemplate", this) as FrameworkElement).ManipulationBoundaryFeedback += (sender, args) => args.Handled = true;
		}
		#endregion


		public Visibility IconVisibility
		{
			get { return (Visibility) GetValue(IconVisibilityProperty); }
			set { SetValue(IconVisibilityProperty, value); }
		}
		public Visibility CloseButtonVisibility
		{
			get { return (Visibility) GetValue(CloseButtonVisibilityProperty); }
			set { SetValue(CloseButtonVisibilityProperty, value); }
		}
		public Visibility MaximizeButtonVisibility
		{
			get { return (Visibility) GetValue(MaximizeButtonVisibilityProperty); }
			set { SetValue(MaximizeButtonVisibilityProperty, value); }
		}
		public Visibility MinimizeButtonVisibility
		{
			get { return (Visibility) GetValue(MinimizeButtonVisibilityProperty); }
			set { SetValue(MinimizeButtonVisibilityProperty, value); }
		}

		public Visibility HeaderVisibility
		{
			get { return (Visibility) GetValue(HeaderVisibilityProperty); }
			set { SetValue(HeaderVisibilityProperty, value); }
		}
		public object Header
		{
			get { return GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}
		public Brush HeaderBackground
		{
			get { return (Brush) GetValue(HeaderBackgroundProperty); }
			set { SetValue(HeaderBackgroundProperty, value); }
		}
		public DataTemplate HeaderTemplate
		{
			get { return (DataTemplate) GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}

		public object Footer
		{
			get { return GetValue(FooterProperty); }
			set { SetValue(FooterProperty, value); }
		}
		public DataTemplate FooterTemplate
		{
			get { return (DataTemplate) GetValue(FooterTemplateProperty); }
			set { SetValue(FooterTemplateProperty, value); }
		}
		public Visibility FooterVisibility
		{
			get { return (Visibility) GetValue(FooterVisibilityProperty); }
			set { SetValue(FooterVisibilityProperty, value); }
		}


		public double Scale
		{
			get { return (double) GetValue(ScaleProperty); }
			set { SetValue(ScaleProperty, value); }
		}
		public bool ScaleActivated
		{
			get { return (bool) GetValue(ScaleActivatedProperty); }
			set { SetValue(ScaleActivatedProperty, value); }
		}


		public Thickness ResizeThickness
		{
			get { return (Thickness) GetValue(ResizeThicknessProperty); }
			set { SetValue(ResizeThicknessProperty, value); }
		}
		public RelayCommand CloseCommand
		{
			get { return _closeCommand ?? (_closeCommand = new RelayCommand(Close)); }
		}
		public RelayCommand MinimizeCommand
		{
			get { return _minimizeCommand ?? (_minimizeCommand = new RelayCommand(() => { WindowState = WindowState.Minimized; })); }
		}
		public RelayCommand MaximizeCommand
		{
			get { return _maximizeCommand ?? (_maximizeCommand = new RelayCommand(() => { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; })); }
		}
		public RelayCommand OpenFeedbackCommand
		{
			get { return _openWebPageCommand ?? (_openWebPageCommand = new RelayCommand(() =>
			{
				CsWindow window = new CsWindow();
				AWindowDragMove.SetIsActive(window,true);
				window.FooterVisibility = Visibility.Collapsed;
				window.HeaderVisibility = Visibility.Collapsed;
				window.Width = 600;
				window.Height = 350;
				window.Owner = this;
				window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				window.Topmost = true;
				window.Content = new FeedbackControl();
				window.MaximizeButtonVisibility = Visibility.Collapsed;
				window.MinimizeButtonVisibility = Visibility.Collapsed;
				window.Padding = new Thickness(8,5,8,5);
				window.ShowDialog();
			})); }
		}
	}





	public class WindowRzMarginCalculator : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var borderThickness = (Thickness) value;
			var target = (AWindowResizeBorder.Target) parameter;

			if (target == AWindowResizeBorder.Target.Right)
				return new Thickness(borderThickness.Right*-1, 0, 0, 0);
			if (target == AWindowResizeBorder.Target.Bottom)
				return new Thickness(0, borderThickness.Bottom*-1, 0, 0);
			if (target == AWindowResizeBorder.Target.Left)
				return new Thickness(0, 0, borderThickness.Right*-1, 0);
			if (target == AWindowResizeBorder.Target.Top)
				return new Thickness(0, 0, 0, borderThickness.Top*-1);



			if (target == AWindowResizeBorder.Target.TopLeft)
				return new Thickness(0, 0, borderThickness.Left*-1, borderThickness.Top*-1);
			if (target == AWindowResizeBorder.Target.TopRight)
				return new Thickness(borderThickness.Right*-1, 0, 0, borderThickness.Top*-1);
			if (target == AWindowResizeBorder.Target.BottomLeft)
				return new Thickness(0, borderThickness.Bottom*-1, borderThickness.Left*-1, 0);
			if (target == AWindowResizeBorder.Target.BottomRight)
				return new Thickness(borderThickness.Right*-1, borderThickness.Bottom*-1, 0, 0);




			return new Thickness(0);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}