// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-12</date>

using System;
using System.Globalization;
using System.Windows.Data;
using CsWpfBase.Global;





namespace CsWpfBase.Themes.Resources.Converters.image
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class Conv_PercentToQualityImage : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = System.Convert.ToDouble(value);
			if (val < 20)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-Con-ConnectionQuality1"];
			if (val < 40)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-Con-ConnectionQuality2"];
			if (val < 60)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-Con-ConnectionQuality3"];
			if (val < 80)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-Con-ConnectionQuality4"];

			return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-Con-ConnectionQuality5"];
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			this.ThrowOneWayException();
			return null;
		}
	}
}