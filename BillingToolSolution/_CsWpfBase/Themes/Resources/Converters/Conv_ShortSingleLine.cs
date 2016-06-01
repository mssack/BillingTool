// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.Globalization;
using System.Windows.Data;






namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_ShortSingleLine : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(value as string))
				return null;

			var length = 100;
			if (parameter is int)
				length = (int) parameter;
			var val = (string) value;
			val = val.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

			return val.Length > length ? val.Substring(0, length) + "..." : val;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		#endregion
	}
}