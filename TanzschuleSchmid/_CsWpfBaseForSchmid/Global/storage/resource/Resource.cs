// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.resource
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgResource : Base
	{
		#region SINGLETON CLASS
		private static CsgResource _instance;
		private static readonly object SingletonLock = new object();
		private CsgResource()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgResource I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgResource());
				}
			}
		}
		#endregion


		/// <summary>Provides application internal path system routines for addressing embedded resources.</summary>
		public CsgResourcesPath Path
		{
			get { return CsgResourcesPath.I; }
		}
		/// <summary>Provides application internal file reading routines for embedded resources.</summary>
		public CsgResourceFile File
		{
			get { return CsgResourceFile.I; }
		}
		/// <summary>Subset of resource dictionaries used by this application.</summary>
		public CsgResourceDictionary Dictionary
		{
			get { return CsgResourceDictionary.I; }
		}
	}
}