// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;





namespace CsWpfBase.Themes.Controls.Basics
{
#pragma warning disable 1591
	[ContentProperty("Content")]
	public class SeparatorBorder : ContentControl
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof (bool), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		public static readonly DependencyProperty BottomProperty = DependencyProperty.Register("Bottom", typeof (bool), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof (bool), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		public static readonly DependencyProperty RightProperty = DependencyProperty.Register("Right", typeof (bool), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		private static readonly DependencyPropertyKey InnerPaddingPropertyKey = DependencyProperty.RegisterReadOnly("InnerPadding", typeof (Thickness), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(Thickness), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty InnerPaddingProperty = InnerPaddingPropertyKey.DependencyProperty;
		private static readonly DependencyPropertyKey InnerMarginPropertyKey = DependencyProperty.RegisterReadOnly("InnerMargin", typeof (Thickness), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(Thickness), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty InnerMarginProperty = InnerMarginPropertyKey.DependencyProperty;
		public static readonly DependencyProperty InnerPaddingThicknessProperty = DependencyProperty.Register("InnerPaddingThickness", typeof (double), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		public static readonly DependencyProperty InnerMarginThicknessProperty = DependencyProperty.Register("InnerMarginThickness", typeof (double), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		public static readonly DependencyProperty InnerBorderThicknessProperty = DependencyProperty.Register("InnerBorderThickness", typeof (double), typeof (SeparatorBorder), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((SeparatorBorder) o).ReCalc()});
		#endregion


		static SeparatorBorder()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (SeparatorBorder), new FrameworkPropertyMetadata(typeof (SeparatorBorder)));
		}
		public bool Top
		{
			get { return (bool) GetValue(TopProperty); }
			set { SetValue(TopProperty, value); }
		}
		public bool Bottom
		{
			get { return (bool) GetValue(BottomProperty); }
			set { SetValue(BottomProperty, value); }
		}
		public bool Left
		{
			get { return (bool) GetValue(LeftProperty); }
			set { SetValue(LeftProperty, value); }
		}
		public bool Right
		{
			get { return (bool) GetValue(RightProperty); }
			set { SetValue(RightProperty, value); }
		}

		public Thickness InnerPadding
		{
			get { return (Thickness) GetValue(InnerPaddingProperty); }
			private set { SetValue(InnerPaddingPropertyKey, value); }
		}

		public Thickness InnerMargin
		{
			get { return (Thickness) GetValue(InnerMarginProperty); }
			private set { SetValue(InnerMarginPropertyKey, value); }
		}

		public double InnerPaddingThickness
		{
			get { return (double) GetValue(InnerPaddingThicknessProperty); }
			set { SetValue(InnerPaddingThicknessProperty, value); }
		}
		public double InnerMarginThickness
		{
			get { return (double) GetValue(InnerMarginThicknessProperty); }
			set { SetValue(InnerMarginThicknessProperty, value); }
		}

		public double InnerBorderThickness
		{
			get { return (double) GetValue(InnerBorderThicknessProperty); }
			set { SetValue(InnerBorderThicknessProperty, value); }
		}

		private void ReCalc()
		{
			InnerPadding = new Thickness(Left ? InnerPaddingThickness : 0, Top ? InnerPaddingThickness : 0, Right ? InnerPaddingThickness : 0, Bottom ? InnerPaddingThickness : 0);
			InnerMargin = new Thickness(Left ? InnerMarginThickness : 0, Top ? InnerMarginThickness : 0, Right ? InnerMarginThickness : 0, Bottom ? InnerMarginThickness : 0);
			BorderThickness = new Thickness(Left ? InnerBorderThickness : 0, Top ? InnerBorderThickness : 0, Right ? InnerBorderThickness : 0, Bottom ? InnerBorderThickness : 0);
		}
	}
}