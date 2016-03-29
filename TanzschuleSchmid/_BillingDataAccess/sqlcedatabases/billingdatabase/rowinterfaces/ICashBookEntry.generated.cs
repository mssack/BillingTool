//********************************************************************
//
//                 AUTOGENERATED CONTENT DO NOT MODIFY
//                      PRODUCED BY CsWpfBase.Db
//
//********************************************************************
//
//
//Copyright (c) 2014 - 2016 All rights reserved Christian Sack
//<author>Christian Sack</author>
//<email>service.christian@sack.at</email>
//<website>christian.sack.at</website>
//<date>2016-03-29 22:55:08</date>



using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.models;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.router;
using ICashBookEntry=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.ICashBookEntry;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces
{
	///	<summary>Interface for <see cref="CashBookEntry"/> can be used to create POCO object or other models.</summary>
	[CsDbDataRowInterface(Database = "BillingDatabase", TableName = "CashBook", Generated = "16.03.29 22:55:08", Hash = "2715BF8F6986AC66AFB886AAA01585A0")]
	public interface ICashBookEntry
	{
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>]</summary>
		Guid Id { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ReferenceNumber</c>]</summary>
		Int32 ReferenceNumber { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Date</c>]</summary>
		DateTime Date { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>AmountGross</c>]</summary>
		Decimal AmountGross { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TaxPercent</c>]</summary>
		Decimal TaxPercent { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Issuer</c>]</summary>
		String Issuer { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Recipient</c>]</summary>
		String Recipient { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Text</c>]</summary>
		String Text { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternRecipientId</c>]</summary>
		String InternRecipientId { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternDescription</c>]</summary>
		String InternDescription { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LastEdited</c>]</summary>
		DateTime LastEdited { get; set; }
	}
}