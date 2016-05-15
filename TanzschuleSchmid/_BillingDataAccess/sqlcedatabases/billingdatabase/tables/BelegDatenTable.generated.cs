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
//<date>2016-05-15 16:08:28</date>



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
using BelegData=BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData;
using IBelegData=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IBelegData;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>BelegDaten</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="BelegData"/>' as <see cref="DataRow"/>.<para/>
	///		[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegDaten</c>].[<c>StornoBelegId</c>]<para/>
	///		[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegPostens</c>].[<c>BelegDataId</c>]<para/>
	///		[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>MailedBelege</c>].[<c>BelegDataId</c>]<para/>
	///		[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>PrintedBelege</c>].[<c>BelegDataId</c>]
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.BelegDaten): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "BelegDaten", Generated = "16.05.15 16:08:28", Hash = "D92E0DF2FE1EE26BAF2E91CB545B04D0")]
	[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
	[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
	[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
	[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
	public partial class BelegDatenTable : CsDbTable<BelegData>
	{
		#region CONSTANTS
		///<summary>The native table name (BelegDaten).</summary>
		public const string NativeName = "BelegDaten";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[Id]</c> column. Property = <see cref="BelegData.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[StateName]</c> column. Property = <see cref="BelegData.StateName"/>.</summary>
		public const string StateNameCol = "StateName";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[TypName]</c> column. Property = <see cref="BelegData.TypName"/>.</summary>
		public const string TypNameCol = "TypName";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[Datum]</c> column. Property = <see cref="BelegData.Datum"/>.</summary>
		public const string DatumCol = "Datum";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[KassenId]</c> column. Property = <see cref="BelegData.KassenId"/>.</summary>
		public const string KassenIdCol = "KassenId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[KassenOperator]</c> column. Property = <see cref="BelegData.KassenOperator"/>.</summary>
		public const string KassenOperatorCol = "KassenOperator";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[Nummer]</c> column. Property = <see cref="BelegData.Nummer"/>.</summary>
		public const string NummerCol = "Nummer";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[UmsatzZähler]</c> column. Property = <see cref="BelegData.UmsatzZähler"/>.</summary>
		public const string UmsatzZählerCol = "UmsatzZähler";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[StornoBelegId]</c> column. Property = <see cref="BelegData.StornoBelegId"/>.</summary>
		public const string StornoBelegIdCol = "StornoBelegId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[BetragBrutto]</c> column. Property = <see cref="BelegData.BetragBrutto"/>.</summary>
		public const string BetragBruttoCol = "BetragBrutto";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[BetragNetto]</c> column. Property = <see cref="BelegData.BetragNetto"/>.</summary>
		public const string BetragNettoCol = "BetragNetto";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[ZusatzText]</c> column. Property = <see cref="BelegData.ZusatzText"/>.</summary>
		public const string ZusatzTextCol = "ZusatzText";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[PrintCount]</c> column. Property = <see cref="BelegData.PrintCount"/>.</summary>
		public const string PrintCountCol = "PrintCount";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[MailCount]</c> column. Property = <see cref="BelegData.MailCount"/>.</summary>
		public const string MailCountCol = "MailCount";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[Empfänger]</c> column. Property = <see cref="BelegData.Empfänger"/>.</summary>
		public const string EmpfängerCol = "Empfänger";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[EmpfängerId]</c> column. Property = <see cref="BelegData.EmpfängerId"/>.</summary>
		public const string EmpfängerIdCol = "EmpfängerId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[ZahlungsReferenz]</c> column. Property = <see cref="BelegData.ZahlungsReferenz"/>.</summary>
		public const string ZahlungsReferenzCol = "ZahlungsReferenz";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[Comment]</c> column. Property = <see cref="BelegData.Comment"/>.</summary>
		public const string CommentCol = "Comment";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegDaten].[CommentLastChanged]</c> column. Property = <see cref="BelegData.CommentLastChanged"/>.</summary>
		public const string CommentLastChangedCol = "CommentLastChanged";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StateName</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int StateNameMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>TypName</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int TypNameMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenId</c>]<para/>
			///		MaxLength = 255;
			/// </summary>
			public static int KassenIdMaxLength = 255;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int KassenOperatorMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZusatzText</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int ZusatzTextMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Empfänger</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int EmpfängerMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>EmpfängerId</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int EmpfängerIdMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZahlungsReferenz</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int ZahlungsReferenzMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Comment</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int CommentMaxLength = 536870911;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public BelegDatenTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [BelegDaten]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [BelegDaten]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [BelegDaten] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public BelegData FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [BelegDaten] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public BelegData LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as BelegData;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public BelegData Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as BelegData;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<BelegData>>> _byStornoBelegId = new Dictionary<Guid?, CsWeakReference<ContractCollection<BelegData>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegDaten WHERE [StornoBelegId] = '<paramref name="stornoBelegId"/>'</c><para/>
		///		[<c>BelegDaten</c>].[<c>StornoBelegId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		public ContractCollection<BelegData> FindOrLoad_By_StornoBelegId(Guid? stornoBelegId)
		{
			if (stornoBelegId == null)
				return null;
			CsWeakReference<ContractCollection<BelegData>> weak;
			ContractCollection<BelegData> result;
		
			if (_byStornoBelegId.TryGetValue(stornoBelegId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@belegData => Equals(@belegData.StornoBelegId, stornoBelegId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{StornoBelegIdCol}] = '{stornoBelegId}'", false);
				result = CreateContractCollection(@belegData => Equals(@belegData.StornoBelegId, stornoBelegId));
			}
		
			if (weak == null)
				_byStornoBelegId.Add(stornoBelegId, weak = new CsWeakReference<ContractCollection<BelegData>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegDaten WHERE [StornoBelegId] = '<paramref name="stornoBelegId"/>'</c><para/>
		///		[<c>BelegDaten</c>].[<c>StornoBelegId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		public ContractCollection<BelegData> LoadThenFind_By_StornoBelegId(Guid? stornoBelegId)
		{
			if (stornoBelegId == null)
				return null;
			CsWeakReference<ContractCollection<BelegData>> weak;
			ContractCollection<BelegData> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{StornoBelegIdCol}]F = '{stornoBelegId}'", false);
		
			if (_byStornoBelegId.TryGetValue(stornoBelegId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@belegData => Equals(@belegData.StornoBelegId, stornoBelegId));
		
			if (weak == null)
				_byStornoBelegId.Add(stornoBelegId, weak = new CsWeakReference<ContractCollection<BelegData>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegDaten WHERE [StornoBelegId] = '<paramref name="stornoBelegId"/>'</c><para/>
		///		[<c>BelegDaten</c>].[<c>StornoBelegId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		public ContractCollection<BelegData> Find_By_StornoBelegId(Guid? stornoBelegId)
		{
			if (stornoBelegId == null)
				return null;
			CsWeakReference<ContractCollection<BelegData>> weak;
			ContractCollection<BelegData> result;
		
			if (_byStornoBelegId.TryGetValue(stornoBelegId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@belegData => Equals(@belegData.StornoBelegId, stornoBelegId));
		
		    if (weak == null)
				_byStornoBelegId.Add(stornoBelegId, weak = new CsWeakReference<ContractCollection<BelegData>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public BelegData AddAsNewRow(IBelegData item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}