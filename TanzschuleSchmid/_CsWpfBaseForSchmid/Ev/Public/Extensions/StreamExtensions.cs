// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-16</date>

using System;
using System.IO;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Collapses a lot of stream extensions.</summary>
	public static class StreamExtensions
	{
		/// <summary>Reads the stream completely into a binary array. It does that by re validating if everything has arrived.</summary>
		public static byte[] ToByteArray(this Stream sr, int length)
		{
			var data = new byte[length];
			var pos = 0;
			while (pos < data.Length)
			{
				var bytesRead = sr.Read(data, pos, data.Length - pos);
				if (bytesRead == 0)
					throw new IOException("Premature end of data");// End of data and we didn't finish reading. Oops.
				pos += bytesRead;
			}
			return data;
		}
		/// <summary>Reads the stream completely into a binary array. It does that by re validating if everything has arrived.</summary>
		public static byte[] ToByteArray(this Stream sr, long length)
		{
			var data = new byte[length];
			var pos = 0;
			while (pos < data.Length)
			{
				var bytesRead = sr.Read(data, pos, data.Length - pos);
				if (bytesRead == 0)
					throw new IOException("Premature end of data");// End of data and we didn't finish reading. Oops.
				pos += bytesRead;
			}
			return data;
		}
	}
}