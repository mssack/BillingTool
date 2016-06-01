// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Utilitys.searializer.v1.reflection
{
	/// <summary>The reflected properties of members including classes, properties and other attributes or types.</summary>
	public abstract class CssTypeDefinition : Base
	{
		#region Abstract
		/// <summary>Gets the full qualified assembly name</summary>
		public abstract string FullQualifiedAssemblyName { get; protected set; }
		/// <summary>The type of the definition.</summary>
		public abstract Type Type { get; protected set; }
		/// <summary>Gets a boolean indicating if the type is known in this assembly and can be created.</summary>
		public abstract bool IsKnownType { get; }
		#endregion


		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			if (Type != null)
				return Type.Name;
			return FullQualifiedAssemblyName;
		}
	}



}