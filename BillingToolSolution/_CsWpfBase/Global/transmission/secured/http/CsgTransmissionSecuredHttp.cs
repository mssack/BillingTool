// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-29</date>

using System;
using System.Security.Cryptography;
using System.Web;






namespace CsWpfBase.Global.transmission.secured.http
{
	/// <summary>Wraps a bunch of secured http routines.</summary>
	public class CsgTransmissionSecuredHttp
	{
		private static CsgTransmissionSecuredHttp _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsgTransmissionSecuredHttp I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgTransmissionSecuredHttp());
				}
			}
		}

		private CsgTransmissionSecuredHttp()
		{
		}


		/// <summary>
		///     Creates a new <see cref="HttpCryptoClientSession" />, which can perform asymmetric/symmetric hybrid encryption operations. It is used with the
		///     <see cref="HttpCryptoServerSession" /> on the other side of the transmission.
		/// </summary>
		public HttpCryptoClientSession ClientSession(RSACryptoServiceProvider publicKey, string website)
		{
			return new HttpCryptoClientSession(publicKey, website);
		}

		/// <summary>
		///     Creates a <see cref="HttpCryptoServerSession" />, which can perform asymmetric/symmetric hybrid encryption operations. It is used with the
		///     <see cref="HttpCryptoClientSession" /> on the other side of the transmission.
		/// </summary>
		public HttpCryptoServerSession ServerSession(RSACryptoServiceProvider privateKey, HttpRequestBase request)
		{
			return new HttpCryptoServerSession(privateKey, request);
		}
	}
}