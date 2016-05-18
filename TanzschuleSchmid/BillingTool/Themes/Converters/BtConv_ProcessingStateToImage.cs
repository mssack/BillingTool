// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-05</date>

using System;
using System.Globalization;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Global;






namespace BillingTool.Themes.Converters
{
#pragma warning disable 1591
	// ReSharper disable InconsistentNaming
	public class BtConv_ProcessingStateToImage : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (ProcessingStates)value;
			if (val == ProcessingStates.NotProcessed)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-State-NotStarted"];
			if (val == ProcessingStates.Processing)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-State-Loading"];
			if (val == ProcessingStates.Processed)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-State-Success"];
			if (val == ProcessingStates.Failed)
				return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-State-Faulted"];

			return CsGlobal.Storage.Resource.Dictionary.Standard["GIco-State-NotStarted"];
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}