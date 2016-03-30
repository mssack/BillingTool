// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Collections.Generic;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt"/> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class CommandLine_NewCashBookEntrySetting : Base, IConfigNewCashBookEntrySettings
	{
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";
		private const string ParamPrefix = "NCE";
		private static CommandLine_NewCashBookEntrySetting _instance;
		private static readonly object SingletonLock = new object();





		/// <summary>Returns the singleton instance</summary>
		internal static CommandLine_NewCashBookEntrySetting I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CommandLine_NewCashBookEntrySetting());
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

		private CommandLine_NewCashBookEntrySetting()
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
			get { return _amountGross; }
			set { SetProperty(ref _amountGross, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TaxPercent</c>]</summary>
		public decimal TaxPercent
		{
			get { return _taxPercent; }
			set { SetProperty(ref _taxPercent, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Issuer</c>]</summary>
		public string Issuer
		{
			get { return _issuer; }
			set { SetProperty(ref _issuer, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Recipient</c>]</summary>
		public string Recipient
		{
			get { return _recipient; }
			set { SetProperty(ref _recipient, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Text</c>]</summary>
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternRecipientId</c>]</summary>
		public string InternRecipientId
		{
			get { return _internRecipientId; }
			set { SetProperty(ref _internRecipientId, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternDescription</c>]</summary>
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


		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		public void Interpret(List<string> commands)
		{
			foreach (var item in commands.ToArray())
			{
				var found = true;


				if (IsParameter(nameof(AmountGross), item))
					AmountGross = Convert.ToDecimal(ParseValue(nameof(AmountGross), item));
				else if (IsParameter(nameof(TaxPercent), item))
					TaxPercent = Convert.ToDecimal(ParseValue(nameof(TaxPercent), item));
				else if (IsParameter(nameof(Issuer), item))
					Issuer = ParseValue(nameof(Issuer), item);
				else if (IsParameter(nameof(Recipient), item))
					Recipient = ParseValue(nameof(Recipient), item);
				else if (IsParameter(nameof(Text), item))
					Text = ParseValue(nameof(Text), item);
				else if (IsParameter(nameof(InternRecipientId), item))
					InternRecipientId = ParseValue(nameof(InternRecipientId), item);
				else
					found = false;


				if (found)
					commands.Remove(item);
			}
		}

		private bool IsParameter(string val, string item)
		{
			return item.StartsWith(ParamPrefix + val, StringComparison.OrdinalIgnoreCase);
		}

		private string ParseValue(string val, string item)
		{
			return item.Substring((ParamPrefix + val).Length + 1);
		}
	}
}