// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.configuration._enums;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	public sealed class ConfigFiles : Base
	{
		private static ConfigFiles _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFiles I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFiles());
				}
			}
		}

		private ConfigFiles()
		{
		}

		/// <summary>
		///     The <see cref="General" /> sub configuration holds all uncategorize able configurations like <see cref="StartupModes" />, the database location
		///     and other general configurations.
		/// </summary>
		public ConfigFile_GeneralSettings General => ConfigFile_GeneralSettings.I;


		/// <summary>
		///     The <see cref="Mail" /> sub configuration holds all configurable properties for the internal mail client like the SMTP server, username, password
		///     and so on.
		/// </summary>
		public ConfigFile_MailSettings Mail => ConfigFile_MailSettings.I;


		/// <summary>
		///     The <see cref="NewBelegData" /> sub configuration holds all configurable properties for <see cref="BelegData" />'s like default
		///     values.
		/// </summary>
		public ConfigFile_NewBelegData NewBelegData => ConfigFile_NewBelegData.I;
	}
}