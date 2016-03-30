// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-15</date>

using System;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.interfaceParts
{
	/// <summary>a member of the interface.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcRowInterface_Member : FileTemplate
	{
		internal CsDbcRowInterface_Member(CsDbcTableRow_Column column)
		{
			Column = column;
		}

		private CsDbcTableRow_Column Column { get; }

		[Key]
		private string Name => Column.UnsignedVersion == null ? Column.Name : Column.UnsignedVersion.Name;
		[Key]
		private string Type => Column.UnsignedVersion == null ? (Column.Type) : Column.UnsignedVersion.Type;
		[Key]
		private string Description => $"[<c>{Column.CodeBundle.Architecture.Name}</c>].[<c>{Column.Architecture.Owner.Name}</c>].[<c>{Column.Architecture.Name}</c>]";
	}
}