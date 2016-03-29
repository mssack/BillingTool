// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datasetParts
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbcSet_RelationEntry : FileTemplate
	{
		public CsDbcSet_RelationEntry(CsDbCodeRelation relation)
		{
			Relation = relation;
		}

		public CsDbCodeRelation Relation { get; }

		[Key]
		private string Name => Relation.Architecture.Name;
		[Key]
		private string PkTableProperty => Relation.PkKey.Row.Table.PropertyName;
		[Key]
		private string PkTableName => Relation.PkKey.Row.Table.Name;
		[Key]
		private string PkColumnName => Relation.PkKey.NativeNameConstant;
		[Key]
		private string FkTableProperty => Relation.FkKey.Row.Table.PropertyName;
		[Key]
		private string FkTableName => Relation.FkKey.Row.Table.Name;
		[Key]
		private string FkColumnName => Relation.FkKey.NativeNameConstant;
		[Key]
		private string DeleteRule => $"Rule.{Relation.Architecture.DeleteRule}";
		[Key]
		private string UpdateRule => $"Rule.{Relation.Architecture.UpdateRule}";
	}
}