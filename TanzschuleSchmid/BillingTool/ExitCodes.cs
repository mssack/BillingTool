// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;






namespace BillingTool
{
	/// <summary>The exit codes are used to pass information about the applications exit states to the calling process.</summary>
	[Serializable]
	public enum ExitCodes : int
	{
		/// <summary>the application exited with fatal error.</summary>
		Failure = -1,
		/// <summary>the application closed successfully.</summary>
		Success = 0,
		/// <summary>a new cash book entry was created and saved to the database.</summary>
		NewBonCreated = 100,
		/// <summary>the cash book entry which should be created was aborted by the user.</summary>
		NewBonAborted = 101,
	}
}