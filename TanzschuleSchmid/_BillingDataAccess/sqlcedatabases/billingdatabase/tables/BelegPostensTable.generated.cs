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
using BelegPosten=BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten;
using IBelegPosten=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IBelegPosten;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>BelegPostens</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="BelegPosten"/>' as <see cref="DataRow"/>.<para/>
	///		[<c>BelegPostens</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]<para/>
	///		[<c>BelegPostens</c>].[<c>PostenId</c>] &#62;&#62;&#62;&#62; [<c>Postens</c>].[<c>Id</c>]<para/>
	///		[<c>BelegPostens</c>].[<c>SteuersatzId</c>] &#62;&#62;&#62;&#62; [<c>Steuersätze</c>].[<c>Id</c>]
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.BelegPostens): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "BelegPostens", Generated = "16.05.14 11:36:09", Hash = "A99D6CC14A5DA34D4120878EA6F01DF2")]
	[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
	[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
	[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
	public partial class BelegPostensTable : CsDbTable<BelegPosten>
	{
		#region CONSTANTS
		///<summary>The native table name (BelegPostens).</summary>
		public const string NativeName = "BelegPostens";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[Id]</c> column. Property = <see cref="BelegPosten.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[BelegDataId]</c> column. Property = <see cref="BelegPosten.BelegDataId"/>.</summary>
		public const string BelegDataIdCol = "BelegDataId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[SteuersatzId]</c> column. Property = <see cref="BelegPosten.SteuersatzId"/>.</summary>
		public const string SteuersatzIdCol = "SteuersatzId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[PostenId]</c> column. Property = <see cref="BelegPosten.PostenId"/>.</summary>
		public const string PostenIdCol = "PostenId";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[CreationDate]</c> column. Property = <see cref="BelegPosten.CreationDate"/>.</summary>
		public const string CreationDateCol = "CreationDate";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[Anzahl]</c> column. Property = <see cref="BelegPosten.Anzahl"/>.</summary>
		public const string AnzahlCol = "Anzahl";
		///<summary>Holds native column name of <c>[BillingDatabase].[BelegPostens].[Reduziert]</c> column. Property = <see cref="BelegPosten.Reduziert"/>.</summary>
		public const string ReduziertCol = "Reduziert";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public BelegPostensTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [BelegPostens]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [BelegPostens]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [BelegPostens] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public BelegPosten FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [BelegPostens] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public BelegPosten LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as BelegPosten;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public BelegPosten Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as BelegPosten;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>> _byBelegDataId = new Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		public ContractCollection<BelegPosten> FindOrLoad_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.BelegDataId, belegDataId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{BelegDataIdCol}] = '{belegDataId}'", false);
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.BelegDataId, belegDataId));
			}
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		public ContractCollection<BelegPosten> LoadThenFind_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{BelegDataIdCol}]F = '{belegDataId}'", false);
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.BelegDataId, belegDataId));
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		public ContractCollection<BelegPosten> Find_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.BelegDataId, belegDataId));
		
		    if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>> _byPostenId = new Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [PostenId] = '<paramref name="postenId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>PostenId</c>] &#62;&#62;&#62;&#62; [<c>Postens</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		public ContractCollection<BelegPosten> FindOrLoad_By_PostenId(Guid? postenId)
		{
			if (postenId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_byPostenId.TryGetValue(postenId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.PostenId, postenId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{PostenIdCol}] = '{postenId}'", false);
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.PostenId, postenId));
			}
		
			if (weak == null)
				_byPostenId.Add(postenId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [PostenId] = '<paramref name="postenId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>PostenId</c>] &#62;&#62;&#62;&#62; [<c>Postens</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		public ContractCollection<BelegPosten> LoadThenFind_By_PostenId(Guid? postenId)
		{
			if (postenId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{PostenIdCol}]F = '{postenId}'", false);
		
			if (_byPostenId.TryGetValue(postenId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.PostenId, postenId));
		
			if (weak == null)
				_byPostenId.Add(postenId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [PostenId] = '<paramref name="postenId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>PostenId</c>] &#62;&#62;&#62;&#62; [<c>Postens</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		public ContractCollection<BelegPosten> Find_By_PostenId(Guid? postenId)
		{
			if (postenId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_byPostenId.TryGetValue(postenId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.PostenId, postenId));
		
		    if (weak == null)
				_byPostenId.Add(postenId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>> _bySteuersatzId = new Dictionary<Guid?, CsWeakReference<ContractCollection<BelegPosten>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [SteuersatzId] = '<paramref name="steuersatzId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>SteuersatzId</c>] &#62;&#62;&#62;&#62; [<c>Steuersätze</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public ContractCollection<BelegPosten> FindOrLoad_By_SteuersatzId(Guid? steuersatzId)
		{
			if (steuersatzId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_bySteuersatzId.TryGetValue(steuersatzId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.SteuersatzId, steuersatzId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{SteuersatzIdCol}] = '{steuersatzId}'", false);
				result = CreateContractCollection(@belegPosten => Equals(@belegPosten.SteuersatzId, steuersatzId));
			}
		
			if (weak == null)
				_bySteuersatzId.Add(steuersatzId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [SteuersatzId] = '<paramref name="steuersatzId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>SteuersatzId</c>] &#62;&#62;&#62;&#62; [<c>Steuersätze</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public ContractCollection<BelegPosten> LoadThenFind_By_SteuersatzId(Guid? steuersatzId)
		{
			if (steuersatzId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{SteuersatzIdCol}]F = '{steuersatzId}'", false);
		
			if (_bySteuersatzId.TryGetValue(steuersatzId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.SteuersatzId, steuersatzId));
		
			if (weak == null)
				_bySteuersatzId.Add(steuersatzId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM BelegPostens WHERE [SteuersatzId] = '<paramref name="steuersatzId"/>'</c><para/>
		///		[<c>BelegPostens</c>].[<c>SteuersatzId</c>] &#62;&#62;&#62;&#62; [<c>Steuersätze</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_SteuersatzId", PkTable = "Steuersätze", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "SteuersatzId")]
		public ContractCollection<BelegPosten> Find_By_SteuersatzId(Guid? steuersatzId)
		{
			if (steuersatzId == null)
				return null;
			CsWeakReference<ContractCollection<BelegPosten>> weak;
			ContractCollection<BelegPosten> result;
		
			if (_bySteuersatzId.TryGetValue(steuersatzId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@belegPosten => Equals(@belegPosten.SteuersatzId, steuersatzId));
		
		    if (weak == null)
				_bySteuersatzId.Add(steuersatzId, weak = new CsWeakReference<ContractCollection<BelegPosten>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public BelegPosten AddAsNewRow(IBelegPosten item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}