// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.methods
{
	/// <summary>This method is used to invalidate a complete row.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_Invalidate : FileTemplate
	{
		internal CsDbcTableRow_Invalidate(CsDbCodeDataRow owner)
		{
			Owner = owner;
		}

		/// <summary>Gets the name of the Invalidate Method.</summary>
		[Key]
		public string Name => "Invalidate";
		[Key]
		private string Columns => Owner.Columns.Select(x => $"OnPropertyChanged(\"{x.Name}\");").Join("\r\n\t");



		private CsDbCodeDataRow Owner { get; }
	}
}