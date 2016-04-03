// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
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
		private static ImageRenderer ImageRenderer => ImageRenderer.I;
		private static PdfCreator PdfCreator => PdfCreator.I;
		private static readonly StaTaskScheduler StaTaskScheduler = new StaTaskScheduler(2);
		private static readonly StaTaskScheduler StaPrinterScheduler = new StaTaskScheduler(1);
		

		/// <summary>Processes all <see cref="ProcessingStates.NotProcessed" /> linked outputs.</summary>
		public static Task<Task[]> Process(BelegData data, IContainPrinterConfiguration printConfig, IContainMailConfiguration mailConfig)
		{
			var tasks = new List<Task>();
			tasks.AddRange(data.MailedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed).Select(x => Process(x, mailConfig)));
			tasks.AddRange(data.PrintedBelege.Where(x => x.ProcessingState == ProcessingStates.NotProcessed).Select(x => Process(x, printConfig)));


			var taskarray = tasks.ToArray();
			var t = new Task(() => Task.WaitAll(taskarray), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			var continuationTask = t.ContinueWith(task => { data.DataSet.SaveAnabolic(); data.DataSet.AcceptChanges(); return taskarray; }, TaskScheduler.FromCurrentSynchronizationContext());
			return continuationTask;
		}

		/// <summary>Processes the <paramref name="data" /> and adjusts the <see cref="MailedBeleg.ProcessingState" /> row.</summary>
		public static Task<MailedBeleg> Process(MailedBeleg data, IContainMailConfiguration mailConfig)
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
					var smtpClient = new SmtpClient
					{
						Host = mailConfig.SmtpServer,
						UseDefaultCredentials = false,
						Credentials = new NetworkCredential(mailConfig.SmtpUsername, mailConfig.SmtpPassword),
						Timeout = 5*60*1000,
						EnableSsl = mailConfig.SmtpEnableSsl
					};



					var message = new MailMessage
					{
						From = new MailAddress(mailConfig.SmtpMailAddress),
						Subject = data.Betreff,
						IsBodyHtml = false,
						Body = data.Text
					};
					message.To.Add(data.TargetMailAddress);
					message.Attachments.Add(pdfLifeLine.AsMailAttachment());


					smtpClient.Send(message);
					message.Dispose();
					smtpClient.Dispose();
				}
			}, TaskCreationOptions.LongRunning);
			var continuationTask = t.ContinueWith(task =>
			{
				data.ProcessingDate = DateTime.Now;
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
		public static Task<PrintedBeleg> Process(PrintedBeleg data, IContainPrinterConfiguration printConfig)
		{
			if (data.ProcessingState != ProcessingStates.NotProcessed)
				throw new InvalidOperationException($"The {data} has currently the wrong state.");
			if (data.BelegData.State == BelegDataStates.Unknown)
				throw new InvalidOperationException($"The {data} cannot be printed because the {nameof(BelegData)} is currently of state {data.BelegData.State} which is not a valid state for mailing.");

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			data.ProcessingState = ProcessingStates.Processing;
			data.PrinterDevice = printConfig.Printer.PrintQueue.FullName;

			var t = new Task(() =>
			{
				var image = new Image { Source = ProcessFormat(data.BelegData, data.OutputFormat) };
				printConfig.Printer.PrintVisual(image, $"{data.BelegData}");
			}, TaskCreationOptions.LongRunning);
			var continuationTask = t.ContinueWith(task =>
			{
				data.ProcessingDate = DateTime.Now;
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


		private static BitmapSource ProcessFormat(BelegData data, OutputFormats format)
		{
			if (format == OutputFormats.StandardBonV1)
				return ImageRenderer.BonV1(data);
			else
				throw new InvalidOperationException($"The format {format} is not a valid format for a rendering of data {data}.");
		}
	}
}