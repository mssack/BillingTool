// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;





namespace CsWpfBase.Global.message
{
	/// <summary>Interaction logic for CsMessageWindow.xaml</summary>
	internal partial class CsMessageWindow : Window
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof (CsMessage), typeof (CsMessageWindow), new FrameworkPropertyMetadata {DefaultValue = default(CsMessage), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		public CsMessageWindow(CsMessage message)
		{
			Message = message;

			var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x=>x.IsActive);

			if (activeWindow != null)
				Owner = activeWindow;
			else if (Application.Current.MainWindow.IsInitialized && Application.Current.MainWindow.IsVisible)
				Owner = Application.Current.MainWindow;

			Topmost = true;
			InitializeComponent();
		}
		public CsMessage Message
		{
			get { return (CsMessage) GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

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
	}
}