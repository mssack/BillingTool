// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-16</date>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;






namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_ExceptionToExceptionList : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Exception))
				return null;

			var lst = new List<Exception>();
			var item = (Exception) value;

			lst.Add(item);

			item = item.InnerException;
			while (item != null)
			{
				lst.Add(item);
				item = item.InnerException;
			}


			return lst;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
		#endregion
	}
}