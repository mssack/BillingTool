// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;
using Microsoft.Win32;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors
{
#pragma warning disable 1591
	/// <summary>Editor for image fields</summary>
	public class ImageParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (BitmapSource), typeof (ImageParam), new FrameworkPropertyMetadata {DefaultValue = default(BitmapSource), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowPreviewProperty = DependencyProperty.Register("AllowPreview", typeof (bool), typeof (ImageParam), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private ICommand _openDialogCommand;
		private ICommand _removeImageCommand;


		static ImageParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageParam), new FrameworkPropertyMetadata(typeof (ImageParam)));
		}

		/// <summary>The value which needs to be edited.</summary>
		public BitmapSource Value
		{
			get { return (BitmapSource) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		/// <summary>Gets or sets if the preview button should be displayed.</summary>
		public bool AllowPreview
		{
			get { return (bool) GetValue(AllowPreviewProperty); }
			set { SetValue(AllowPreviewProperty, value); }
		}
		/// <summary>Command for opening a dialog to select the new Image.</summary>
		public ICommand ChangeImageCommand
		{
			get { return _openDialogCommand ?? (_openDialogCommand = new RelayCommand(() => { Value = new OpenFileDialog().GatherImage() ?? Value; })); }
		}
		/// <summary>Sets the image value to null.</summary>
		public ICommand DeleteImageCommand
		{
			get { return _removeImageCommand ?? (_removeImageCommand = new RelayCommand(() => { Value = null; })); }
		}
	}
}