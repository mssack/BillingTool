// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-06</date>

using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolDataAccess.DatabaseCreation
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

			Execute_TableScripts();
			Execute_RelationScripts();

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


		/// <summary>Executes all scripts inside table folder. Files have to have property 'embedded_Resource' set.</summary>
		private void Execute_TableScripts()
		{
			var callingAssembly = Assembly.GetCallingAssembly();
			var manifestResourceNames = callingAssembly.GetManifestResourceNames();
			var tableScripts = manifestResourceNames
										.Where(x => x.Contains("SqlCeScripts.Tables"))
										.SelectMany(x => Get_SqlScript(x).Split("#Split#"))
										.Select(x => x.Trim(' ', '\r', '\n'))
										.ToArray();


			tableScripts.ForEach(Execute_SqlScript);
		}

		/// <summary>Executes all scripts inside relation folder. Files have to have property 'embedded_Resource' set.</summary>
		private void Execute_RelationScripts()
		{
			var relationScripts = Assembly.GetCallingAssembly().GetManifestResourceNames()
											.Where(x => x.Contains("SqlCeScripts.Relations"))
											.SelectMany(x => Get_SqlScript(x).Split("#Split#"))
											.Select(x => x.Trim(' ', '\r', '\n'))
											.ToArray();


			relationScripts.ForEach(Execute_SqlScript);
		}

		/// <summary>
		///     Returns the file content of a text file inside folder 'BillingDataAccessGenerator\DatabaseCreation\SQL Scripts\{
		///     <paramref name="scriptName" />}.txt'
		/// </summary>
		private string Get_SqlScript(string scriptName)
		{
			// ReSharper disable once AssignNullToNotNullAttribute
			using (var file = new StreamReader(Assembly.GetCallingAssembly().GetManifestResourceStream(scriptName)))
				return file.ReadToEnd();
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