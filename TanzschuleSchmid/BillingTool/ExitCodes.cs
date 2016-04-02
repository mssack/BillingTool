// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






// ReSharper disable InconsistentNaming

namespace BillingTool
{
	/// <summary>The exit codes are used to pass information about the applications exit states to the calling process.</summary>
	[Serializable]
	public enum ExitCodes
	{
		/// <summary>the application exited with fatal error.</summary>
		FatalError = -1,
		/// <summary>the application closed successfully. No answer.</summary>
		Success = 0,
		/// <summary>a <see cref="BelegData" /> was created and saved to the database.</summary>
		BelegDataCreation_Succeeded = 100,
		/// <summary>the <see cref="BelegData" /> which should be created was canceled.</summary>
		BelegDataCreation_Aborted = 101,
	}
}