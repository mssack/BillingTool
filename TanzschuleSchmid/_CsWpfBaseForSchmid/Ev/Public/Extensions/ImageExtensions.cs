// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of functions for converting from and to <see cref="BitmapImage" />.</summary>
	[DebuggerStepThrough]
	public static class ImageExtensions
	{
		/// <summary>
		///     Resizes the image to a defined maximum. Preserves the aspect ratio.
		///     <para>Image[200x100].ResizeToMaximum(100,100) results in a new image of size [100x50]</para>
		///     <para>Image[100x200].ResizeToMaximum(100,100) results in a new image of size [50x100]</para>
		/// </summary>
		/// <param name="image">The image from which the new image will be created.</param>
		/// <param name="maximumWidth">The maximum allowed width for the new image.</param>
		/// <param name="maximumHeight">The maximum allowed height for the new image.</param>
		public static BitmapSource ResizeToMaximum(this BitmapSource image, int maximumWidth, int maximumHeight)
		{
			if (image == null)
				return null;


			double xAdj = (double) image.PixelWidth/maximumWidth, yAdj = (double) image.PixelHeight/maximumHeight;

			if ((xAdj > 1 || yAdj > 1))
			{
				if (xAdj > yAdj)
				{
					var tbBitmap = new TransformedBitmap(image, new ScaleTransform(1/xAdj, 1/xAdj));
					return BitmapFrame.Create(tbBitmap);
				}
				else
				{
					var tbBitmap = new TransformedBitmap(image, new ScaleTransform(1/yAdj, 1/yAdj));
					return BitmapFrame.Create(tbBitmap);
				}
			}
			return image;
		}

		/// <summary>
		///     Resizes the image to a defined maximum pixel amount. Preserves the aspect ratio.
		///     <para>Image[100x100].ResizeToMaximum(100) results in a new image of size [10x10]</para>
		///     <para>Image[200x50].ResizeToMaximum(100) results in a new image of size [20x5]</para>
		/// </summary>
		/// <param name="image">The image from which the new image will be created.</param>
		/// <param name="pixels">The maximum amount of pixels in the new image.</param>
		public static BitmapSource ResizeToMaximum(this BitmapSource image, long pixels)
		{
			if (image == null)
				return null;

			var pixelAmountBefore = image.PixelWidth*image.PixelHeight;

			if (pixels >= pixelAmountBefore)
				return image;

			//width * height=amount
			//width / height=relation
			//
			//amount=relation * height * height
			//new.height = root(amount / relation)
			//
			//adjust = new.height / old.height

			var adjust = Math.Sqrt(pixels/(image.PixelWidth/(double) image.PixelHeight))/image.PixelHeight;

			var tbBitmap = new TransformedBitmap(image, new ScaleTransform(adjust, adjust));
			return BitmapFrame.Create(tbBitmap);
		}

		/// <summary>Creates a converted copy of the image (jpg formatted).</summary>
		public static byte[] ConvertTo_JpgByteArray(this BitmapSource input, int qualityLevel = 100)
		{
			var encoder = new JpegBitmapEncoder {QualityLevel = qualityLevel};
			return input.ConvertTo_UsingEncoder(encoder);
		}

		/// <summary>Creates a converted copy of the image (png formatted).</summary>
		public static byte[] ConvertTo_PngByteArray(this BitmapSource input, PngInterlaceOption interlace = PngInterlaceOption.Default)
		{
			var encoder = new PngBitmapEncoder {Interlace = interlace};
			return input.ConvertTo_UsingEncoder(encoder);
		}

		/// <summary>Creates a converted copy of the image (gif formatted).</summary>
		public static byte[] ConvertTo_GifByteArray(this BitmapSource input)
		{
			var encoder = new GifBitmapEncoder();
			return input.ConvertTo_UsingEncoder(encoder);
		}

		/// <summary>Creates a converted copy of the image (bmp formatted).</summary>
		public static byte[] ConvertTo_BmpByteArray(this BitmapSource input)
		{
			var encoder = new BmpBitmapEncoder();
			return input.ConvertTo_UsingEncoder(encoder);
		}

		/// <summary>Creates a converted copy of the image (tiff formatted).</summary>
		public static byte[] ConvertTo_TiffByteArray(this BitmapSource input, TiffCompressOption compressOption = TiffCompressOption.None)
		{
			var encoder = new TiffBitmapEncoder {Compression = compressOption};
			return input.ConvertTo_UsingEncoder(encoder);
		}

		/// <summary>Creates a converted copy of the image (wmp formatted).</summary>
		public static byte[] ConvertTo_WmpByteArray(this BitmapSource input)
		{
			var encoder = new WmpBitmapEncoder();
			//TODO include Options
			return input.ConvertTo_UsingEncoder(encoder);
		}


		/// <summary>
		///     Converts an buffer, created by one of the following methods, to an <see cref="BitmapImage" />.
		///     <para>
		///         <see cref="ConvertTo_JpgByteArray" /> or
		///     </para>
		///     <para>
		///         <see cref="ConvertTo_PngByteArray" /> or
		///     </para>
		///     <para>
		///         <see cref="ConvertTo_TiffByteArray" /> or
		///     </para>
		///     <para>
		///         <see cref="ConvertTo_GifByteArray" /> or
		///     </para>
		///     <para>
		///         <see cref="ConvertTo_BmpByteArray" /> or
		///     </para>
		///     <para>
		///         <see cref="ConvertTo_WmpByteArray" />
		///     </para>
		/// </summary>
		public static BitmapSource ConvertTo_Image(this byte[] data)
		{
			if (data == null || data.Length == 0)
				return null;
			BitmapImage img;
			using (var sr = new MemoryStream(data))
			{
				sr.Seek(0, SeekOrigin.Begin);
				img = new BitmapImage();
				img.BeginInit();
				img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.StreamSource = sr;
				img.EndInit();
				sr.Close();
			}
			return img;
		}

		private static byte[] ConvertTo_UsingEncoder(this BitmapSource input, BitmapEncoder encoder)
		{
			if (input == null)
				return null;
			var stream = new MemoryStream();


			encoder.Frames.Add(BitmapFrame.Create(input));
			encoder.Save(stream);
			var serializedImage = stream.ToArray();

			stream.Close();
			stream.Dispose();
			return serializedImage;
		}
	}
}