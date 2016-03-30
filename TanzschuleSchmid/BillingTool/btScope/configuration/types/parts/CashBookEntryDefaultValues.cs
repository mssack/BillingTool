// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.types.parts
{
	/// <summary>This class represents the default values for a new instance of type <see cref="CashBookEntry" />.</summary>
	[Serializable]
	public sealed class CashBookEntryDefaultValues : Base, ICashBookEntry
	{
		private decimal _amountGross;
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


		#region Overrides/Interfaces
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>]</summary>
		public Guid Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ReferenceNumber</c>]</summary>
		public int ReferenceNumber
		{
			get { return _referenceNumber; }
			set { SetProperty(ref _referenceNumber, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Date</c>]</summary>
		public DateTime Date
		{
			get { return _date; }
			set { SetProperty(ref _date, value); }
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
			get { return _lastEdited; }
			set { SetProperty(ref _lastEdited, value); }
		}
		#endregion
	}
}