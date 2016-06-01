// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Ev
{
	/// <summary>Stores the configuration for the assembly</summary>
	internal class CsWpfBaseConfig : ConfigFileBase
	{
		private static CsWpfBaseConfig _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsWpfBaseConfig I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsWpfBaseConfig(new Uri(CsGlobal.Storage.Resource.Path.Get("CsWpfBase", "config.txt"), UriKind.RelativeOrAbsolute)));
				}
			}
		}
		private string _agreementDeveloperAddress;
		private string _agreementDeveloperName;
		private string _csOnlineDebugServer;
		private string _csOnlineServer;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private CsWpfBaseConfig(Uri path)
			: base(path)
		{
			Load();
		}

		/// <summary>Gets or sets the CsOnlineServer.</summary>
		[Key]
		public string CsOnlineServer
		{
			get { return _csOnlineServer; }
			private set { SetProperty(ref _csOnlineServer, value); }
		}
		/// <summary>Gets or sets the CsOnlineDebugServer.</summary>
		[Key]
		public string CsOnlineDebugServer
		{
			get { return _csOnlineDebugServer; }
			private set { SetProperty(ref _csOnlineDebugServer, value); }
		}
		/// <summary>Gets or sets the AgreementDeveloperName.</summary>
		[Key]
		public string AgreementDeveloperName
		{
			get { return _agreementDeveloperName; }
			private set { SetProperty(ref _agreementDeveloperName, value); }
		}
		/// <summary>Gets or sets the AgreementDeveloperAddress.</summary>
		[Key]
		public string AgreementDeveloperAddress
		{
			get { return _agreementDeveloperAddress; }
			private set { SetProperty(ref _agreementDeveloperAddress, value); }
		}
	}
}