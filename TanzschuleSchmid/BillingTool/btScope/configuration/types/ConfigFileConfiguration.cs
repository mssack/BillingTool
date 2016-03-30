﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.IO;
using BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.types
{
	/// <summary>The default program configuration. This configuration is used whenever no start params are defined.</summary>
	public sealed class ConfigFileConfiguration : ConfigFileBase, ICashBookEntry
	{
		private static ConfigFileConfiguration _instance;
		private static readonly object SingletonLock = new object();


		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFileConfiguration I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFileConfiguration(CsGlobal.Storage.Private.GetFilePathByName("StaticConfiguration")));
				}
			}
		}

		private decimal _amountGross;
		private string _databaseFilePath;
		private DateTime _date;
		private Guid _id;
		private string _internDescription;
		private string _internRecipientId;
		private string _issuer;
		private DateTime _lastEdited;
		private string _recipient;
		private int _referenceNumber;
		private decimal _taxPercent;
		private string _text;

		private ConfigFileConfiguration(FileInfo path) : base(path)
		{
		}

		private ConfigFileConfiguration(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>]</summary>
		[Key]
		public Guid Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ReferenceNumber</c>]</summary>
		[Key]
		public int ReferenceNumber
		{
			get { return _referenceNumber; }
			set { SetProperty(ref _referenceNumber, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Date</c>]</summary>
		[Key]
		public DateTime Date
		{
			get { return _date; }
			set { SetProperty(ref _date, value); }
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
		[Key]
		public DateTime LastEdited
		{
			get { return _lastEdited; }
			set { SetProperty(ref _lastEdited, value); }
		}
		#endregion


		/// <summary>The data base file path which is used to access the database.</summary>
		public string DatabaseFilePath
		{
			get { return _databaseFilePath; }
			set { SetProperty(ref _databaseFilePath, value); }
		}
	}
}