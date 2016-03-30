// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows.Media;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Bunch of data extension methods.</summary>
	public static class ColorExtensions
	{
		/// <summary>Returns a DataTables content for debug purpose. </summary>
		public static string GetHexRepresentation(this Color color, bool includeAlpha = true)
		{
			return "#" + (includeAlpha ? color.A.ToString("X2") : "") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}
	}
}