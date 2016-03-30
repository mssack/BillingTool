// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.merged
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt"/> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class Merged_NewCashBookEntry : Base, IConfigNewCashBookEntrySettings
	{
		private static Merged_NewCashBookEntry _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this merged property.";
		private const string SetErrorMessage = "You cannot set merged property's.";

		/// <summary>Returns the singleton instance</summary>
		internal static Merged_NewCashBookEntry I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Merged_NewCashBookEntry());
				}
			}
		}

		private Merged_NewCashBookEntry()
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
		public decimal AmountGross
		{
			get { return GetMergedValue(setting => setting.AmountGross); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TaxPercent</c>]</summary>
		public decimal TaxPercent
		{
			get { return GetMergedValue(setting => setting.TaxPercent); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Issuer</c>]</summary>
		public string Issuer
		{
			get { return GetMergedValue(setting => setting.Issuer); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Recipient</c>]</summary>
		public string Recipient
		{
			get { return GetMergedValue(setting => setting.Recipient); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Text</c>]</summary>
		public string Text
		{
			get { return GetMergedValue(setting => setting.Text); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternRecipientId</c>]</summary>
		public string InternRecipientId
		{
			get { return GetMergedValue(setting => setting.InternRecipientId); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternDescription</c>]</summary>
		public string InternDescription
		{
			get { return GetMergedValue(setting => setting.InternDescription); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LastEdited</c>]</summary>
		public DateTime LastEdited
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		#endregion


		/// <summary>Try to get the value from command line configuration. If command line configuration is default use the value from configuration file.</summary>
		private T GetMergedValue<T>(Func<IConfigNewCashBookEntrySettings, T> get)
		{
			var value = get(Bt.Config.CommandLine.NewCashBookEntry);
			if (value != null && !Equals(default(T), value))
				return value;
			return get(Bt.Config.File.NewCashBookEntry);
		}
	}
}