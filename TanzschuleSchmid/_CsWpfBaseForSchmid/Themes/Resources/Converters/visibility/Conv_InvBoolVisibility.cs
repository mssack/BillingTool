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
	public class Conv_InvBoolVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TwowayCheck(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return TwowayCheck(value);
		}

		private object TwowayCheck(object value)
		{
			if (value is Visibility)
			{
				var val = (Visibility) value;
				return val != Visibility.Visible; //returns boolean
			}
			if (value is bool)
			{
				var val = (bool) value;
				return !val ? Visibility.Visible : Visibility.Collapsed;
			}
			if (value == null)
				return Visibility.Visible;

			this.ThrowInvalidDataException(value);
			return null;
		}
	}
}