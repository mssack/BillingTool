// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Db.models;






namespace CsWpfBase.Db.attributes
{
	/// <summary>The Attribute for the <see cref="CsDbDataSet" />.</summary>
	public class CsDbDataSetAttribute : Attribute
	{
		/// <summary>The name of the data base.</summary>
		public string Name { get; set; }
		/// <summary>The generation string</summary>
		public string Generated { get; set; }
		/// <summary>The generation hash</summary>
		public string Hash { get; set; }



		/// <summary>Returns the attribute as C# Code</summary>
		public string ToCode()
		{
			var setters = "(";
			if (!string.IsNullOrEmpty(Name))
				setters = $"{setters}Name = \"{Name}\", ";
			if (!string.IsNullOrEmpty(Generated))
				setters = $"{setters}Generated = \"{Generated}\", ";
			if (!string.IsNullOrEmpty(Hash))
				setters = $"{setters}Hash = \"{Hash}\", ";

			if (setters == "(")
				setters = "";
			else
				setters = setters.Substring(0, setters.Length - 2) + ")";
			return "[" + "CsDbDataSet" + setters + "]";
		}
	}
}