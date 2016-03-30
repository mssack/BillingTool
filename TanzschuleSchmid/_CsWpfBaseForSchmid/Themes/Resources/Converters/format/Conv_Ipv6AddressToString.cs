// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.format
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_Ipv6AddressToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IPAddress)
			{
				var convert = value.ToString().ToUpper();
				var indexOf = convert.IndexOf('%');
				if (indexOf == -1)
					return convert;
				return convert.Substring(0, indexOf);
			}
			throw new Exception();
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}