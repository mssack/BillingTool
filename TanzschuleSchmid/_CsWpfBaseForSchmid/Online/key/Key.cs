// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-16</date>

using System;
using System.Security.Cryptography;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;






namespace CsWpfBase.Online.key
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoKey : Base
	{
		private static CsoKey _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsoKey I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoKey());
				}
			}
		}


		private RSACryptoServiceProvider _v1;

		private CsoKey()
		{
			_v1 = new RSACryptoServiceProvider();
			_v1.FromXmlString(CsGlobal.Storage.Resource.File.Read("CsWpfBase", "_files/publickeys/csonlinekey1.txt"));
		}

		/// <summary>Version 1 key, public only in client applications.</summary>
		public RSACryptoServiceProvider V1
		{
			get { return _v1; }
			private set { SetProperty(ref _v1, value); }
		}
		
	}
}