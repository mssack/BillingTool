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
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.logging
{
	/// <summary>This class is used for Logging purpose.</summary>
	public class BtLogging
	{
		private static BtLogging _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BtLogging I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BtLogging());
				}
			}
		}

		private BtLogging()
		{
		}


		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		/// <param name="filePath">!!!DO NOT PASS PARAMETER!!!</param>
		/// <param name="method">!!!DO NOT PASS PARAMETER!!!</param>
		public void New(string titel, string content, LogTypes logType = LogTypes.Information, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{
			Debug.Assert(filePath != null, "filePath != null");


			var fileInfo = new FileInfo(filePath);

			Bt.Db.EnsureConnectivity();
			var log = Bt.Db.Billing.Logs.NewRow();
			log.Type = logType;
			log.Title = titel;
			log.CodePosition = fileInfo.Name.Replace(".xaml", "").Replace(".cs", "").Replace(fileInfo.Extension, "") + "." + method + "(~)";
			log.CommandLine = Environment.GetCommandLineArgs().Skip(1).Join(" ");
			log.Content = content;
			Bt.Db.Billing.Logs.Add(log);
			Bt.Db.Billing.Logs.SaveChanges();
			Bt.Db.Billing.Logs.AcceptChanges();
		}

		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		/// <param name="filePath">!!!DO NOT PASS PARAMETER!!!</param>
		/// <param name="method">!!!DO NOT PASS PARAMETER!!!</param>
		public void New(LogTitels titel, string content, LogTypes logType = LogTypes.Information, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{

			New(titel.ToString(), content, logType, filePath, method);
		}
	}
}