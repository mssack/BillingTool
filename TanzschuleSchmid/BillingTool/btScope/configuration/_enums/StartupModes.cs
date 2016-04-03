// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using BillingTool.Windows;






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
		/// <summary>The <see cref="Window_BelegData_Approve" /> will be opened.</summary>
		[Description("Beleg Bestätigung")] ApproveBelegData,
		/// <summary>The <see cref="Window_KassenConfiguration" /> will be opened.</summary>
		[Description("KassenConfiguration")] KassenConfiguration,
		/// <summary>The <see cref="DatabaseWindow" /> will be opened.</summary>
		[Description("Database")] Database,
	}
}