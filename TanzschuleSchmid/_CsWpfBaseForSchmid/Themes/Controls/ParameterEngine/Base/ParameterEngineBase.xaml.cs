// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Themes.Controls.ParameterEngine.Base
{
#pragma warning disable 1591
	/// <summary>The parameter base should not be used directly instead use <see cref="ParameterEngineBase" /> control.</summary>
	[ContentProperty("Content")]
	public class ParameterEngineBase : Control
	{
		#region DP Keys
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof (object), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ParameterEngineBase) o).ContentChanged(args.OldValue, args.NewValue)});
		public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof (DataTemplate), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof (BitmapSource), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(BitmapSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.RegisterAttached("IconVisibility", typeof (Visibility), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof (object), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ParameterEngineBase) o).HeaderChanged(args.OldValue, args.NewValue)});
		public static readonly DependencyProperty HeaderStringFormatProperty = DependencyProperty.Register("HeaderStringFormat", typeof (string), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof (DataTemplate), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderAlignmentProperty = DependencyProperty.RegisterAttached("HeaderAlignment", typeof (VerticalAlignment), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(VerticalAlignment), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty IconSizeProperty = DependencyProperty.RegisterAttached("IconSize", typeof (double), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty HelpProperty = DependencyProperty.Register("Help", typeof (object), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ParameterEngineBase) o).HelpChanged(args.OldValue, args.NewValue)});
		public static readonly DependencyProperty HelpTemplateProperty = DependencyProperty.Register("HelpTemplate", typeof (DataTemplate), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ExtensionTemplateProperty = DependencyProperty.Register("ExtensionTemplate", typeof (DataTemplate), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ParameterEngineBase) o).ExtensionTemplateChanged(args.OldValue, args.NewValue)});
		public static readonly DependencyProperty ContentAlignmentProperty = DependencyProperty.Register("ContentAlignment", typeof (HorizontalAlignment), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(HorizontalAlignment), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ExtensionMinWidthProperty = DependencyProperty.Register("ExtensionMinWidth", typeof (double), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register("HeaderVisibility", typeof (Visibility), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(Visibility), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof (bool), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AutoSelectProperty = DependencyProperty.Register("AutoSelect", typeof (bool), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof (TextAlignment), typeof (ParameterEngineBase), new FrameworkPropertyMetadata {DefaultValue = default(TextAlignment), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		public static double GetIconSize(DependencyObject obj)
		{
			return (double) obj.GetValue(IconSizeProperty);
		}

		public static void SetIconSize(DependencyObject obj, double value)
		{
			obj.SetValue(IconSizeProperty, value);
		}

		public static HorizontalAlignment GetHeaderAlignment(DependencyObject obj)
		{
			return (HorizontalAlignment) obj.GetValue(HeaderAlignmentProperty);
		}

		public static void SetHeaderAlignment(DependencyObject obj, HorizontalAlignment value)
		{
			obj.SetValue(HeaderAlignmentProperty, value);
		}

		public static Visibility GetIconVisibility(DependencyObject obj)
		{
			return (Visibility) obj.GetValue(IconVisibilityProperty);
		}

		public static void SetIconVisibility(DependencyObject obj, Visibility value)
		{
			obj.SetValue(IconVisibilityProperty, value);
		}

		public static BitmapSource GetIcon(DependencyObject obj)
		{
			return (BitmapSource) obj.GetValue(IconProperty);
		}

		public static void SetIcon(DependencyObject obj, BitmapSource value)
		{
			obj.SetValue(IconProperty, value);
		}


		static ParameterEngineBase()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ParameterEngineBase), new FrameworkPropertyMetadata(typeof (ParameterEngineBase)));
		}

		/// <summary>The content of the <see cref="ParameterEngineBase" />.</summary>
		public object Content
		{
			get { return GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
		/// <summary>the template associated with the content.</summary>
		public DataTemplate ContentTemplate
		{
			get { return (DataTemplate) GetValue(ContentTemplateProperty); }
			set { SetValue(ContentTemplateProperty, value); }
		}
		/// <summary>sets the alignment of the control content.</summary>
		public HorizontalAlignment ContentAlignment
		{
			get { return (HorizontalAlignment) GetValue(ContentAlignmentProperty); }
			set { SetValue(ContentAlignmentProperty, value); }
		}

		/// <summary>The header will be presented on the left side it is contained in a shared size group.</summary>
		public object Header
		{
			get { return GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}
		/// <summary>the string format which will be used if header is presented as string.</summary>
		public string HeaderStringFormat
		{
			get { return (string) GetValue(HeaderStringFormatProperty); }
			set { SetValue(HeaderStringFormatProperty, value); }
		}
		/// <summary>the template associated with the header.</summary>
		public DataTemplate HeaderTemplate
		{
			get { return (DataTemplate) GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}
		/// <summary>the alignment of the header.</summary>
		public VerticalAlignment HeaderAlignment
		{
			get { return (VerticalAlignment) GetValue(HeaderAlignmentProperty); }
			set { SetValue(HeaderAlignmentProperty, value); }
		}

		public Visibility HeaderVisibility
		{
			get { return (Visibility) GetValue(HeaderVisibilityProperty); }
			set { SetValue(HeaderVisibilityProperty, value); }
		}


		/// <summary>The help will be presented on a hover base.</summary>
		public object Help
		{
			get { return GetValue(HelpProperty); }
			set { SetValue(HelpProperty, value); }
		}
		/// <summary>the template associated with the help.</summary>
		public DataTemplate HelpTemplate
		{
			get { return (DataTemplate) GetValue(HelpTemplateProperty); }
			set { SetValue(HelpTemplateProperty, value); }
		}


		/// <summary>The Icon on the left side from the header.</summary>
		public BitmapSource Icon
		{
			get { return (BitmapSource) GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		/// <summary>The icons visibility.</summary>
		public Visibility IconVisibility
		{
			get { return (Visibility) GetValue(IconVisibilityProperty); }
			set { SetValue(IconVisibilityProperty, value); }
		}
		/// <summary>the width and height of the icon.</summary>
		public double IconSize
		{
			get { return (double) GetValue(IconSizeProperty); }
			set { SetValue(IconSizeProperty, value); }
		}
		/// <summary>Gets or sets whether the control is read only</summary>
		public bool IsReadOnly
		{
			get { return (bool) GetValue(IsReadOnlyProperty); }
			set { SetValue(IsReadOnlyProperty, value); }
		}
		/// <summary>Defines whether the content will be selected on focus.</summary>
		public bool AutoSelect
		{
			get { return (bool) GetValue(AutoSelectProperty); }
			set { SetValue(AutoSelectProperty, value); }
		}

		public TextAlignment TextAlignment
		{
			get { return (TextAlignment) GetValue(TextAlignmentProperty); }
			set { SetValue(TextAlignmentProperty, value); }
		}

		/// <summary>Do not use this</summary>
		public DataTemplate ExtensionTemplate
		{
			get { return (DataTemplate) GetValue(ExtensionTemplateProperty); }
			set { SetValue(ExtensionTemplateProperty, value); }
		}

		public double ExtensionMinWidth
		{
			get { return (double) GetValue(ExtensionMinWidthProperty); }
			set { SetValue(ExtensionMinWidthProperty, value); }
		}

		private void ContentChanged(object oldValue, object newValue)
		{
			if (newValue is DependencyObject)
				NameScope.SetNameScope(newValue as DependencyObject, this.GetContainingNameScope());
			if (newValue is FrameworkElement)
				BindingOperations.SetBinding(newValue as FrameworkElement, DataContextProperty, new Binding("DataContext") {Source = this});
		}

		private void HeaderChanged(object oldValue, object newValue)
		{
			if (newValue is DependencyObject)
				NameScope.SetNameScope(newValue as DependencyObject, this.GetContainingNameScope());
			if (newValue is FrameworkElement)
				BindingOperations.SetBinding(newValue as FrameworkElement, DataContextProperty, new Binding("DataContext") {Source = this});
		}

		private void HelpChanged(object oldValue, object newValue)
		{
			if (newValue is DependencyObject)
				NameScope.SetNameScope(newValue as DependencyObject, this.GetContainingNameScope());
			if (newValue is FrameworkElement)
				BindingOperations.SetBinding(newValue as FrameworkElement, DataContextProperty, new Binding("DataContext") {Source = this});
		}

		private void ExtensionTemplateChanged(object oldValue, object newValue)
		{
		}
	}
}