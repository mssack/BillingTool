// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;





namespace CsWpfBase.Themes.Controls.Basics
{
#pragma warning disable 1591
	public class InvalidableTextBox : TextBox
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty IsInvalidProperty = DependencyProperty.Register("IsInvalid", typeof (bool), typeof (InvalidableTextBox), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

		public static readonly DependencyProperty InvalidIconProperty = DependencyProperty.Register("InvalidIcon", typeof (BitmapImage), typeof (InvalidableTextBox), new FrameworkPropertyMetadata {DefaultValue = default(BitmapImage), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});

		public new static readonly DependencyProperty ToolTipProperty = DependencyProperty.Register("ToolTip", typeof (ToolTip), typeof (InvalidableTextBox), new FrameworkPropertyMetadata {DefaultValue = default(ToolTip), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((InvalidableTextBox) o).ToolTipChanged()});
		#endregion


		static InvalidableTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (InvalidableTextBox), new FrameworkPropertyMetadata(typeof (InvalidableTextBox)));
		}


		public bool IsInvalid
		{
			get { return (bool) GetValue(IsInvalidProperty); }
			set { SetValue(IsInvalidProperty, value); }
		}
		public BitmapImage InvalidIcon
		{
			get { return (BitmapImage) GetValue(InvalidIconProperty); }
			set { SetValue(InvalidIconProperty, value); }
		}
		public new ToolTip ToolTip
		{
			get { return (ToolTip) GetValue(ToolTipProperty); }
			set { SetValue(ToolTipProperty, value); }
		}

		private void ToolTipChanged()
		{
			if (ToolTip != null)
				ToolTip.SetBinding(DataContextProperty, new Binding("DataContext") {Source = this});
		}
	}
}