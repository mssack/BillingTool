// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.logic
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_InvIsNullOrEmpty : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool rv;
			if (value == null)
				rv = true;
			else if (value is string)
				rv = String.IsNullOrEmpty(value as string);
			else if (value is int)
				rv = ((int)value) == 0;
			else
				rv = false;
			return !rv;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}