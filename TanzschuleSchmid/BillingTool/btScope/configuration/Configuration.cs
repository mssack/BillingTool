// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingTool.btScope.configuration.types;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration
{
	/// <summary>The <see cref="Bt.Config"/> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Configuration : Base
	{
		private static Configuration _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Configuration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Configuration());
				}
			}
		}


		private CommandLineConfiguration _commandLine;
		private ConfigFileConfiguration _file;
		private MergedConfiguration _merged;

		private Configuration()
		{
		}




		/// <summary>
		///     The <see cref="Merged" /> configuration merges the <see cref="File" /> and the <see cref="CommandLine" /> configuration into one valid
		///     configuration set
		/// </summary>
		public MergedConfiguration Merged
		{
			get { return _merged; }
			set { SetProperty(ref _merged, value); }
		}
		/// <summary>The config file configuration is the weakest configuration. This configuration will be used if no other configuration is supplied.</summary>
		public ConfigFileConfiguration File
		{
			get { return _file; }
			set { SetProperty(ref _file, value); }
		}
		/// <summary>The command line parameters overrides all configurations done in the <see cref="File" /> configuration.</summary>
		public CommandLineConfiguration CommandLine
		{
			get { return _commandLine; }
			set { SetProperty(ref _commandLine, value); }
		}
	}
}