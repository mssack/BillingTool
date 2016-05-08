// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces
{
	/// <summary>Used to share common properties between output rows for the row of type <see cref="BelegData" />. An example would be
	///     <see cref="MailedBeleg" />.</summary>
	public interface IOutputBeleg
	{
		#region Abstract
		/// <summary>The date and time of the processing.</summary>
		DateTime ProcessingDate { get; }
		/// <summary>Gets the state of the current processing state. This determines whether the operation could complete successfully.</summary>
		ProcessingStates ProcessingState { get; }
		/// <summary>Gets the exception of the process if ProcessingState is <see cref="ProcessingStates.Failed" />.</summary>
		string ProcessingException { get; }
		/// <summary>Gets the output format type for the output.</summary>
		OutputFormat OutputFormat { get; }
		#endregion
	}


}