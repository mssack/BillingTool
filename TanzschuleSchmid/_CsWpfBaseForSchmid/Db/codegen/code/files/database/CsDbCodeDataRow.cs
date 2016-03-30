// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.associated;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.methods;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.referencing;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>A file for a table class.</summary>
	[Serializable]
	internal class CsDbCodeDataRow : FileTemplate
	{
		private CsDbCodeDataRowInterface _interface;
		private CsDbcTableRow_Methods _methods;

		private string _name;

		/// <summary>Creates a new code set.</summary>
		public CsDbCodeDataRow(CsDbCodeDataTable table)
		{
			Table = table;
			Columns = Architecture.Columns.Select(x => new CsDbcTableRow_Column(x, this)).ToArray();
			PkColumn = Columns.FirstOrDefault(x => x.Architecture == Architecture.PrimaryColumn);
			UnsignedColumns = Columns.Where(x => x.UnsignedVersion != null).Select(x => x.UnsignedVersion).ToArray();
		}

		/// <summary>Gets the underlaying architecture.</summary>
		public CsDbArcTable Architecture => Table.Architecture;
		/// <summary>Gets the Owner.</summary>
		public CsDbCodeBundleForDb CodeBundle => Table.CodeBundle;
		/// <summary>Gets the associated table.</summary>
		public CsDbCodeDataTable Table { get; }
		/// <summary>Gets the associated interface</summary>
		public CsDbCodeDataRowInterface Interface => _interface ?? (_interface = new CsDbCodeDataRowInterface(this));



		/// <summary>Gets the columns inside this data row.</summary>
		public CsDbcTableRow_Column[] Columns { get; }
		/// <summary>Gets the columns inside this data row.</summary>
		public CsDbcTableRow_UnsignedColumn[] UnsignedColumns { get; }
		/// <summary>Gets the primary key column.</summary>
		public CsDbcTableRow_Column PkColumn { get; set; }
		/// <summary>Gets the row reload method.</summary>
		public CsDbcTableRow_Methods Methods => _methods ?? (_methods = new CsDbcTableRow_Methods(this));

		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = Table.SingularName); }
			set { SetProperty(ref _name, value); }
		}
		internal IEnumerable<CsDbcTableRow_AssociatedProperty> AssociatedProperties => Table.Relations.Where(x => x.FkKey.Row == this).Select(x => x.AssociatedProperty);
		internal IEnumerable<CsDbcTableRow_ReferencingProperty> ReferencingProperties => Table.Relations.Where(x => x.PkKey.Row == this).Select(x => x.ReferencingProperty);

		[Key]
		private string InterfaceName => Interface.Name;

		[Key]
		private string DefaultDescription => $"DataRow([<c>{CodeBundle.Architecture.Name}</c>].[<c>{Architecture.Name}</c>]): row model of <see cref=\"{Table.Name}\"/>.";

		[Key(IncludeInHash = false)]
		private string CsDbDataRowAttribute => $"{new CsDbDataRowAttribute {Database = Architecture.Owner.Name, TableName = Architecture.Name, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode()}";

		[Key]
		private string DebuggerDisplayAttribute => "[DebuggerDisplay(\"" + (PkColumn == null ? $"DataRow({CodeBundle.Architecture.Name}.{Architecture.Name})" : $"DataRow({CodeBundle.Architecture.Name}.{Architecture.Name}): {PkColumn.Name} = '{{{PkColumn.Name}}}'") + "\")]";

		[Key]
		private string DataContextType => CodeBundle.Owner.Context.FullQualifiedName;

		[Key]
		private string DataSetType => CodeBundle.DataSet.Name;

		[Key]
		private string DataContextPropertyName => Table.DataContextPropertyName;

		[Key]
		private string DataSetPropertyName => Table.DataSetPropertyName;

		[Key]
		private string TableType => Table.Name;





		[Key(Name = "Columns")]
		private string TmpColumns => Columns.Select(x => x.GetString(1)).Join("\r\n\t");

		[Key(Name = "UnsignedColumns", ValuePrefix = "\r\n\r\n\r\n\r\n\t#region Unsigned Extension\r\n\t", ValueSuffix = "\r\n\t#endregion")]
		private string TmpUnsignedColumns => UnsignedColumns.Length == 0 ? null : UnsignedColumns.Select(x => x.GetString(1)).Join("\r\n\t");

		[Key(Name = "Methods")]
		private string TmpMethods => Methods.GetString(1);
	}
}