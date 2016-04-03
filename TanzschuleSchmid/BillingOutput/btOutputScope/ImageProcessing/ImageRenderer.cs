// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingOutput.Controls;
using CsWpfBase.Ev.Public.Extensions;
using BonV1Visual = BillingOutput.Controls.BonVisuals.BonV1Visual;






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


		public BitmapSource BonV1(BelegData data, double scalingFactor = 1.5)
		{
			var visual = new BonV1Visual(data);
			ApplyScalingFactor(visual, scalingFactor);
			return visual.ConvertTo_Image();
		}

		private void ApplyScalingFactor(FrameworkElement control, double scalingFactor = 1.5)
		{
			control.LayoutTransform = new ScaleTransform(scalingFactor, scalingFactor, 0.5, 0.5);
		}
	}
}