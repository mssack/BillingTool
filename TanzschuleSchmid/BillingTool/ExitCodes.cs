// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-17</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






// ReSharper disable InconsistentNaming

namespace BillingTool
{
	/// <summary>The exit codes are used to pass information about the applications exit states to the calling process.</summary>
	[Serializable]
	[Flags]
	public enum ExitCodes
	{
		/// <summary>the application closed successfully. No error occurred.</summary>
		Success = 1 << 0,


		/// <summary>some <see cref="BelegData" /> were created and saved to the database.</summary>
		BelegData_Created = 1 << 1,
		/// <summary>some <see cref="BelegData" /> which should be created were canceled.</summary>
		BelegData_Creation_Aborted = 1 << 2,
		/// <summary>some <see cref="BelegData" /> were storniert.</summary>
		BelegData_Storniert = 1 << 3,

		/// <summary>some <see cref="BelegData" /> were printed.</summary>
		BelegData_Print_Success = 1 << 4,
		/// <summary>some <see cref="BelegData" /> could not be printed. This is not an absolut error which sets the <see cref="Success" /> flag to zero.</summary>
		BelegData_Print_Error = 1 << 5,

		/// <summary>some <see cref="BelegData" /> were mailed.</summary>
		BelegData_Mail_Success = 1 << 6,
		/// <summary>some <see cref="BelegData" /> could not be mailed. This is not an absolut error which sets the <see cref="Success" /> flag to zero.</summary>
		BelegData_Mail_Error = 1 << 7,

		/// <summary>some options were being stored.</summary>
		Options_Saved = 1 << 8,


		/// <summary>the application exited with fatal unhandled error.</summary>
		Error_Unhandled = 1 << 15,
		/// <summary>Occurs when no valid configuration is available. Typically all operations are aborted.</summary>
		Error_No_ValidConfiguration = 1 << 16,
		/// <summary>Occurs when no database is available. Typically all operations are aborted.</summary>
		Error_No_DatabaseAvailable = 1 << 17,
		/// <summary>Occurs when the database is used by another process, or is an incompatible type.</summary>
		Error_No_DatabaseConnectionPossible = 1 << 18,
		/// <summary>Occurs when the database is used by another process, or is an incompatible type.</summary>
		Error_Invalid_StartupParam = 1 << 19,
		/// <summary>Occurs if no Kassenoperator is defined.</summary>
		Error_No_KassenOperator = 1 << 20,
		/// <summary>Occurs if no business-name is defined.</summary>
		Error_No_BusinessName = 1 << 20,

		/// <summary>Occurs if some other non described error occurs.</summary>
		Error_Others = 1 << 32,
	}
}