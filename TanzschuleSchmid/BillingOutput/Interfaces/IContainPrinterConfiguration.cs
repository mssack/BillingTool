// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows.Controls;






namespace BillingOutput.Interfaces
{
	/// <summary>The printer configurations needed for printing Bons.</summary>
	public interface IContainPrinterConfiguration
	{
		#region Abstract
		/// <summary>The printer for the BON's.</summary>
		PrintDialog Printer { get; }
		#endregion
	}
}