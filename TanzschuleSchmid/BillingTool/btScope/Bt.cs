// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingTool.btScope.configuration;
using BillingTool.btScope.db;
using BillingTool.btScope.functions;
using BillingTool.btScope.logging;






namespace BillingTool.btScope
{
	/// <summary>
	///     The programming interface for all relevant functions inside the <see cref="BillingTool" /> name space. Use this static class whenever an
	///     interaction with this program is needed.
	/// </summary>
	public static class Bt
	{
		/// <summary>
		///     The configuration of the current application lifeline. This can be modified by simple modifying the configuration file or passing parameters to
		///     the program.
		/// </summary>
		public static BtConfiguration Config => BtConfiguration.I;
		/// <summary>Used to log events. Use this whenever there is need for logging.</summary>
		public static BtLogging Logging => BtLogging.I;
		/// <summary>Used to interact with the underlaying database. Use <see cref="Config" /> to set the current database.</summary>
		public static BtDb Db => BtDb.I;
		/// <summary>Default functions used to interact wit the user.</summary>
		public static BtUiFunctions UiFunctions => BtUiFunctions.I;
	}
}