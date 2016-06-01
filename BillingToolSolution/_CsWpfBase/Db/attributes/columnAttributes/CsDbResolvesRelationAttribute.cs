// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Db.codegen.code.extensions;






namespace CsWpfBase.Db.attributes.columnAttributes
{
	/// <summary>Defines the relation this method or property uses to resolve the data.</summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class CsDbResolvesRelationAttribute : Attribute
	{
		/// <summary>ctor</summary>
		public CsDbResolvesRelationAttribute(string relationName)
		{
			RelationName = relationName;
		}

		/// <summary>ctor</summary>
		internal CsDbResolvesRelationAttribute(CsDbCodeRelation relation) : this(relation.Architecture.Name)
		{
			PkTable = relation.PkKey.Architecture.Owner.Name;
			PkColumn = relation.PkKey.Architecture.Name;

			FkTable = relation.FkKey.Architecture.Owner.Name;
			FkColumn = relation.FkKey.Architecture.Name;
		}

		/// <summary>The name of the relation.</summary>
		public string RelationName { get; }

		/// <summary>Primary key table.</summary>
		public string PkTable { get; set; }
		/// <summary>Primary key column</summary>
		public string PkColumn { get; set; }

		/// <summary>Foreign key table.</summary>
		public string FkTable { get; set; }
		/// <summary>Foreign key column</summary>
		public string FkColumn { get; set; }


		/// <summary>Converts the attribute and its current content to the c# code.</summary>
		public string GetCode()
		{
			return $"[CsDbResolvesRelation(\"{RelationName}\", PkTable = \"{PkTable}\", PkColumn = \"{PkColumn}\", FkTable = \"{FkTable}\", FkColumn = \"{FkColumn}\")]";
		}
	}
}