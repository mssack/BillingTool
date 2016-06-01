// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Global.storage.compression
{
	/// <summary>See <see cref="CsGlobal" />.</summary>
	public class CsgCompression
	{
		private static CsgCompression _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgCompression I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgCompression());
				}
			}
		}

		private CsgCompression()
		{
		}

		/// <summary>Compression methods for zip files.</summary>
		public CsgCompressionZip Zip => CsgCompressionZip.I;
	}
}