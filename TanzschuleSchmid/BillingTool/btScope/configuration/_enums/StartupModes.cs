// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingTool.Windows;






namespace BillingTool.btScope.configuration._enums
{
	/// <summary>The possible runtime modes of the application.</summary>
	[Serializable]
	public enum StartupModes
	{
		/// <summary>No startup mode is defined. The application will exit.</summary>
		Undefined,
		/// <summary>The <see cref="DeveloperWindow" /> will be opened. In this <see cref="StartupModes" /> real DAMAGE could be done.</summary>
		Developer,
		/// <summary>The <see cref="NewCashBookEntryWindow" /> will be opened.</summary>
		NewCashBookEntry,
		/// <summary>The <see cref="ConfigurationWindow" /> will be opened.</summary>
		Configuration,
		/// <summary>The <see cref="ProductInformationWindow" /> will be opened.</summary>
		ProductInformation,
	}
}