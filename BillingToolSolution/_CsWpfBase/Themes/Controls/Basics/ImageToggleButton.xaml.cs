// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;





namespace CsWpfBase.Themes.Controls.Basics
{
#pragma warning disable 1591
	public class ImageToggleButton : ToggleButton
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty TrueImageProperty = DependencyProperty.Register("TrueImage", typeof (ImageSource), typeof (ImageToggleButton), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FalseImageProperty = DependencyProperty.Register("FalseImage", typeof (ImageSource), typeof (ImageToggleButton), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty NullImageProperty = DependencyProperty.Register("NullImage", typeof (ImageSource), typeof (ImageToggleButton), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HelpProperty = DependencyProperty.Register("Help", typeof (object), typeof (ImageToggleButton), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty HelpTemplateProperty = DependencyProperty.Register("HelpTemplate", typeof (DataTemplate), typeof (ImageToggleButton), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static ImageToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageToggleButton), new FrameworkPropertyMetadata(typeof (ImageToggleButton)));
		}
		public ImageSource TrueImage
		{
			get { return (ImageSource) GetValue(TrueImageProperty); }
			set { SetValue(TrueImageProperty, value); }
		}
		public ImageSource FalseImage
		{
			get { return (ImageSource) GetValue(FalseImageProperty); }
			set { SetValue(FalseImageProperty, value); }
		}
		public ImageSource NullImage
		{
			get { return (ImageSource) GetValue(NullImageProperty); }
			set { SetValue(NullImageProperty, value); }
		}

		public object Help
		{
			get { return GetValue(HelpProperty); }
			set { SetValue(HelpProperty, value); }
		}
		public DataTemplate HelpTemplate
		{
			get { return (DataTemplate) GetValue(HelpTemplateProperty); }
			set { SetValue(HelpTemplateProperty, value); }
		}
	}
}