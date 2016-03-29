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
	/// <summary>The copy methods takes care of the cloning process into an interface and from an interface.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_CopyMethods : FileTemplate
	{
		internal CsDbcTableRow_CopyMethods(CsDbCodeDataRow row)
		{
			Row = row;
		}

		/// <summary>Copy from method name.</summary>
		[Key]
		public string CopyFromName => "Copy_From";

		/// <summary>Copy to method name.</summary>
		[Key]
		public string CopyToName => "Copy_To";

		internal CsDbCodeDataRow Row { get; }



		[Key]
		private string InterfaceName => Row.Interface.Name;
		[Key]
		private string CopyToMethodBody => Row.Columns.Select(x =>
		{
			var name = x.UnsignedVersion == null ? x.Name : x.UnsignedVersion.Name;
			if (x.Row.PkColumn != x)
				return $"target.{name} = this.{name};";
			else
				return $"if (includePrimaryKey) target.{name} = this.{name};";
		}).Join("\r\n\t");
		[Key]
		private string CopyFromMethodBody => Row.Columns.Select(x =>
		{
			var name = x.UnsignedVersion == null ? x.Name : x.UnsignedVersion.Name;
			if (x.Row.PkColumn != x)
				return $"this.{name} = source.{name};";
			else
				return $"if (includePrimaryKey) this.{name} = source.{name};";
		}).Join("\r\n\t");
	}
}