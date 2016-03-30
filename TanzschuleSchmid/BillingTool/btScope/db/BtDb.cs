// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.IO;
using BillingDataAccess.DatabaseCreation;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.Router;
using BillingTool.btScope.configuration.types;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.db
{
	/// <summary>THe <see cref="Bt.Db" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class BtDb : Base
	{
		private static BtDb _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BtDb I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BtDb());
				}
			}
		}



		private BillingDatabase _billing;
		private SqlCeRouter _router;

		private BtDb()
		{
		}

		/// <summary>
		///     The billing database loaded from <see cref="CommandLineConfiguration.DatabaseFilePath" />. You have to call <see cref="Connect" /> before
		///     accessing this property. On application exit don't forget to save (<see cref="CsDbDataSetBase.SaveUnspecific" />) changes. Then close connection.
		/// </summary>
		public BillingDatabase Billing
		{
			get { return _billing; }
			private set { SetProperty(ref _billing, value); }
		}
		/// <summary>The Router is the main connectivity tool. It is used to connect the <see cref="Billing" /> database with the SQLCe database.</summary>
		private SqlCeRouter Router
		{
			get { return _router; }
			set { SetProperty(ref _router, value); }
		}



		/// <summary>Creates the database if it does not exist.</summary>
		public void Init()
		{
			var fileInfo = new FileInfo(CommandLineConfiguration.I.DatabaseFilePath);
			if (fileInfo.Exists)
				return;

			CreateDatabase();
		}

		/// <summary>
		///     Used to connect to database file specified in <see cref="CommandLineConfiguration" />. Take care a second call will result in an
		///     <see cref="InvalidOperationException" />. To ensure connectivity use <see cref="EnsureConnectivity" />.
		/// </summary>
		public void Connect()
		{
			if (Billing != null)
				throw new InvalidOperationException($"The {nameof(BtDb)} is already connected. The method {nameof(Connect)} was called twice. Use {nameof(EnsureConnectivity)} instead.");


			Router = new SqlCeRouter(CommandLineConfiguration.I.DatabaseFilePath);
			Router.Open();

			Billing = new BillingDatabase();
			Billing.Set_DbProxy(Router);
			Billing.LoadSchema();
		}

		/// <summary>
		///     Ensures that the database is connected to the program and accessible. This method can be called multiple times while application is running. It
		///     is advisable to call this method before each database call to ensure connectivity.
		/// </summary>
		public void EnsureConnectivity()
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
		///     Used to disconnect from database file specified in <see cref="CommandLineConfiguration" />. DANGER all unsaved files will be lost. Ensure that
		///     you call at least <see cref="CsDbDataSetBase.SaveUnspecific" /> before call <see cref="Disconnect" />.
		/// </summary>
		public void Disconnect()
		{
			if (Billing == null)
				return;

			Billing.Dispose();
			Router.Dispose();
			Billing = null;
		}


		/// <summary>Creates the database. Throws an Exception if it already exists.</summary>
		public void CreateDatabase()
		{
			var fileInfo = new FileInfo(CommandLineConfiguration.I.DatabaseFilePath);
			CreateDatabase(fileInfo);
		}


		private void CreateDatabase(FileInfo fi)
		{
			using (var installer = new DatabaseInstaller(fi.FullName))
			{
				installer.Install();
			}
			Bt.Logging.New(LogTitels.DatenbankErstellt, "Eine neue Datenbank wurde erstellt");
		}
	}
}