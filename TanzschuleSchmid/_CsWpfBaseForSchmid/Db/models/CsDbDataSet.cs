﻿// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Db.models.internalhelper;






namespace CsWpfBase.Db.models
{
	/// <summary>The base class for all data sets generated by the cs db engine</summary>
	[Serializable]
	public abstract class CsDbDataSet : CsDbDataSetBase
	{
		[field: NonSerialized] private ObservableCollection<CsDbTableBase> _collection;
		private bool _schemaLoaded;

		/// <summary>ctor</summary>
		public CsDbDataSet()
		{
			Tables.CollectionChanged += Tables_CollectionChanged;
		}


		/// <summary>Gets the bind able collection of the tables. This is useful in binding scenery's</summary>
		public ObservableCollection<CsDbTableBase> Collection => _collection ?? (_collection = new ObservableCollection<CsDbTableBase>(Tables.OfType<CsDbTableBase>()));

		/// <summary>Gets the owning data context for this data set. The owning context is the relative addressing method for other databases on the same server.</summary>
		public CsDbDataContext DataContext { get; set; }


		/// <summary>Load all schema from all tables.</summary>
		public void LoadSchema()
		{
			if (_schemaLoaded)
				return;

			foreach (var tableName in TableNames)
			{
				GetTableByName(tableName).LoadSchema();
			}

			_schemaLoaded = true;
		}

		/// <summary>Sets the db proxy by an <see cref="IDbProxyAssociateable"/> type.</summary>
		public void Set_DbProxy<T>() where T: IDbProxyAssociateable
		{
			var instance = Activator.CreateInstance<T>();
			instance.Associate(DataSetName);
			DbProxy = instance;
		}
		/// <summary>Sets the db proxy.</summary>
		public void Set_DbProxy(IDbProxy proxy)
		{
			DbProxy = proxy;
		}

		/// <summary>
		///     Executes a command on a data set. The results tables have to match with the native table names in data base. Otherwise the tables cannot be
		///     associated to the right table.
		/// </summary>
		/// <param name="command">The sql command which delivers multiple tables.</param>
		/// <param name="preserveChanges">if true, the current row version will not be changed.</param>
		public void ExecuteCommand(string command, bool preserveChanges = true)
		{
			var dataSet = DbProxy.ExecuteDataSetCommand(command);
			foreach (DataTable table in dataSet.Tables)
			{
				var targetTable = GetTableByName(table.TableName);
				targetTable.Merge(table, preserveChanges);
			}
		}

		/// <summary>Creates a new native data set with the schema and data from the current fixed typed dataset.</summary>
		public DataSet CloneTo_Native()
		{
			var legacySet = new DataSet(DataSetName);
			foreach (CsDbTable table in Tables)
			{
				legacySet.Tables.Add(table.CloneTo_Native());
			}
			return legacySet;
		}

		/// <summary>Loads the data from the native dataset.</summary>
		public void LoadFrom_Native(DataSet set)
		{
			foreach (DataTable table in set.Tables)
			{
				GetTableByName(table.TableName).Merge(table);
			}
		}

		/// <summary>Overrides the has been loaded flag on all tables</summary>
		public void SetHasBeenLoaded(bool value = true)
		{
			foreach (CsDbTable table in Tables)
			{
				table.HasBeenLoaded = value;
			}
		}


		/// <summary>Find a table inside the table collection. If it does not exist create it.</summary>
		protected virtual T GetTable<T>(string name) where T : DataTable
		{
			if (Tables.Contains(name))
				return (T) Tables[name];
			var table = (T) Activator.CreateInstance(typeof (T));
			Tables.Add(table);
			return table;
		}

		private void Tables_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (_collection == null)
				return;
			if (e.Action == CollectionChangeAction.Add)
				_collection.Add((CsDbTableBase) e.Element);
			if (e.Action == CollectionChangeAction.Remove)
				_collection.Remove((CsDbTableBase) e.Element);

		}


		/// <summary>Adds the specific set of arguments as an relation to the data set.</summary>
		/// <param name="name">The name of the relation.</param>
		/// <param name="parent">the parent data column.</param>
		/// <param name="child">the child column.</param>
		/// <param name="deleteRule">the delete rule for the relation.</param>
		/// <param name="updateRule">the update rule for the relation.</param>
		protected void AddRelation(string name, DataColumn parent, DataColumn child, Rule deleteRule, Rule updateRule)
		{
			var relation = new DataRelation(name, parent, child);
			Relations.Add(relation);
			relation.ChildKeyConstraint.DeleteRule = deleteRule;
			relation.ChildKeyConstraint.UpdateRule = updateRule;
		}

		/// <summary>Adds anabolic changes to a specific data set. This means only added and modified rows will be included.</summary>
		protected static void AddAnabolicChanges(DataSet targetSet, DataTable table)
		{
			var dtA = table.GetChanges(DataRowState.Added | DataRowState.Modified);
			if (dtA != null)
				targetSet.Tables.Add(dtA);
		}

		/// <summary>Adds katabolic changes to a specific data set. This means only deleted and modified rows will be included.</summary>
		protected static void AddKatabolicChanges(DataSet targetSet, DataTable table)
		{
			var dtA = table.GetChanges(DataRowState.Deleted | DataRowState.Modified);
			if (dtA != null)
				targetSet.Tables.Add(dtA);
		}
		
	}



}