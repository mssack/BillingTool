// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of Extension methods for <see cref="string" />.</summary>
	[DebuggerStepThrough]
	public static class StringHandlingExtensions
	{
		/// <summary>Extension method for <see cref="string.IsNullOrEmpty" />.</summary>
		public static bool IsNullOrEmpty(this string input)
		{
			return string.IsNullOrEmpty(input);
		}

		/// <summary>Converts the string to another string with the first char ToLower</summary>
		/// <example>NameOf => nameOf</example>
		public static string ToLowerName(this string input)
		{
			return char.ToLower(input[0]) + input.Substring(1);
		}

		/// <summary>
		///     Expands a string to a specific length by adding a text (<paramref name="expansiontext" />) to the beginning of the input or the end. If
		///     <paramref name="input" /> already exceeds or is equal to <paramref name="length" /> the input is returned.
		/// </summary>
		/// <param name="input">the text to be expanded</param>
		/// <param name="length">the destination length</param>
		/// <param name="expansiontext">the text which is used to expand the input.</param>
		/// <param name="insertBefore">Defines if expansion text is added at position 0</param>
		public static string Expand(this string input, int length = 40, string expansiontext = " ", bool insertBefore = false)
		{
			if (input.Length > length)
				return input;


			var charsToAdd = length - input.Length;
			var sb = new StringBuilder("", length);
			while (charsToAdd != 0)
			{
				if (charsToAdd < expansiontext.Length)
				{
					sb.Append(expansiontext.Substring(0, charsToAdd));
					charsToAdd = charsToAdd - charsToAdd;
				}
				else
				{
					sb.Append(expansiontext);
					charsToAdd = charsToAdd - expansiontext.Length;
				}
			}
			if (insertBefore)
				sb.Append(input);
			else
				sb.Insert(0, input);

			return sb.ToString();
		}

		/// <summary>
		///     Cuts a string to a specific length by removing characters from the end of a string. If input length is already appropriate the method returns the
		///     input.
		/// </summary>
		/// <param name="input">the text to be shortened</param>
		/// <param name="length">the destination length</param>
		/// <param name="appendix">the last characters of shortened string will be replaced with this sequence</param>
		public static string Cut(this string input, int length = 100, string appendix = "...")
		{
			if (string.IsNullOrEmpty(input))
				return input;
			if (input.Length < length)
				return input;
			if (appendix.Length > length)
				throw new ArgumentException(@"appendix length is bigger then destination length", "appendix");




			input = input.Substring(0, length - appendix.Length) + appendix;

			return input;
		}

		/// <summary>
		///     Cuts a string to a specific length by removing characters from the middle of a string. If input length is already appropriate the method returns
		///     the input.
		/// </summary>
		/// <param name="input">the string to be shortened.</param>
		/// <param name="length">the destination length</param>
		/// <param name="insert">the character to be inserted in the middle</param>
		public static string CutMiddle(this string input, int length = 100, string insert = "~")
		{
			if (string.IsNullOrEmpty(input))
				return input;
			if (input.Length < length)
				return input;
			if (insert.Length > length)
				throw new ArgumentException(@"insert length is bigger then destination length", "insert");



			var centerInput = input.Length/2;
			var toRemove = (input.Length - length) + insert.Length;
			var toRemoveHalf = toRemove/2;
			var insertPoint = centerInput - toRemoveHalf;
			input = input.Remove(insertPoint, toRemove).Insert(insertPoint, insert);



			return input;
		}

		/// <summary>Extension method for <see cref="Regex.Split(string, string)" />.</summary>
		public static string[] Split(this string input, string pattern)
		{
			return Regex.Split(input, pattern);
		}

		/// <summary>Calculates the distance to the next string</summary>
		public static int LevenstheinDistance(this string input, string to)
		{
			var n = input.Length;
			var m = to.Length;
			var d = new int[n + 1, m + 1];

			if (n == 0)
			{
				return m;
			}

			if (m == 0)
			{
				return n;
			}

			for (var i = 0; i <= n; d[i, 0] = i++)
			{
			}

			for (var j = 0; j <= m; d[0, j] = j++)
			{
			}

			for (var i = 1; i <= n; i++)
			{
				for (var j = 1; j <= m; j++)
				{
					var cost = (to[j - 1] == input[i - 1]) ? 0 : 1;

					d[i, j] = Math.Min(
						Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
						d[i - 1, j - 1] + cost);
				}
			}
			return d[n, m];
		}

		/// <summary>
		///     Calculates a hash from the <paramref name="input" />. Result in format of 000-000. CAVE: This is not a save hashing algorithm hash collisions
		///     should be considered as normal.
		/// </summary>
		public static string SmallHash(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;
			return Objects.SmallHash.FromString(input);
		}

		/// <summary>Calculates a hash from the <paramref name="input" />. Hex representation.</summary>
		public static string Sha1Hash(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;
			return Encoding.UTF8.GetBytes(input).Sha1Hash().ConvertTo_Hex();
		}

		/// <summary>Calculates a hash from the <paramref name="input" />. Hex representation.</summary>
		public static string Md5Hash(this string input)
		{
			if (string.IsNullOrEmpty(input))
				return string.Empty;

			return Encoding.UTF8.GetBytes(input).Md5Hash().ConvertTo_Hex();
		}
	}
}