// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>The <see cref="ProcessingStates" /> for the <see cref="PrintedBeleg" /> row and the <see cref="MailedBeleg" /> row.</summary>
	public enum ProcessingStates
	{
		/// <summary>Unknown state.</summary>
		Unknown,
		/// <summary>The row is currently not processed similar to not mailed or not printed.</summary>
		[EnumDescription("Nicht prozessiert")] NotProcessed,
		/// <summary>The row is currently processing (mailing or printing).</summary>
		[EnumDescription("Laufend")] Processing,
		/// <summary>The row is currently processed it is either sent or printed.</summary>
		[EnumDescription("OK")] Processed,
		/// <summary>Something went wrong and the mail could not be sent or printed.</summary>
		[EnumDescription("Fehler")] Failed,
	}
}