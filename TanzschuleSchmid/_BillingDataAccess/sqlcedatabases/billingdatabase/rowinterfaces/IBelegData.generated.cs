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
//<date>2016-04-02 21:24:13</date>



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
using IBelegData=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IBelegData;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces
{
	///	<summary>Interface for <see cref="BelegData"/> can be used to create POCO object or other models.</summary>
	[CsDbDataRowInterface(Database = "BillingDatabase", TableName = "BelegDaten", Generated = "16.04.02 21:24:13", Hash = "8A4B7D7A990142E59F1FCC16C6FEBBF6")]
	public interface IBelegData
	{
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Id</c>]</summary>
		Guid Id { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StateName</c>]</summary>
		String StateName { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>TypName</c>]</summary>
		String TypName { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Datum</c>]</summary>
		DateTime Datum { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenId</c>]</summary>
		String KassenId { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>]</summary>
		String KassenOperator { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Nummer</c>]</summary>
		Int32 Nummer { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>UmsatzZähler</c>]</summary>
		Decimal UmsatzZähler { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StornoBelegId</c>]</summary>
		Guid? StornoBelegId { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragBrutto</c>]</summary>
		Decimal BetragBrutto { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragNetto</c>]</summary>
		Decimal BetragNetto { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZusatzText</c>]</summary>
		String ZusatzText { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>PrintCount</c>]</summary>
		Int32 PrintCount { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>MailCount</c>]</summary>
		Int32 MailCount { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Empfänger</c>]</summary>
		String Empfänger { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>EmpfängerId</c>]</summary>
		String EmpfängerId { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Kommentar</c>]</summary>
		String Kommentar { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZuletztGeändert</c>]</summary>
		DateTime ZuletztGeändert { get; set; }
	}
}