// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.ComponentModel;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.Windows;
using BillingTool.Windows.privileged;






namespace BillingTool.btScope.configuration._enums
{
	/// <summary>The possible runtime modes of the application.</summary>
	[Serializable]
	public enum StartupModes
	{
		/// <summary>No startup mode is defined. The application will exit.</summary>
		[Description("Nicht-startbar")] Undefined,

		/// <summary>The <see cref="DeveloperWindow" /> will be opened. In this <see cref="StartupModes" /> real DAMAGE could be done.</summary>
		[Description("Development")] Developer,
		/// <summary>The <see cref="Window_DatabaseViewer" /> will be opened.</summary>
		[Description("Database")]
		Database,


		/// <summary>The <see cref="Window_BelegData_Viewer" /> will be opened. Can be used to reprint, mail or even Storno a <see cref="BelegData" />.</summary>
		[Description("Belege View")] BelegDataViewer,
		/// <summary>The <see cref="Window_BelegData_Approve" /> will be opened.</summary>
		[Description("Beleg Bestätigung")] BelegDataApprove,
		/// <summary>The <see cref="Window_Options" /> will be opened.</summary>
		[Description("Optionen")] Options,
	}
}