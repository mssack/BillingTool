// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Windows;
using BillingToolGitControl.Control;
using BillingToolGitControl.NewRc;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingToolGitControl
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		#region Overrides/Interfaces
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			if (e.Args.Length != 0 && e.Args[0] == "NewRC")
			{
				var question = new NewRc.NewRcControl();
				var messageResult = CsGlobal.Message.Push(question, CsMessage.Types.Information, "Neuer Release Client", CsMessage.MessageButtons.YesNo);
				if (messageResult == CsMessage.MessageResults.Yes)
					new RcCreatorRuntime(question.Message).Run();
			}
			else
			{
				ShutdownMode = ShutdownMode.OnLastWindowClose;
				var w = new ControlWindow();
				w.Show();
				return;
			}
			Environment.Exit(0);
		}
		#endregion
	}



}