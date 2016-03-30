// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
	internal class CsDbArcGen_SqlCe : IGenerateCsDbArcContext
	{
		private static DataTable ExecuteCommand(CsDbRouter router, string cmd)
		{
			using (var adapter = router.GetDefaultAdapter(cmd))
			{
				var target = new DataTable();
				adapter.Fill(target);
				return target;
			}
		}

		private readonly CsDbRouter[] _routers;

		/// <summary>The generated architecture</summary>
		protected CsDbArchitecture Architecture { get; } = new CsDbArchitecture();

		public CsDbArcGen_SqlCe(params CsDbRouter[] routers)
		{
			_routers = routers;
			Architecture.Name = "RenameMe";
		}


		#region Overrides/Interfaces
		/// <summary>Generates the architecture.</summary>
		public CsDbArchitecture Generate()
		{

			FetchDatabases();
			return Architecture;
		}
		#endregion


		private void FetchDatabases()
		{

			//CsDb.CodeGen.Tracing.Trace($"Context('{Architecture.Name}') databases: [{_routers.Select(x => $"'{x.Connection.DataSource}'").Join()}]");

			foreach (var router in _routers)
			{


				try
				{
					router.Open();
				}
				catch (Exception exc)
				{
					CsDb.CodeGen.Tracing.Trace($"Fetch architecture for  FAILED: {exc.Message}", 1);
					continue;
				}
				var dataSource = new FileInfo(router.Connection.Database);
				var dataSourceName = dataSource.Name.Substring(0, dataSource.Name.LastIndexOf('.'));
				CsDb.CodeGen.Tracing.Trace($"Fetch architecture for {dataSourceName}", 1);


				new DatabaseCreator(this, router).Generate(dataSourceName);
			}
		}


		private class DatabaseCreator
		{

			private static readonly string _namingConventionTable = "__CsDb.NamingConventions";

			private static readonly string CreateNaminConventionTableScript = CsGlobal.Storage.Resource.File.Read("CsWpfBase", @"Db\codegen\architecture\generators\sqlScripts\NamingConventionTable_Script_SqlCe.txt");

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
					if (defaultValue.ToLower() == "getdate()")
						return CsDb.CodeGen.Statics.DateTimeNowFunction;
					return Convert.ToDateTime(defaultValue.Trim('(', ')', '\''));
				}
				if (dotNetType == typeof (Guid))
				{
					if (defaultValue.ToLower() == "(newid())")
						return CsDb.CodeGen.Statics.NewGuidFunction;
					if (defaultValue.ToLower() == "newid()")
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

			public DatabaseCreator(CsDbArcGen_SqlCe owner, CsDbRouter router)
			{
				Owner = owner;
				Router = router;
			}

			private CsDbArcGen_SqlCe Owner { get; }
			private CsDbRouter Router { get; }
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
				//InsertNamingConventions();
			}

			private void FetchNamingConventions()
			{
				var result = ExecuteCommand(Router, $"SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{_namingConventionTable}'");
				if (result.Rows[0][0].Equals(0))
				{
					using (var cmd = Router.GetCommand())
					{
						cmd.Connection = Router.Connection;
						cmd.CommandText = CreateNaminConventionTableScript;
						cmd.CommandType = CommandType.Text;
						cmd.ExecuteNonQuery();
					}
					CsDb.CodeGen.Tracing.Trace($"CREATED TABLE [{_namingConventionTable}] IN DATABASE", 2);
					return;
				}

				var namingConventions = ExecuteCommand(Router, $"SELECT * FROM [{_namingConventionTable}]");
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

			private void InsertNamingConventions()
			{
				foreach (var table in Architecture.Tables)
				{
					ExecuteCommand(Router, $"INSERT INTO [__CsDb.NamingConventions] (ID, Type, NativeName, Singular, Plural) VALUES ('{Guid.NewGuid()}', 'Table', '{table.Name}', '{table.Name}', '{table.Name}')");
				}
			}

			private void FetchPrimaryColumnNames()
			{
				var descriptionTable = ExecuteCommand(Router, "SELECT * FROM INFORMATION_SCHEMA.INDEXES");
				PrimaryColumnNameMapping = descriptionTable.Rows.Cast<DataRow>().Where(x => (bool) x["PRIMARY_KEY"]).GroupBy(x => x["TABLE_NAME"].ToString()).ToDictionary(x => x.Key, x => x.OrderBy(x1 => x1["ORDINAL_POSITION"]).First()["COLUMN_NAME"].ToString());
			}

			private void FetchTablesAndViews()
			{
				var schemaTable = ExecuteCommand(Router, "SELECT * FROM INFORMATION_SCHEMA.TABLES");

				foreach (var row in schemaTable.Rows.OfType<DataRow>())
				{
					var name = row["TABLE_NAME"].ToString();
					var type = row["TABLE_TYPE"].ToString();
					if (type == "TABLE")
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
				var descriptionTable = ExecuteCommand(Router, "SELECT TABLE_NAME [Table], COLUMN_NAME [Column], DESCRIPTION [Description] FROM INFORMATION_SCHEMA.COLUMNS");


				foreach (var tableOrView in Architecture.Tables.OfType<CsDbArcTableViewBase>().Concat(Architecture.Views))
				{
					var schemaTable = ExecuteCommand(Router, $"SELECT * FROM INFORMATION_SCHEMA.Columns WHERE TABLE_NAME = '{tableOrView.Name}'"); //TODO maybe potential for performance improvement
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
						column.DefaultValue = row["COLUMN_DEFAULT"] == DBNull.Value ? null : row["COLUMN_DEFAULT"].ToString().Trim('\r', '\n');
						

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

				DataTable schemaTable = null;

				if (Router.Connection.ServerVersion == "3.5.8080.0")
				{
					schemaTable = ExecuteCommand(Router, "SELECT Name, FK_Table, FK_Column, TABLE_NAME [PK_Table], COLUMN_NAME [PK_Column],UpdateRule, DeleteRule, second.ORDINAL_POSITION FROM (SELECT a.CONSTRAINT_NAME [Name], CONSTRAINT_TABLE_NAME [FK_Table], COLUMN_NAME [FK_Column], UNIQUE_CONSTRAINT_NAME [uq], UNIQUE_CONSTRAINT_TABLE_NAME [uqt], UPDATE_RULE [UpdateRule], DELETE_RULE [DeleteRule] FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE a join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS b ON a.CONSTRAINT_NAME = b.CONSTRAINT_NAME)  f join INFORMATION_SCHEMA.KEY_COLUMN_USAGE second ON second.CONSTRAINT_NAME = f.uq and second.TABLE_NAME = f.uqt");

				}
				else
				{
					schemaTable = ExecuteCommand(Router, "SELECT Name, FK_Table, FK_Column, TABLE_NAME [PK_Table], COLUMN_NAME [PK_Column],UpdateRule,DeleteRule FROM (SELECT a.CONSTRAINT_NAME [Name], CONSTRAINT_TABLE_NAME [FK_Table], COLUMN_NAME [FK_Column], UNIQUE_CONSTRAINT_NAME [uq], UPDATE_RULE [UpdateRule], DELETE_RULE [DeleteRule] FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE a join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS b ON a.CONSTRAINT_NAME = b.CONSTRAINT_NAME) f join INFORMATION_SCHEMA.KEY_COLUMN_USAGE second ON second.CONSTRAINT_NAME = f.uq");
				}


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