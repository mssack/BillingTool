// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

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
		NewBelegData_Created = 100,
		/// <summary>the <see cref="BelegData" /> which should be created was canceled.</summary>
		BelegDataCreation_Aborted = 101,


		/// <summary>Occurs when no valid configuration is available. Typically all operations are aborted.</summary>
		No_ValidConfiguration = 1001,
		/// <summary>Occurs when no database is available. Typically all operations are aborted.</summary>
		No_DatabaseAvailable = 1002,
		/// <summary>Occurs when the database is used by another process, or is an incompatible type.</summary>
		No_DatabaseConnectionPossible = 1003,


		/// <summary>Occurs when the database is used by another process, or is an incompatible type.</summary>
		Invalid_StartupParam = 1004,
		/// <summary>Occurs if no kassen operator is defined.</summary>
		No_KassenOperator = 1005,
	}
}