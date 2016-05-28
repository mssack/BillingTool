// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
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

		private Configuration()
		{
		}


		/// <summary>Contains all local (for the current Environment) specific settings. Like mail setting, database path or others.</summary>
		public ConfigFile_Local Local => ConfigFile_Local.I;

		/// <summary>
		///     The <see cref="Control" /> scope contains all runtime relevant settings. This settings will manipulate how the user can interact with the
		///     program. Or this settings can be used to pre-configure informations for the user.
		/// </summary>
		public Control Control => Control.I;
	}
}