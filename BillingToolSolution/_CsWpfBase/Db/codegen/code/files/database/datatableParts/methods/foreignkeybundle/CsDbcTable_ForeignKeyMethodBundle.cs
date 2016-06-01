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
	/// <summary>collapses select methods for a relation.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_ForeignKeyMethodBundle : CsDbCodeRelationFileTemplate
	{
		internal CsDbcTable_ForeignKeyMethodBundle(CsDbCodeRelation relation) : base(relation)
		{
			relation.FkMethods = this;
			relation.PkMethods = PkTable.Methods.PrimaryKey;


			FindMethod = new CsDbcTable_ForeignKeyFindMethod(relation, this);
			LoadThenFindMethod = new CsDbcTable_ForeignKeyLoadThenFindMethod(relation, this);
			FindOrLoadMethod = new CsDbcTable_ForeignKeyFindOrLoadMethod(relation, this);
		}

		/// <summary>The method to Find row in local data.</summary>
		public CsDbcTable_ForeignKeyFindMethod FindMethod { get; set; }
		/// <summary>The method to load and then find row in local data.</summary>
		public CsDbcTable_ForeignKeyLoadThenFindMethod LoadThenFindMethod { get; set; }
		/// <summary>The method to find and then Load row in local data.</summary>
		public CsDbcTable_ForeignKeyFindOrLoadMethod FindOrLoadMethod { get; set; }

		[Key]
		internal string RowType => FkRow.Name;
		[Key]
		private string ParamNameSelector => FkColumn.DotNetAttributes.Type.IsValueType ? $"{ParamName}.Value" : ParamName;
		[Key]
		internal string ParamType => FkColumn.DotNetAttributes.Type.IsValueType ? $"{FkColumn.DotNetAttributes.Type.Name}?" : FkColumn.DotNetAttributes.Type.Name;
		internal string ParamName => FkColumn.Name.ToLowerName();
		internal string DefaultDescription => $"Query <c>SELECT (DefaultSqlSelector) FROM {FkTable.NativeName} WHERE [{FkColumn.Name}] = '<paramref name=\"{ParamName}\"/>'</c>";

		[Key(Name = "LoadThenFindMethod")]
		private string TmpLoadThenFindMethod => LoadThenFindMethod.GetString();
		[Key(Name = "FindMethod")]
		private string TmpFindMethod => FindMethod.GetString();
		[Key(Name = "FindOrLoadMethod")]
		private string TmpFindOrLoadMethod => FindOrLoadMethod.GetString();

		[Key]
		internal string CacheFieldName => $"_by{FkColumn.Name}";
	}
}