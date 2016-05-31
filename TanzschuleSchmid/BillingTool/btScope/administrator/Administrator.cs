// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using BillingTool.btScope.administrator.licence;
using BillingTool.Windows._installation;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.administrator
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Administrator : Base
	{
		private static Administrator _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Administrator I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Administrator());
				}
			}
		}

		private string _license;

		private Administrator()
		{
		}

		/// <summary>Gets or sets the License.</summary>
		public string License => _license ?? (_license = new EULAGerman().GetString());

		/// <summary>Opens the <see cref="Window_LicenceAgreement" /> window.</summary>
		public bool AskForLicenseAgreement()
		{
			var w = new Window_LicenceAgreement();
			return w.ShowDialog() ?? false;
		}
	}
}