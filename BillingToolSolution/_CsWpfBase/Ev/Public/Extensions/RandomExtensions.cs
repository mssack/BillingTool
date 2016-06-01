// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Linq;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of random extensions.</summary>
	public static class RandomExtensions
	{
		/// <summary>Gets a random value for a primitive type, Strings will be created with length between 10-255 characters.</summary>
		public static object Next_By_Type(this Random rand, Type type)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);

			if (underlyingType != null && rand.NextBoolean())
				return null;

			type = underlyingType ?? type;

			if (type == typeof (bool))
				return rand.NextBoolean();
			if (type == typeof (char))
				return rand.NextChar();
			if (type == typeof (string))
				return rand.NextString(rand.Next(10, 256));
			if (type == typeof (sbyte))
				return rand.NextSByte();
			if (type == typeof (short))
				return rand.NextInt16();
			if (type == typeof (int))
				return rand.NextInt32();
			if (type == typeof (long))
				return rand.NextInt64();
			if (type == typeof (byte))
				return rand.NextByte();
			if (type == typeof (ushort))
				return rand.NextUInt16();
			if (type == typeof (uint))
				return rand.NextUInt32();
			if (type == typeof (ulong))
				return rand.NextUInt64();
			if (type == typeof (float))
				return rand.NextFloat();
			if (type == typeof (double))
				return rand.NextDouble();
			if (type == typeof (decimal))
				return rand.NextDecimal();


			throw new Exception("The type is no primitive.");
		}


		/// <summary>Generates a random alphanumerical string of a specific <paramref name="length" />.</summary>
		public static string NextString(this Random rand, int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length).Select(s => s[rand.Next(s.Length)]).ToArray());
		}

		/// <summary>Creates a new random <see cref="bool" />.</summary>
		public static bool NextBoolean(this Random rand)
		{
			return rand.Next(0, 2) == 1;
		}

		/// <summary>Creates a new random <see cref="char" />.</summary>
		public static char NextChar(this Random rand)
		{

			return (char) rand.Next(char.MinValue, '\u1000');
		}

		/// <summary>Creates a new random <see cref="byte" />.</summary>
		public static byte NextByte(this Random rand)
		{
			return rand.BufferOfSize(sizeof (byte))[0];
		}

		/// <summary>Creates a new random <see cref="sbyte" />.</summary>
		public static sbyte NextSByte(this Random rand)
		{
			return (sbyte) rand.BufferOfSize(sizeof (sbyte))[0];
		}

		/// <summary>Creates a new random <see cref="ushort" />.</summary>
		public static ushort NextUInt16(this Random rand)
		{
			return BitConverter.ToUInt16(rand.BufferOfSize(sizeof (ushort)), 0);
		}

		/// <summary>Creates a new random <see cref="uint" />.</summary>
		public static uint NextUInt32(this Random rand)
		{
			return BitConverter.ToUInt32(rand.BufferOfSize(sizeof (uint)), 0);
		}

		/// <summary>Creates a new random <see cref="ulong" />.</summary>
		public static ulong NextUInt64(this Random rand)
		{
			return BitConverter.ToUInt64(rand.BufferOfSize(sizeof (ulong)), 0);
		}

		/// <summary>Creates a new random <see cref="short" />.</summary>
		public static short NextInt16(this Random rand)
		{
			return BitConverter.ToInt16(rand.BufferOfSize(sizeof (short)), 0);
		}

		/// <summary>Creates a new random <see cref="int" />.</summary>
		public static int NextInt32(this Random rand)
		{
			return BitConverter.ToInt32(rand.BufferOfSize(sizeof (int)), 0);
		}

		/// <summary>Creates a new random <see cref="long" />.</summary>
		public static long NextInt64(this Random rand)
		{
			return BitConverter.ToInt64(rand.BufferOfSize(sizeof (long)), 0);
		}

		/// <summary>Creates a new random <see cref="float" />.</summary>
		public static float NextFloat(this Random rand)
		{
			return BitConverter.ToSingle(rand.BufferOfSize(sizeof (float)), 0);
		}

		/// <summary>Creates a new random <see cref="double" />.</summary>
		public static double NextDouble(this Random rand)
		{
			return BitConverter.ToDouble(rand.BufferOfSize(sizeof (double)), 0);
		}

		/// <summary>Creates a new random <see cref="decimal" />.</summary>
		public static decimal NextDecimal(this Random rand)
		{
			var s = GetDecimalScale(rand);
			var a = (int) (uint.MaxValue*rand.NextDouble());
			var b = (int) (uint.MaxValue*rand.NextDouble());
			var c = (int) (uint.MaxValue*rand.NextDouble());
			var n = rand.NextDouble() >= 0.5;
			return new decimal(a, b, c, n, s);
		}

		/// <summary>Creates a new random buffer of a specific <paramref name="size" />.</summary>
		public static byte[] BufferOfSize(this Random rand, int size)
		{
			var buf = new byte[8];
			rand.NextBytes(buf);
			return buf;
		}

		private static byte GetDecimalScale(Random r)
		{
			for (byte i = 0; i <= 28; i++)
			{
				if (r.NextDouble() >= 0.1)
					return i;
			}
			return 0;
		}
	}
}