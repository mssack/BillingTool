// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using BillingTool._SharedEnumerations;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.control
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class Control_General : Base
	{
		private static Control_General _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Control_General I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Control_General());
				}
			}
		}


		private StartupModes _startupMode;

		private Control_General()
		{
		}

		/// <summary>The mode decides how the application will be started or which next action will be performed.</summary>
		public StartupModes StartupMode
		{
			get { return _startupMode; }
			set { SetProperty(ref _startupMode, value); }
		}


		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		internal void Interpret(List<string> commands)
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