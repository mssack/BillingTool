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
//<date>2016-05-31 16:32:57</date>



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
using PrintedBeleg=BillingDataAccess.sqlcedatabases.billingdatabase.rows.PrintedBeleg;
using IPrintedBeleg=BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces.IPrintedBeleg;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	#pragma warning disable 657
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///	<summary>
	///		'[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>].[<c>PrintedBelege</c>]': A Table inside '[<c>SqlCeDatabases</c>].[<c>#BillingDatabase#</c>]' database with '<see cref="PrintedBeleg"/>' as <see cref="DataRow"/>.<para/>
	///		[<c>PrintedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]<para/>
	///		[<c>PrintedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
	///	</summary>
	[Serializable]
	[DebuggerDisplay("DataTable(BillingDatabase.PrintedBelege): Rows[{Rows.Count}]")]
	[CsDbDataTable(Database = "BillingDatabase", Name = "PrintedBelege", Generated = "16.05.31 16:32:57", Hash = "C187C7A24FA99488AA859A09020ABCDC")]
	[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
	[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
	public partial class PrintedBelegeTable : CsDbTable<PrintedBeleg>
	{
		#region CONSTANTS
		///<summary>The native table name (PrintedBelege).</summary>
		public const string NativeName = "PrintedBelege";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[Id]</c> column. Property = <see cref="PrintedBeleg.Id"/>.</summary>
		public const string IdCol = "Id";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[BelegDataId]</c> column. Property = <see cref="PrintedBeleg.BelegDataId"/>.</summary>
		public const string BelegDataIdCol = "BelegDataId";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[ProcessingStateNumber]</c> column. Property = <see cref="PrintedBeleg.ProcessingStateNumber"/>.</summary>
		public const string ProcessingStateNumberCol = "ProcessingStateNumber";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[ProcessingDate]</c> column. Property = <see cref="PrintedBeleg.ProcessingDate"/>.</summary>
		public const string ProcessingDateCol = "ProcessingDate";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[ProcessingException]</c> column. Property = <see cref="PrintedBeleg.ProcessingException"/>.</summary>
		public const string ProcessingExceptionCol = "ProcessingException";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[OutputFormatId]</c> column. Property = <see cref="PrintedBeleg.OutputFormatId"/>.</summary>
		public const string OutputFormatIdCol = "OutputFormatId";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[PrinterDevice]</c> column. Property = <see cref="PrintedBeleg.PrinterDevice"/>.</summary>
		public const string PrinterDeviceCol = "PrinterDevice";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[Comment]</c> column. Property = <see cref="PrintedBeleg.Comment"/>.</summary>
		public const string CommentCol = "Comment";
		///<summary>Holds native column name of <c>[BillingDatabase].[PrintedBelege].[CommentLastChanged]</c> column. Property = <see cref="PrintedBeleg.CommentLastChanged"/>.</summary>
		public const string CommentLastChangedCol = "CommentLastChanged";
	
		/// <summary> Contains attribute values for the columns</summary>
		public static class Cols
		{
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>Id</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute Id = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>BelegDataId</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "BelegDataId", Type = "uniqueidentifier", IsNullable = "NO"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute BelegDataId = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "BelegDataId", Type = "uniqueidentifier", IsNullable = "NO"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingStateNumber</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingStateNumber", Type = "int", Default = "0", IsNullable = "NO"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute ProcessingStateNumber = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingStateNumber", Type = "int", Default = "0", IsNullable = "NO"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingDate</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingDate", Type = "datetime", Default = "getdate()", IsNullable = "NO"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute ProcessingDate = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingDate", Type = "datetime", Default = "getdate()", IsNullable = "NO"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>ProcessingException</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingException", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute ProcessingException = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "ProcessingException", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>OutputFormatId</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "OutputFormatId", Type = "uniqueidentifier", IsNullable = "NO"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute OutputFormatId = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "OutputFormatId", Type = "uniqueidentifier", IsNullable = "NO"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>PrinterDevice</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "PrinterDevice", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute PrinterDevice = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "PrinterDevice", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>Comment</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "Comment", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute Comment = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "Comment", Type = "ntext", MaxLength = "536870911", IsNullable = "YES"};
			/// <summary>
			///		Get the attribute from the column [<c>BillingDatabase</c>].[<c>PrintedBelege</c>].[<c>CommentLastChanged</c>]<para/>
			///		#Mode# = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "CommentLastChanged", Type = "datetime", IsNullable = "YES"};
			/// </summary>
			public static CsDbNativeDataColumnAttribute CommentLastChanged = new CsDbNativeDataColumnAttribute {Table = "PrintedBelege", Name = "CommentLastChanged", Type = "datetime", IsNullable = "YES"};
		}
		#endregion
	
	
	
	
	
	
	
		///<summary>Default constructor for save <see cref="DataTable"/> operations</summary>
		public PrintedBelegeTable()
		{
			TableName = NativeName;
		}
	
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => DataSet.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => (BillingDatabase) base.DataSet;
	
	
	
	
	
	
	
		#region FUNC<Overrides>
		
		///	<summary><c>SELECT (DefaultSqlSelector) FROM [PrintedBelege]</c><para>The default selector is the * operator</para></summary>
		public override void DownloadRows()
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}]", false);
			HasBeenLoaded = true;
		}
		///	<summary><c>SELECT <paramref name="top"/> (DefaultSqlSelector) FROM [PrintedBelege]</c><para>The default selector is the * operator</para></summary>
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
		///		find an item in local data where Id = '<paramref name="id"/>'. If nothing is found QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [PrintedBelege] WHERE [Id] = '<paramref name="id"/>'</c>.<para/>
		///		If no primary key is set execute <see cref="LoadThenFind"/> instead.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public PrintedBeleg FindOrLoad(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				return LoadThenFind(id.Value);
		
			return Find(id.Value) ?? LoadThenFind(id.Value);
		}
		///<summary>
		///		QUERY WITH <c>SELECT {DefaultSqlSelector} FROM [PrintedBelege] WHERE [Id] = '<paramref name="id"/>'</c> THEN find an item in local data where Id = '<paramref name="id"/>'.<para/>
		///		IMPORTENT: Sets primary key if not set already.<para/>
		///</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public PrintedBeleg LoadThenFind(Guid? id)
		{
			if (id == null)
				return null;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [Id] = '{id}'", false);
			
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as PrintedBeleg;
		}
		///	<summary>
		///		find an item in local data where Id = '<paramref name="id"/>'. IMPORTENT: Sets primary key if not set already.<para/>
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public PrintedBeleg Find(Guid? id)
		{
			if (id == null)
				return null;
		
			if (PrimaryKey.Length == 0)
				PrimaryKey = new[] { Columns[IdCol] };
		
			return Rows.Find(id.Value) as PrintedBeleg;
		}
		#endregion
		
		
		
		
		
		#region FUNC<Foreign Key>
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<PrintedBeleg>>> _byBelegDataId = new Dictionary<Guid?, CsWeakReference<ContractCollection<PrintedBeleg>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<PrintedBeleg> FindOrLoad_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.BelegDataId, belegDataId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{BelegDataIdCol}] = '{belegDataId}'", false);
				result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.BelegDataId, belegDataId));
			}
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<PrintedBeleg> LoadThenFind_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{BelegDataIdCol}]F = '{belegDataId}'", false);
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.BelegDataId, belegDataId));
		
			if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [BelegDataId] = '<paramref name="belegDataId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>BelegDataId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<PrintedBeleg> Find_By_BelegDataId(Guid? belegDataId)
		{
			if (belegDataId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			if (_byBelegDataId.TryGetValue(belegDataId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.BelegDataId, belegDataId));
		
		    if (weak == null)
				_byBelegDataId.Add(belegDataId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		[field:NonSerialized] private Dictionary<Guid?, CsWeakReference<ContractCollection<PrintedBeleg>>> _byOutputFormatId = new Dictionary<Guid?, CsWeakReference<ContractCollection<PrintedBeleg>>>();
		
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<PrintedBeleg> FindOrLoad_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			if (HasBeenLoaded == true || weak != null)
				result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.OutputFormatId, outputFormatId));
			else 
			{
				DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{OutputFormatIdCol}] = '{outputFormatId}'", false);
				result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.OutputFormatId, outputFormatId));
			}
		
			if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<PrintedBeleg> LoadThenFind_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			DownloadRows($"SELECT {DefaultSqlSelector} FROM {NativeName} WHERE [{OutputFormatIdCol}]F = '{outputFormatId}'", false);
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
			result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.OutputFormatId, outputFormatId));
		
			if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		///	<summary> 
		///		Query <c>SELECT (DefaultSqlSelector) FROM PrintedBelege WHERE [OutputFormatId] = '<paramref name="outputFormatId"/>'</c><para/>
		///		[<c>PrintedBelege</c>].[<c>OutputFormatId</c>] &#62;&#62;&#62;&#62; [<c>OutputFormats</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<PrintedBeleg> Find_By_OutputFormatId(Guid? outputFormatId)
		{
			if (outputFormatId == null)
				return null;
			CsWeakReference<ContractCollection<PrintedBeleg>> weak;
			ContractCollection<PrintedBeleg> result;
		
			if (_byOutputFormatId.TryGetValue(outputFormatId, out weak) && weak.TryGetTarget(out result))
				return result;
		
		
			result = CreateContractCollection(@printedBeleg => Equals(@printedBeleg.OutputFormatId, outputFormatId));
		
		    if (weak == null)
				_byOutputFormatId.Add(outputFormatId, weak = new CsWeakReference<ContractCollection<PrintedBeleg>>(result));
			else
				weak.SetTarget(result);
		
			return result;
		}
		#endregion
		
		
		/// <summary>Creates a row then copy's the data from the <paramref name="item"/> and adds it to the row collection.</summary>
		public PrintedBeleg AddAsNewRow(IPrintedBeleg item)
		{
			var row = NewRow();
			row.Copy_From(item, true);
			Add(row);
			return row;
		}
	}
}