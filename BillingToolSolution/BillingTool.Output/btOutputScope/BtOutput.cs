// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingToolOutput.btOutputScope.ImageProcessing;
using BillingToolOutput.btOutputScope.printer;
using BillingToolOutput.btOutputScope.PdfCreation;
using BillingToolOutput.Interfaces;
using BillingToolOutput.threading;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolOutput.btOutputScope
{
	/// <summary>Used to access all output capacity's.</summary>
	public static class BtOutput
	{
		private static readonly StaTaskScheduler StaTaskScheduler = new StaTaskScheduler(2);
		internal static ImageRenderer ImageRenderer => ImageRenderer.I;
		internal static PdfCreator PdfCreator => PdfCreator.I;


		/// <summary>Processes all <see cref="ProcessingStates.NotProcessed" /> linked outputs.</summary>
		public static Task<Task[]> Process(BelegData data, IContainMailConfiguration mailConfig)
		{
			var tasks = new List<Task>();
			tasks.AddRange(data.MailedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed).Select(x => Process(x, mailConfig)));
			tasks.AddRange(data.PrintedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed).Select(Process));


			var taskarray = tasks.ToArray();
			var t = new Task<Task[]>(() =>
			{
				try
				{
					Task.WaitAll(taskarray);
				}
				catch (Exception)
				{
					
				}
				return taskarray;
			}, TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}

		/// <summary>Processes the <paramref name="data" /> and adjusts the <see cref="MailedBeleg.ProcessingState" /> row.</summary>
		private static Task<MailedBeleg> Process(MailedBeleg data, IContainMailConfiguration mailConfig)
		{
			if (data.ProcessingState != ProcessingStates.NotProcessed)
				throw new InvalidOperationException($"The {data} has currently the wrong state.");
			if (data.BelegData.State == BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {data} cannot be printed because the {nameof(BelegData)} is currently of state {data.BelegData.State} which is not a valid state for printing.");


			var context = TaskScheduler.FromCurrentSynchronizationContext();
			data.ProcessingState = ProcessingStates.Processing;

			var t = new Task(() =>
			{
				var image = ImageRenderer.Render(data.BelegData, data.OutputFormat, 10);
				using (var pdfLifeLine = PdfCreator.CreatePdf(data.BelegData, data.OutputFormat, image))
				{
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
							Subject = data.Betreff,
							IsBodyHtml = false,
							Body = data.Text,
						})
						{
							if (!string.IsNullOrEmpty(data.Bcc))
							{
								data.Bcc = data.Bcc.Replace(';', ',').Replace("\r\n", "\n").Replace("\n", ",").Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).Where(x => x.IsValidMailAddress()).Join(", ");
								if (!string.IsNullOrEmpty(data.Bcc))
									message.Bcc.Add(data.Bcc);
							}

							message.To.Add(data.TargetMailAddress);
							message.Attachments.Add(pdfLifeLine.AsMailAttachment());
							smtpClient.Send(message);
						}
					}
				}
			}, TaskCreationOptions.LongRunning);
			var continuationTask = t.ContinueWith(task =>
			{
				data.ProcessingDate = DateTime.Now;
				data.OutputFormat.LastUsedDate = data.ProcessingDate;
				data.BelegData.MailCount++;
				if (task.Exception != null && task.IsFaulted)
				{
					data.ProcessingState = ProcessingStates.Failed;
					data.ProcessingException = task.Exception.MostInner().Message;
					throw task.Exception;
				}

				data.ProcessingState = ProcessingStates.Processed;
				data.ProcessingException = null;
				return data;
			}, context);
			t.Start(StaTaskScheduler);
			return continuationTask;
		}

		/// <summary>Processes the <paramref name="data" /> and adjusts the <see cref="PrintedBeleg.ProcessingState" /> row.</summary>
		private static Task<PrintedBeleg> Process(PrintedBeleg data)
		{
			if (data.ProcessingState != ProcessingStates.NotProcessed)
				throw new InvalidOperationException($"The {data} has currently the wrong state.");
			if (data.BelegData.State == BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {data} cannot be printed because the {nameof(BelegData)} is currently of state {data.BelegData.State} which is not a valid state for mailing.");


			return new BtPrinter(data).Print();
		}
	}







}