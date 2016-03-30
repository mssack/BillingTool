// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Utilitys
{
#pragma warning disable 1591
	/// <summary>The Number is used for an dynamic calculation of different number types.</summary>
	public class DynamicNumber
	{
		private Type _type;
		private object _value;
		/// <summary>Init the number with an value.</summary>
		public DynamicNumber(Type type)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);
			_type = underlyingType ?? type;
		}

		/// <summary>Gets the value</summary>
		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}
		/// <summary>Gets the type of the value</summary>
		public Type Type => _type ?? (_type = _value.GetType());


		public bool IsByte => Type == typeof (byte);

		public bool IsInt16 => Type == typeof (short);

		public bool IsInt32 => Type == typeof (int);

		public bool IsInt64 => Type == typeof (long);

		public bool IsUInt16 => Type == typeof (ushort);

		public bool IsUInt32 => Type == typeof (uint);

		public bool IsUInt64 => Type == typeof (ulong);

		public bool IsDouble => Type == typeof (double);

		public bool IsDecimal => Type == typeof (decimal);

		public byte Byte => Cast<byte>();

		public short Int16 => Cast<short>();

		public int Int32 => Cast<int>();

		public long Int64 => Cast<long>();

		public ushort UInt16 => Cast<ushort>();

		public uint UInt32 => Cast<uint>();

		public ulong UInt64 => Cast<ulong>();

		public double Double => Cast<double>();

		public decimal Decimal => Cast<decimal>();

		private T Cast<T>()
		{
			if (Value == null)
				return default(T);
			return (T) Value;
		}
		private static T Convert<T>(object o)
		{
			if (o == null)
				return default(T);
			if (o.GetType() != typeof (T))
				return (T) System.Convert.ChangeType(o, typeof (T));
			return (T) o;
		}

		/// <summary>
		/// Adds an value of type object from the dynamic number. This object can be of any numeric type or a <see cref="DynamicNumber"/>.
		/// </summary>
		public static object operator +(DynamicNumber n1, object value)
		{
			if (n1.IsByte)
			{
				var val0 = n1.Byte;
				var val1 = Convert<byte>(value);
				if (val0/2.0 + val1/2.0 > byte.MaxValue/2.0)
					return byte.MaxValue;
				return (byte) (val0 + val1);
			}
			if (n1.IsUInt16)
			{
				var val0 = n1.UInt16;
				var val1 = Convert<ushort>(value);
				if (val0/2.0 + val1/2.0 > ushort.MaxValue/2.0)
					return ushort.MaxValue;
				return (ushort) (val0 + val1);
			}
			if (n1.IsUInt32)
			{
				var val0 = n1.UInt32;
				var val1 = Convert<uint>(value);
				if (val0/2.0 + val1/2.0 > uint.MaxValue/2.0)
					return uint.MaxValue;
				return val0 + val1;
			}
			if (n1.IsUInt64)
			{
				var val0 = n1.UInt64;
				var val1 = Convert<ulong>(value);
				if (val0/(decimal) 2.0 + val1/(decimal) 2.0 >= ulong.MaxValue/(decimal) 2.0)
					return ulong.MaxValue;
				var opAddition = val0 + val1;
				return opAddition;
			}
			if (n1.IsInt16)
			{
				var val0 = n1.Int16;
				var val1 = Convert<short>(value);
				if (Math.Abs(val0/2.0 + val1/2.0) > short.MaxValue/2.0)
					return val0 < 0 ? short.MinValue : short.MaxValue;
				return (short) (val0 + val1);
			}
			if (n1.IsInt32)
			{
				var val0 = n1.Int32;
				var val1 = Convert<int>(value);
				if (Math.Abs(val0/2.0 + val1/2.0) > int.MaxValue/2.0)
					return val0 < 0 ? int.MinValue : int.MaxValue;
				return val0 + val1;
			}
			if (n1.IsInt64)
			{
				var val0 = n1.Int64;
				var val1 = Convert<long>(value);
				if (Math.Abs(val0/(decimal) 2.0 + val1/(decimal) 2.0) > long.MaxValue/(decimal) 2.0)
					return val0 < 0 ? long.MinValue : long.MaxValue;
				return val0 + val1;
			}
			if (n1.IsDouble)
			{
				var val0 = n1.Double;
				var val1 = Convert<double>(value);
				if (Math.Abs(val0/2.0 + val1/2.0) > double.MaxValue/2.0)
					return val0 < 0 ? double.MinValue : double.MaxValue;
				return val0 + val1;
			}
			if (n1.IsDecimal)
			{
				var val0 = n1.Decimal;
				var val1 = Convert<decimal>(value);
				var halfMax = decimal.MaxValue/(decimal) 2.0;
				var halfres = Math.Abs(val0/(decimal) 2.0 + val1/(decimal) 2.0);
				if (halfres >= halfMax)
					return val0 < 0 ? decimal.MinValue : decimal.MaxValue;
				return val0 + val1;
			}
			return 1;
		}

		/// <summary>
		/// Subtracts an value of type object from the dynamic number. This object can be of any numeric type or a <see cref="DynamicNumber"/>.
		/// </summary>
		public static object operator -(DynamicNumber n1, object value)
		{
			if (n1.IsByte)
			{
				var val0 = n1.Byte;
				var val1 = Convert<byte>(value);

				if (val1 >= val0)
					return byte.MinValue;
				return (byte) (val0 - val1);
			}
			if (n1.IsUInt16)
			{
				var val0 = n1.UInt16;
				var val1 = Convert<UInt16>(value);

				if (val1 >= val0)
					return UInt16.MinValue;
				return (UInt16) (val0 - val1);
			}
			if (n1.IsUInt32)
			{
				var val0 = n1.UInt32;
				var val1 = Convert<UInt32>(value);

				if (val1 >= val0)
					return UInt32.MinValue;
				return val0 - val1;
			}
			if (n1.IsUInt64)
			{
				var val0 = n1.UInt64;
				var val1 = Convert<UInt64>(value);

				if (val1 >= val0)
					return UInt64.MinValue;
				return val0 - val1;
			}
			if (n1.IsInt16)
			{
				var val0 = n1.Int16;
				var val1 = Convert<Int16>(value);

				if (val0 < 0 && val1 > 0 && Math.Abs(Int16.MinValue - val0) <= val1)
					return Int16.MinValue;
				return (Int16) (val0 - val1);
			}
			if (n1.IsInt32)
			{
				var val0 = n1.Int32;
				var val1 = Convert<Int32>(value);

				if (val0 < 0 && val1 > 0 && Math.Abs(Int32.MinValue - val0) <= val1)
					return Int32.MinValue;
				return val0 - val1;
			}
			if (n1.IsInt64)
			{
				var val0 = n1.Int64;
				var val1 = Convert<Int64>(value);

				if (val0 < 0 && val1 > 0 && Math.Abs(Int64.MinValue - val0) <= val1)
					return Int64.MinValue;
				return val0 - val1;
			}
			if (n1.IsDouble)
			{
				var val0 = n1.Double;
				var val1 = Convert<Double>(value);

				if (val0 < 0 && val1 > 0 && Math.Abs(Double.MinValue - val0) <= val1)
					return Double.MinValue;
				return val0 - val1;
			}
			if (n1.IsDecimal)
			{
				var val0 = n1.Decimal;
				var val1 = Convert<Decimal>(value);

				if (val0 < 0 && val1 > 0 && Math.Abs(Decimal.MinValue - val0) <= val1)
					return Decimal.MinValue;
				return val0 - val1;
			}
			return null;
		}

		/// <summary>
		/// Compares the dynamic number with an object. This object can be of any numeric type or a <see cref="DynamicNumber"/>.
		/// </summary>
		public static bool operator <(DynamicNumber n1, object value)
		{
			if (n1.IsByte)
				return n1.Byte < Convert<byte>(value);
			if (n1.IsInt16)
				return n1.Int16 < Convert<short>(value);
			if (n1.IsInt32)
				return n1.Int32 < Convert<int>(value);
			if (n1.IsInt64)
				return n1.Int64 < Convert<long>(value);
			if (n1.IsUInt16)
				return n1.UInt16 < Convert<ushort>(value);
			if (n1.IsUInt32)
				return n1.UInt32 < Convert<uint>(value);
			if (n1.IsUInt64)
				return n1.UInt64 < Convert<ulong>(value);
			if (n1.IsDouble)
				return n1.Double < Convert<double>(value);
			if (n1.IsDecimal)
				return n1.Decimal < Convert<decimal>(value);

			throw new InvalidOperationException();
		}
		/// <summary>
		/// Compares the dynamic number with an object. This object can be of any numeric type or a <see cref="DynamicNumber"/>.
		/// </summary>
		public static bool operator >(DynamicNumber n1, object value)
		{
			if (n1.IsByte)
				return n1.Byte > Convert<byte>(value);
			if (n1.IsInt16)
				return n1.Int16 > Convert<short>(value);
			if (n1.IsInt32)
				return n1.Int32 > Convert<int>(value);
			if (n1.IsInt64)
				return n1.Int64 > Convert<long>(value);
			if (n1.IsUInt16)
				return n1.UInt16 > Convert<ushort>(value);
			if (n1.IsUInt32)
				return n1.UInt32 > Convert<uint>(value);
			if (n1.IsUInt64)
				return n1.UInt64 > Convert<ulong>(value);
			if (n1.IsDouble)
				return n1.Double > Convert<double>(value);
			if (n1.IsDecimal)
				return n1.Decimal > Convert<decimal>(value);

			throw new InvalidOperationException();
		}

		/// <summary>
		/// Multiplies with -1 or throws an exception if the number format does not allow negativ numbers.
		/// </summary>
		public object Invert()
		{
			if (IsByte || IsUInt16 || IsUInt32 || IsUInt64)
				return Value;

			if (IsInt16)
				return Cast<short>()*-1;
			if (IsInt32)
				return Cast<int>()*-1;
			if (IsInt64)
				return Cast<long>()*-1;
			if (IsDouble)
				return Cast<double>()*-1;
			if (IsDecimal)
				return Cast<decimal>()*-1;

			throw new Exception();
		}
	}
}