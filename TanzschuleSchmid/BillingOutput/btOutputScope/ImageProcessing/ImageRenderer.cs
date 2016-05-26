// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingOutput.Controls.BonVisuals;
using BillingOutput.Controls._shared;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingOutput.btOutputScope.ImageProcessing
{
	internal class ImageRenderer
	{
		private static ImageRenderer _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ImageRenderer I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ImageRenderer());
				}
			}
		}

		private ImageRenderer()
		{
		}


		public BitmapSource Render(BelegData data, OutputFormat format, double scalingFactor = 4)
		{
			var visual = new AnyBonVisual { Item = data, OutputFormat = format };
			ApplyScalingFactor(visual, scalingFactor);
			var image = visual.ConvertTo_Image();
			image.Freeze();
			return image;
		}


		private void ApplyScalingFactor(FrameworkElement control, double scalingFactor = 1.5)
		{
			control.LayoutTransform = new ScaleTransform(scalingFactor, scalingFactor, 0.5, 0.5);
		}
	}
}