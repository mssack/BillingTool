// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.ComponentModel;






namespace CsWpfBase.Ev.Attributes
{
	/// <summary>Expand default Description attribute with an DisplayNameDescription</summary>
	public class EnumDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>ctor</summary>
		public EnumDescriptionAttribute(string name, string description = null) : base(description)
		{
			Name = name;
		}

		/// <summary>The display name of the enum.</summary>
		public string Name { get; set; }
	}
}