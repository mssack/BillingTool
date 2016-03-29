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
using BillingDataAccess.DatabaseCreation;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.router;






namespace BillingTool.Runtime
{
	/// <summary>The database access module. Used to access data from the database.</summary>
	public static class Db
	{
		/// <summary>
		///     The billing database loaded from <see cref="RuntimeConfiguration.DatabaseFilePath" />. You have to call <see cref="Connect" /> before accessing
		///     this property. On application exit don't forget to save (<see cref="CsDbDataSetBase.SaveUnspecific" />) changes. Then close connection.
		/// </summary>
		public static BillingDatabase Billing { get; private set; }
		private static SqlCeRouter Router { get; set; }


		/// <summary>Creates the database if it does not exist.</summary>
		public static void Init()
		{
			var fileInfo = new FileInfo(RuntimeConfiguration.I.DatabaseFilePath);
			if (fileInfo.Exists)
				return;

			CreateDatabase();
		}

		/// <summary>
		///     Used to connect to database file specified in <see cref="RuntimeConfiguration" />. Take care a second call will result in an
		///     <see cref="InvalidOperationException" />. To ensure connectivity use <see cref="EnsureConnectivity" />.
		/// </summary>
		public static void Connect()
		{
			if (Billing != null)
				throw new InvalidOperationException($"The {nameof(Db)} is already connected. The method {nameof(Connect)} was called twice. Use {nameof(EnsureConnectivity)} instead.");


			Router = new SqlCeRouter(RuntimeConfiguration.I.DatabaseFilePath);
			Router.Open();

			Billing = new BillingDatabase();
			Billing.Set_DbProxy(Router);
			Billing.LoadSchema();
		}

		/// <summary>
		///     Ensures that the database is connected to the program and accessible. This method can be called multiple times while application is running. It
		///     is advisable to call this method before each database call to ensure connectivity.
		/// </summary>
		public static void EnsureConnectivity()
		{
			if (Billing == null)
			{
				Connect();
				return;
			}

			if (Router.State.IsConnected)
				return;
			Router.Open();

			if (!Router.State.IsConnected)
				throw Router.State.LastException;
		}

		/// <summary>
		///     Used to disconnect from database file specified in <see cref="RuntimeConfiguration" />. DANGER all unsaved files will be lost. Ensure that you
		///     call at least <see cref="CsDbDataSetBase.SaveUnspecific" /> before call <see cref="Disconnect" />.
		/// </summary>
		public static void Disconnect()
		{
			if (Billing == null)
				return;

			Billing.Dispose();
			Router.Dispose();
			Billing = null;
		}


		/// <summary>Creates the database. Throws an Exception if it already exists.</summary>
		public static void CreateDatabase()
		{
			var fileInfo = new FileInfo(RuntimeConfiguration.I.DatabaseFilePath);
			CreateDatabase(fileInfo);
		}


		private static void CreateDatabase(FileInfo fi)
		{
			using (var installer = new DatabaseInstaller(fi.FullName))
			{
				installer.Install();
			}
			Logs.New(LogTitels.DatenbankErstellt, "Eine neue Datenbank wurde erstellt", LogTypes.Information);
		}



		/// <summary>Used to connect to the file, provides all connectivity functions needed for the <see cref="BillingDatabase" />.</summary>
		private class SqlCeRouter : CsDbRouter
		{
			private readonly string _databaseFilePath;

			public SqlCeRouter(string databaseFilePath)
			{
				_databaseFilePath = databaseFilePath;
			}


			#region Overrides/Interfaces
			/// <summary>Executes a command and delivers the result.</summary>
			/// <returns>
			///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
			///     rows.
			/// </returns>
			public override DataSet ExecuteDataSetCommand(string command, object tag = null)
			{
				var commands = command.Split(';');
				var targetSet = new DataSet();
				foreach (var cmd in commands)
				{
					var table = ExecuteCommand(cmd);
					targetSet.Tables.Add(table);
				}
				return targetSet;
			}

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
}