// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Windows;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace ReleaseCandidateExporter
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		#region Overrides/Interfaces
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			GitChangesQuestion question = new GitChangesQuestion();
			var messageResult = CsGlobal.Message.Push(question, CsMessage.Types.Information, "Neuer Release Client", CsMessage.MessageButtons.YesNo);

			if (messageResult == CsMessage.MessageResults.Yes)
				new ExportRuntime(question.Message).Run();

			Environment.Exit(0);
		}
		#endregion
	}



}