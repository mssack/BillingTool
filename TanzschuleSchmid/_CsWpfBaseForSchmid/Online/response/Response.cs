// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Online.response
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoResponse : Base
	{
		private static CsoResponse _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsoResponse I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoResponse());
				}
			}
		}

		private CsoResponse()
		{
		}

		/// <summary>Processes server Responses</summary>
		public CsoResponseProcess Process
		{
			get { return CsoResponseProcess.I; }
		}
	}
}