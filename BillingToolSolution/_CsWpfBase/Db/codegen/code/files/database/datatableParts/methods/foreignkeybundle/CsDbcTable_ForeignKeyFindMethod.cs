// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.extensions;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.codegen.code.files.database.datatableParts.methods.foreignkeybundle
{
	/// <summary>The find method for a foreign key.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_ForeignKeyFindMethod : CsDbCodeRelationFileTemplate
	{
		internal CsDbcTable_ForeignKeyFindMethod(CsDbCodeRelation relation, CsDbcTable_ForeignKeyMethodBundle owner) : base(relation)
		{
			Owner = owner;
		}

		/// <summary>Get the name of the find method.</summary>
		[Key]
		public string Name => $"Find_By_{FkColumn.Name}";
		[Key]
		private string CacheFieldName => Owner.CacheFieldName;


		[Key]
		private string RowType => Owner.RowType;
		[Key]
		private string LowerRowType => $"@{Owner.RowType.ToLowerName()}";
		[Key]
		private string ParamType => Owner.ParamType;
		[Key]
		private string ParamName => Owner.ParamName;



		[Key]
		private string FkColumnName => FkColumn.Name;



		[Key]
		private string DefaultDescription => Owner.DefaultDescription;
		[Key]
		private string RelationDescription => Relation.Get_RelationDescription_PointingTowardPk();
		[Key]
		private string Attributes => Relation.Attribute.GetCode();



		private CsDbcTable_ForeignKeyMethodBundle Owner { get; }
	}
}