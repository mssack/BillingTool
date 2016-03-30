// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;





namespace CsWpfBase.Themes.Controls._shared
{
	internal static class Extensions
	{
		public static TType GetPart<TType>(this ControlTemplate template, string name, FrameworkElement templatedParant) where TType : class
		{
			object o = template.FindName(name, templatedParant);
			if (o == null)
				throw new InvalidOperationException("The '" + name + "' has to be included in the template for the '" + templatedParant.GetType().Name + "' control.");
			if ((o as TType) == null)
				throw new InvalidOperationException("The '" + name + "' must be of type '" + typeof (TType).Name + "'");
			return o as TType;
		}
	}
}