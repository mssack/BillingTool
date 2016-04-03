// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class ConfigurationsTable
	{
		/// <summary>
		///     The last applied <see cref="BelegData.Nummer" />. Increment this number by one each time a new <see cref="BelegData" /> is added to the
		///     database.
		/// </summary>
		public int LastBelegNummer
		{
			get { return GetValue(1); }
			set { SetValue(value); }
		}
		/// <summary>Each time a new <see cref="BelegData" /> is added the <see cref="BelegData.BetragBrutto" /> has to be added to the
		///     <see cref="Umsatzzähler" />.</summary>
		public decimal Umsatzzähler
		{
			get { return GetValue(0); }
			set { SetValue(value); }
		}
		/// <summary>The last used <see cref="Steuersatz.Kürzel" />. Each time a new <see cref="Steuersatz" /> is added increment this value by one.</summary>
		public char LastSteuersatzKürzel
		{
			get { return GetValue((char)('A' - 1)); }
			set { SetValue(value); }
		}

		private void LoadConfigurations()
		{
			DownloadRows();

		}

		private T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			var config = GetRow(name, defaultValue == null ? null : defaultValue.ToString());

			if (string.IsNullOrEmpty(config.Value))
			{
				config.Value = defaultValue == null ? null : defaultValue.ToString();
				return defaultValue;
			}

			return (T) Convert.ChangeType(config.Value, typeof (T));
		}

		private void SetValue(object value, [CallerMemberName] string name = null)
		{
			var config = GetRow(name, value.ToString());
			config.Value = value.ToString();
		}

		private Configuration GetRow(string name, string defaultVal)
		{
			if (!HasBeenLoaded)
				DownloadRows();

			var config = Find(name);
			if (config != null)
				return config;


			config = NewRow();
			config.Name = name;
			config.Value = defaultVal;
			Add(config);

			return config;
		}
	}
}