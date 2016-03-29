// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
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
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			GenerateDatabaseClasses();
			CsGlobal.App.Exit();
		}

		private void GenerateDatabaseClasses()
		{
			string sampleDatabase = $@"C:\Users\chris\Desktop\testdatenbank1.sdf";
			var architecture = CsDb.CodeGen.Create.Architecture_From_SqlCe(new SqlCeRouter(sampleDatabase));
			architecture.Name = "SqlCeDatabases";
			var codeBundle = architecture.GetCodeBundle();
			var parentDirectory = Path.Combine(new FileInfo("a").Directory.Parent.Parent.Parent.FullName, "_BillingDataAccess");
			codeBundle.SetPaths(Path.Combine(parentDirectory, ""), "BillingDataAccess", Path.Combine(parentDirectory, "_BillingDataAccess.csproj"));
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