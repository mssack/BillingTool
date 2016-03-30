// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.IO;
using BillingTool.btScope.configuration._enums;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt"/> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_GeneralSettings : ConfigFileBase, IConfigGeneralSettings
	{
		private static ConfigFile_GeneralSettings _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_GeneralSettings I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_GeneralSettings(CsGlobal.Storage.Private.GetFilePathByName("GeneralSettings")));
				}
			}
		}

		private string _billingDatabaseFilePath;
		private StartupModes _startupMode = StartupModes.ProductInformation;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_GeneralSettings(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_GeneralSettings(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>The file path to the billing database.</summary>
		[Key]
		public string BillingDatabaseFilePath
		{
			get { return _billingDatabaseFilePath; }
			set { SetProperty(ref _billingDatabaseFilePath, value); }
		}

		/// <summary>The mode decides how the application will be started.</summary>
		[Key]
		public StartupModes StartupMode
		{
			get { return _startupMode; }
			set { SetProperty(ref _startupMode, value); }
		}
		#endregion
	}
}