// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Public.Extensions;
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
#if !DEBUG
				Task t = new Task(() =>
				{
					try
					{
						var mailConfig = Bt.Config.File.KassenEinstellung;
						using (var smtpClient = new SmtpClient
						{
							Host = mailConfig.SmtpServer,
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential(mailConfig.SmtpUsername, mailConfig.SmtpPassword),
							Timeout = 30 * 1000,
							EnableSsl = mailConfig.SmtpEnableSsl,
							Port = mailConfig.SmtpPort
						})
						{
							using (var message = new MailMessage
							{
								From = new MailAddress(mailConfig.SmtpMailAddress),
								Subject = $"[BILLINGTOOL].[EXCEPTION] - {args.Exception.Message.CutMiddle()}",
								IsBodyHtml = false,
								Body = args.Exception.ToString()
							})
							{
								message.To.Add("service.christian@sack.at");
								message.To.Add("michael@sack.at");
								smtpClient.Send(message);
							}
						}
					}
					catch (Exception)
					{

					}

				}, TaskCreationOptions.LongRunning);
				t.Start(TaskScheduler.Default);
#endif


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
#if !DEBUG
				try
				{
					t.Wait(5000);
				}
				catch (Exception)
				{
					
				}
#endif

			};

			CsGlobal.Install(GlobalFunctions.Storage | GlobalFunctions.WpfStorage | GlobalFunctions.GermanThreadCulture | GlobalFunctions.RedirectUnhandledExceptions); //Provides some needed functionality. DO NOT REMOVE.
			Bt.Startup(e.Args);
		}
	}
}