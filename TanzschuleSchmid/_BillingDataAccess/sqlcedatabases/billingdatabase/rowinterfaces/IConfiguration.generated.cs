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
//<date>2016-04-20 09:45:53</date>



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
using IConfiguration=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IConfiguration;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces
{
	///	<summary>Interface for <see cref="Configuration"/> can be used to create POCO object or other models.</summary>
	[CsDbDataRowInterface(Database = "BillingDatabase", TableName = "Configurations", Generated = "16.04.20 09:45:53", Hash = "1E685D952169BE1D69A84B56EBB6AC34")]
	public interface IConfiguration
	{
		///	<summary>[<c>BillingDatabase</c>].[<c>Configurations</c>].[<c>Name</c>]</summary>
		String Name { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>Configurations</c>].[<c>Value</c>]</summary>
		String Value { get; set; }
		///	<summary>[<c>BillingDatabase</c>].[<c>Configurations</c>].[<c>LastChanged</c>]</summary>
		DateTime LastChanged { get; set; }
	}
}