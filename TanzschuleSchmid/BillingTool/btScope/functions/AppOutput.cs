﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-30</date>

using System;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.AppOutput" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class AppOutput : Base
	{
		private static AppOutput _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static AppOutput I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new AppOutput());
				}
			}
		}

		private AppOutput()
		{
		}


		/// <summary>Sets the application exit code by setting property <see cref="Environment.ExitCode" /> according.</summary>
		public void SetExitCode(ExitCodes code)
		{
			Environment.ExitCode = (int) code;
		}

		internal void SetExitCode(BillingToolException.Types type)
		{
			SetExitCode((ExitCodes) type);
		}
	}
}