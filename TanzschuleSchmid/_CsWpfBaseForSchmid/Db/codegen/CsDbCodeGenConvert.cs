// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Collections.Generic;
using System.Data;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen
{
	/// <summary>For further infos see <see cref="CsDb" />.</summary>
	[Serializable]
	public sealed class CsDbCodeGenConvert : Base
	{
		/// <summary>SqlDbType to c# type mapping</summary>
		private static readonly Dictionary<SqlDbType, Type> SqlDbTypeMappings = new Dictionary<SqlDbType, Type>
		{
			{SqlDbType.Bit, typeof (bool)},
			{SqlDbType.VarBinary, typeof (byte[])},
			{SqlDbType.Image, typeof (byte[])},
			{SqlDbType.Binary, typeof (byte[])},
			{SqlDbType.Variant, typeof (object)},
			{SqlDbType.UniqueIdentifier, typeof (Guid)},
			{SqlDbType.Text, typeof (string)},
			{SqlDbType.Xml, typeof (string)},
			{SqlDbType.VarChar, typeof (string)},
			{SqlDbType.NChar, typeof (string)},
			{SqlDbType.NVarChar, typeof (string)},
			{SqlDbType.NText, typeof (string)},
			{SqlDbType.Char, typeof (string)},
			{SqlDbType.Date, typeof (DateTime)},
			{SqlDbType.DateTime, typeof (DateTime)},
			{SqlDbType.DateTime2, typeof (DateTime)},
			{SqlDbType.DateTimeOffset, typeof (DateTimeOffset)},
			{SqlDbType.Time, typeof (TimeSpan)},
			{SqlDbType.Timestamp, typeof (byte[])},
			{SqlDbType.Real, typeof (float)},
			{SqlDbType.TinyInt, typeof (byte)},
			{SqlDbType.SmallInt, typeof (short)},
			{SqlDbType.Int, typeof (int)},
			{SqlDbType.BigInt, typeof (long)},
			{SqlDbType.Float, typeof (double)},
			{SqlDbType.SmallMoney, typeof (decimal)},
			{SqlDbType.Money, typeof (decimal)},
			{SqlDbType.Decimal, typeof (decimal)},
		};

		private static CsDbCodeGenConvert _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsDbCodeGenConvert I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbCodeGenConvert());
				}
			}
		}


		private CsDbCodeGenConvert()
		{
		}

		/// <summary>Converts the <see cref="SqlDbType" /> into the matching <see cref="Type" />.</summary>
		public Type ToType(SqlDbType sqltype)
		{
			return SqlDbTypeMappings[sqltype];
		}

		/// <summary>Converts a db name to a valid c# name</summary>
		public string ToMemberName(string native, bool removeLeadingPlural)
		{
			native = char.ToUpper(native[0]) + native.Substring(1);


			while (native.Contains("_"))
			{
				var indexOf = native.IndexOf("_", StringComparison.Ordinal);
				if (indexOf == native.Length - 1)
					native = native.Substring(0, native.Length - 1);
				else
					native = native.Substring(0, indexOf) + char.ToUpper(native[indexOf + 1]) + native.Substring(indexOf + 2);
			}
			while (native.Contains("."))
			{
				var indexOf = native.IndexOf(".", StringComparison.Ordinal);
				native = native.Substring(0, indexOf) + char.ToUpper(native[indexOf + 1]) + native.Substring(indexOf + 2);
			}
			while (native.Contains(" "))
			{
				var indexOf = native.IndexOf(" ", StringComparison.Ordinal);
				native = native.Substring(0, indexOf) + char.ToUpper(native[indexOf + 1]) + native.Substring(indexOf + 2);
			}
			native = native.Replace("ID", "Id").Replace("iD", "Id");

			if (removeLeadingPlural && native[native.Length - 1] == 's')
				return native.Substring(0, native.Length - 1);

			return native;
		}
	}
}