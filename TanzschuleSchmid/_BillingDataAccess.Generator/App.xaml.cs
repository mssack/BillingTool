// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Windows;
using CsWpfBase.Db;
using CsWpfBase.Db.router;
using CsWpfBase.Global;






namespace BillingDataAccessGenerator
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		/// <summary>the relative path to the sample db inside project.</summary>
		public string SampleDatabaseFile => Path.Combine(new FileInfo("a").Directory.Parent.Parent.FullName, "SampleDb.sdf");
		/// <summary>the relative path to the target project folder.</summary>
		public string TargetProjectFolder => Path.Combine(new FileInfo("a").Directory.Parent.Parent.Parent.FullName, "_BillingDataAccess");


		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			GenerateDatabaseClasses();
			CheckNamingConventions();
			CsGlobal.App.Exit();
		}

		/// <summary>Inserts the right naming conventions. If the database file is newly generated you have to execute the db class generator two times.</summary>
		private void CheckNamingConventions()
		{
			var router = new SqlCeRouter(SampleDatabaseFile);
			var table = new DataTable("__CsDb.NamingConventions");
			router.LoadTable(table);

			if (table.Rows.Count != 0)
				return;

			table.Rows.Add(Guid.NewGuid(), "Table", "CashBook", "CashBookEntry", "CashBook");
			table.Rows.Add(Guid.NewGuid(), "Table", "Logs", "Log", "Logs");

			router.SaveChanges(table);
		}

		private void GenerateDatabaseClasses()
		{
			var architecture = CsDb.CodeGen.Create.Architecture_From_SqlCe(new SqlCeRouter(SampleDatabaseFile));
			architecture.Name = "SqlCeDatabases";
			var codeBundle = architecture.GetCodeBundle();
			codeBundle.SetPaths(Path.Combine(TargetProjectFolder, ""), "BillingDataAccess", Path.Combine(TargetProjectFolder, "_BillingDataAccess.csproj"));
			codeBundle.Write();
		}
	}



	internal class SqlCeRouter : CsDbRouter
	{
		private readonly string _databaseFilePath;

		public SqlCeRouter(string databaseFilePath)
		{
			_databaseFilePath = databaseFilePath;
		}


		#region Overrides/Interfaces
		/// <summary>Creates a new <see cref="DbDataAdapter" /> of the specific type using the command. Do not change any settings of the data adapter.</summary>
		public override DbDataAdapter GetAdapter(DbCommand cmd)
		{
			return new SqlCeDataAdapter((SqlCeCommand) cmd);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommand" />.</summary>
		public override DbCommand GetCommand()
		{
			return new SqlCeCommand();
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommandBuilder" />. Use <paramref name="adapter" /> for initialization.</summary>
		public override DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter)
		{
			return new SqlCeCommandBuilder((SqlCeDataAdapter) adapter);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbConnection" />.</summary>
		public override DbConnection Initialize()
		{
			return new SqlCeConnection($"data source={_databaseFilePath}");
		}
		#endregion
	}
}