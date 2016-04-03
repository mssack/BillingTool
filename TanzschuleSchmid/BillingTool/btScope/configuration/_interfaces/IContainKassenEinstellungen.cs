// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;






namespace BillingTool.btScope.configuration._interfaces
{
	/// <summary>Stores Kassen specific configurations.</summary>
	public interface IContainKassenEinstellungen
	{
		#region Abstract
		/// <summary>The file path to the billing database.</summary>
		string BillingDatabaseFilePath { get; }
		/// <summary>The unique id of the current Kassa.</summary>
		string KassenId { get; }
		/// <summary>The scaling of the whole application.</summary>
		double Scaling { get; }
		#endregion
	}
}