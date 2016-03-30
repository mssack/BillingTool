﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Collections.Generic;
using BillingTool.btScope.configuration._enums;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class CommandLine_GeneralSetting : Base, IConfigGeneralSettings
	{
		private static CommandLine_GeneralSetting _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CommandLine_GeneralSetting I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CommandLine_GeneralSetting());
				}
			}
		}


		private string _billingDatabaseFilePath;
		private StartupModes _startupMode;

		private CommandLine_GeneralSetting()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The file path to the billing database.</summary>
		public string BillingDatabaseFilePath
		{
			get { return _billingDatabaseFilePath; }
			set { SetProperty(ref _billingDatabaseFilePath, value); }
		}
		/// <summary>The mode decides how the application will be started.</summary>
		public StartupModes StartupMode
		{
			get { return _startupMode; }
			set { SetProperty(ref _startupMode, value); }
		}
		#endregion


		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		public void Interpret(List<string> commands)
		{
			foreach (var item in commands.ToArray())
			{
				var found = true;


				if (string.Equals(item, StartupModes.Developer.ToString(), StringComparison.OrdinalIgnoreCase))
					StartupMode = StartupModes.Developer;
				else if (string.Equals(item, StartupModes.NewCashBookEntry.ToString(), StringComparison.OrdinalIgnoreCase))
					StartupMode = StartupModes.NewCashBookEntry;
				else if (item.StartsWith(nameof(BillingDatabaseFilePath), StringComparison.OrdinalIgnoreCase))
					BillingDatabaseFilePath = item.Substring(nameof(BillingDatabaseFilePath).Length + 1);
				else
					found = false;


				if (found)
					commands.Remove(item);
			}
		}
	}



}