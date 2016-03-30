// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.IO;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt"/> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_MailSettings : ConfigFileBase, IConfigMailSettings
	{
		private static ConfigFile_MailSettings _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_MailSettings I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_MailSettings(CsGlobal.Storage.Private.GetFilePathByName("MailSettings")));
				}
			}
		}

		private string _senderMailAddress = "";
		private bool _senderSmtpEnableSsl;
		private string _senderSmtpPassword;
		private ushort _senderSmtpPort = 25;
		private string _senderSmtpServer;
		private string _senderSmtpUsername;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_MailSettings(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_MailSettings(Uri packUri) : base(packUri)
		{
		}


		/// <summary>Gets or sets the mail address from which the mail should be send.</summary>
		[Key]
		public string SenderMailAddress
		{
			get { return _senderMailAddress; }
			set { SetProperty(ref _senderMailAddress, value); }
		}
		/// <summary>Gets or sets the SMTP server the mail should be send to.</summary>
		[Key]
		public string SenderSmtpServer
		{
			get { return _senderSmtpServer; }
			set { SetProperty(ref _senderSmtpServer, value); }
		}
		/// <summary>Gets or sets the SMTP server port the mail should be send to.</summary>
		[Key]
		public ushort SenderSmtpPort
		{
			get { return _senderSmtpPort; }
			set { SetProperty(ref _senderSmtpPort, value); }
		}
		/// <summary>Gets or sets the enable ssl mode.</summary>
		[Key]
		public bool SenderSmtpEnableSsl
		{
			get { return _senderSmtpEnableSsl; }
			set { SetProperty(ref _senderSmtpEnableSsl, value); }
		}
		/// <summary>Gets or sets the UserName for the SMTP server.</summary>
		[Key]
		public string SenderSmtpUsername
		{
			get { return _senderSmtpUsername; }
			set { SetProperty(ref _senderSmtpUsername, value); }
		}
		/// <summary>Gets or sets the Password for the SMTP server.</summary>
		[Key]
		public string SenderSmtpPassword
		{
			get { return _senderSmtpPassword; }
			set { SetProperty(ref _senderSmtpPassword, value); }
		}
	}
}