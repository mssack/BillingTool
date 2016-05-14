// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-15</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.Router;
using BillingTool.btScope.configuration.merged;
using BillingTool.Exceptions;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.db
{
	/// <summary>THe <see cref="Bt.Db" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Db : Base
	{
		private static Db _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Db I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Db());
				}
			}
		}



		private BillingDatabase _billing;
		private SqlCeRouter _router;

		private Db()
		{
		}

		/// <summary>
		///     The billing database loaded from <see cref="MergedConfiguration" />. You have to call <see cref="Connect" /> before accessing this property. On
		///     application exit don't forget to save (<see cref="CsDbDataSetBase.SaveUnspecific" />) changes. Then close connection.
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

		/// <summary>DO not call this.</summary>
		public void EnsureConnectivity()
		{
			if (Billing == null)
			{
				try
				{
					Connect();
				}
				catch (Exception exc)
				{
					throw new BillingToolException(BillingToolException.Types.No_DatabaseConnectionPossible, "Die Verbindung zur Datenbank konnte nicht aufgebaut werden. Siehe innere Exception", exc);
				}
			}

			if (Router.State.IsConnected)
				return;

			Router.Open();
			if (!Router.State.IsConnected)
				throw new BillingToolException(BillingToolException.Types.No_DatabaseConnectionPossible, "Die Verbindung zur Datenbank konnte nicht aufgebaut werden. Siehe innere Exception", Router.State.LastException); 
		}


		/// <summary>Used to determine whether the connection to the database is opened.</summary>
		public bool IsConnected()
		{
			return Router.State.IsConnected;
		}

		/// <summary>
		///     Used to connect to database file specified in <see cref="MergedConfiguration" />. Take care a second call will result in an
		///     <see cref="InvalidOperationException" />. To ensure connectivity use <see cref="EnsureConnectivity" />.
		/// </summary>
		private void Connect()
		{
			if (Billing != null)
				throw new InvalidOperationException($"The {nameof(Db)} is already connected. The method {nameof(Connect)} was called twice. Use {nameof(Bt.AppOutput)} instead.");


			Router = new SqlCeRouter(Bt.Config.File.KassenEinstellung.BillingDatabaseFilePath);
			Router.Open();

			Billing = new BillingDatabase();
			Billing.Set_DbProxy(Router);
			Billing.LoadSchema();
		}

		/// <summary>
		///     Used to disconnect from database file specified in <see cref="MergedConfiguration" />. DANGER all unsaved files will be lost. Ensure that you
		///     call at least <see cref="CsDbDataSetBase.SaveUnspecific" /> before call <see cref="Disconnect" />.
		/// </summary>
		private void Disconnect()
		{
			if (Billing == null)
				return;

			Billing.Dispose();
			Router.Dispose();
			Billing = null;
		}
	}
}