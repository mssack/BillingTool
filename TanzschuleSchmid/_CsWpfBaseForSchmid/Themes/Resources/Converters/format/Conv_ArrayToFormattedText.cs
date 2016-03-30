// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.format
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_ArrayToFormattedText : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var array = value as IEnumerable;
			if (array == null)
				return "";

			return String.Join(parameter is string ? parameter as string : "\r\n", array.OfType<object>().Select(x => x.ToString()));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}