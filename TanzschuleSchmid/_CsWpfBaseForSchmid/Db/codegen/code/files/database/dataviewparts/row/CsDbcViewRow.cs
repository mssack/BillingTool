// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.dataviewparts.row.columns;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.dataviewparts.row
{
	/// <summary>Template for a row file for a view.</summary>
	internal class CsDbcViewRow : FileTemplate
	{
		private CsDbcViewRow_Column[] _columns;
		private string _name;

		internal CsDbcViewRow(CsDbCodeDataView table)
		{
			Table = table;
		}



		/// <summary>Gets the columns associated with this row.</summary>
		public CsDbcViewRow_Column[] Columns => _columns ?? (_columns = Architecture.Columns.Select(x => new CsDbcViewRow_Column(x, this)).ToArray());



		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = Table.SingularName); }
			set { SetProperty(ref _name, value); }
		}

		internal CsDbCodeDataView Table { get; }
		internal CsDbArcView Architecture => Table.Architecture;
		/// <summary>Gets the owning code base.</summary>
		internal CsDbCodeBundleForDb CodeBundle => Table.CodeBundle;


		[Key]
		private string DefaultDescription => $"!VIEW: DataRow([<c>{CodeBundle.Architecture.Name}</c>].[<c>{Architecture.Name}</c>]): row model of <see cref=\"{Table.Name}\"/>.";
		[Key]
		private string DebuggerDisplayAttribute => $"[DebuggerDisplay(\"!VIEW: DataRow({CodeBundle.Architecture.Name}.{Architecture.Name})\")]";
		[Key(IncludeInHash = false)]
		private string CsDbDataViewRowAttribute => new CsDbViewRowAttribute {Database = Architecture.Owner.Name, ViewName = Architecture.Name, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode();
		[Key]
		private string TableType => Table.Name;


		[Key(Name = "Columns")]
		private string TmpColumns => Columns.Select(x => x.GetString(1)).Join("\r\n\r\n\t");
		[Key]
		private string UnsignedColumns => Columns.Where(x => x.UnsignedVersion != null).Select(x => x.UnsignedVersion.GetString(1)).Join("\r\n\r\n\t");
	}
}