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
//<date>2016-05-07 18:01:35</date>



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
using MailedBeleg=BillingDataAccess.sqlcedatabases.billingdatabase.rows.MailedBeleg;
using IMailedBeleg=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IMailedBeleg;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>MailedBelege</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="MailedBeleg"/>' as <see cref="DataRow"/>.<para/>
	///		[<c>MailedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]<para/>
	///		[<c>MailedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.MailedBelege): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "MailedBelege", Generated = "16.05.07 18:01:35", Hash = "B738CB163543330F110158C896413969")]
	[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
	[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
	public partial class MailedBelegeTable : CsDbTable<MailedBeleg>
	{
		#region CONSTANTS
		///<summary>The native table name (MailedBelege).</summary>
		public const string NativeName = "MailedBelege";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[Id]</c> column. Property = <see cref="MailedBeleg.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[BelegDataId]</c> column. Property = <see cref="MailedBeleg.BelegDataId"/>.</summary>
		public const string BelegDataIdCol = "BelegDataId";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[ProcessingStateName]</c> column. Property = <see cref="MailedBeleg.ProcessingStateName"/>.</summary>
		public const string ProcessingStateNameCol = "ProcessingStateName";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[ProcessingDate]</c> column. Property = <see cref="MailedBeleg.ProcessingDate"/>.</summary>
		public const string ProcessingDateCol = "ProcessingDate";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[ProcessingException]</c> column. Property = <see cref="MailedBeleg.ProcessingException"/>.</summary>
		public const string ProcessingExceptionCol = "ProcessingException";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[OutputFormatId]</c> column. Property = <see cref="MailedBeleg.OutputFormatId"/>.</summary>
		public const string OutputFormatIdCol = "OutputFormatId";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[TargetMailAddress]</c> column. Property = <see cref="MailedBeleg.TargetMailAddress"/>.</summary>
		public const string TargetMailAddressCol = "TargetMailAddress";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[Betreff]</c> column. Property = <see cref="MailedBeleg.Betreff"/>.</summary>
		public const string BetreffCol = "Betreff";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[Text]</c> column. Property = <see cref="MailedBeleg.Text"/>.</summary>
		public const string TextCol = "Text";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[Comment]</c> column. Property = <see cref="MailedBeleg.Comment"/>.</summary>
		public const string CommentCol = "Comment";
		///<summary>Holds native column name of <c>[BillingDatabase].[MailedBelege].[CommentLastChanged]</c> column. Property = <see cref="MailedBeleg.CommentLastChanged"/>.</summary>
		public const string CommentLastChangedCol = "CommentLastChanged";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class ColAttributes
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>ProcessingStateName</c>]<para/>
			///		MaxLength = 100;
			/// </summary>
			public static int ProcessingStateNameMaxLength = 100;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>ProcessingException</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int ProcessingExceptionMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>TargetMailAddress</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int TargetMailAddressMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>Betreff</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int BetreffMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>Text</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int TextMaxLength = 536870911;
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>MailedBelege</c>].[<c>Comment</c>]<para/>
			///		MaxLength = 536870911;
			/// </summary>
			public static int CommentMaxLength = 536870911;
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public MailedBelegeTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [MailedBelege]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [MailedBelege]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [MailedBelege] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public MailedBeleg FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [MailedBelege] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public MailedBeleg LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as MailedBeleg;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public MailedBeleg Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as MailedBeleg;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<MailedBeleg>>> _byBelegDataId = new Dictionary<Guid?, CsWeakReference<ContractCollection<MailedBeleg>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<MailedBeleg> FindOrLoad_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.BelegDataId, belegDataId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{BelegDataIdCol}] = '{belegDataId}'", false);
				result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.BelegDataId, belegDataId));
			}
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<MailedBeleg> LoadThenFind_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{BelegDataIdCol}]F = '{belegDataId}'", false);
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.BelegDataId, belegDataId));
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<MailedBeleg> Find_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.BelegDataId, belegDataId));
		
		    if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<MailedBeleg>>> _byOutputFormatId = new Dictionary<Guid?, CsWeakReference<ContractCollection<MailedBeleg>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<MailedBeleg> FindOrLoad_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.OutputFormatId, outputFormatId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{OutputFormatIdCol}] = '{outputFormatId}'", false);
				result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.OutputFormatId, outputFormatId));
			}
		
			if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<MailedBeleg> LoadThenFind_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{OutputFormatIdCol}]F = '{outputFormatId}'", false);
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.OutputFormatId, outputFormatId));
		
			if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM MailedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>MailedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<MailedBeleg> Find_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<MailedBeleg>> weak;
			ContractCollection<MailedBeleg> result;
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@mailedBeleg => Equals(@mailedBeleg.OutputFormatId, outputFormatId));
		
		    if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<MailedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public MailedBeleg AddAsNewRow(IMailedBeleg item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}