// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media.Imaging;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using PdfSharp.Drawing;
using PdfSharp.Pdf;






namespace BillingToolOutput.btOutputScope.PdfCreation
{
	internal class PdfLifeLine : Base
	{
		private FileInfo _file;
		private PdfDocument _pdfDoc;
		private BelegData _belegData;

		public PdfLifeLine(BelegData data, OutputFormat format, BitmapSource image)
		{
			_belegData = data;
			File = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMP", $"{Guid.NewGuid()}.pdf"));
			File.CreateDirectory_IfNotExists();
			File.DeleteFile_IfExists();

			PdfDoc = new PdfDocument();
			var page = PdfDoc.AddPage();
			page.Width = new XUnit(image.PixelWidth/ image.DpiX, XGraphicsUnit.Inch);
			page.Height = new XUnit(image.PixelHeight/ image.DpiY, XGraphicsUnit.Inch);

			XGraphics.FromPdfPage(page).DrawImage(XImage.FromStream(new MemoryStream(image.ConvertTo_JpgByteArray(format.ImageQuality))), new Point(0,0));

			PdfDoc.Save(File.FullName);
			PdfDoc.Dispose();
			File.Refresh();
		}


		#region Overrides/Interfaces
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public override void Dispose()
		{
			base.Dispose();
			File.DeleteFile_IfExists();
		}
		#endregion


		/// <summary>Gets or sets the File.</summary>
		public FileInfo File
		{
			get { return _file; }
			private set { SetProperty(ref _file, value); }
		}
		/// <summary>Gets or sets the PdfDoc.</summary>
		private PdfDocument PdfDoc
		{
			get { return _pdfDoc; }
			set { SetProperty(ref _pdfDoc, value); }
		}


		public Attachment AsMailAttachment()
		{
			var attachment = new Attachment(File.FullName, MediaTypeNames.Application.Pdf);
			var disp = attachment.ContentDisposition;
			disp.CreationDate = File.CreationTime; 
			disp.ModificationDate = File.LastWriteTime; 
			disp.ReadDate = File.LastAccessTime;
			disp.FileName = $"Beleg vom {_belegData.Datum.ToString("dd-MM-yy")}.pdf";
			disp.Size = File.Length;
			disp.DispositionType = DispositionTypeNames.Attachment;
			return attachment;
		}
	}
}