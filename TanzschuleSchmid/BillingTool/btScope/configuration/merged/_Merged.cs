// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.configuration._enums;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.merged
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	public sealed class MergedConfiguration : Base
	{
		private static MergedConfiguration _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static MergedConfiguration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new MergedConfiguration());
				}
			}
		}

		private MergedConfiguration()
		{
		}


		/// <summary>
		///     The <see cref="General" /> sub configuration holds all uncategorize able configurations like <see cref="StartupModes" />, the database location
		///     and other general configurations.
		/// </summary>
		public Merged_GeneralSettings General => Merged_GeneralSettings.I;


		/// <summary>
		///     The <see cref="NewCashBookEntry" /> sub configuration holds all configurable properties for <see cref="CashBookEntry" />'s like default
		///     values.
		/// </summary>
		public Merged_NewCashBookEntry NewCashBookEntry => Merged_NewCashBookEntry.I;
	}
}