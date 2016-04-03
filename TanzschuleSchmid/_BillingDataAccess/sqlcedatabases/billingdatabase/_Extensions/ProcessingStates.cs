// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The <see cref="ProcessingStates" /> for the <see cref="PrintedBeleg" /> row and the <see cref="MailedBeleg" /> row.</summary>
	public enum ProcessingStates
	{
		/// <summary>Unknown state.</summary>
		Unknown,
		/// <summary>The row is currently not processed similar to not mailed or not printed.</summary>
		NotProcessed,
		/// <summary>The row is currently processing (mailing or printing).</summary>
		Processing,
		/// <summary>The row is currently processed it is either sent or printed.</summary>
		Processed,
		/// <summary>Something went wrong and the mail could not be sent or printed.</summary>
		Failed,
	}
}