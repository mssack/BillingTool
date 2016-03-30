// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CsWpfBase.Db.codegen.architecture.generators.bases;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.architecture.parts.bases;
using CsWpfBase.Db.codegen.code.namingconventions;
using CsWpfBase.Db.router;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace CsWpfBase.Db.codegen.architecture.generators
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbArcGen_Sql : IGenerateCsDbArcContext
	{
		private static readonly string[] SystemTables = {"master", "tempdb", "model", "msdb"};

		private static DataTable ExecuteCommand(CsDbRouter router, string cmd)
		{
			using (var adapter = router.GetDefaultAdapter(cmd))
			{
				var target = new DataTable();
				adapter.Fill(target);
				return target;
			}
		}

		/// <summary>The generated architecture</summary>
		protected CsDbArchitecture Architecture { get; } = new CsDbArchitecture();



		/// <summary>Creates the generator with an existing router.</summary>
		/// <param name="router">The router must not be opened before.</param>
		public CsDbArcGen_Sql(CsDbRouter router)
		{
			DbRouter = router;
		}


		#region Overrides/Interfaces
		/// <summary>Generates the architecture.</summary>
		public CsDbArchitecture Generate()
		{
			DbRouter.Open();
			Architecture.Name = DbRouter.Connection.DataSource;

			FetchDatabases();
			return Architecture;
		}
		#endregion


		private CsDbRouter DbRouter { get; }


		private void FetchDatabases()
		{
			var databases = DbRouter.Connection.GetSchema("databases").Rows.OfType<DataRow>().Select(row => (string) row[0]).Where(x => !SystemTables.Contains(x)).ToArray();
			CsDb.CodeGen.Tracing.Trace($"Context('{Architecture.Name}') databases: [{databases.Select(x => $"'{x}'").Join()}]");

			foreach (var database in databases)
			{
				try
				{
					DbRouter.Connection.ChangeDatabase(database);
				}
				catch (Exception exc)
				{
					CsDb.CodeGen.Tracing.Trace($"Fetch architecture for {database} FAILED: {exc.Message}", 1);
					continue;
				}
				CsDb.CodeGen.Tracing.Trace($"Fetch architecture for {database}", 1);

				new DatabaseCreator(this).Generate(database);
			}
		}



		private class DatabaseCreator
		{

			private static readonly string _namingConventionTable = "__CsDb.NamingConventions";

			private static readonly string CreateNaminConventionTableScript = CsGlobal.Storage.Resource.File.Read("CsWpfBase", @"Db\codegen\architecture\generators\sqlScripts\NamingConventionTable_Script_Sql.txt");

			private static int GetMaxDistanceToOutside(CsDbArcTable table)
			{
				if (table.Relations.Count == 0)
					return 0;

				var maxValue = int.MinValue;
				var outside = true;
				foreach (var relation in table.Relations)
				{
					if (relation.PrimaryKey.Owner == table)
						continue;
					outside = false;
					var distance = GetMaxDistanceToOutside(relation.PrimaryKey.Owner) + 1;
					if (maxValue < distance)
						maxValue = distance;
				}
				return outside ? 0 : maxValue;
			}


			private static object ToDefaultValue(string defaultValue, Type dotNetType)
			{
				if (defaultValue == null)
					return null;


				if (dotNetType.IsNumericType())
					return Convert.ChangeType(defaultValue.Trim('(', ')'), dotNetType);


				if (dotNetType == typeof (DateTime))
				{
					if (defaultValue.ToLower() == "(getdate())")
						return CsDb.CodeGen.Statics.DateTimeNowFunction;
					return Convert.ToDateTime(defaultValue.Trim('(', ')', '\''));
				}
				if (dotNetType == typeof (Guid))
				{
					if (defaultValue.ToLower() == "(newid())")
						return CsDb.CodeGen.Statics.NewGuidFunction;
					throw new InvalidOperationException("Unknown Default value found. Include a conversion to a valid C# instance. If it is a function please use the 'CsDb.CodeGen.Statics' name space to set the value to a string.");
				}
				if (dotNetType == typeof (string))
				{
					var lower = defaultValue.ToLower();
					if (lower == "(null)")
						return null;
					if (lower.StartsWith("('") && lower.EndsWith("')"))
						return defaultValue.Substring(2, defaultValue.Length - 4);
					if (lower.StartsWith("(n'") && lower.EndsWith("')"))
						return defaultValue.Substring(3, defaultValue.Length - 5);
					throw new InvalidOperationException("Unknown Default value found. Include a conversion to a valid C# instance. If it is a function please use the 'CsDb.CodeGen.Statics' name space to set the value to a string.");
				}
				if (dotNetType == typeof (bool))
				{
					if (defaultValue.ToLower() == "((0))")
						return false;
					if (defaultValue.ToLower() == "((1))")
						return true;
				}


				// Include a conversion to a valid C# instance.
				// If it is a function please use the 'CsDb.CodeGen.Statics' name space
				// Other exception in code generation will follow. Use description there to include conversion logic.

				throw new InvalidOperationException("Unknown Default value found. Include a conversion to a valid C# instance. If it is a function please use the 'CsDb.CodeGen.Statics' name space to set the value to a string.");
			}

			private static Rule ToRule(string value)
			{
				switch (value)
				{
					case "NO ACTION":
						return Rule.None;
					case "CASCADE":
						return Rule.Cascade;
					case "SET NULL":
						return Rule.SetNull;
				}
				throw new NotImplementedException();
			}

			public DatabaseCreator(CsDbArcGen_Sql owner)
			{
				Owner = owner;
			}

			private CsDbArcGen_Sql Owner { get; }
			private CsDbRouter DbRouter => Owner.DbRouter;
			private CsDbArcDatabase Architecture { get; set; }
			/// <summary>Table to primary column name.</summary>
			private Dictionary<string, string> PrimaryColumnNameMapping { get; set; }


			public void Generate(string name)
			{


				Architecture = Owner.Architecture.NewDatabase(name);

				FetchNamingConventions();
				FetchPrimaryColumnNames();
				FetchTablesAndViews();
				FetchColumns();
				FetchRelations();
				CalculateLevels();

			}

			private void FetchNamingConventions()
			{
				var result = ExecuteCommand(DbRouter, $"SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{_namingConventionTable}'");
				if (result.Rows[0][0].Equals(0))
				{
					using (var cmd = DbRouter.GetCommand())
					{
						cmd.CommandText = CreateNaminConventionTableScript;
						cmd.CommandType = CommandType.Text;
						cmd.ExecuteNonQuery();
					}
					CsDb.CodeGen.Tracing.Trace($"CREATED TABLE [{_namingConventionTable}] IN DATABASE", 2);
					return;
				}

				var namingConventions = ExecuteCommand(DbRouter, $"SELECT * FROM [{_namingConventionTable}]");
				var rows = namingConventions.Rows.OfType<DataRow>().ToArray();
				foreach (var row in rows)
				{
					var convention = NamingConvention.ParseFromRow(row);
					if (((string) row["Type"]).StartsWith("Table"))
						Architecture.TableNameConventions.Add(convention.NativeName, convention);
					else
						Architecture.RelationNameConventions.Add(convention.NativeName, convention);
				}
			}

			private void FetchPrimaryColumnNames()
			{
				var dataTable = DbRouter.Connection.GetSchema("IndexColumns");
				//Debug.WriteLine(dataTable.GetShortTableString("", 20));
				//Filter 231 cause these keys are no primary keys
				PrimaryColumnNameMapping = dataTable.Rows.Cast<DataRow>().Where(x => x["constraint_name"].ToString().StartsWith("PK_")).GroupBy(x => x["table_name"].ToString()).ToDictionary(x => x.Key, x => x.First()["column_name"].ToString());
			}

			private void FetchTablesAndViews()
			{
				var schemaTable = DbRouter.Connection.GetSchema("Tables");

				foreach (var row in schemaTable.Rows.OfType<DataRow>())
				{
					var name = row["TABLE_NAME"].ToString();
					var type = row["TABLE_TYPE"].ToString();
					if (type == "BASE TABLE")
					{
						if (name != _namingConventionTable)
							Architecture.CreateTable(name);
					}
					else if (type == "VIEW")
						Architecture.CreateView(name);
					else
						throw new Exception("Invalid object");

				}
			}

			private void FetchColumns()
			{
				var descriptionTable = ExecuteCommand(DbRouter, "select st.name [Table], sc.name [Column], sep.value [Description] from sys.tables st inner join sys.columns sc on st.object_id = sc.object_id left join sys.extended_properties sep on st.object_id = sep.major_id and sc.column_id = sep.minor_id and sep.name = 'MS_Description'");


				foreach (var tableOrView in Architecture.Tables.OfType<CsDbArcTableViewBase>().Concat(Architecture.Views))
				{
					var schemaTable = DbRouter.Connection.GetSchema("Columns", new[] {null, null, tableOrView.Name}); //TODO maybe potential for performance improvement
					var columns = schemaTable.Rows.Cast<DataRow>().OrderBy(x => x["ORDINAL_POSITION"]).ToArray();

					string primaryColumnName;
					PrimaryColumnNameMapping.TryGetValue(tableOrView.Name, out primaryColumnName);


					foreach (var row in columns)
					{
						var column = tableOrView.CreateColumn();

						column.Name = row["COLUMN_NAME"].ToString();

						column.Type = row["DATA_TYPE"].ToString();
						column.Nullable = row["IS_NULLABLE"] == DBNull.Value ? null : row["IS_NULLABLE"].ToString();
						column.MaxLength = row["CHARACTER_MAXIMUM_LENGTH"] == DBNull.Value ? null : row["CHARACTER_MAXIMUM_LENGTH"].ToString();
						column.DefaultValue = row["COLUMN_DEFAULT"] == DBNull.Value ? null : row["COLUMN_DEFAULT"].ToString();

						column.DotNetType = CsDb.CodeGen.Convert.ToType((SqlDbType) Enum.Parse(typeof (SqlDbType), column.Type, true));
						column.DotNetIsNullable = column.Nullable == "YES";
						column.DotNetMaxLength = column.MaxLength == null ? -1 : Convert.ToInt32(column.MaxLength);
						column.DotNetDefaultValue = ToDefaultValue(column.DefaultValue, column.DotNetType);


						var descriptionRows = descriptionTable.Select($"Table = '{tableOrView.Name}' AND Column = '{column.Name}'");
						if (descriptionRows.Length != 0)
							column.Description = descriptionRows[0]["Description"] == DBNull.Value ? null : descriptionRows[0]["Description"].ToString();


						if (column.Name == primaryColumnName)
							((CsDbArcTable) tableOrView).PrimaryColumn = column;
					}
				}
			}

			private void FetchRelations()
			{

				var schemaTable = ExecuteCommand(DbRouter, "SELECT KF.CONSTRAINT_NAME Name, KF.TABLE_NAME FK_Table, KF.COLUMN_NAME FK_Column, KP.TABLE_NAME PK_Table, KP.COLUMN_NAME PK_Column, RC.MATCH_OPTION MatchOption, RC.UPDATE_RULE UpdateRule, RC.DELETE_RULE DeleteRule FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS RC JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KF ON RC.CONSTRAINT_NAME = KF.CONSTRAINT_NAME JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KP ON RC.UNIQUE_CONSTRAINT_NAME = KP.CONSTRAINT_NAME");


				foreach (DataRow row in schemaTable.Rows)
				{
					var name = row["Name"].ToString();
					var fkTableName = row["FK_Table"].ToString();
					var fkColumnName = row["FK_Column"].ToString();
					var pkTableName = row["PK_Table"].ToString();
					var pkColumnName = row["PK_Column"].ToString();
					var updateRule = ToRule(row["UpdateRule"].ToString());
					var deleteRule = ToRule(row["DeleteRule"].ToString());



					CsDbArcTable fkTable = null;
					CsDbArcTable pkTable = null;
					foreach (var table in Architecture.Tables)
					{
						if (table.Name == fkTableName)
							fkTable = table;
						if (table.Name == pkTableName)
							pkTable = table;
						if (fkTable != null && pkTable != null)
							break;
					}
					if (fkTable == null || pkTable == null)
						continue;

					var fkColumn = fkTable.Columns.FirstOrDefault(x => x.Name == fkColumnName);
					var pkColumn = pkTable.Columns.FirstOrDefault(x => x.Name == pkColumnName);

					if (fkColumn == null || pkColumn == null)
						continue;

					var relation = CsDbArcRelation.Create(name, pkColumn, fkColumn);
					relation.DeleteRule = deleteRule;
					relation.UpdateRule = updateRule;
				}
			}

			private void CalculateLevels()
			{
				foreach (var table in Architecture.Tables)
				{
					table.Level = GetMaxDistanceToOutside(table);
				}
			}
		}
	}
}