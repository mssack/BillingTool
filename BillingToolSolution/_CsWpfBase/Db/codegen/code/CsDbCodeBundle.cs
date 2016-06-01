// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsWpfBase.Db.codegen.architecture;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.extensions;
using CsWpfBase.Db.codegen.code.files.database;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.associated;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.referencing;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.code
{
	/// <summary>Defines a set of source code files and folders for a particular database server.</summary>
	[Serializable]
	public class CsDbCodeBundle : Base
	{
		private CsDbCodeBundle(CsDbArchitecture architecture)
		{
			Architecture = architecture;
		}


		/// <summary>The data base server architecture.</summary>
		public CsDbArchitecture Architecture { get; set; }

		/// <summary>The name space used for this set of databases.</summary>
		public string NameSpace { get; private set; }
		/// <summary>The source directory for the databases the context and all stuff belonging to the data base server.</summary>
		public string Directory { get; private set; }
		/// <summary>The file path for the target project.</summary>
		public string ProjectFilePath { get; private set; }
		/// <summary>Gets the file extension which is appended to each file: default '.generated.cs'.</summary>
		public string FileExtension { get; set; } = ".generated.cs";

		internal CsDbCodeDataContext Context { get; set; }

		/// <summary>The data bases inside this server.</summary>
		internal CsDbCodeBundleForDb[] Databases { get; set; }

		/// <summary>Sets the target paths accordingly.</summary>
		/// <param name="parentDirectory">the owning directory where the database should be generated at.</param>
		/// <param name="nameSpace">The root name space should be same like of the object inside the <paramref name="parentDirectory" />.</param>
		/// <param name="projectFile">The file of the owning project.</param>
		public void SetPaths(string parentDirectory, string nameSpace, string projectFile)
		{
			NameSpace = $"{nameSpace}.{Architecture.Name.ToLower()}";
			Directory = Path.Combine(parentDirectory, Architecture.Name.ToLower());
			ProjectFilePath = projectFile;
		}

		/// <summary>Writes the code bundle to the file system.</summary>
		public void Write(bool checkHash = true)
		{
			new CsDbCodeBundleWriter(this).Start(checkHash);
		}

		/// <summary>Creates the code bundle from an existing <see cref="CsDbArchitecture" />.</summary>
		internal static CsDbCodeBundle FromArchitecture(CsDbArchitecture architecture)
		{
			var codeBundle = new CsDbCodeBundle(architecture);

			codeBundle.Context = new CsDbCodeDataContext(codeBundle);
			codeBundle.Databases = architecture.Databases.Select(x => CsDbCodeBundleForDb.FromArchitecture(codeBundle, x)).ToArray();

			return codeBundle;
		}
	}



	/// <summary>Defines a set of source code files and folders for a particular database.</summary>
	internal class CsDbCodeBundleForDb : Base
	{
		private CsDbCodeBundleForDb(CsDbCodeBundle owner, CsDbArcDatabase architecture)
		{
			Owner = owner;
			Architecture = architecture;
		}

		/// <summary>The data set code file for the data base.</summary>
		public CsDbCodeDataSet DataSet { get; set; }
		/// <summary>The code files for the tables inside this database.</summary>
		public CsDbCodeDataTable[] Tables { get; set; }
		/// <summary>The code files for the views inside this database.</summary>
		public CsDbCodeDataView[] Views { get; set; }
		/// <summary>The code files for the rows inside this database.</summary>
		public IEnumerable<CsDbCodeDataRow> Rows => Tables.Select(x => x.Row);
		/// <summary>The code files for the data row interfaces inside this database.</summary>
		public IEnumerable<CsDbCodeDataRowInterface> RowInterfaces => Rows.Select(x => x.Interface);
		/// <summary>The data base architecture.</summary>
		public CsDbArcDatabase Architecture { get; set; }


		/// <summary>The name space for the data set.</summary>
		internal string DataSetNameSpace => $"{DataBaseNameSpace}.dataset";
		/// <summary>The name space for the tables.</summary>
		internal string TablesNameSpace => $"{DataBaseNameSpace}.tables";
		/// <summary>The name space for the views.</summary>
		internal string ViewsNameSpace => $"{DataBaseNameSpace}.views";
		/// <summary>The name space for the rows.</summary>
		internal string RowsNameSpace => $"{DataBaseNameSpace}.rows";
		/// <summary>The name space for the row interfaces.</summary>
		internal string RowInterfacesNameSpace => $"{DataBaseNameSpace}.rowinterfaces";


		/// <summary>The directory for the data set.</summary>
		internal string DataSetDirectory => Path.Combine(DataBaseDirectory, "dataset");
		/// <summary>The directory for the tables.</summary>
		internal string TablesDirectory => Path.Combine(DataBaseDirectory, "tables");
		/// <summary>The directory for the views.</summary>
		internal string ViewsDirectory => Path.Combine(DataBaseDirectory, "views");
		/// <summary>The directory for the rows.</summary>
		internal string RowsDirectory => Path.Combine(DataBaseDirectory, "rows");
		/// <summary>The directory for the row interfaces.</summary>
		internal string RowInterfacesDirectory => Path.Combine(DataBaseDirectory, "rowinterfaces");

		internal CsDbCodeBundle Owner { get; }


		private string DataBaseNameSpace => $"{Owner.NameSpace}.{CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, false).ToLower()}";
		private string DataBaseDirectory => Path.Combine(Owner.Directory, CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, false).ToLower());



		/// <summary>Creates the code base from an existing <see cref="CsDbArcDatabase" />.</summary>
		public static CsDbCodeBundleForDb FromArchitecture(CsDbCodeBundle owner, CsDbArcDatabase architecture)
		{
			var bundleForDb = new CsDbCodeBundleForDb(owner, architecture);
			bundleForDb.DataSet = new CsDbCodeDataSet(architecture, bundleForDb);

			var rvTables = new List<CsDbCodeDataTable>();
			var rvViews = new List<CsDbCodeDataView>();

			var columnMapping = new Dictionary<CsDbArcColumn, CsDbcTableRow_Column>();

			foreach (var table in architecture.Tables)
			{
				var rvTable = new CsDbCodeDataTable(table, bundleForDb);

				var columns = rvTable.Row.Columns.ToList();
				foreach (var archiColumn in table.Columns)
				{
					var codeColumn = columns.FirstOrDefault(x => x.Architecture == archiColumn);
					columnMapping.Add(archiColumn, codeColumn);
				}
				rvTables.Add(rvTable);
			}

			foreach (var view in architecture.Views)
			{
				var rvView = new CsDbCodeDataView(view, bundleForDb);
				rvViews.Add(rvView);
			}


			bundleForDb.Tables = rvTables.ToArray();
			bundleForDb.Views = rvViews.ToArray();

			foreach (var relation in architecture.Relations)
			{
				var pkColumn = columnMapping[relation.PrimaryKey];
				var fkColumn = columnMapping[relation.ForeignKey];
				var csDbCodeRelation = CsDbCodeRelation.Create(relation, pkColumn, fkColumn);
				csDbCodeRelation.ReferencingProperty = new CsDbcTableRow_ReferencingProperty(csDbCodeRelation);
				csDbCodeRelation.AssociatedProperty = new CsDbcTableRow_AssociatedProperty(csDbCodeRelation);
			}

			return bundleForDb;
		}
	}
}