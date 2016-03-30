// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Linq;
using System.Text;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps string builder functions.</summary>
	public static class StringBuilderExtensions
	{
		/// <summary>Takes a sub string out of the <see cref="StringBuilder" />.</summary>
		public static string Substring(this StringBuilder sb, int startIndex, int length)
		{
			return sb.ToString(startIndex, length);
		}

		/// <summary>Removes a char from the <see cref="StringBuilder" />
		/// </summary>
		public static StringBuilder Remove(this StringBuilder sb, char ch)
		{
			for (int i = 0; i < sb.Length;)
			{
				if (sb[i] == ch)
					sb.Remove(i, 1);
				else
					i++;
			}
			return sb;
		}

		/// <summary>Removes characters from end of the <see cref="StringBuilder" />;</summary>
		public static StringBuilder RemoveFromEnd(this StringBuilder sb, int num)
		{
			return sb.Remove(sb.Length - num, num);
		}

		/// <summary>Clears the string builder.</summary>
		public static void Clear(this StringBuilder sb)
		{
			sb.Clear();
		}

		/// <summary>Trim left spaces of string</summary>
		public static StringBuilder LTrim(this StringBuilder sb)
		{
			if (sb.Length != 0)
			{
				int length = 0;
				int num2 = sb.Length;
				while ((sb[length] == ' ') && (length < num2))
				{
					length++;
				}
				if (length > 0)
				{
					sb.Remove(0, length);
				}
			}
			return sb;
		}

		/// <summary>Trim right spaces of string</summary>
		/// <param name="sb"></param>
		/// <returns></returns>
		public static StringBuilder RTrim(this StringBuilder sb)
		{
			if (sb.Length != 0)
			{
				int length = sb.Length;
				int num2 = length - 1;
				while ((sb[num2] == ' ') && (num2 > -1))
				{
					num2--;
				}
				if (num2 < (length - 1))
				{
					sb.Remove(num2 + 1, (length - num2) - 1);
				}
			}
			return sb;
		}

		/// <summary>Trim spaces around string</summary>
		/// <param name="sb"></param>
		/// <returns></returns>
		public static StringBuilder Trim(this StringBuilder sb)
		{
			if (sb.Length != 0)
			{
				int length = 0;
				int num2 = sb.Length;
				while ((sb[length] == ' ') && (length < num2))
				{
					length++;
				}
				if (length > 0)
				{
					sb.Remove(0, length);
					num2 = sb.Length;
				}
				length = num2 - 1;
				while ((sb[length] == ' ') && (length > -1))
				{
					length--;
				}
				if (length < (num2 - 1))
				{
					sb.Remove(length + 1, (num2 - length) - 1);
				}
			}
			return sb;
		}

		/// <summary>Get index of a char</summary>
		public static int IndexOf(this StringBuilder sb, char value)
		{
			return IndexOf(sb, value, 0);
		}

		/// <summary>Get index of a char starting from a given index</summary>
		public static int IndexOf(this StringBuilder sb, char value, int startIndex)
		{
			for (int i = startIndex; i < sb.Length; i++)
			{
				if (sb[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Get index of a string</summary>
		public static int IndexOf(this StringBuilder sb, string value)
		{
			return IndexOf(sb, value, 0, false);
		}

		/// <summary>Get index of a string from a given index</summary>
		public static int IndexOf(this StringBuilder sb, string value, int startIndex)
		{
			return IndexOf(sb, value, startIndex, false);
		}

		/// <summary>Get index of a string with case option</summary>
		public static int IndexOf(this StringBuilder sb, string value, bool ignoreCase)
		{
			return IndexOf(sb, value, 0, ignoreCase);
		}

		/// <summary>Get index of a string from a given index with case option</summary>
		public static int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase)
		{
			int num3;
			int length = value.Length;
			int num2 = (sb.Length - length) + 1;
			if (ignoreCase == false)
			{
				for (int i = startIndex; i < num2; i++)
				{
					if (sb[i] == value[0])
					{
						num3 = 1;
						while ((num3 < length) && (sb[i + num3] == value[num3]))
						{
							num3++;
						}
						if (num3 == length)
						{
							return i;
						}
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num2; j++)
				{
					if (char.ToLower(sb[j]) == char.ToLower(value[0]))
					{
						num3 = 1;
						while ((num3 < length) && (char.ToLower(sb[j + num3]) == char.ToLower(value[num3])))
						{
							num3++;
						}
						if (num3 == length)
						{
							return j;
						}
					}
				}
			}
			return -1;
		}
		/// <summary>
		///     Gets the last index of string. Beginning at the <paramref name="startIndex" /> and searching backwards to the
		///     beginning of the string
		/// </summary>
		public static int LastIndexOf(this StringBuilder sb, string value, int startIndex = -1)
		{
			int num3;
			int length = value.Length;
			char last = value.Last();
			if (startIndex == -1)
				startIndex = sb.Length - 1;
			for (int i = startIndex; i >= length - 1; i--)
			{
				if (sb[i] == last)
				{
					num3 = 1;
					while ((length != num3) && (sb[i - num3] == value[length - 1 - num3]))
					{
						num3++;
					}
					if (num3 == length)
					{
						return i - (num3 - 1);
					}
				}
			}
			return -1;
		}

		/// <summary>Determine whether a string starts with a given text</summary>
		public static bool StartsWith(this StringBuilder sb, string value)
		{
			return StartsWith(sb, value, 0, false);
		}

		/// <summary>Determine whether a string starts with a given text (with case option)</summary>
		public static bool StartsWith(this StringBuilder sb, string value, bool ignoreCase)
		{
			return StartsWith(sb, value, 0, ignoreCase);
		}

		/// <summary>Determine whether a string is begin with a given text</summary>
		public static bool StartsWith(this StringBuilder sb, string value, int startIndex, bool ignoreCase)
		{
			int length = value.Length;
			int num2 = startIndex + length;
			if (ignoreCase == false)
			{
				for (int i = startIndex; i < num2; i++)
				{
					if (sb[i] != value[i - startIndex])
					{
						return false;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num2; j++)
				{
					if (char.ToLower(sb[j]) != char.ToLower(value[j - startIndex]))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}