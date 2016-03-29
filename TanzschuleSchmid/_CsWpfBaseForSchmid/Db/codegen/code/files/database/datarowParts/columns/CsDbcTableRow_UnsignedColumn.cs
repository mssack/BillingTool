// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.columns
{
	/// <summary>Used to wrap 'Unsigned_' data columns around another wrapper to provide unsigned values.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_UnsignedColumn : FileTemplate
	{
		/// <summary>ctor</summary>
		public CsDbcTableRow_UnsignedColumn(CsDbcTableRow_Column baseColumn)
		{
			BaseColumn = baseColumn;
		}


		/// <summary>Gets the name of the unsigned property.</summary>
		[Key]
		public string Name => BaseColumn.Architecture.Name.Replace("Unsigned_", "");



		[Key]
		private string AbsoluteDatabasePath => $"[<c>{BaseColumn.Architecture.Owner.Owner.Name}</c>].[<c>{BaseColumn.Architecture.Owner.Name}</c>].[<c>{BaseColumn.Architecture.Name}</c>]";
		[Key(ValueSuffix = "\r\n")]
		private string Attributes => $"[DependsOn(\"{BaseColumn.Name}\")]";
		[Key]
		internal string Type => BaseColumn.Architecture.DotNetType.ToUnsigned().Name + (BaseColumn.DotNetAttributes.IsNullable ? "?" : "");



		[Key]
		private string SignedProperty => BaseColumn.Name;
		[Key]
		private string SignedType => BaseColumn.DotNetAttributes.Type.Name + (BaseColumn.DotNetAttributes.IsNullable ? "?" : "");




		private CsDbcTableRow_Column BaseColumn { get; }
	}
}