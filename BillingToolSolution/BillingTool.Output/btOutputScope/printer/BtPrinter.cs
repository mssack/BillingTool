// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingToolOutput.threading;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolOutput.btOutputScope.printer
{
	internal sealed class BtPrinter : Base
	{
		private static readonly StaTaskScheduler PrintScheduler = new StaTaskScheduler(1);

		public BtPrinter(PrintedBeleg data)
		{
			Data = data;
		}

		/// <summary>Gets or sets the Data.</summary>
		public PrintedBeleg Data { get; }
		private PrintDialog Dialog { get; set; }
		private BitmapSource Image { get; set; }
		private FixedDocument Document { get; set; }

		private double PaperHeight => Dialog.PrintableAreaHeight;
		private double PaperWidth => Dialog.PrintableAreaWidth;
		private int ImageHeight => Image.PixelHeight;
		private int ImageWidth => Image.PixelWidth;


		public Task<PrintedBeleg> Print()
		{

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			Data.ProcessingState = ProcessingStates.Processing;
			var t = new Task(PrintDocument, TaskCreationOptions.LongRunning);
			var continuationTask = t.ContinueWith(FinalizeData, context);
			t.Start(PrintScheduler);
			return continuationTask;
		}

		private void PrintDocument()
		{
			Dialog = new PrintDialog
			{
				PrintQueue = new PrintQueue(new PrintServer(), Data.PrinterDevice)
			};
			CreateImage();
			CreateDocument();
			CreatePages();

			Dialog.PrintDocument(Document.DocumentPaginator, $"{Data.BelegData}");
		}

		private void CreateImage()
		{
			Image = BtOutput.ImageRenderer.Render(Data.BelegData, Data.OutputFormat);
		}

		private void CreateDocument()
		{
			Document = new FixedDocument();
		}

		private void CreatePages()
		{
			int paperNumber = 1;

			int paperHeightForImage = (int)((ImageWidth / PaperWidth) * PaperHeight);
			while (ImageHeight - paperNumber * paperHeightForImage > -paperHeightForImage)
			{
				var paperImage = GetPaperImage(paperNumber);

				double pW = PaperWidth;
				double pH = paperImage.PixelHeight * PaperWidth/paperImage.PixelWidth;


				var page = new FixedPage
				{
					Width = pW,
					Height = pH,
					Margin = new Thickness(0),
				};
				RenderOptions.SetBitmapScalingMode(page, BitmapScalingMode.HighQuality);
				var image = new Image
				{
					Width = pW,
					Height = pH,
					Margin = new Thickness(0),
					Stretch = Stretch.Uniform,
					StretchDirection = StretchDirection.Both,
					Source = paperImage
				};
				FixedPage.SetLeft(image, 0);
				FixedPage.SetTop(image, 0);
				page.Children.Add(image);

				var pageContent = new PageContent
				{
					Width = pW,
					Height = pH,
					Margin = new Thickness(0),
					Child = page,
				};

				Document.Pages.Add(pageContent);
				paperNumber++;
			}
		}

		private BitmapSource GetPaperImage(int paperNumber)
		{
			int paperHeightForImage = (int)((ImageWidth/PaperWidth)*PaperHeight);
			if (paperNumber* paperHeightForImage > ImageHeight)
			{
				int imageStartHeight = (paperNumber - 1) * paperHeightForImage;
				return new CroppedBitmap(Image, new Int32Rect(0, imageStartHeight, ImageWidth, ImageHeight-imageStartHeight));
			}
			else
			{
				int imageStartHeight = (paperNumber - 1) * paperHeightForImage;
				return new CroppedBitmap(Image, new Int32Rect(0, imageStartHeight, ImageWidth, paperHeightForImage));
			}
		}

		private PrintedBeleg FinalizeData(Task task)
		{
			Data.ProcessingDate = DateTime.Now;
			Data.OutputFormat.LastUsedDate = Data.ProcessingDate;
			Data.BelegData.PrintCount++;
			if (task.Exception != null && task.IsFaulted)
			{
				Data.ProcessingState = ProcessingStates.Failed;
				Data.ProcessingException = task.Exception.MostInner().Message;
				throw task.Exception;
			}

			Data.ProcessingState = ProcessingStates.Processed;
			Data.ProcessingException = null;
			return Data;
		}
	}
}