// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-21</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using CsWpfBase.Ev.Public.Extensions;





namespace CsWpfBase.Themes.Controls.Containers
{
#pragma warning disable 1591

	[ContentProperty("Content")]
	public class PopupButton : Control
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof (bool), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register("PopupContent", typeof (object), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register("PopupContentTemplate", typeof (DataTemplate), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof (object), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((PopupButton) o).ContentChanged(args.OldValue, args.NewValue); }});
		public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof (DataTemplate), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof (PlacementMode), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(PlacementMode), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register("PlacementTarget", typeof (object), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PopupPaddingProperty = DependencyProperty.Register("PopupPadding", typeof (Thickness), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(Thickness), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PopupBorderBrushProperty = DependencyProperty.Register("PopupBorderBrush", typeof (Brush), typeof (PopupButton), new FrameworkPropertyMetadata {DefaultValue = default(Brush), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private Border _partClickableBorder;
		static PopupButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (PopupButton), new FrameworkPropertyMetadata(typeof (PopupButton)));
		}
		public PopupButton()
		{
			this.KeyDown += PopupButton_KeyDown;
		}

		void PopupButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				IsOpen = !IsOpen;
				e.Handled = true;
				PartPopup.Focus();
				Keyboard.Focus(PartPopup);
			}
		}

		#region Overrides
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template != null)
			{
				PartPopup = Template.FindName("PART_Popup", this) as Popup;
				PartClickableBorder = Template.FindName("PART_ClickableBorder", this) as Border;
			}
		}
		#endregion


		public object Content
		{
			get { return GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		public DataTemplate ContentTemplate
		{
			get { return (DataTemplate) GetValue(ContentTemplateProperty); }
			set { SetValue(ContentTemplateProperty, value); }
		}
		public bool IsOpen
		{
			get { return (bool) GetValue(IsOpenProperty); }
			set { SetValue(IsOpenProperty, value); }
		}
		public object PopupContent
		{
			get { return GetValue(PopupContentProperty); }
			set { SetValue(PopupContentProperty, value); }
		}
		public DataTemplate PopupContentTemplate
		{
			get { return (DataTemplate) GetValue(PopupContentTemplateProperty); }
			set { SetValue(PopupContentTemplateProperty, value); }
		}

		public Thickness PopupPadding
		{
			get { return (Thickness) GetValue(PopupPaddingProperty); }
			set { SetValue(PopupPaddingProperty, value); }
		}

		public Brush PopupBorderBrush
		{
			get { return (Brush) GetValue(PopupBorderBrushProperty); }
			set { SetValue(PopupBorderBrushProperty, value); }
		}

		public Popup PartPopup { get; private set; }
		public Border PartClickableBorder
		{
			get { return _partClickableBorder; }
			set
			{
				if (_partClickableBorder != null)
				{
					_partClickableBorder.PreviewMouseLeftButtonUp -= MouseLeftButtonUpHandler;
					_partClickableBorder.MouseLeftButtonDown -= MouseLeftButtonUpHandler;
				}
				_partClickableBorder = value;
				if (_partClickableBorder != null)
				{
					_partClickableBorder.PreviewMouseLeftButtonUp += MouseLeftButtonUpHandler;
					_partClickableBorder.MouseLeftButtonDown += MouseLeftButtonDownHandler;
				}
			}
		}

		public PlacementMode Placement
		{
			get { return (PlacementMode) GetValue(PlacementProperty); }
			set { SetValue(PlacementProperty, value); }
		}

		public object PlacementTarget
		{
			get { return GetValue(PlacementTargetProperty); }
			set { SetValue(PlacementTargetProperty, value); }
		}
		private void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		private void MouseLeftButtonUpHandler(object sender, MouseButtonEventArgs e)
		{
			IsOpen = !IsOpen;
			e.Handled = true;
		}
		private void ContentChanged(object oldValue, object newValue)
		{
			if (newValue is DependencyObject)
			{
				NameScope.SetNameScope(newValue as DependencyObject, this.GetContainingNameScope());
			}
		}
	}
}