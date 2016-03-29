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
//<date>2016-03-29 21:25:02</date>



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
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using CashBookEntry=BillingDataAccess.sqlcedatabases.billingdatabase.rows.CashBookEntry;
using ICashBookEntry=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.ICashBookEntry;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>CashBook</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="CashBookEntry"/>' as <see cref="DataRow"/>.<para/>
	///		
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.CashBook): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "CashBook", Generated = "16.03.29 21:25:02", Hash = "991B8602A67CEF78952C02835AA6F7DB")]
	
	public partial class CashBookTable : CsDbTable<CashBookEntry>
	{
		#region CONSTANTS
		///<summary>The native table name (CashBook).</summary>
		public const string NativeName = "CashBook";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Id]</c> column. Property = <see cref="CashBookEntry.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[ReferenceNumber]</c> column. Property = <see cref="CashBookEntry.ReferenceNumber"/>.</summary>
		public const string ReferenceNumberCol = "ReferenceNumber";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[CustomerId]</c> column. Property = <see cref="CashBookEntry.CustomerId"/>.</summary>
		public const string CustomerIdCol = "CustomerId";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[CustomerName]</c> column. Property = <see cref="CashBookEntry.CustomerName"/>.</summary>
		public const string CustomerNameCol = "CustomerName";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Date]</c> column. Property = <see cref="CashBookEntry.Date"/>.</summary>
		public const string DateCol = "Date";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Description]</c> column. Property = <see cref="CashBookEntry.Description"/>.</summary>
		public const string DescriptionCol = "Description";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[AmountGross]</c> column. Property = <see cref="CashBookEntry.AmountGross"/>.</summary>
		public const string AmountGrossCol = "AmountGross";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[TaxPercent]</c> column. Property = <see cref="CashBookEntry.TaxPercent"/>.</summary>
		public const string TaxPercentCol = "TaxPercent";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[LastEdited]</c> column. Property = <see cref="CashBookEntry.LastEdited"/>.</summary>
		public const string LastEditedCol = "LastEdited";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>CustomerId</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int CustomerIdMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>CustomerName</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int CustomerNameMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Description</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int DescriptionMaxLength = 536870911;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public CashBookTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [CashBook]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [CashBook]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows(int top)
		{
			DownloadRows($"SELECT TOP {top} {DefaultSqlSelector} FROM [{NativeName}]", false);
		}
		
		
		///<summary>This method calls <see cref="FindOrLoad"/>.</summary>
		public override CsDbRowBase Generic_FindOrLoad(object id)
		{
			return id==null ? null : FindOrLoad((Guid) id);
		}
		///<summary>This method calls <see cref="LoadThenFind"/>.</summary>
		public override CsDbRowBase Generic_LoadThenFind(object id)
		{
			return id==null ? null : LoadThenFind((Guid) id);
		}
		///<summary>This method calls <see cref="Find"/>.</summary>
		public override CsDbRowBase Generic_Find(object id)
		{
			return id==null ? null : Find((Guid) id);
		}
		#endregion
		
		
		
		
		#region FUNC<Primary Key>
		
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [CashBook] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public CashBookEntry FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [CashBook] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		
		public CashBookEntry LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as CashBookEntry;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public CashBookEntry Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as CashBookEntry;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public CashBookEntry AddAsNewRow(ICashBookEntry item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}