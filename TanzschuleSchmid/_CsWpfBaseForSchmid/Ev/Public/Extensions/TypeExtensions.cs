// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Collections.Generic;
using System.Reflection;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps type methods.</summary>
	public static class TypeExtensions
	{

		private static readonly HashSet<Type> Numerics = new HashSet<Type>(new[] {typeof (byte),typeof (sbyte), typeof (ushort), typeof (uint), typeof (ulong), typeof (short), typeof (int), typeof (long), typeof (double), typeof (decimal), typeof (float),});

		/// <summary>Checks if type is one of the following types:
		///     <para><c>byte, ushort, uint, ulong, short, int, long, double, decimal, float,</c></para>
		/// </summary>
		public static bool IsNumericType(this Type type)
		{
			return Numerics.Contains(type);
		}

		/// <summary>Checks if the type is a null able.</summary>
		public static bool IsNullable(this Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		/// <summary>Checks if the type is a null able and if it is it gives back the underlaying type.</summary>
		public static bool IsNullable(this Type type, out Type underlayingType)
		{
			var innerType = Nullable.GetUnderlyingType(type);
			underlayingType = innerType ?? type;
			return innerType != null;
		}

		/// <summary>Checks if the type is a primitive including special checks. See params for more informations.</summary>
		/// <param name="type"></param>
		/// <param name="includeString">Includes strings into the list of valid primitives.</param>
		/// <param name="includeDecimal">Includes decimals into the list of valid primitives.</param>
		/// <param name="includeNullable">Includes null ables into the list of valid primitives.</param>
		public static bool IsSpecialPrimitive(this Type type, bool includeString = true, bool includeDecimal = true, bool includeNullable = true)
		{
			if (type.IsPrimitive)
				return true;
			if (includeString && type == typeof (string))
				return true;
			if (includeDecimal && type == typeof (decimal))
				return true;
			if (includeNullable)
			{
				var underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
					return underlyingType.IsPrimitive;
			}

			return false;
		}


		/// <summary>Converts a numeric type into its unsigned equivalent.</summary>
		public static Type ToUnsigned(this Type type)
		{
			if (type == typeof (sbyte))
				return typeof (byte);
			if (type == typeof (short))
				return typeof (ushort);
			if (type == typeof (int))
				return typeof (uint);
			if (type == typeof (long))
				return typeof (ulong);
			throw new Exception("Invalid Type requested");
		}

		/// <summary>Finds all fields including the private fields on the base classes.</summary>
		public static FieldInfo[] GetFields_IncludingBaseClasses(this Type type, BindingFlags flags)
		{
			var fi = new List<FieldInfo>();
			while (type != null)
			{
				fi.AddRange(type.GetFields(flags | BindingFlags.DeclaredOnly));
				type = type.BaseType;
			}
			return fi.ToArray();
		}
	}
}