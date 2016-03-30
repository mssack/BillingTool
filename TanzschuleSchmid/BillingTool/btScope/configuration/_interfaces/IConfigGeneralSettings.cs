// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.ComponentModel;
using BillingTool.btScope.configuration._enums;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration._interfaces
{
	/// <summary>Describes all general configurations which can be done either by command line or configuration file.</summary>
	public interface IConfigGeneralSettings
	{
		#region Abstract
		/// <summary>see <see cref="Base.PropertyChanged" /> class for further information.</summary>
		event PropertyChangedEventHandler PropertyChanged;
		/// <summary>The file path to the billing database.</summary>
		string BillingDatabaseFilePath { get; set; }
		/// <summary>The mode decides how the application will be started.</summary>
		StartupModes StartupMode { get; set; }
		#endregion
	}
}