// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using BillingTool.btScope.configuration._enums;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class CommandLine_GeneralSetting : Base
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


		private StartupModes _startupMode;

		private CommandLine_GeneralSetting()
		{
		}

		/// <summary>The mode decides how the application will be started.</summary>
		public StartupModes StartupMode
		{
			get { return _startupMode; }
			set { SetProperty(ref _startupMode, value); }
		}


		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		public void Interpret(List<string> commands)
		{
			foreach (var item in commands.ToArray())
			{
				var found = true;
				StartupModes mode;
				if (Enum.TryParse(item, true, out mode))
					StartupMode = mode;
				else
					found = false;


				if (found)
					commands.Remove(item);
			}
		}
	}



}