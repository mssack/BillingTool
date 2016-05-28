// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-22</date>

using System;
using System.Linq;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool._SharedEnumerations;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	public sealed class CommandLines : Base
	{
		private static CommandLines _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CommandLines I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CommandLines());
				}
			}
		}

		private string _currentConfiguration;

		private CommandLines()
		{
		}

		/// <summary>Gets or sets the CurrentConfiguration.</summary>
		public string CurrentConfiguration
		{
			get { return _currentConfiguration; }
			set { SetProperty(ref _currentConfiguration, value); }
		}

		/// <summary>
		///     The <see cref="General" /> sub configuration holds all uncategorize able configurations like <see cref="StartupModes" />, the database location
		///     and other general configurations.
		/// </summary>
		public CommandLine_GeneralSetting General => CommandLine_GeneralSetting.I;


		/// <summary>The <see cref="NewBelegData" /> sub configuration holds all configurable properties for <see cref="BelegData" />'s like default values.</summary>
		public CommandLine_NewBelegData NewBelegData => CommandLine_NewBelegData.I;


		/// <summary>
		///     Interprets command line params passed to the application. Use this method on <see cref="Application.Startup" /> to ensure that the correct
		///     configuration is loaded.
		/// </summary>
		public void Interpret(string[] startParams)
		{
			CurrentConfiguration = startParams.Join(" ");
			var concanatedParams = CurrentConfiguration.Replace("//", "#######ALÖÄSÖ######").Split("/").Select(x => x.Trim().Replace("#######ALÖÄSÖ######", "//")).Where(x => !string.IsNullOrEmpty(x)).ToList();
			General.Interpret(concanatedParams);
			NewBelegData.Interpret(concanatedParams);
		}
	}



}