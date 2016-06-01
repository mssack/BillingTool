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
using CsWpfBase.Themes.Controls.Editors.Base;
using Microsoft.Win32;





namespace CsWpfBase.Themes.Controls.Editors
{
#pragma warning disable 1591
	public class ImageEditor : EditorBase<BitmapSource>
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty AllowPreviewProperty = DependencyProperty.Register("AllowPreview", typeof (bool), typeof (ImageEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AllowSaveProperty = DependencyProperty.Register("AllowSave", typeof (bool), typeof (ImageEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private ICommand _openDialogCommand;
		private ICommand _removeImageCommand;
		static ImageEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageEditor), new FrameworkPropertyMetadata(typeof (ImageEditor)));
		}

		public bool AllowPreview
		{
			get { return (bool) GetValue(AllowPreviewProperty); }
			set { SetValue(AllowPreviewProperty, value); }
		}
		public bool AllowSave
		{
			get { return (bool) GetValue(AllowSaveProperty); }
			set { SetValue(AllowSaveProperty, value); }
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