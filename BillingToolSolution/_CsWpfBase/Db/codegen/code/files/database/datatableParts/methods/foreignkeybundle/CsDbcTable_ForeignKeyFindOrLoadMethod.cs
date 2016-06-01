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
	/// <summary>The load then find method for a foreign key.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_ForeignKeyFindOrLoadMethod : CsDbCodeRelationFileTemplate
	{
		internal CsDbcTable_ForeignKeyFindOrLoadMethod(CsDbCodeRelation relation, CsDbcTable_ForeignKeyMethodBundle owner) : base(relation)
		{
			Owner = owner;
		}

		/// <summary>Gets the name of the method LoadThenFind_By</summary>
		[Key]
		public string Name => $"FindOrLoad_By_{FkColumn.Name}";

		private CsDbcTable_ForeignKeyMethodBundle Owner { get; }

		[Key]
		private string RowType => Owner.RowType;
		[Key]
		private string LowerRowType => $"@{Owner.RowType.ToLowerName()}";
		[Key]
		private string ParamName => Owner.ParamName;
		[Key]
		private string ParamType => Owner.ParamType;
		[Key]
		private string CacheFieldName => Owner.CacheFieldName;
		[Key]
		private string HasBeenLoadedPropertyName => "HasBeenLoaded";



		[Key]
		private string FkTableNativeNameConstant => FkTable.NativeNameConstant;
		[Key]
		private string FkColumnNativeNameConstant => FkColumn.NativeNameConstant;
		[Key]
		private string FkColumnName => FkColumn.Name;
		[Key]
		private string FindMethodName => Owner.FindMethod.Name;





		[Key]
		private string DefaultDescription => Owner.DefaultDescription;
		[Key]
		private string RelationDescription => Relation.Get_RelationDescription_PointingTowardPk();
		[Key]
		private string Attributes => Relation.Attribute.GetCode();
	}
}