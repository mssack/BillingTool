// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-14</date>

using System;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datatableParts.methods
{
	/// <summary>Collapses methods which are overriden from base class.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_Overrides : FileTemplate
	{
		internal CsDbcTable_Overrides(CsDbCodeDataTable table, CsDbcTable_Methods owner)
		{
			Table = table;
			Owner = owner;
		}
		


		/// <summary>Get the name of the LoadData method</summary>
		[Key]
		public string LoadDataMethodName => "DownloadRows";
		/// <summary>Get the name of the generic_FindOrLoad method</summary>
		[Key]
		public string GenericFindOrLoadMethodName => "Generic_FindOrLoad";
		/// <summary>Get the name of the generic_LoadThenFind method</summary>
		[Key]
		public string GenericLoadThenFindMethodName => "Generic_LoadThenFind";
		/// <summary>Get the name of the generic_Find method</summary>
		[Key]
		public string GenericFindMethodName => "Generic_Find";

		private CsDbcTable_Methods Owner { get; }
		private CsDbCodeDataTable Table { get; }
		private CsDbcTableRow_Column PkColumn => Table.Row.PkColumn;


		[Key]
		private string ParamName => PkColumn == null ? "noval" : PkColumn.Name.ToLowerName();


		[Key]
		private string TableNativeName => Table.NativeName;
		[Key]
		private string TableNativeNameConstant => Table.NativeNameConstant;

		[Key]
		private string FindOrLoadMethodSummary => PkColumn == null ? "DO NOT USE THIS METHOD. This table does not contain a primary key." : $"This method calls <see cref=\"{Table.Methods.PrimaryKey.FindOrLoadName}\"/>.";
		[Key]
		private string LoadThenFindMethodSummary => PkColumn == null ? "DO NOT USE THIS METHOD. This table does not contain a primary key." : $"This method calls <see cref=\"{Table.Methods.PrimaryKey.LoadThenFindName}\"/>.";
		[Key]
		private string FindMethodSummary => PkColumn == null ? "DO NOT USE THIS METHOD. This table does not contain a primary key." : $"This method calls <see cref=\"{Table.Methods.PrimaryKey.FindName}\"/>.";

		[Key]
		private string FindOrLoadMethodBody => PkColumn == null ? "throw new NotImplementedException(\"No primary key defined in this table.\");" : $"return {ParamName}==null ? null : {Table.Methods.PrimaryKey.FindOrLoadName}(({PkColumn.DotNetAttributes.Type.Name}) {ParamName});";
		[Key]
		private string LoadThenFindMethodBody => PkColumn == null ? "throw new NotImplementedException(\"No primary key defined in this table.\");" : $"return {ParamName}==null ? null : {Table.Methods.PrimaryKey.LoadThenFindName}(({PkColumn.DotNetAttributes.Type.Name}) {ParamName});";
		[Key]
		private string FindMethodBody => PkColumn == null ? "throw new NotImplementedException(\"No primary key defined in this table.\");" : $"return {ParamName}==null ? null : {Table.Methods.PrimaryKey.FindName}(({PkColumn.DotNetAttributes.Type.Name}) {ParamName});";
	}
}