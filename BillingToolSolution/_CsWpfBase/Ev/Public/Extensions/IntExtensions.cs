// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Collapsing extension methods for <see cref="int" />
	/// </summary>
	public static class IntExtensions
	{
		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this UInt64? value)
		{
			if (value == null)
				return null;
			return ToByteSizeString(value.Value);
		}
		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this UInt64 value)
		{
			if (value < 1024)
				return value + " B";

			var val = value/1024.0;

			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " KB";
			val = val/1024.0;
			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " MB";
			val = val/1024.0;
			if (val < 1024)
				return Math.Round(val, 1).ToString("0.0") + " GB";


			val = val/1024.0;
			return Math.Round(val, 3).ToString("0.000") + " TB";
		}
		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this UInt32? value)
		{
			if (value == null)
				return null;
			return ToByteSizeString(value.Value);
		}
		/// <summary>Format <paramref name="value" /> into a human readable string Like: <c>150.0 KB</c> or <c>1.2 GB</c>.</summary>
		public static string ToByteSizeString(this UInt32 value)
		{
			return ToByteSizeString((ulong)value);
		}
	}
}