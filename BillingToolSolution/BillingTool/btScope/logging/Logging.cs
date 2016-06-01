// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.logging
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class Logging
	{
		private static Logging _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Logging I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Logging());
				}
			}
		}

		private Logging()
		{
		}


		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		/// <param name="filePath">!!!DO NOT PASS PARAMETER!!!</param>
		/// <param name="method">!!!DO NOT PASS PARAMETER!!!</param>
		public void New(string titel, string content, LogTypes logType = LogTypes.Undefined, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{
			Debug.Assert(filePath != null, "filePath != null");

			if (logType == LogTypes.Fatal && !Bt.IsInitialized())
			{
				return;
			}
		}

		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		/// <param name="filePath">!!!DO NOT PASS PARAMETER!!!</param>
		/// <param name="method">!!!DO NOT PASS PARAMETER!!!</param>
		public void New(LogTitels titel, string content, LogTypes logType = LogTypes.Undefined, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{
			New(titel.GetDescription(), content, logType, filePath, method);
		}



	}
}