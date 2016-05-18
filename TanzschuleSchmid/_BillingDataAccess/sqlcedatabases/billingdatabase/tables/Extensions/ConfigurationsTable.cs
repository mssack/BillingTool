﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class ConfigurationsTable
	{
		private ConfigurationsTableDefaults _defaults;
		private ConfigurationsTableDesign _design;
		private ConfigurationsTableDataIntegrity _dataIntegrity;



		/// <summary>Contains all default values for different database wide properties.</summary>
		public ConfigurationsTableDefaults Default => _defaults ?? (_defaults = new ConfigurationsTableDefaults(this));

		/// <summary>Contains all design values for different database wide properties.</summary>
		public ConfigurationsTableDesign Design => _design ?? (_design = new ConfigurationsTableDesign(this));

		/// <summary>Contains all data integrity values for different database wide properties.</summary>
		public ConfigurationsTableDataIntegrity DataIntegrity => _dataIntegrity ?? (_dataIntegrity = new ConfigurationsTableDataIntegrity(this));




		internal T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			if (DbProxy == null)
				return default(T);


			var config = GetRow(name, defaultValue == null ? null : defaultValue.ToString());

			if (string.IsNullOrEmpty(config.Value))
			{
				config.Value = defaultValue == null ? null : defaultValue.ToString();
				return defaultValue;
			}

			if (typeof(T) == typeof(Guid))
				return (T) (object) Guid.Parse(config.Value);

			return (T) Convert.ChangeType(config.Value, typeof(T));
		}

		internal void SetValue(object value, [CallerMemberName] string name = null)
		{
			var config = GetRow(name, value?.ToString());
			config.LastChanged = DateTime.Now;
			config.Value = value?.ToString();
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
			config.LastChanged = DateTime.Now;
			Add(config);

			return config;
		}
	}
}