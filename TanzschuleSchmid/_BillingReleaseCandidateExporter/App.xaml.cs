// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Windows;






namespace ReleaseCandidateExporter
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		#region Overrides/Interfaces
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			new ExportRuntime().Run();

			Environment.Exit(0);
		}
		#endregion
	}



}