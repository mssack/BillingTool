// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;






namespace CsWpfBase.Global.message
{
	/// <summary>Interaction logic for CsMessageWindow.xaml</summary>
	public partial class CsMessageWindow : Window
	{
		internal static double DefaultContentScaling = 1.0;


		/// <summary>ctor</summary>
		public CsMessageWindow(CsMessage message)
		{
			Message = message;

			var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

			if (activeWindow != null)
				Owner = activeWindow;
			else if (Application.Current.MainWindow.IsInitialized && Application.Current.MainWindow.IsVisible)
				Owner = Application.Current.MainWindow;

			Topmost = true;

			this.LayoutTransform = new ScaleTransform(DefaultContentScaling, DefaultContentScaling, 0.5, 0.5);

			InitializeComponent();

		}

		/// <summary>the message</summary>
		public CsMessage Message
		{
			get { return (CsMessage) GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

		/// <summary>Gets or sets the enable state for the yes button.</summary>
		public bool YesOkButtonEnabled
		{
			get { return (bool) GetValue(YesOkButtonEnabledProperty); }
			set { SetValue(YesOkButtonEnabledProperty, value); }
		}


		/// <summary>shows the window.</summary>
		public new CsMessage.MessageResults ShowDialog()
		{
			base.ShowDialog();
			return Message.Result;
		}

		private void DialogButtonClicked(object sender, RoutedEventArgs e)
		{
			var button = e.OriginalSource as Button;
			Message.Result = (CsMessage.MessageResults) button.Tag;
			Close();
		}
#pragma warning disable 1591
		public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(CsMessage), typeof(CsMessageWindow), new FrameworkPropertyMetadata {DefaultValue = default(CsMessage), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty YesOkButtonEnabledProperty = DependencyProperty.Register("YesOkButtonEnabled", typeof(bool), typeof(CsMessageWindow), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}