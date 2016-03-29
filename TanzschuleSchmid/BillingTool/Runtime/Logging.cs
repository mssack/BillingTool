// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.IO;
using System.Runtime.CompilerServices;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Runtime
{
	/// <summary>This class is used for Logging purpose.</summary>
	public static class Logs
	{
		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		public static void New(string titel, string content, LogTypes logType, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{
			var fileInfo = new FileInfo(filePath);



			Db.EnsureConnectivity();
			var log = Db.Billing.Logs.NewRow();
			log.Type = logType;
			log.Title = titel;
			log.CodePosition = fileInfo.Name.Replace(fileInfo.Extension, "") + "." + method;
			log.CommandLine = Environment.CommandLine;
			log.Content = content;
			Db.Billing.Logs.Add(log);
			Db.Billing.Logs.SaveChanges();
			Db.Billing.Logs.AcceptChanges();
		}
		/// <summary>Creates a new <see cref="Log" /> in the database.</summary>
		/// <param name="titel">The title of the log.</param>
		/// <param name="content">The content of the log.</param>
		/// <param name="logType">The type of the log.</param>
		public static void New(LogTitels titel, string content, LogTypes logType, [CallerFilePath] string filePath = null, [CallerMemberName] string method = null)
		{
			New(titel.ToString(),content, logType, filePath, method);
		}
	}
}