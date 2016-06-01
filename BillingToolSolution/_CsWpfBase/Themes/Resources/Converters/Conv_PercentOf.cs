// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-05</date>

using System;
using System.Globalization;
using System.Windows.Data;






namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_PercentOf : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
				return value;



			var val = (double) value;

			var percent = System.Convert.ToDouble(parameter.ToString().Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator).Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
			return val*percent;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		#endregion
	}
}