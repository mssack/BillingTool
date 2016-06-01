// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters.logic
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	[DebuggerStepThrough]
	public class Conv_DateTimeIsInFuture : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return false;
			var time = (DateTime) value;

			if (time > DateTime.Now)
				return true;
			return false;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}