// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingTool.btScope;
using BillingTool.btScope.configuration.types;
using BillingTool.Modes.developer;
using BillingTool.Modes.newCashBookEntry;
using BillingTool.Runtime.types;
using CsWpfBase.Global;






namespace BillingTool
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			if (e.Args.Length == 0)
			{
				//TODO Open information window
				CsGlobal.App.Exit();
			}
			ParseCommandLine(e.Args);
			ExecuteMode();
		}

		private void ParseCommandLine(string[] data)
		{
			for (var i = 0; i < data.Length; i++)
			{
				var arg = data[i];

				if (!arg.StartsWith("/"))
					continue;

				if (string.Equals(arg, "/developer", StringComparison.OrdinalIgnoreCase))
					CommandLineConfiguration.I.RuntimeMode = RuntimeModes.Developer;
				else if (string.Equals(arg, "/newcashbookentry", StringComparison.OrdinalIgnoreCase))
					CommandLineConfiguration.I.RuntimeMode = RuntimeModes.NewCashBookEntry;
				else if (string.Equals(arg, "/createdatabase", StringComparison.OrdinalIgnoreCase))
					CommandLineConfiguration.I.RuntimeMode = RuntimeModes.NewCashBookEntry;
				else if (string.Equals(arg, "/database", StringComparison.OrdinalIgnoreCase))
				{
					if (i + 1 >= data.Length)
						throw new Exception("You have to pass a database file if you use startup parameter '/database'");

					CommandLineConfiguration.I.DatabaseFilePath = data[++i]; //Use the next argument as file path.
				}
				else
				{
					throw new Exception($"Invalid startup argument['{arg}']. For more information start program without parameters.");
				}
			}
		}

		private void ExecuteMode()
		{
			if (CommandLineConfiguration.I.RuntimeMode == RuntimeModes.Developer)
			{
				Current.MainWindow = new DeveloperWindow();
				Current.MainWindow.Show();
			}
			else if (CommandLineConfiguration.I.RuntimeMode == RuntimeModes.NewCashBookEntry)
			{
				if (CommandLineConfiguration.I.CreateDatabaseIfNotExist)
					Bt.Db.Init();

				Current.MainWindow = Bt.UiFunctions.NewCashBookEntry();
			}
			else if (CommandLineConfiguration.I.RuntimeMode == RuntimeModes.CreateDatabase)
			{
				Bt.Db.CreateDatabase();

				CsGlobal.App.Exit();
			}
		}
	}
}