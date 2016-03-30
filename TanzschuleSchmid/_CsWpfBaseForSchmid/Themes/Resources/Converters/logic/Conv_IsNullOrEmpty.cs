// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-22</date>

using System;
using System.Globalization;
using System.Windows.Data;






namespace CsWpfBase.Themes.Resources.Converters.logic
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_IsNullOrEmpty : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return true;
			if (value is string)
				return string.IsNullOrEmpty(value as string);
			if (value is int)
				return ((int) value) == 0;
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		#endregion
	}
}