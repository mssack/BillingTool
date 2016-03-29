// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Db.codegen.code.files.database.datatableParts.methods.foreignkeybundle;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datatableParts.methods
{
	/// <summary>Holds all methods which are applied to the data table.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTable_Methods : FileTemplate
	{
		private CsDbcTable_ForeignKeyMethodBundle[] _foreignKeyBundle;
		private CsDbcTable_Overrides _overrides;
		private CsDbcTable_PrimaryKeyMethods _primaryKey;

		internal CsDbcTable_Methods(CsDbCodeDataTable table)
		{
			Table = table;
		}

		/// <summary>Collapses Methods for the primary key</summary>
		public CsDbcTable_Overrides Overrides => _overrides ?? (_overrides = new CsDbcTable_Overrides(Table, this));
		/// <summary>Collapses Methods for the primary key</summary>
		public CsDbcTable_PrimaryKeyMethods PrimaryKey => _primaryKey ?? (_primaryKey = Table.Row.PkColumn == null ? null : new CsDbcTable_PrimaryKeyMethods(Table));
		/// <summary>All foreign key method bundles from this table.</summary>
		public CsDbcTable_ForeignKeyMethodBundle[] ForeignKeyBundles => _foreignKeyBundle ?? (_foreignKeyBundle = Table.Relations.Where(x => x.FkKey.Row.Table == Table).Select(x => new CsDbcTable_ForeignKeyMethodBundle(x)).ToArray());

		/// <summary>The owner table.</summary>
		private CsDbCodeDataTable Table { get; }



		[Key(ValuePrefix = "#region FUNC<Overrides>\r\n", ValueSuffix = "\r\n#endregion")]
		private string OverridesMethods => Overrides?.GetString();


		[Key(ValuePrefix = "#region FUNC<Primary Key>\r\n", ValueSuffix = "\r\n#endregion")]
		private string PrimaryKeyMethods => PrimaryKey?.GetString();


		[Key(ValuePrefix = "#region FUNC<Foreign Key>\r\n", ValueSuffix = "\r\n#endregion")]
		private string ForeignKeyMethods => ForeignKeyBundles?.Select(x => x.GetString()).Join("\r\n");

		[Key]
		private string InterfaceName => Table.Row.Interface.Name;
		[Key]
		private string CopyFromMethodName => Table.Row.Methods.CopyMethods.CopyFromName;
		[Key]
		private string RowType => Table.Row.Name;
	}
}