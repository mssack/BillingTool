// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Data.Common;
using System.Data.SqlServerCe;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.router;






namespace BillingTool.Runtime
{
	/// <summary>The database access module. Used to access data from the database.</summary>
	public static class Db
	{
		/// <summary>
		///     The billing database loaded from <see cref="RuntimeConfiguration.DatabaseFilePath" />. You have to call <see cref="Connect" /> before accessing
		///     this property. On application exit dont forget to save (<see cref="CsDbDataSetBase.SaveUnspecific" />) changes. Then close connection.
		/// </summary>
		public static BillingDatabase Billing { get; private set; }


		/// <summary>Used to connect to database file specified in <see cref="RuntimeConfiguration" />.</summary>
		public static void Connect()
		{
			if (Billing != null)
			{
				Disconnect();
			}
			var router = new SqlCeRouter(RuntimeConfiguration.I.DatabaseFilePath);
			router.Open();

			Billing = new BillingDatabase();
			Billing.Set_DbProxy(router);
		}

		/// <summary>Used to disconnect from database file specified in <see cref="RuntimeConfiguration" />. DANGER all unsaved files will be lost.</summary>
		public static void Disconnect()
		{
			if (Billing == null)
				return;

			((SqlCeRouter) Billing.DbProxy).Dispose();
			Billing.Dispose();
			Billing = null;
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