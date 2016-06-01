// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;





namespace CsWpfBase.Themes.Controls.Basics
{
#pragma warning disable 1591
	public class Hr : Control
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty BrushProperty = DependencyProperty.Register("Brush", typeof (Brush), typeof (Hr), new FrameworkPropertyMetadata {DefaultValue = default(Brush), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof (double), typeof (Hr), new FrameworkPropertyMetadata {DefaultValue = default(double), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static Hr()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Hr), new FrameworkPropertyMetadata(typeof (Hr)));
		}
		public Brush Brush
		{
			get { return (Brush) GetValue(BrushProperty); }
			set { SetValue(BrushProperty, value); }
		}
		public double Thickness
		{
			get { return (double) GetValue(ThicknessProperty); }
			set { SetValue(ThicknessProperty, value); }
		}
	}
}