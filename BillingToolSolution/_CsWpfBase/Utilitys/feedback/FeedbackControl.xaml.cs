// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Online;
using CsWpfBase.Online.packets.v1.client;
using CsWpfBase.Themes.Controls.Containers;






namespace CsWpfBase.Utilitys.feedback
{
	/// <summary>Interaction logic for FeedbackControl.xaml</summary>
	public partial class FeedbackControl : ItemControl<CsopClientFeedback>
	{
		/// <summary>ctor</summary>
		public FeedbackControl()
		{
			Item = new CsopClientFeedback {SenderName = Environment.UserName, Title = "I have a dream!"};
			InitializeComponent();
		}

		private void SendClicked(object sender, RoutedEventArgs e)
		{
			if (CheckInput() == false) 
				return;


			var button = sender as Button;
			button.IsEnabled = false;



			CsOnline.SendAsync.Feedback(Item).ContinueWith(t =>
			{
				button.IsEnabled = true;
				if (t.IsFaulted)
				{
					CsGlobal.Message.Push(t.Exception.MostInner(), CsMessage.Types.Error);
					return;
				}
				CsGlobal.Message.Push("Thank you for your feedback!");
				Window.GetWindow(this).Close();
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private bool CheckInput()
		{
			if (String.IsNullOrEmpty(Item.SenderMail) || !IsValidEmail(Item.SenderMail))
			{
				CsGlobal.Message.Push("You must specify an valid e-mail address", CsMessage.Types.Warning);
				return false;
			}
			if (String.IsNullOrEmpty(Item.SenderName))
			{
				CsGlobal.Message.Push("You must specify a name.", CsMessage.Types.Warning);
				return false;
			}
			if (String.IsNullOrEmpty(Item.Title))
			{
				CsGlobal.Message.Push("You must specify a title.", CsMessage.Types.Warning);
				return false;
			}
			if (String.IsNullOrEmpty(Item.Text))
			{
				CsGlobal.Message.Push("You must specify a Text.", CsMessage.Types.Warning);
				return false;
			}
			return true;
		}
		bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		private void OpenWebsite(object sender, RoutedEventArgs e)
		{
			CsGlobal.RunExternal.OpenWebpage("http://www.christian.sack.at/");
		}
	}
}