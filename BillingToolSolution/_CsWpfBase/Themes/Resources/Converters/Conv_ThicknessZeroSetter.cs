// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_ThicknessZeroSetter : IValueConverter
	{
		public object Convert(object value, Type targetType, object thicknessString, CultureInfo culture)
		{
			var actual = (Thickness) value;
			var replace = ((string) thicknessString);
			var values = replace.Split(',');
			var lengths = values.Select(System.Convert.ToDouble).ToArray();
			Thickness param;


			if (lengths.Length == 1)
				param = new Thickness(lengths[0]);
			else if (lengths.Length == 2)
				param = new Thickness(lengths[0], lengths[1], lengths[0], lengths[1]);
			else if (lengths.Length == 4)
				param = new Thickness(lengths[0], lengths[1], lengths[2], lengths[3]);
			else
			{
				throw new ArgumentException();
			}


			// ReSharper disable CompareOfFloatsByEqualityOperator
			return new Thickness(param.Left == 0.0 ? 0 : actual.Left, param.Top == 0.0 ? 0 : actual.Top, param.Right == 0.0 ? 0 : actual.Right, param.Bottom == 0.0 ? 0 : actual.Bottom);
			// ReSharper restore CompareOfFloatsByEqualityOperator
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}