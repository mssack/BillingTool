// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.visibility
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_NullToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int && (int) value == 0)
				return parameter ?? Visibility.Collapsed;
			if (value == null)
				return parameter ?? Visibility.Collapsed;
			if (value is string && String.IsNullOrEmpty(value as string))
				return parameter ?? Visibility.Collapsed;
			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}