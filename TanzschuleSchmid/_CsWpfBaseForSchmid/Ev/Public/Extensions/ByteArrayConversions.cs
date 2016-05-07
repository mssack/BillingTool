// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-22</date>

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of functions for converting byte[]'s.</summary>
	public static class ByteArrayConversions
	{
		/// <summary>Converts an byte[] to an Base64 string</summary>
		public static string ConvertTo_Base64(this byte[] input, Base64FormattingOptions options = Base64FormattingOptions.InsertLineBreaks)
		{
			if (input == null)
				return null;
			return Convert.ToBase64String(input, options);
		}

		/// <summary>Converts an byte[] to an hex string. For each byte there will be two characters in hex.</summary>
		public static string ConvertTo_Hex(this byte[] input, string delimiter = "")
		{
			if (input == null)
				return null;
			return BitConverter.ToString(input).Replace("-", delimiter);
		}

		/// <summary>Converts an Base64 string to an byte[].</summary>
		public static byte[] ConvertTo_Bytes(this string input)
		{
			if (input == null)
				return null;
			return Convert.FromBase64String(input);
		}

		/// <summary>Converts an <see cref="SerializableAttribute"/> object into an byte[] by using the <see cref="BinaryFormatter"/>.</summary>
		public static byte[] ConvertTo_Bytes(this object input)
		{

			if (input == null)
				throw new ArgumentException("The object is null.", nameof(input));
			if (input is byte[])
				throw new ArgumentException($"The object can not be of type byte[].");

			var binaryFormatter = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				binaryFormatter.Serialize(ms, input);
				return ms.ToArray();
			}
		}
		/// <summary>Converts an <see cref="SerializableAttribute"/> object into an byte[] by using the <see cref="BinaryFormatter"/>.</summary>
		public static object ConvertTo_Object(this byte[] input)
		{
			if (input == null)
				throw new ArgumentException("The input is null.", nameof(input));

			var binaryFormatter = new BinaryFormatter();
			using (var ms = new MemoryStream(input))
			{
				return binaryFormatter.Deserialize(ms);
			}
		}
		/// <summary>Converts an <see cref="SerializableAttribute"/> object into an byte[] by using the <see cref="BinaryFormatter"/>.</summary>
		public static T ConvertTo_Object<T>(this byte[] input)
		{
			return (T)input.ConvertTo_Object();
		}

		/// <summary>
		///     SHA-1 produces a 160-bit (20-byte) hash value known as a message digest. A SHA-1 hash value is typically rendered as a hexadecimal number, 40
		///     digits long.
		/// </summary>
		public static byte[] Sha1Hash(this byte[] input)
		{
			if (input == null)
				return null;

			using (var sha1 = new SHA1Managed())
			{
				return sha1.ComputeHash(input);
			}
		}

		/// <summary>
		///     MD5 produces a 128-bit (16-byte) hash value known as a message-digest. The Algorithm is a widely used cryptographic hash function producing a
		///     hash value, typically expressed in text format as a 32 digit hexadecimal number. MD5 has been utilized in a wide variety of cryptographic
		///     applications, and is also commonly used to verify data integrity.
		/// </summary>
		public static byte[] Md5Hash(this byte[] input)
		{
			if (input == null)
				return null;

			using (var md5 = new MD5CryptoServiceProvider())
			{
				return md5.ComputeHash(input);
			}
		}
	}
}