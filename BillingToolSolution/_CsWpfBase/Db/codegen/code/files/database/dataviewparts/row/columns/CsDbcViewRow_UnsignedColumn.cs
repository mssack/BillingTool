// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.dataviewparts.row.columns
{
	/// <summary>Used to wrap 'Unsigned_' data columns around another wrapper to provide unsigned values.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcViewRow_UnsignedColumn : FileTemplate
	{
		/// <summary>ctor</summary>
		public CsDbcViewRow_UnsignedColumn(CsDbcViewRow_Column baseColumn)
		{
			BaseColumn = baseColumn;
		}

		/// <summary>Gets the name of the unsigned property.</summary>
		[Key]
		public string Name => BaseColumn.Architecture.Name.Replace("Unsigned_", "");



		[Key]
		private string Attributes => $"[DependsOn(\"{BaseColumn.Name}\")]";

		[Key]
		private string Type => BaseColumn.Architecture.DotNetType.ToUnsigned().Name + (BaseColumn.DotNetAttributes.IsNullable ? "?" : "");

		[Key]
		private string SignedName => BaseColumn.Name;





		private CsDbcViewRow_Column BaseColumn { get; }
	}
}