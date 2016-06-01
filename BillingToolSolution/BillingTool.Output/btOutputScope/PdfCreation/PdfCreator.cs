// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows.Media.Imaging;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingToolOutput.btOutputScope.PdfCreation
{
	internal class PdfCreator : Base
	{
		private static PdfCreator _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static PdfCreator I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new PdfCreator());
				}
			}
		}

		private PdfCreator()
		{
		}


		public PdfLifeLine CreatePdf(BelegData data, OutputFormat format, BitmapSource image)
		{
			return new PdfLifeLine(data, format, image);
		}
	}
}