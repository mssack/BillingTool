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
using Configuration=BillingDataAccess.sqlcedatabases.billingdatabase.rows.Configuration;
using IConfiguration=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IConfiguration;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>Configurations</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="Configuration"/>' as <see cref="DataRow"/>.<para/>
	///		
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.Configurations): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "Configurations", Generated = "16.05.14 11:36:09", Hash = "ED709A115FE66C56C5734F79B6BF3287")]
	
	public partial class ConfigurationsTable : CsDbTable<Configuration>
	{
		#region CONSTANTS
		///<summary>The native table name (Configurations).</summary>
		public const string NativeName = "Configurations";
		///<summary>Holds native column name of <c>[BillingDatabase].[Configurations].[Name]</c> column. Property = <see cref="Configuration.Name"/>.</summary>
		public const string NameCol = "Name";
		///<summary>Holds native column name of <c>[BillingDatabase].[Configurations].[Value]</c> column. Property = <see cref="Configuration.Value"/>.</summary>
		public const string ValueCol = "Value";
		///<summary>Holds native column name of <c>[BillingDatabase].[Configurations].[LastChanged]</c> column. Property = <see cref="Configuration.LastChanged"/>.</summary>
		public const string LastChangedCol = "LastChanged";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Configurations</c>].[<c>Name</c>]<para/>
			///		MaxLength = 100;
			/// </summary>
			public static int NameMaxLength = 100;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>Configurations</c>].[<c>Value</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int ValueMaxLength = 536870911;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public ConfigurationsTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [Configurations]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [Configurations]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows(int top)
		{
			DownloadRows($"SELECT TOP {top} {DefaultSqlSelector} FROM [{NativeName}]", false);
		}
		
		
		///<summary>This method calls <see cref="FindOrLoad"/>.</summary>
		public override CsDbRowBase Generic_FindOrLoad(object name)
		{
			return name==null ? null : FindOrLoad((String) name);
		}
		///<summary>This method calls <see cref="LoadThenFind"/>.</summary>
		public override CsDbRowBase Generic_LoadThenFind(object name)
		{
			return name==null ? null : LoadThenFind((String) name);
		}
		///<summary>This method calls <see cref="Find"/>.</summary>
		public override CsDbRowBase Generic_Find(object name)
		{
			return name==null ? null : Find((String) name);
		}
		#endregion
		
		
		
		
		#region FUNC<Primary Key>
		
		///	<summary>
		///		find an item in local data where Name = '<paramref name="name"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Configurations] WHERE [Name] = '<paramref name="name"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public Configuration FindOrLoad(String name)
		{
			if (name == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(name);
		
			return Find(name) ?? LoadThenFind(name);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [Configurations] WHERE [Name] = '<paramref name="name"/>'</c> THEN find an item in local data where Name = '<paramref name="name"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		
		public Configuration LoadThenFind(String name)
		{
			if (name == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Name] = '{name}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[NameCol] };
		
			return Rows.Find(name) as Configuration;
		}
		///	<summary>
		///		find an item in local data where Name = '<paramref name="name"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		
		public Configuration Find(String name)
		{
			if (name == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[NameCol] };
		
			return Rows.Find(name) as Configuration;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public Configuration AddAsNewRow(IConfiguration item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}