// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-29</date>

using System;
using CsWpfBase.Global.transmission.secured.http;






namespace CsWpfBase.Global.transmission.secured
{
	/// <summary>Wraps a bunch of secured transmission protocols.</summary>
	public sealed class CsgTransmissionSecured
	{
		private static CsgTransmissionSecured _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsgTransmissionSecured I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgTransmissionSecured());
				}
			}
		}

		private CsgTransmissionSecured()
		{
		}

		/// <summary>Supports http related encryption helper.</summary>
		public CsgTransmissionSecuredHttp Http => CsgTransmissionSecuredHttp.I;
	}



}