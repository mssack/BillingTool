// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using BillingTool.RuntimeSettings.types;
using CsWpfBase.Ev.Objects;






namespace BillingTool.RuntimeSettings
{
	/// <summary>The runtime configuration gives feedback about the current application mode and configuration.</summary>
	public sealed class RuntimeConfiguration : Base
	{
		private static RuntimeConfiguration _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static RuntimeConfiguration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new RuntimeConfiguration());
				}
			}
		}


		private string _databaseFilePath;
		private RuntimeModes _runtimeMode;

		private RuntimeConfiguration()
		{
		}

		/// <summary>The file path for the database.</summary>
		public string DatabaseFilePath
		{
			get { return _databaseFilePath; }
			set { SetProperty(ref _databaseFilePath, value); }
		}
		/// <summary>The runtime mode decides which functionality the program provides. This should be decided at application startup.</summary>
		public RuntimeModes RuntimeMode
		{
			get { return _runtimeMode; }
			set { SetProperty(ref _runtimeMode, value); }
		}
	}
}