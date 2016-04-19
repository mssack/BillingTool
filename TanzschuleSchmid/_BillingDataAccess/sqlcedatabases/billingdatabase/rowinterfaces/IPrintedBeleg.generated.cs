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
//<date>2016-04-19 15:25:55</date>



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
using IPrintedBeleg=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IPrintedBeleg;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces
{
	///	<summary>Interface for <see cref="PrintedBeleg"/> can be used to create POCO object or other models.</summary>
	[CsDbDataRowInterface(Database = "BillingDatabase", TableName = "PrintedBelege", Generated = "16.04.19 15:25:55", Hash = "1A1CFDE2298B0A6B113540A77FD90F52")]
	public interface IPrintedBeleg
	{
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>Id</c>]</summary>
		Guid Id { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>BelegDataId</c>]</summary>
		Guid BelegDataId { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingStateName</c>]</summary>
		String ProcessingStateName { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingDate</c>]</summary>
		DateTime ProcessingDate { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingException</c>]</summary>
		String ProcessingException { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>OutputFormatName</c>]</summary>
		String OutputFormatName { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>PrinterDevice</c>]</summary>
		String PrinterDevice { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>Comment</c>]</summary>
		String Comment { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>CommentLastChanged</c>]</summary>
		DateTime? CommentLastChanged { get; set; }
	}
}