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
//<date>2016-04-02 19:05:26</date>



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
using Steuersatz=BillingDataAccess.sqlcedatabases.billingdatabase.rows.Steuersatz;
using ISteuersatz=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.ISteuersatz;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>Steuersätze</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="Steuersatz"/>' as <see cref="DataRow"/>.<para/>
	///		[<c>Steuersätze</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegPostens</c>].[<c>SteuersatzId</c>]
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.Steuersätze): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "Steuersätze", Generated = "16.04.02 19:05:26", Hash = "53B93ED9017CD11341DB74458C99A13E")]
	[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
	public partial class SteuersätzeTable : CsDbTable<Steuersatz>
	{
		#region CONSTANTS
		///<summary>The native table name (Steuersätze).</summary>
		public const string NativeName = "Steuersätze";
		///<summary>Holds native column name of <c>[BillingDatabase].[Steuersätze].[Id]</c> column. Property = <see cref="Steuersatz.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[Steuersätze].[Name]</c> column. Property = <see cref="Steuersatz.Name"/>.</summary>
		public const string NameCol = "Name";
		///<summary>Holds native column name of <c>[BillingDatabase].[Steuersätze].[Kürzel]</c> column. Property = <see cref="Steuersatz.Kürzel"/>.</summary>
		public const string KürzelCol = "Kürzel";
		///<summary>Holds native column name of <c>[BillingDatabase].[Steuersätze].[Percent]</c> column. Property = <see cref="Steuersatz.Percent"/>.</summary>
		public const string PercentCol = "Percent";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Steuersätze</c>].[<c>Name</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int NameMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Steuersätze</c>].[<c>Kürzel</c>]<para/>
			///		MaxLength = 10;
			/// </summary>
			public static int KürzelMaxLength = 10;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public SteuersätzeTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [Steuersätze]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [Steuersätze]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Steuersätze] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public Steuersatz FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Steuersätze] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public Steuersatz LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as Steuersatz;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public Steuersatz Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as Steuersatz;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public Steuersatz AddAsNewRow(ISteuersatz item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}