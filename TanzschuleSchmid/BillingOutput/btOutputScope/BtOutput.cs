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
using System.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingOutput.btOutputScope.ImageProcessing;
using BillingOutput.btOutputScope.PdfCreation;
using BillingOutput.Interfaces;
using BillingOutput.TaskSchedulers;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingOutput.btOutputScope
{
	/// <summary>Used to access all output capacity's.</summary>
	public static class BtOutput
	{
		private static readonly StaTaskScheduler StaTaskScheduler = new StaTaskScheduler(2);
		private static readonly StaTaskScheduler StaPrinterScheduler = new StaTaskScheduler(1);
		private static ImageRenderer ImageRenderer => ImageRenderer.I;
		private static PdfCreator PdfCreator => PdfCreator.I;


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
				var image = ProcessFormat(data.BelegData, data.OutputFormat);
				using (var pdfLifeLine = PdfCreator.CreatePdf(data.BelegData, image))
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
							Body = data.Text
						})
						{
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

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			data.ProcessingState = ProcessingStates.Processing;

			var t = new Task(() =>
			{
				var processFormat = ProcessFormat(data.BelegData, data.OutputFormat);
				var image = new Image {Source = processFormat, Width = processFormat.PixelWidth, Height = processFormat.PixelHeight};
				image.Measure(new Size(image.Width, image.Height));
				image.Arrange(new Rect(new Size(image.Width, image.Height)));
				image.UpdateLayout();

				var printServer = new PrintServer();
				var printDialog = new PrintDialog
				{
					PrintQueue = new PrintQueue(printServer,data.PrinterDevice)
				};
				printDialog.PrintVisual(image, $"{data.BelegData}");

			}, TaskCreationOptions.LongRunning);
			var continuationTask = t.ContinueWith(task =>
			{
				data.ProcessingDate = DateTime.Now;
				data.OutputFormat.LastUsedDate = data.ProcessingDate;
				data.BelegData.PrintCount++;
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
			t.Start(StaPrinterScheduler);
			return continuationTask;
		}
		

		private static BitmapSource ProcessFormat(BelegData data, OutputFormat format)
		{
			if (format.BonLayout == BonLayouts.Unknown)
				throw new InvalidOperationException($"The format {format} is not a valid format for a rendering of data {data}.");
			return ImageRenderer.Render(data, format);
		}
	}





}