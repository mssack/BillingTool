// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.IO;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.types
{
	/// <summary>The default program configuration. This configuration is used whenever no start params are defined.</summary>
	public sealed class ConfigFileConfiguration : ConfigFileBase
	{
		private static ConfigFileConfiguration _instance;
		private static readonly object SingletonLock = new object();


		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFileConfiguration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFileConfiguration(CsGlobal.Storage.Private.GetFilePathByName("StaticConfiguration")));
				}
			}
		}


		private string _databaseFilePath;

		private ConfigFileConfiguration(FileInfo path) : base(path)
		{
		}
		private ConfigFileConfiguration(Uri packUri) : base(packUri)
		{
		}

		/// <summary>The data base file path which is used to access the database.</summary>
		public string DatabaseFilePath
		{
			get { return _databaseFilePath; }
			set { SetProperty(ref _databaseFilePath, value); }
		}
	}
}