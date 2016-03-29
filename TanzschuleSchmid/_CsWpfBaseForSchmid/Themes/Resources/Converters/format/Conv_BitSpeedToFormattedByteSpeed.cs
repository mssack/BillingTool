// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.format
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_BitSpeedToFormattedByteSpeed : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var d = System.Convert.ToInt64(value);
			return Convert(d);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		public static string Convert(Int64 value)
		{
			var d = System.Convert.ToDouble(value)/8.0;

			var typs = new[] {"B/s", "KB/s", "MB/s", "GB/s", "TB/s"};

			var typ = 0;
			while (d >= 1024.0)
			{
				d = d/1024.0;
				typ++;
			}
			if (typ > 0)
				return d.ToString("0.# ") + typs[typ];
			return d.ToString("0 ") + typs[typ];
		}
	}
}