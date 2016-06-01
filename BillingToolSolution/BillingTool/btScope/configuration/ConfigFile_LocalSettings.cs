// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using System.IO;
using BillingToolOutput.Interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_LocalSettings : ConfigFileBase, IContainMailConfiguration
	{
		private static ConfigFile_LocalSettings _instance;
		private static readonly object SingletonLock = new object();
		internal static FileInfo FileName => CsGlobal.Storage.Private.GetFilePathByName("_LocalSettings");
		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_LocalSettings I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_LocalSettings(FileName));
				}
			}
		}

		private string _billingDatabaseFilePath;
		private string _dataVersion;
		private string _defaultPrinter;
		private string _kassenId;
		private double _scaling = 1.3;
		private bool _smtpEnableSsl;


		private string _smtpMailAddress;
		private string _smtpPassword;
		private ushort _smtpPort = 25;
		private string _smtpServer;
		private string _smtpUsername;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_LocalSettings(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_LocalSettings(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>Gets or sets the mail address from which the mail should be send.</summary>
		[Key]
		public string SmtpMailAddress
		{
			get { return _smtpMailAddress; }
			set { SetProperty(ref _smtpMailAddress, value); }
		}
		/// <summary>Gets or sets the SMTP server the mail should be send to.</summary>
		[Key]
		public string SmtpServer
		{
			get { return _smtpServer; }
			set { SetProperty(ref _smtpServer, value); }
		}
		/// <summary>Gets or sets the SMTP server port the mail should be send to.</summary>
		[Key]
		public ushort SmtpPort
		{
			get { return _smtpPort; }
			set { SetProperty(ref _smtpPort, value); }
		}
		/// <summary>Gets or sets the enable ssl mode.</summary>
		[Key]
		public bool SmtpEnableSsl
		{
			get { return _smtpEnableSsl; }
			set { SetProperty(ref _smtpEnableSsl, value); }
		}
		/// <summary>Gets or sets the UserName for the SMTP server.</summary>
		[Key]
		public string SmtpUsername
		{
			get { return _smtpUsername; }
			set { SetProperty(ref _smtpUsername, value); }
		}
		/// <summary>Gets or sets the Password for the SMTP server.</summary>
		[Key]
		public string SmtpPassword
		{
			get { return _smtpPassword; }
			set { SetProperty(ref _smtpPassword, value); }
		}
		#endregion


		/// <summary>The file path to the billing database.</summary>
		[Key]
		public string BillingDatabaseFilePath
		{
			get { return _billingDatabaseFilePath; }
			set
			{
				if (SetProperty(ref _billingDatabaseFilePath, value)) OnPropertyChanged(nameof(IsValid));
			}
		}
		/// <summary>The unique id of the current Kassa.</summary>
		[Key]
		public string KassenId
		{
			get { return _kassenId; }
			set
			{
				if (SetProperty(ref _kassenId, value)) OnPropertyChanged(nameof(IsValid));
			}
		}
		/// <summary>Gets or sets the Scaling.</summary>
		[Key]
		public double Scaling
		{
			get { return _scaling; }
			set { SetProperty(ref _scaling, value); }
		}
		/// <summary>Gets or sets the Default_PrinterName.</summary>
		[Key]
		public string DefaultPrinter
		{
			get { return _defaultPrinter; }
			set { SetProperty(ref _defaultPrinter, value); }
		}
		/// <summary>Gets or sets the DataVersion.</summary>
		[Key]
		public string DataVersion
		{
			get { return _dataVersion; }
			set { SetProperty(ref _dataVersion, value); }
		}

		/// <summary>Check if all fields which are important are present.</summary>
		public bool IsValid => !string.IsNullOrEmpty(BillingDatabaseFilePath) && !string.IsNullOrEmpty(KassenId);
	}



}