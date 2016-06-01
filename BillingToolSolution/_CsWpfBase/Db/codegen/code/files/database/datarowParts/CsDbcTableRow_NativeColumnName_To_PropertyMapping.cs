// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-31</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts
{
	// ReSharper disable once InconsistentNaming
	[Serializable]
	internal class CsDbcTableRow_NativeColumnName_To_PropertyMapping : FileTemplate
	{
		public CsDbcTableRow_NativeColumnName_To_PropertyMapping(CsDbCodeDataRow row)
		{
			Row = row;
		}

		/// <summary>Gets the owning code base.</summary>
		public CsDbCodeBundleForDb CodeBundle => Row.CodeBundle;
		/// <summary>Gets the owning row.</summary>
		public CsDbCodeDataRow Row { get; }

		[Key]
		private string RowType => Row.Interface.Name;
		[Key]
		private string Entries => Row.Columns.Select(x => $"{{ {Row.Table.Name}.{x.NativeNameConstant}, type.GetProperty(nameof({x.Name})) }}").Join(",\r\n\t\t\t");
	}
}