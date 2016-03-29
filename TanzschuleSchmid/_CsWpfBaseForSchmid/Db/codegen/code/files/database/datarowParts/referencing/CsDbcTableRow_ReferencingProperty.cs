// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.extensions;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.referencing
{
	/// <summary>The link to the foreign key table of this row.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_ReferencingProperty : CsDbCodeRelationFileTemplate
	{
		internal CsDbcTableRow_ReferencingProperty(CsDbCodeRelation relation) : base(relation)
		{
		}



		/// <summary>The name of the property.</summary>
		[Key]
		public string Name => Relation.Convention?.Plural ?? FkTable.PluralName;


		/// <summary>The field which stores the collection.</summary>
		[Key]
		public string DataFieldName => $"_weak{Name}";


		internal string DataField => $"private ContractCollection<{RowType}> {DataFieldName};";


		[Key]
		private string RowType => FkRow.Name;



		[Key]
		private string TableProperty => FkTable.PropertyName;
		[Key]
		private string FindOrLoadMethodName => Relation.FkMethods.FindOrLoadMethod.Name;
		[Key]
		private string FindMethodName => Relation.FkMethods.FindMethod.Name;
		[Key]
		private string PkColumnName => PkColumn.Name;



		[Key]
		private string SelectDescription => Relation.Get_FkSelectDescription();
		[Key]
		private string RelationDescription => Relation.Get_RelationDescription_PointingTowardFk();
		[Key]
		private string Attribute => Relation.Attribute.GetCode();
	}
}