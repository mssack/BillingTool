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
//<date>2016-04-02 16:52:38</date>



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
using BillingDataAccess.sqlcedatabases.billingdatabase.views;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.dataset
{
	/// <summary>'[<c>SqlCeDatabasesContext</c>].[<c>BillingDatabase</c>]': a dataset/database inside context [<c>SqlCeDatabasesContext</c>] providing the tables and views inside database [<c>BillingDatabase</c>].</summary>
	[Serializable] [DebuggerStepThrough]
	[DebuggerDisplay("DB[BillingDatabase]: Tables[{Tables.Count}]")]
	[CsDbDataSet(Name = "BillingDatabase", Generated = "16.04.02 16:52:38", Hash = "FB4FE70EB0255606718BC3DE4FA23F06")]
	public partial class BillingDatabase : CsDbDataSet
	{
	
	
	#region StaticDefinitions: Table names, Relations, schema description,...
	
		private static DataSet _schemaSet;
		private static CsDbRelation[] _csDbRelations;
		private static Dictionary<Type, CsDbRelation[]> _csDbRelationsPerTableType;
	
	
		/// <summary>The database name for the the database 'BillingDatabase' </summary>
		public const string NativeName = "BillingDatabase";
	
	
		///	<summary>Gets a list of the native names of all tables inside the database.</summary>
		public static string[] StaticTableNames = new string[]{"BelegDaten", "BelegPostens", "Configurations", "Logs", "MailedBelege", "Postens", "PrintedBelege", "Steuersätze"};
	
	
		///	<summary>Gets a list of the native relations of all tables inside the data set. With this set of relations you can use reflection to dynamically get linked rows.</summary>
		public static CsDbRelation[] StaticCsDbRelations
		{
			get
			{
				if (_csDbRelations != null)
					return _csDbRelations;
				_csDbRelations = new[]{new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegDatenTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegDatenTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData), "BelegDaten", "Id", "BelegDaten", "StornoBelegId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("StornoBelegId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("StornoBeleg"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("StornierendeBelege")),
					new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegDatenTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegPostensTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten), "BelegDaten", "Id", "BelegPostens", "BelegDataId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("BelegDataId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("Data"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("Postens")),
					new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegDatenTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.MailedBelegeTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.MailedBeleg), "BelegDaten", "Id", "MailedBelege", "BelegDataId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.MailedBeleg).GetProperty("BelegDataId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.MailedBeleg).GetProperty("BelegData"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("MailedBelege")),
					new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegDatenTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.PrintedBelegeTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.PrintedBeleg), "BelegDaten", "Id", "PrintedBelege", "BelegDataId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.PrintedBeleg).GetProperty("BelegDataId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.PrintedBeleg).GetProperty("BelegData"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegData).GetProperty("PrintedBelege")),
					new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.PostensTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Posten), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegPostensTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten), "Postens", "Id", "BelegPostens", "PostenId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Posten).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("PostenId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("Posten"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Posten).GetProperty("BelegPostens")),
					new CsDbRelation(typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.SteuersätzeTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Steuersatz), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.tables.BelegPostensTable), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten), "Steuersätze", "Id", "BelegPostens", "SteuersatzId", typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Steuersatz).GetProperty("Id"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("SteuersatzId"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.BelegPosten).GetProperty("Steuersatz"), typeof(BillingDataAccess.sqlcedatabases.billingdatabase.rows.Steuersatz).GetProperty("Steuersätze")),			};
				return _csDbRelations;
			}
		}
		
		///	<summary>Gets a list of the native relations of all tables inside the data set. With this set of relations you can use reflection to dynamically get linked rows. Use table type as key.</summary>
		public static Dictionary<Type, CsDbRelation[]> StaticCsDbRelationsPerTableType
		{
			get
			{
				if (_csDbRelationsPerTableType != null)
					return _csDbRelationsPerTableType;
	
					
				var dict = new Dictionary<Type, List<CsDbRelation>>();
				foreach (var relation in StaticCsDbRelations)
				{
					List<CsDbRelation> pkrelations;
					if (!dict.TryGetValue(relation.PkTableType, out pkrelations))
					{
						pkrelations = new List<CsDbRelation>();
						dict.Add(relation.PkTableType, pkrelations);
					}
					if (!pkrelations.Contains(relation))
						pkrelations.Add(relation);
	
	
					List<CsDbRelation> fkrelations;
					if (!dict.TryGetValue(relation.FkTableType, out fkrelations))
					{
						fkrelations = new List<CsDbRelation>();
						dict.Add(relation.FkTableType, fkrelations);
					}
					if (!fkrelations.Contains(relation))
						fkrelations.Add(relation);
				}
				return _csDbRelationsPerTableType = dict.ToDictionary(x => x.Key, x => x.Value.ToArray());
			}
		}
	
	
	
	
		///	<summary>Gets a list of the native relations of all tables inside the data set. With this set of relations you can use reflection to dynamically get linked rows.</summary>
		public override CsDbRelation[] CsDbRelations => StaticCsDbRelations;
	
		///	<summary>Gets a list of the native relations of all tables inside the data set. With this set of relations you can use reflection to dynamically get linked rows.</summary>
		public override Dictionary<Type, CsDbRelation[]> CsDbRelationsPerTableType => StaticCsDbRelationsPerTableType;
		
		///	<summary>Gets a list of the native names of all tables inside the database.</summary>
		public override string[] TableNames => StaticTableNames;
	
	
	
		/// <summary> Used as a database template for the schema.</summary>
		public override DataSet SchemaSet
		{
			get
			{
				if (_schemaSet != null)
					return _schemaSet;
	
				_schemaSet = DbProxy.ExecuteDataSetCommand(TableNames.Select(tableName => $"SELECT TOP(0) * FROM [{tableName}]").Join(";"));
				for (int i = 0; i < TableNames.Length; i++)
				{
					string tableName = TableNames[i];
					_schemaSet.Tables[i].TableName = tableName;
				}
				return _schemaSet;
			}
		}
	#endregion
	
	
	#region WPF Extension
		///<summary>Use this to propagate an instance of the data set trough the logical tree of an WPF control.</summary>
		public static readonly DependencyProperty InstanceProperty = DependencyProperty.RegisterAttached("Instance", typeof (BillingDatabase), typeof (BillingDatabase), new FrameworkPropertyMetadata(default(BillingDatabase), FrameworkPropertyMetadataOptions.Inherits));
		///<summary>Use this to propagate an instance of the data set trough the logical tree of an WPF control.</summary>
		public static void SetInstance(DependencyObject element, BillingDatabase value)
		{
			element.SetValue(InstanceProperty, value);
		}
		///<summary>Use this to get the propagated instance from a control inside the logical tree. You have to set the property anywhere in upstream to get it with this method.</summary>
		public static BillingDatabase GetInstance(DependencyObject element)
		{
			return (BillingDatabase) element.GetValue(InstanceProperty);
		}
	#endregion
	
	
		public BillingDatabase()
		{
			DataSetName = "BillingDatabase";
		}
	
		///<summary>Gets the owning data context for this data set. The owning context is the relative addressing method for other databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext
		{
			get { return (BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext) base.DataContext; }
			internal set { base.DataContext = value; }
		}
	
		///<summary>Gets the native name of the owning data context or the native name of the database server associated with this.</summary>
		public override string DataContextName => DataContext?.Name ?? "SqlCeDatabasesContext";
	
	
	#region Tables
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>BelegDaten</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public BelegDatenTable BelegDaten => GetTable<BelegDatenTable>("BelegDaten");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>BelegPostens</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public BelegPostensTable BelegPostens => GetTable<BelegPostensTable>("BelegPostens");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>Configurations</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public ConfigurationsTable Configurations => GetTable<ConfigurationsTable>("Configurations");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>Logs</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public LogsTable Logs => GetTable<LogsTable>("Logs");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>MailedBelege</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public MailedBelegeTable MailedBelege => GetTable<MailedBelegeTable>("MailedBelege");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>Postens</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public PostensTable Postens => GetTable<PostensTable>("Postens");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>PrintedBelege</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public PrintedBelegeTable PrintedBelege => GetTable<PrintedBelegeTable>("PrintedBelege");
	
		///<summary>Get or Create DataTable([<c>BillingDatabase</c>].[<c>Steuersätze</c>]) => If table does not exist in <see cref="Tables"/> collection, it will be created and inserted automatically.</summary>
		public SteuersätzeTable Steuersätze => GetTable<SteuersätzeTable>("Steuersätze");
	#endregion
	
	
	
	
	#region Views
		
	#endregion
	
		private bool _constraintsLoaded = false;
	
		///<summary>First it loads the schema then the relations and after that it enforces the constraint's.</summary>
		public override void LoadConstraints()
		{
			if (_constraintsLoaded)
				return;
	
			LoadSchema();
	
			AddRelation("FK_BelegData_StornoBelegId", BelegDaten.Columns[BelegDatenTable.IdCol], BelegDaten.Columns[BelegDatenTable.StornoBelegIdCol], Rule.None, Rule.None);
			AddRelation("FK_BelegPostens_BelegDataId", BelegDaten.Columns[BelegDatenTable.IdCol], BelegPostens.Columns[BelegPostensTable.BelegDataIdCol], Rule.Cascade, Rule.None);
			AddRelation("FK_MailedBelege_BelegDataId", BelegDaten.Columns[BelegDatenTable.IdCol], MailedBelege.Columns[MailedBelegeTable.BelegDataIdCol], Rule.Cascade, Rule.None);
			AddRelation("FK_PrintedBelege_BelegDataId", BelegDaten.Columns[BelegDatenTable.IdCol], PrintedBelege.Columns[PrintedBelegeTable.BelegDataIdCol], Rule.Cascade, Rule.None);
			AddRelation("FK_BelegPostens_PostenId", Postens.Columns[PostensTable.IdCol], BelegPostens.Columns[BelegPostensTable.PostenIdCol], Rule.Cascade, Rule.None);
			AddRelation("FK_BelegPostens_SteuersatzId", Steuersätze.Columns[SteuersätzeTable.IdCol], BelegPostens.Columns[BelegPostensTable.SteuersatzIdCol], Rule.Cascade, Rule.None);
			
			_constraintsLoaded = true;
		}
		///<summary>Saves the tables in an order which is good for creating new items.</summary>
		public override void SaveAnabolic(object tag = null)
		{
			BillingDatabase targetSet = new BillingDatabase();
	
			if(Tables.Contains("BelegDaten")) AddAnabolicChanges(targetSet, BelegDaten);
			if(Tables.Contains("Configurations")) AddAnabolicChanges(targetSet, Configurations);
			if(Tables.Contains("Logs")) AddAnabolicChanges(targetSet, Logs);
			if(Tables.Contains("Postens")) AddAnabolicChanges(targetSet, Postens);
			if(Tables.Contains("Steuersätze")) AddAnabolicChanges(targetSet, Steuersätze);
			if(Tables.Contains("BelegPostens")) AddAnabolicChanges(targetSet, BelegPostens);
			if(Tables.Contains("MailedBelege")) AddAnabolicChanges(targetSet, MailedBelege);
			if(Tables.Contains("PrintedBelege")) AddAnabolicChanges(targetSet, PrintedBelege);
			
			if (targetSet.Tables.Count != 0)
				DbProxy.SaveChanges(targetSet.CloneTo_Native(), tag);
		}
		///<summary>Saves the tables in an order which is good for deleting items.</summary>
		public override void SaveKatabolic(object tag = null)
		{
			BillingDatabase targetSet = new BillingDatabase();
	
			if(Tables.Contains("BelegPostens")) AddKatabolicChanges(targetSet, BelegPostens);
			if(Tables.Contains("MailedBelege")) AddKatabolicChanges(targetSet, MailedBelege);
			if(Tables.Contains("PrintedBelege")) AddKatabolicChanges(targetSet, PrintedBelege);
			if(Tables.Contains("BelegDaten")) AddKatabolicChanges(targetSet, BelegDaten);
			if(Tables.Contains("Configurations")) AddKatabolicChanges(targetSet, Configurations);
			if(Tables.Contains("Logs")) AddKatabolicChanges(targetSet, Logs);
			if(Tables.Contains("Postens")) AddKatabolicChanges(targetSet, Postens);
			if(Tables.Contains("Steuersätze")) AddKatabolicChanges(targetSet, Steuersätze);
			
			if (targetSet.Tables.Count != 0)
				DbProxy.SaveChanges(targetSet.CloneTo_Native(), tag);
		}
		///<summary>Get the right table by its table name</summary>
		public override CsDbTableBase GetTableByName(string nativeName)
		{
			switch (nativeName)
			{
				case "BelegDaten":
					return BelegDaten;
				case "BelegPostens":
					return BelegPostens;
				case "Configurations":
					return Configurations;
				case "Logs":
					return Logs;
				case "MailedBelege":
					return MailedBelege;
				case "Postens":
					return Postens;
				case "PrintedBelege":
					return PrintedBelege;
				case "Steuersätze":
					return Steuersätze;
				default:
					throw new Exception($"Table with native name [{nativeName}] not found.");
			}
		}
	}
	
}