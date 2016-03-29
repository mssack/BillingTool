// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Data;





namespace CsWpfBase.Themes.Resources.Converters
{
	[DebuggerStepThrough]
	internal static class ConverterAddins
	{
		public static void ThrowInvalidDataException(this IValueConverter owner, object val, [CallerMemberName] string methodName = null)
		{
			throw new InvalidOperationException("Der Converter: <" + owner.GetType().Name + "> erhielt einen unbekannten Typen(" + val.GetType().Name + ")!");
		}
		public static void ThrowOneWayException(this IValueConverter owner, [CallerMemberName] string methodName = null)
		{
			throw new InvalidOperationException("Der Converter: <" + owner.GetType().Name + "> ist ein OneWayConverter. Aufruf der Methode(" + methodName + ") ist ungültig!");
		}
		public static void ThrowOneWayException(this IMultiValueConverter owner, [CallerMemberName] string methodName = null)
		{
			throw new InvalidOperationException("Der Converter: <" + owner.GetType().Name + "> ist ein OneWayConverter. Aufruf der Methode(" + methodName + ") ist ungültig!");
		}
	}
}