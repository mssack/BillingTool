// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Globalization;
using System.Windows.Markup;





namespace CsWpfBase.Ev.Public.Markups
{
#pragma warning disable 1591
	public class XmlLanguage : MarkupExtension
	{
		private readonly System.Windows.Markup.XmlLanguage _language;
		public XmlLanguage(CultureInfo culture)
		{
			_language = System.Windows.Markup.XmlLanguage.GetLanguage(culture.IetfLanguageTag);
		}


		#region Overrides
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return _language;
		}
		#endregion
	}
}