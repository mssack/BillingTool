// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-17</date>

using System;






namespace CsWpfBase.Utilitys.searializer.attributes
{
	/// <summary>
	///     Used to define an alias for a field. This alias allows older versions of the software to identify this field. Especially when renaming fields in
	///     new versions
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class CssLegacyAliasAttribute : Attribute
	{
		/// <summary>ctor</summary>
		/// <param name="name">The alias name for the field.</param>
		public CssLegacyAliasAttribute(string name)
		{
			Name = name;
		}

		/// <summary>The alias name of the field.</summary>
		public string Name { get; }
	}
}