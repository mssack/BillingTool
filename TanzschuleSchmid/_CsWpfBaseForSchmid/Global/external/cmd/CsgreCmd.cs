// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.external.cmd
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	public sealed class CsgreCmd : Base
	{
		private static CsgreCmd _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgreCmd I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgreCmd());
				}
			}
		}

		private CsgreCmd()
		{
		}

		/// <summary>Execute CMD command in a hidden CMD window.</summary>
		public CsgreHiddenCmd Hidden
		{
			get { return CsgreHiddenCmd.I; }
		}
		/// <summary>Execute CMD command in a visible CMD window.</summary>
		public CsgreVisibleCmd Visible
		{
			get { return CsgreVisibleCmd.I; }
		}
	}
}