// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CsWpfBase.Ev.Public.Extensions;





namespace CsWpfBase.Themes.Resources.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_PercentToColor : IValueConverter
	{
		public static readonly SolidColorBrush Blue = new SolidColorBrush(Colors.SteelBlue);
		public static readonly SolidColorBrush Orange = new SolidColorBrush(Colors.DarkOrange);
		public static readonly SolidColorBrush Tomato = new SolidColorBrush(Colors.Tomato);
		public static readonly SolidColorBrush Red = new SolidColorBrush(Colors.Red);
		public Conv_PercentToColor()
		{
			if (Blue.IsFrozen)
				return;
			Blue.Freeze();
			Orange.Freeze();
			Tomato.Freeze();
			Red.Freeze();
		}
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var d = (double) value;
			return Convert(d);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}

		public static SolidColorBrush Convert(double percent)
		{
			if (percent >= 90)
				return Blue;
			if (percent >= 80)
				return Orange;

			if (percent >= 70)
				return Tomato;
			return Red;
		}
		public static string ConvertToHex(double percent)
		{
			if (percent >= 90)
				return Blue.Color.GetHexRepresentation(false);
			if (percent >= 80)
				return Orange.Color.GetHexRepresentation(false);

			if (percent >= 70)
				return Tomato.Color.GetHexRepresentation(false);
			return Red.Color.GetHexRepresentation(false);
		}
		public static string ConvertToColorName(double percent)
		{
			if (percent >= 90)
				return "Blue";
			if (percent >= 80)
				return "Orange";

			if (percent >= 70)
				return "Tomato";
			return "Red";
		}
	}
}