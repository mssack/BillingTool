// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.methods
{
	/// <summary>The reload method for a single datarow.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_Reload : FileTemplate
	{
		internal CsDbcTableRow_Reload(CsDbcTableRow_Column primaryColumn)
		{
			PrimaryColumn = primaryColumn;

		}


		/// <summary>Gets the name of the reload method</summary>
		[Key]
		public string Name => "Reload";

		private CsDbcTableRow_Column PrimaryColumn { get; }

		[Key]
		private string DataRowName => PrimaryColumn.Row.Name;
		[Key]
		private string NativeTableName => PrimaryColumn.Architecture.Owner.Name;
		[Key]
		private string NativePrimaryColumn => PrimaryColumn.Architecture.Name;
		[Key]
		private string PrimaryColumnName => PrimaryColumn.Name;
		[Key]
		private string NativeTableNameConstant => PrimaryColumn.Row.Table.NativeNameConstant;
		[Key]
		private string NativePrimaryColumnConstant => PrimaryColumn.NativeNameConstant;
		[Key]
		private string DataSetName => PrimaryColumn.CodeBundle.DataSet.Name;
		[Key]
		private string DataSetFullName => PrimaryColumn.CodeBundle.DataSet.FullQualifiedName;
		[Key]
		private string TableProperty => PrimaryColumn.Row.Table.PropertyName;
		[Key]
		private string TableName => PrimaryColumn.Row.Table.Name;
	}
}