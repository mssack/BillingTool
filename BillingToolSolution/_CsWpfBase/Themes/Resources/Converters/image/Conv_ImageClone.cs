// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Themes.Resources.Converters.image
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_ImageClone : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var src = value as BitmapSource;
			return src?.ConvertTo_PngByteArray().ConvertTo_Image();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		#endregion
	}
}