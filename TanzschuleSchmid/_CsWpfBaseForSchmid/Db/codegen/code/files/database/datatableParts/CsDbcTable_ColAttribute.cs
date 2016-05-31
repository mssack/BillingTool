// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datatableParts
{
	/// <summary>Used for col attribute providing.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_ColAttribute : FileTemplate
	{
		public CsDbcTable_ColAttribute(CsDbcTableRow_Column column)
		{
			Column = column;
		}

		private CsDbcTableRow_Column Column { get; }
		

		[Key]
		private string Name => $"{Column.Name}";

		[Key]
		private string Type => Column.NativeAttributes.GetType().Name;

		[Key]
		private string Value => Column.NativeAttributes.To_NewObject_Code();

		[Key]
		private string DatabaseName => Column.CodeBundle.Architecture.Name;

		[Key]
		private string NativeTableName => Column.Architecture.Owner.Name;

		[Key]
		private string NativeColumnName => Column.Architecture.Name;

		
	}
}