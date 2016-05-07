// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.IO;
using BillingOutput.Interfaces;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_KassenEinstellung : ConfigFileBase, IContainKassenEinstellungen, IContainMailConfiguration
	{
		private static ConfigFile_KassenEinstellung _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_KassenEinstellung I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_KassenEinstellung(CsGlobal.Storage.Private.GetFilePathByName("Kasseneinstellungen")));
				}
			}
		}

		private string _billingDatabaseFilePath;
		private string _kassenId;
		private string _printerName;
		private double _scaling = 1.3;
		private bool _smtpEnableSsl;


		private string _smtpMailAddress;
		private string _smtpPassword;
		private ushort _smtpPort = 25;
		private string _smtpServer;
		private string _smtpUsername;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_KassenEinstellung(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_KassenEinstellung(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
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





		/// <summary>Gets or sets the Scaling.</summary>
		[Key]
		public double Scaling
		{
			get { return _scaling; }
			set { SetProperty(ref _scaling, value); }
		}
		#endregion


		/// <summary>Gets or sets the PrinterName.</summary>
		[Key]
		public string PrinterName
		{
			get { return _printerName; }
			set { SetProperty(ref _printerName, value); }
		}

		/// <summary>Check if all fields which are important are present.</summary>
		public bool IsValid => !string.IsNullOrEmpty(BillingDatabaseFilePath) && !string.IsNullOrEmpty(KassenId);
	}



}