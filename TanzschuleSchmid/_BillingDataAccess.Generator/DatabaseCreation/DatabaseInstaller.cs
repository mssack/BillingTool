// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace BillingDataAccessGenerator.DatabaseCreation
{
	/// <summary>
	///     The database installer is used for the first installation of the database. This should only be useful for setting up a new billing line, or when
	///     a new customer use this software. The database which is used is created with the 'System.Data.SqlServerCe' Version='4.0.8482.1' can be found and
	///     referenced by searching in NUGET manager under 'System.Data.SqlServerCe'. The name of the NUGET package is 'System.Data.SqlServerCe_unofficial'.
	/// </summary>
	[Serializable]
	public sealed class DatabaseInstaller : Base
	{
		private SqlCeConnection _connection;
		private string _databaseFilePath;


		/// <summary>ctor</summary>
		public DatabaseInstaller(string databaseFilePath)
		{
			DatabaseFilePath = databaseFilePath;
		}


		#region Overrides/Interfaces
		/// <summary>All references to this object are freed by setting <see cref="Base.PropertyChanged" /> to null. Closes the database connection.</summary>
		public override void Dispose()
		{
			base.Dispose();


			if (Connection == null)
				return;

			Connection.Close();
			Connection.Dispose();
			Connection = null;
		}
		#endregion


		/// <summary>The file path to SQLCE 4.0 Database.</summary>
		public string DatabaseFilePath
		{
			get { return _databaseFilePath; }
			private set { SetProperty(ref _databaseFilePath, value); }
		}
		/// <summary>The SQLCe connection used to create tables and relation inside the database.</summary>
		private SqlCeConnection Connection
		{
			get { return _connection; }
			set { SetProperty(ref _connection, value); }
		}

		/// <summary>Installs a fresh new database. Generates a new .sdf file and fills it with the appropriate tables and relations.</summary>
		public void Install(bool removeExistingFile = false)
		{
			if (string.IsNullOrEmpty(DatabaseFilePath))
				throw new IOException("Invalid database file path. The string can not be empty.");

			var dbFile = new FileInfo(DatabaseFilePath);

			if (dbFile.Exists && removeExistingFile == false)
				throw new IOException("The database file already exist. You have to delete it before you can install a new one.");

			dbFile.CreateDirectory_IfNotExists();
			dbFile.DeleteFile_IfExists();

			CreateDatabaseFile();


			OpenDatabaseFile();

			CreateTables();
			CreateRelations();

			CloseDatabaseFile();
		}


		/// <summary>Creates a fresh new .sdf file.</summary>
		private void CreateDatabaseFile()
		{
			var engine = new SqlCeEngine($"data source={DatabaseFilePath}");
			engine.CreateDatabase();
			engine.Dispose();
		}

		/// <summary>opens the <see cref="Connection" />.</summary>
		private void OpenDatabaseFile()
		{
			Connection = new SqlCeConnection($"data source={DatabaseFilePath}");
			Connection.Open();
		}
		/// <summary>Closes the <see cref="Connection" />.</summary>
		private void CloseDatabaseFile()
		{
			Connection.Close();
			Connection.Dispose();
			Connection = null;
		}

		private void CreateTables()
		{
			string[] orderedScripts = { "CreateConfigurationsTable", "CreateBelegDatenTable", "CreatePrintedBelegeTable", "CreateMailedBelegeTable", "CreateBelegPostensTable", "CreateLogsTable", "CreatePostensTable", "CreateSteuersätzeTable" };

			orderedScripts.ForEach(x=> Execute_SqlScript(Get_SqlScript(x)));
		}
		private void CreateRelations()
		{
			var relationScripts = Get_SqlScript("CreateRelations").Split("\r\n\r\n");
			relationScripts.ForEach(x =>
			{
				Debug.WriteLine(x);
				Execute_SqlScript(x);
			});
		}

		/// <summary>
		///     Returns the file content of a text file inside folder 'BillingDataAccessGenerator\DatabaseCreation\SQL Scripts\{
		///     <paramref name="scriptName" />}.txt'
		/// </summary>
		private string Get_SqlScript(string scriptName)
		{
			return CsGlobal.Storage.Resource.File.Read("BillingDataAccessGenerator", $@"DatabaseCreation\SQL Scripts\{scriptName}.txt");
		}

		/// <summary>Executes a SQL command on the connection.</summary>
		private void Execute_SqlScript(string script)
		{
			var command = new SqlCeCommand(script, Connection);
			// ReSharper disable once ObjectCreationAsStatement
			new SqlCeDataAdapter(command);
			command.ExecuteNonQuery();
		}
	}
}