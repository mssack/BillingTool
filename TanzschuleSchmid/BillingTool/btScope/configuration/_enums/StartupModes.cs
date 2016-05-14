// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.ComponentModel;






namespace BillingTool.btScope.configuration._enums
{
	/// <summary>The possible startup modes of the application. The mode defines the functionality of an application instance.</summary>
	[Serializable]
	public enum StartupModes
	{
		/// <summary>No startup mode is defined. The application will exit.</summary>
		[Description("Nicht definiert")] Undefined,

		/// <summary>for testing purpose.</summary>
		[Description("Database")] Database,


		/// <summary>Allows re-printing, re-mailing or even canceling a previous created BelegData. Can also be used to manually create a new BelegData.</summary>
		[Description("Belege View")] BelegDataViewer,
		/// <summary>Allows the creation of a new BelegData.</summary>
		[Description("Beleg Bestätigung")] BelegDataApprove,
		/// <summary>Allows modifying the options of the program.</summary>
		[Description("Optionen")] Options,
	}
}