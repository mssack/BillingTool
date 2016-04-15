// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using System.IO;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope.configuration.commandLine;
using BillingTool.Exceptions;
using BillingTool.Windows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.Functions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Functions : Base
	{
		private static Functions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Functions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Functions());
				}
			}
		}

		private Functions()
		{
		}


		/// <summary>Sets the application exit code by setting property <see cref="Environment.ExitCode" /> according.</summary>
		public void SetExitCode(ExitCodes code)
		{
			Environment.ExitCode = (int) code;
		}

		internal void SetExitCode(BillingToolException.Types type)
		{
			SetExitCode((ExitCodes)type);
		}
	}
}