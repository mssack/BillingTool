// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Globalization;
using System.Windows.Data;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Themes.Resources.Converters.format
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_EnumToDescription : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = value as Enum;
			if (val != null)
				return val.GetDescription();
			throw new Exception("EnumToDescription Converter Failure");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new Exception("EnumToDescription Converter Failure");
		}
		#endregion
	}
}