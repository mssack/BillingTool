// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.attributes.columnAttributes
{
	/// <summary>A db column attribute for the CsDb system. Holds all the .Net values of the column.</summary>
	public class CsDbDataColumnAttribute : Attribute
	{
		/// <summary>The maximum db data length.</summary>
		public int MaxLength { get; set; }
		/// <summary>Native default value</summary>
		public object Default { get; set; }
		/// <summary>Defines if the column is null able.</summary>
		public bool IsNullable { get; set; }

		internal Type Type { get; set; }

		/// <summary>Returns the attribute as C# Code</summary>
		public string ToCode()
		{
			var setters = "(";
			if (MaxLength != -1)
				setters = $"{setters}MaxLength = {MaxLength}, ";

			if (Default != null)
				setters = $"{setters}Default = {ToCode(Default, Type)}, ";

			if (IsNullable)
				setters = $"{setters}IsNullable = {IsNullable.ToString().ToLower()}, ";

			if (setters == "(")
				setters = "";
			else
				setters = setters.Substring(0, setters.Length - 2) + ")";

			return "[" + "CsDbDataColumn" + setters + "]";
		}

		private static string ToCode(object val, Type targetType)
		{
			if (val == null)
				return "null";
				
			if (val.Equals(CsDb.CodeGen.Statics.DateTimeNowFunction))
				return $"\"DateTime.Now\"";
			if (val.Equals(CsDb.CodeGen.Statics.NewGuidFunction))
				return $"\"Guid.NewGuid()\"";

			if (targetType == typeof (string))
				return $"\"{val}\"";
			if (targetType == typeof (DateTime))
				return $"\"{val}\"";
			if (targetType == typeof (bool))
				return $"{val.ToString().ToLower()}";
			if (targetType == typeof (Guid))
				return $"\"{val}\"";
			if (targetType.IsNumericType())
				return val.ToString();

			throw new NotImplementedException("Type is not implemented");
		}
	}
}