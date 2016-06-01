// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	[DebuggerStepThrough]
	public class Conv_TimeDiffference : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length != 2)
				throw new InvalidOperationException("The Timedifference converter needs two items");
			if (values[0] == null || values[1] == null)
				return TimeSpan.FromSeconds(0);

			return ((DateTime) values[0] - (DateTime) values[1]);
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return new object[] {};
		}
	}
}