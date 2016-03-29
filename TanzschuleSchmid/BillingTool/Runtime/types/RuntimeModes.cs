// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;






namespace BillingTool.Runtime.types
{
	/// <summary>The possible runtime modes of the application.</summary>
	[Serializable]
	public enum RuntimeModes
	{
		/// <summary>No startup mode is defined.</summary>
		Undefined,
		/// <summary>The program is started for an Developer with administrative rights. In this mode real damage could be done. TAKE CARE.</summary>
		Developer,
		/// <summary>The program is started for an new cash book entry.</summary>
		NewCashBookEntry,
	}
}