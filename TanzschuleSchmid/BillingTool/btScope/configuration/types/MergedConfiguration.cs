// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.types
{
	/// <summary>THe <see cref="Bt.Config"/>.Merged scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class MergedConfiguration : Base
	{
		private static MergedConfiguration _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static MergedConfiguration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new MergedConfiguration());
				}
			}
		}

		private MergedConfiguration()
		{
		}
	}
}