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
//<date>2016-03-31 00:05:42</date>



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
	[CsDbDataTable(Database = "BillingDatabase", Name = "CashBook", Generated = "16.03.31 00:05:42", Hash = "414F8B08BA0DAEC167945C068CC87139")]
	
	public partial class CashBookTable : CsDbTable<CashBookEntry>
	{
		#region CONSTANTS
		///<summary>The native table name (CashBook).</summary>
		public const string NativeName = "CashBook";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Id]</c> column. Property = <see cref="CashBookEntry.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[BelegNummer]</c> column. Property = <see cref="CashBookEntry.BelegNummer"/>.</summary>
		public const string BelegNummerCol = "BelegNummer";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[KassenId]</c> column. Property = <see cref="CashBookEntry.KassenId"/>.</summary>
		public const string KassenIdCol = "KassenId";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[BelegAussteller]</c> column. Property = <see cref="CashBookEntry.BelegAussteller"/>.</summary>
		public const string BelegAusstellerCol = "BelegAussteller";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Datum]</c> column. Property = <see cref="CashBookEntry.Datum"/>.</summary>
		public const string DatumCol = "Datum";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[BetragBrutto]</c> column. Property = <see cref="CashBookEntry.BetragBrutto"/>.</summary>
		public const string BetragBruttoCol = "BetragBrutto";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[UmsatzZähler]</c> column. Property = <see cref="CashBookEntry.UmsatzZähler"/>.</summary>
		public const string UmsatzZählerCol = "UmsatzZähler";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[Steuersatz]</c> column. Property = <see cref="CashBookEntry.Steuersatz"/>.</summary>
		public const string SteuersatzCol = "Steuersatz";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[SteuersatzArt]</c> column. Property = <see cref="CashBookEntry.SteuersatzArt"/>.</summary>
		public const string SteuersatzArtCol = "SteuersatzArt";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[LeistungsBeschreibung]</c> column. Property = <see cref="CashBookEntry.LeistungsBeschreibung"/>.</summary>
		public const string LeistungsBeschreibungCol = "LeistungsBeschreibung";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[BelegText]</c> column. Property = <see cref="CashBookEntry.BelegText"/>.</summary>
		public const string BelegTextCol = "BelegText";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[InternEmpfänger]</c> column. Property = <see cref="CashBookEntry.InternEmpfänger"/>.</summary>
		public const string InternEmpfängerCol = "InternEmpfänger";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[InterneEmpfängerId]</c> column. Property = <see cref="CashBookEntry.InterneEmpfängerId"/>.</summary>
		public const string InterneEmpfängerIdCol = "InterneEmpfängerId";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[InterneBeschreibung]</c> column. Property = <see cref="CashBookEntry.InterneBeschreibung"/>.</summary>
		public const string InterneBeschreibungCol = "InterneBeschreibung";
		///<summary>Holds native column name of <c>[BillingDatabase].[CashBook].[ZuletztGeändert]</c> column. Property = <see cref="CashBookEntry.ZuletztGeändert"/>.</summary>
		public const string ZuletztGeändertCol = "ZuletztGeändert";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegAussteller</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int BelegAusstellerMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>SteuersatzArt</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int SteuersatzArtMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LeistungsBeschreibung</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int LeistungsBeschreibungMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegText</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int BelegTextMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternEmpfänger</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int InternEmpfängerMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneEmpfängerId</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int InterneEmpfängerIdMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneBeschreibung</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int InterneBeschreibungMaxLength = 536870911;
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