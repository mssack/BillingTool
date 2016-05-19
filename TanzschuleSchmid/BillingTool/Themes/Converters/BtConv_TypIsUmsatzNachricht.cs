// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Globalization;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingTool.Themes.Converters
{

#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class BtConv_TypIsUmsatzNachricht : IValueConverter
	{
		#region Overrides/Interfaces
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (BelegDataTypes) value;
			return val.IsZeitBon();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
		#endregion
	}
}