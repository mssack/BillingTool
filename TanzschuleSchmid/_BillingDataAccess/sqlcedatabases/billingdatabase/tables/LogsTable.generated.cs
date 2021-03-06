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
//<date>2016-05-14 11:36:09</date>



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
using Log=BillingDataAccess.sqlcedatabases.billingdatabase.rows.Log;
using ILog=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.ILog;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>Logs</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="Log"/>' as <see cref="DataRow"/>.<para/>
	///		
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.Logs): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "Logs", Generated = "16.05.14 11:36:09", Hash = "CEA8ECEE27AAB7882DDF0B968ABE0ABC")]
	
	public partial class LogsTable : CsDbTable<Log>
	{
		#region CONSTANTS
		///<summary>The native table name (Logs).</summary>
		public const string NativeName = "Logs";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[Id]</c> column. Property = <see cref="Log.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[TypeName]</c> column. Property = <see cref="Log.TypeName"/>.</summary>
		public const string TypeNameCol = "TypeName";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[Date]</c> column. Property = <see cref="Log.Date"/>.</summary>
		public const string DateCol = "Date";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[CodePosition]</c> column. Property = <see cref="Log.CodePosition"/>.</summary>
		public const string CodePositionCol = "CodePosition";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[CommandLine]</c> column. Property = <see cref="Log.CommandLine"/>.</summary>
		public const string CommandLineCol = "CommandLine";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[Title]</c> column. Property = <see cref="Log.Title"/>.</summary>
		public const string TitleCol = "Title";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[Content]</c> column. Property = <see cref="Log.Content"/>.</summary>
		public const string ContentCol = "Content";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[Comment]</c> column. Property = <see cref="Log.Comment"/>.</summary>
		public const string CommentCol = "Comment";
		///<summary>Holds native column name of <c>[BillingDatabase].[Logs].[CommentLastChanged]</c> column. Property = <see cref="Log.CommentLastChanged"/>.</summary>
		public const string CommentLastChangedCol = "CommentLastChanged";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>TypeName</c>]<para/>
			///		MaxLength = 100;
			/// </summary>
			public static int TypeNameMaxLength = 100;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>CodePosition</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int CodePositionMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>CommandLine</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int CommandLineMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>Title</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int TitleMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>Content</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int ContentMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Logs</c>].[<c>Comment</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int CommentMaxLength = 536870911;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public LogsTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [Logs]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [Logs]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Logs] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public Log FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Logs] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		
		public Log LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as Log;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public Log Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as Log;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public Log AddAsNewRow(ILog item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}