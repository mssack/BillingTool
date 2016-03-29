// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datatableParts.methods
{
	/// <summary>Collapses primary key methods.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_PrimaryKeyMethods : FileTemplate
	{
		internal CsDbcTable_PrimaryKeyMethods(CsDbCodeDataTable table)
		{
			Table = table;
		}

		/// <summary>The owning table.</summary>
		public CsDbCodeDataTable Table { get; }



		/// <summary>Gets the name of the Find method which is used to select by the primary key in the local data.</summary>
		[Key]
		public string FindName => "Find";
		/// <summary>
		///     Gets the name of the FindOrLoad method which is used to select by the primary key in the local data. If nothing is found the database is
		///     queried.
		/// </summary>
		[Key]
		public string FindOrLoadName => "FindOrLoad";
		/// <summary>Gets the name of the LoadThenFind method which is used to query the data base and after that find the result in local data.</summary>
		[Key]
		public string LoadThenFindName => "LoadThenFind";


		[Key]
		private string RowType => Table.Row.Name;
		[Key]
		private string ParamName => Table.Row.PkColumn.Name.ToLowerName();
		[Key]
		private string ParamNameSelector => Table.Row.PkColumn.DotNetAttributes.Type.IsValueType ? $"{ParamName}.Value" : ParamName;
		[Key]
		private string ParamType => Table.Row.PkColumn.DotNetAttributes.Type.IsValueType ? $"{Table.Row.PkColumn.DotNetAttributes.Type.Name}?" : Table.Row.PkColumn.DotNetAttributes.Type.Name;


		[Key]
		private string NativeColumnNameConstant => Table.Row.PkColumn.NativeNameConstant;


		[Key]
		private string Attributes => Table.Relations.Select(x => x.Attribute.GetCode()).Join("\r\n");



		[Key]
		private string SelectStatement => $"SELECT {{DefaultSqlSelector}} FROM [{{{Table.NativeNameConstant}}}] WHERE [{Table.Row.PkColumn.Architecture.Name}] = '{{{ParamName}}}'";
		[Key]
		private string SelectStatementDesc => $"SELECT {{DefaultSqlSelector}} FROM [{Table.NativeName}] WHERE [{Table.Row.PkColumn.Name}] = '<paramref name=\"{ParamName}\"/>'";
		[Key]
		private string FindInLocalDesc => $"find an item in local data where {Table.Row.PkColumn.Name} = '<paramref name=\"{ParamName}\"/>'";
	}
}