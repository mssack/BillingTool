// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Utilitys.ConfigEngine
{
	/// <summary>Includes a property or a field into the generated configuration file.</summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class ConfigMemberAttribute : Attribute
	{
		/// <summary>The name is used instead of the real property or field name.</summary>
		public string Name { get; set; }
		/// <summary>The string format which will be used to convert member to text.</summary>
		public string StringFormat { get; set; }
		/// <summary>
		///     If use fall back value is true, no exception will be thrown on invalid input instead, the default value for
		///     the type will be taken.
		/// </summary>
		public bool UseFallbackValue { get; set; }
	}
}