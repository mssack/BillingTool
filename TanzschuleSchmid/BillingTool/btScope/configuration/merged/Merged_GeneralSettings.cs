// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingTool.btScope.configuration._enums;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.merged
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt"/> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public class Merged_GeneralSettings : Base, IConfigGeneralSettings
	{
		private static Merged_GeneralSettings _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Merged_GeneralSettings I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Merged_GeneralSettings());
				}
			}
		}

		private Merged_GeneralSettings()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The file path to the billing database.</summary>
		public string BillingDatabaseFilePath
		{
			get { return GetMergedValue(setting => setting.BillingDatabaseFilePath); }
			set { throw new InvalidOperationException("You cannot set merged property's."); }
		}
		/// <summary>The mode decides how the application will be started.</summary>
		public StartupModes StartupMode
		{
			get { return GetMergedValue(setting => setting.StartupMode); }
			set { throw new InvalidOperationException("You cannot set merged property's."); }
		}
		#endregion


		/// <summary>Try to get the value from command line configuration. If command line configuration is default use the value from configuration file.</summary>
		private T GetMergedValue<T>(Func<IConfigGeneralSettings, T> get)
		{
			var value = get(Bt.Config.CommandLine.General);
			if (value != null && !Equals(default(T), value))
				return value;
			return get(Bt.Config.File.General);
		}
	}
}