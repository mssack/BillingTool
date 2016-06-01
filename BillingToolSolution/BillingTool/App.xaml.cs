﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-17</date>

// ReSharper disable RedundantUsingDirective

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using BillingTool.btScope;
using BillingTool.enumerations;
using BillingTool.Exceptions;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var ss = new SplashScreen("Themes/Icons/KassenIcon.png");
			ss.Show(true, true);
			
			Current.DispatcherUnhandledException += (s, args) =>
			{
#if !DEBUG
				var t = new Task(() =>
				{
					try
					{
						var mailConfig = Bt.Config.LocalSettings;
						using (var smtpClient = new SmtpClient
						{
							Host = mailConfig.SmtpServer,
							UseDefaultCredentials = false,
							Credentials = new NetworkCredential(mailConfig.SmtpUsername, mailConfig.SmtpPassword),
							Timeout = 30*1000,
							EnableSsl = mailConfig.SmtpEnableSsl,
							Port = mailConfig.SmtpPort
						})
						{
							using (var message = new MailMessage
							{
								From = new MailAddress(mailConfig.SmtpMailAddress),
								Subject = $"[BILLINGTOOL].[{args.Exception.GetType().Name.ToUpper()}] - {args.Exception.Message.CutMiddle()}",
								IsBodyHtml = false,
								Body = $"Konfiguration = \"{Bt.Config.Control.Current}\"\r\n\r\n\r\n" +
										$"Computer = {CsGlobal.Os.ComputerName} ({CsGlobal.Computer.System.Manufacturer}, {CsGlobal.Computer.System.Model})" + "\r\n" + 
										$"Benutzer = {CsGlobal.Os.CurrentUser.Name} ({CsGlobal.Os.CurrentUser.FullName}, Registered User={CsGlobal.Os.RegisteredUser})" + "\r\n" +
										$"OS = {CsGlobal.Os.Name}" + "\r\n" +
										$"Screen = {CsGlobal.Computer.Screen.TotalWidth}x{CsGlobal.Computer.Screen.TotalHeight}" + "\r\n" +
										$"Drucker = {CsGlobal.Computer.Devices.Printers.Select(x=>$"{x.Name}[{x.DriverName}{(x.Default?" = DEFAULT":"")}]").Join()}" + "\r\n" +
										$"\r\n\r\n\r\n{args.Exception}"
							})
							{
								message.To.Add("service.christian@sack.at");
								message.To.Add("michael@sack.at");
								smtpClient.Send(message);
							}
						}
					}
					catch (Exception exc)
					{

					}

				}, TaskCreationOptions.LongRunning);
				t.Start(TaskScheduler.Default);
#endif


				var billingToolException = args.Exception.CastOrFindInInnerExceptions<BillingToolException>();
				Bt.AppOutput.Remove_ExitCode(ExitCodes.Success);

				if (billingToolException != null && billingToolException.Type != BillingToolException.Types.Undefined)
					Bt.AppOutput.Include_ExitCode((ExitCodes) billingToolException.Type);
				else
					Bt.AppOutput.Include_ExitCode(ExitCodes.Error_Unhandled);

				try
				{
					Bt.Logging.New(LogTitels.UnhandledException, args.Exception.ToString(), LogTypes.Fatal);
				}
				catch (Exception)
				{

				}
				try
				{
					CsGlobal.Message.Push(args.Exception, CsMessage.Types.FatalError);
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
				args.Handled = true;
				Current.Shutdown();

			};




			CsGlobal.Install(GlobalFunctions.Storage | GlobalFunctions.WpfStorage | GlobalFunctions.GermanThreadCulture); //Provides some needed functionality. DO NOT REMOVE.
			Bt.Startup(e.Args);
		}
	}
}