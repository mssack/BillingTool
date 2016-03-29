// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Db.attributes.columnAttributes
{
	/// <summary>A db column attribute for the CsDb system. Holds all the native values of the column.</summary>
	public class CsDbNativeDataColumnAttribute : Attribute
	{
		/// <summary>The native column name.</summary>
		public string Name { get; set; }
		/// <summary>The native database type.</summary>
		public string Type { get; set; }
		/// <summary>The maximum db data length.</summary>
		public string MaxLength { get; set; }
		/// <summary>Native default value</summary>
		public string Default { get; set; }
		/// <summary>Defines if the column is null able.</summary>
		public string IsNullable { get; set; }
		/// <summary>Native description</summary>
		public string Description { get; set; }

		/// <summary>Returns the attribute as C# Code</summary>
		public string ToCode()
		{
			var setters = "(";
			if (!string.IsNullOrEmpty(Name))
				setters = $"{setters}Name = \"{Name}\", ";
			if (!string.IsNullOrEmpty(Type))
				setters = $"{setters}Type = \"{Type}\", ";
			if (!string.IsNullOrEmpty(Default))
				setters = $"{setters}Default = \"{Default.Replace("\"", "'")}\", ";
			if (!string.IsNullOrEmpty(MaxLength))
				setters = $"{setters}MaxLength = \"{MaxLength}\", ";
			if (!string.IsNullOrEmpty(IsNullable))
				setters = $"{setters}IsNullable = \"{IsNullable}\", ";
			if (!string.IsNullOrEmpty(Description))
				setters = $"{setters}Description = \"{Description.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"")}\", ";

			if (setters == "(")
				setters = "";
			else
				setters = setters.Substring(0, setters.Length - 2) + ")";

			return "[" + "CsDbNativeDataColumn" + setters + "]";
		}
	}



}