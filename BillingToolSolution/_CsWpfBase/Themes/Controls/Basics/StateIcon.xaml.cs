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
	public class StateIcon : Control
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty IsRunningProperty = DependencyProperty.Register("IsRunning", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsCancelingProperty = DependencyProperty.Register("IsCanceling", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsCanceledProperty = DependencyProperty.Register("IsCanceled", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsFaultedProperty = DependencyProperty.Register("IsFaulted", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsSucceededProperty = DependencyProperty.Register("IsSucceeded", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register("IsPaused", typeof (bool), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register("IconSize", typeof (double), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty RunningImageProperty = DependencyProperty.Register("RunningImage", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty CanceledImageProperty = DependencyProperty.Register("CanceledImage", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty FaultedImageProperty = DependencyProperty.Register("FaultedImage", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SucceededImageProperty = DependencyProperty.Register("SucceededImage", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty PauseImageProperty = DependencyProperty.Register("PauseImage", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof (ImageSource), typeof (StateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ImageSource), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static StateIcon()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (StateIcon), new FrameworkPropertyMetadata(typeof (StateIcon)));
		}
		public bool IsRunning
		{
			get { return (bool) GetValue(IsRunningProperty); }
			set { SetValue(IsRunningProperty, value); }
		}
		public bool IsCanceled
		{
			get { return (bool) GetValue(IsCanceledProperty); }
			set { SetValue(IsCanceledProperty, value); }
		}
		public bool IsFaulted
		{
			get { return (bool) GetValue(IsFaultedProperty); }
			set { SetValue(IsFaultedProperty, value); }
		}
		public bool IsSucceeded
		{
			get { return (bool) GetValue(IsSucceededProperty); }
			set { SetValue(IsSucceededProperty, value); }
		}


		public bool IsPaused
		{
			get { return (bool) GetValue(IsPausedProperty); }
			set { SetValue(IsPausedProperty, value); }
		}
		public bool IsCanceling
		{
			get { return (bool) GetValue(IsCancelingProperty); }
			set { SetValue(IsCancelingProperty, value); }
		}


		public double IconSize
		{
			get { return (double) GetValue(IconSizeProperty); }
			set { SetValue(IconSizeProperty, value); }
		}

		public ImageSource Icon
		{
			get { return (ImageSource) GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}

		public ImageSource RunningImage
		{
			get { return (ImageSource) GetValue(RunningImageProperty); }
			set { SetValue(RunningImageProperty, value); }
		}

		public ImageSource CanceledImage
		{
			get { return (ImageSource) GetValue(CanceledImageProperty); }
			set { SetValue(CanceledImageProperty, value); }
		}

		public ImageSource FaultedImage
		{
			get { return (ImageSource) GetValue(FaultedImageProperty); }
			set { SetValue(FaultedImageProperty, value); }
		}

		public ImageSource SucceededImage
		{
			get { return (ImageSource) GetValue(SucceededImageProperty); }
			set { SetValue(SucceededImageProperty, value); }
		}

		public ImageSource PauseImage
		{
			get { return (ImageSource) GetValue(PauseImageProperty); }
			set { SetValue(PauseImageProperty, value); }
		}
	}
}