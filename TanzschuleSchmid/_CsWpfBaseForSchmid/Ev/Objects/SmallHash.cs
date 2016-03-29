// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Text;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Ev.Objects
{
	/// <summary>Provides a class for hashing to short strings.</summary>
	public static class SmallHash
	{
		/// <summary>hashing from buffer. Result in format of 000-000</summary>
		public static string FromBuffer(byte[] buffer)
		{
			if (buffer == null || buffer.Length == 0)
				return "000-000";
			uint unSum1 = 0x0001 & 0xFFFF;
			uint unSum2 = (0x0001 >> 16) & 0xFFFF;
			for (var i = 0; i < buffer.Length; i++)
			{
				unSum1 = (unSum1 + buffer[i])%0xFFF1;
				unSum2 = (unSum1 + unSum2)%0xFFF1;
			}
			var hashFromBuffer = (unSum2 << 16) + unSum1;
			var rv = hashFromBuffer.ToString("000000");


			if (rv.Length > 7)
			{
				rv = rv.Substring(0, 8);
			}


			return rv.Substring(0, 3) + "-" + rv.Substring(3, 3);
		}

		/// <summary>hashing from string. Result in format of 000-000</summary>
		public static string FromString(string input)
		{
			if (input.IsNullOrEmpty())
				return FromBuffer(null);


			var utf32 = new UTF32Encoding();
			return FromBuffer(utf32.GetBytes(input));
		}
	}
}