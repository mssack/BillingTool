// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Global
{
	/// <summary>
	///     With the global functions enumeration the <see cref="Global.CsGlobal" /> engine can be partly activated. Use
	///     it in Combination with the following method: <see cref="CsGlobal.Install" />
	/// </summary>
	[Flags]
	public enum GlobalFunctions
	{
		/// <summary>Activates the automated logging of unhandled exceptions.</summary>
		RedirectUnhandledExceptions = 1 << 0,
		/// <summary>
		///     Activates the German thread culture so the <see cref="DateTime.ToString()" /> method will use automatically a
		///     German time format.
		/// </summary>
		GermanThreadCulture = 1 << 1,
		/// <summary>Activates the file system so that the engine have disc writing permissions.</summary>
		Storage = 1 << 2,
		/// <summary>Activates the application setup engine for the application including an installer.</summary>
		Setup = 1 << 3,
		/// <summary>Activates the app data scope.</summary>
		AppData = 1 << 4,
		/// <summary>Activates the WPF storage scope.</summary>
		WpfStorage = 1 << 5,
		/// <summary>Reads the 'config.txt' path at the startup folder in order to change the public file scope.</summary>
		ConfigFile = 1 << 6,
		/// <summary>Opens a agreement window which must be accepted in order to start the application.</summary>
		Agreement = 1 << 7,
		/// <summary>Takes care of usage details and update process.</summary>
		OnlineConnectivity = 1 << 8,
	}
}