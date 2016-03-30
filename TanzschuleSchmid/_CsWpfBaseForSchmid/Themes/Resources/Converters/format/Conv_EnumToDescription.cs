// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows.Data;
using CsWpfBase.Themes.AttachedProperties;





namespace CsWpfBase.Themes.Resources.Converters.format
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_EnumToDescription : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Enum)
			{
				return EnumerationExtension.GetDescription((Enum) value);
			}
			throw new Exception("EnumToDescription Converter Failure");
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new Exception("EnumToDescription Converter Failure");
		}
	}
}