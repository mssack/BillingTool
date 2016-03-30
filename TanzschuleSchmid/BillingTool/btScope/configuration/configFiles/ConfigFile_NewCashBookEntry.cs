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






// ReSharper disable InconsistentNaming

namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_NewCashBookEntry : ConfigFileBase, IConfigNewCashBookEntrySettings
	{
		private static ConfigFile_NewCashBookEntry _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";


		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_NewCashBookEntry I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_NewCashBookEntry(CsGlobal.Storage.Private.GetFilePathByName("NewCashBookSettings")));
				}
			}
		}


		private decimal _amountGross;
		private string _internDescription;
		private string _internRecipientId;
		private string _issuer;
		private string _recipient;
		private decimal _taxPercent;
		private string _text;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewCashBookEntry(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewCashBookEntry(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>]</summary>
		public Guid Id
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ReferenceNumber</c>]</summary>
		public int ReferenceNumber
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Date</c>]</summary>
		public DateTime Date
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>AmountGross</c>]</summary>
		[Key]
		public decimal AmountGross
		{
			get { return _amountGross; }
			set { SetProperty(ref _amountGross, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TaxPercent</c>]</summary>
		[Key]
		public decimal TaxPercent
		{
			get { return _taxPercent; }
			set { SetProperty(ref _taxPercent, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Issuer</c>]</summary>
		[Key]
		public string Issuer
		{
			get { return _issuer; }
			set { SetProperty(ref _issuer, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Recipient</c>]</summary>
		[Key]
		public string Recipient
		{
			get { return _recipient; }
			set { SetProperty(ref _recipient, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Text</c>]</summary>
		[Key]
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternRecipientId</c>]</summary>
		[Key]
		public string InternRecipientId
		{
			get { return _internRecipientId; }
			set { SetProperty(ref _internRecipientId, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternDescription</c>]</summary>
		[Key]
		public string InternDescription
		{
			get { return _internDescription; }
			set { SetProperty(ref _internDescription, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LastEdited</c>]</summary>
		public DateTime LastEdited
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		#endregion
	}
}