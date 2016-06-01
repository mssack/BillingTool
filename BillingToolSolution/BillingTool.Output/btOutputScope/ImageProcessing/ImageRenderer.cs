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
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingToolOutput.Controls._shared;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolOutput.btOutputScope.ImageProcessing
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


		public BitmapSource Render(BelegData data, OutputFormat format, double rahmen)
		{
			if (format.BonLayout == BonLayouts.Unknown)
				throw new InvalidOperationException($"The format {format} is not a valid format for a rendering of data {data}.");
			var border = new Border {Background = new SolidColorBrush(Colors.White), Padding = new Thickness(rahmen)};
			var visual = new AnyBonVisual { Item = data, OutputFormat = format, Padding = new Thickness(0)};
			ApplyScalingFactor(border, format.ImageScaling);
			border.Child = visual;
			var image = border.ConvertTo_Image();
			image.Freeze();
			return image;
		}


		private void ApplyScalingFactor(FrameworkElement control, double scalingFactor = 1.5)
		{
			control.LayoutTransform = new ScaleTransform(scalingFactor, scalingFactor, 0.5, 0.5);
		}
	}
}