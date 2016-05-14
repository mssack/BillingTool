// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-31</date>

using System;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using BillingTool.Exceptions;
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
				var billingToolException = args.Exception as BillingToolException;
				if (billingToolException != null && billingToolException.Type != BillingToolException.Types.Undefined)
				{
					Bt.AppOutput.SetExitCode(billingToolException.Type);
				}
				else
				{
					Bt.AppOutput.SetExitCode(ExitCodes.FatalError);
				}

				try
				{
					Bt.Logging.New(LogTitels.UnhandledException, args.Exception.ToString(), LogTypes.Fatal);
				}
				catch (Exception)
				{

				}
			};

			CsGlobal.Install(GlobalFunctions.Storage | GlobalFunctions.WpfStorage | GlobalFunctions.GermanThreadCulture | GlobalFunctions.RedirectUnhandledExceptions); //Provides some needed functionality. DO NOT REMOVE.
			Bt.Startup(e.Args);
		}
	}
}