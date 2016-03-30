// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.files.database;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.extensions
{
	/// <summary>Used for file template which have a relational background.</summary>
	internal class CsDbCodeRelationFileTemplate : FileTemplate
	{
		internal CsDbCodeRelationFileTemplate(CsDbCodeRelation relation)
		{
			Relation = relation;
		}

		/// <summary>Get the relation this template is based on.</summary>
		public CsDbCodeRelation Relation { get; }


		/// <summary>The dataset of the current code base.</summary>
		protected CsDbCodeDataSet DataSet => PkTable.CodeBundle.DataSet;

		/// <summary>The code class of the primary key column of the <see cref="Relation" />.</summary>
		protected CsDbcTableRow_Column PkColumn => Relation.PkKey;
		/// <summary>The code class of the primary key row of the <see cref="Relation" />.</summary>
		protected CsDbCodeDataRow PkRow => PkColumn.Row;
		/// <summary>The code class of the primary key table of the <see cref="Relation" />.</summary>
		protected CsDbCodeDataTable PkTable => PkRow.Table;

		/// <summary>The code class of the foreign key column of the <see cref="Relation" />.</summary>
		protected CsDbcTableRow_Column FkColumn => Relation.FkKey;
		/// <summary>The code class of the foreign key row of the <see cref="Relation" />.</summary>
		protected CsDbCodeDataRow FkRow => FkColumn.Row;
		/// <summary>The code class of the foreign key table of the <see cref="Relation" />.</summary>
		protected CsDbCodeDataTable FkTable => FkRow.Table;
	}
}