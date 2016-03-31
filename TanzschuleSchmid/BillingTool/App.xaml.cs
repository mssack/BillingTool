// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingTool.btScope;
using CsWpfBase.Global;






namespace BillingTool
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{

			Current.DispatcherUnhandledException += (s, args) =>
			{
				try
				{
					Bt.Logging.New(LogTitels.UnhandledException, args.Exception.ToString(), LogTypes.Fatal);
				}
				catch (Exception)
				{
					
				}
			};


		CsGlobal.Install(GlobalFunctions.Storage | GlobalFunctions.WpfStorage | GlobalFunctions.GermanThreadCulture | GlobalFunctions.RedirectUnhandledExceptions); //Provides some needed functionality. DO NOT REMOVE.
			


			Bt.Config.CommandLine.Interpret(e.Args);
			Bt.UiFunctions.ExecuteConfiguration();
		}
	}
}