// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.dataviewparts.row.columns;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.dataviewparts.constants
{
	/// <summary>Used as a Wrapper for a single constant.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcView_Constant : FileTemplate
	{
		internal CsDbcView_Constant(CsDbcViewRow_Column column)
		{
			Column = column;
		}

		/// <summary>The name of the constant.</summary>
		[Key]
		public string Name => Column.NativeNameConstant;

		private CsDbCodeBundleForDb CodeBundle => Column.CodeBundle;
		private CsDbArcColumn Architecture => Column.Architecture;
		private CsDbcViewRow_Column Column { get; }
		[Key]
		private string Value => Architecture.Name;
		[Key]
		private string DatabaseReference => $"[<c>{CodeBundle.Architecture.Name}</c>].[<c>{Architecture.OwnerView.Name}</c>].[<c>{Architecture.Name}</c>]";
		[Key]
		private string PropertyReference => $"{Column.Row.Name}.{Column.Name}";
	}
}