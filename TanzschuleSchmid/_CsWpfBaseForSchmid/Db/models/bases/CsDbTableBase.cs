// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.models.bases
{
	/// <summary>The base object for all tables inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbTableBase : DataTable, INotifyPropertyChanged, IContainDbProxy
	{
		[field: NonSerialized] private readonly Dictionary<string, string[]> _propertyDependencys;
		[field: NonSerialized] private PropertyChangedEventHandler _propertyChanged;
		[field: NonSerialized] private bool _schemaLoaded;

		/// <summary>ctor</summary>
		protected CsDbTableBase()
		{
			_propertyDependencys = ReflectionHelper.GetPropertyDependencys(GetType());
		}


		#region Abstract
		/// <summary><c>SELECT (DefaultSqlSelector) FROM [{TableName}]</c></summary>
		public abstract void DownloadRows();

		/// <summary><c>SELECT <paramref name="top" /> (DefaultSqlSelector) FROM [{TableName}]</c></summary>
		public abstract void DownloadRows(int top);

		/// <summary>
		///     This is the generic method to select by the primary key. Try to find the <paramref name="primaryKeyValue" /> in local data. If
		///     <paramref name="primaryKeyValue" /> is not found it will be loaded from database.
		/// </summary>
		public abstract CsDbRowBase Generic_FindOrLoad(object primaryKeyValue);

		/// <summary>
		///     This is the generic method to select by the primary key. Load or update data from database then try to find <paramref name="primaryKeyValue" />
		///     in local data.
		/// </summary>
		public abstract CsDbRowBase Generic_LoadThenFind(object primaryKeyValue);

		/// <summary>This is the generic method to select by the primary key. Try to find <paramref name="primaryKeyValue" /> in local data.</summary>
		public abstract CsDbRowBase Generic_Find(object primaryKeyValue);

		// ReSharper disable once InconsistentNaming
		/// <summary>This is the generic version of the Collection property.</summary>
		public abstract IEnumerable Generic_Collection { get; }
		#endregion


		#region Overrides
		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.</summary>
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);

			var rowBase = (CsDbRowBase) e.Row;
			if (e.Action == DataRowAction.Add)
			{
				RegisterRowChangedEvent(rowBase);
			}
			else if (e.Action == DataRowAction.Delete)
			{
				UnregisterRowChangedEvent(rowBase);
			}
			else if (e.Action == DataRowAction.Change)
				rowBase.OnRowChanged();
			else if (e.Action == DataRowAction.Rollback)
				rowBase.OnRowChanged();
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			var rowBase = (CsDbRowBase) e.Row;
			UnregisterRowChangedEvent(rowBase);
		}

		/// <summary>Occurs whenever a value of a column changes directly.</summary>
		protected override void OnColumnChanged(DataColumnChangeEventArgs e)
		{
			base.OnColumnChanged(e);
			((CsDbRowBase) e.Row).OnColumnChanged(e.Column.ColumnName);
		}
		#endregion


		#region Interfaces
		/// <summary>occurs whenever a cell changes or a property inside this class.</summary>
		public virtual event PropertyChangedEventHandler PropertyChanged
		{
			add { _propertyChanged = (PropertyChangedEventHandler) Delegate.Combine(_propertyChanged, value); }
			remove { _propertyChanged = (PropertyChangedEventHandler) Delegate.Remove(_propertyChanged, value); }
		}


		/// <summary>The db proxy all commands should be routed through this.</summary>
		public virtual IDbProxy DbProxy
		{
			get
			{
				var dataSetProxy = (DataSet as IContainDbProxy)?.DbProxy;
				if (dataSetProxy == null)
					throw new Exception("No db proxy found on DataSet. Implement IContainDbProxy on DataSet Level and include this table in the data set.");
				return dataSetProxy;
			}
		}
		#endregion


		///	<summary>If HasBeenLoaded equals true, the generated properties will not execute a sql command. Instead it will search in the locally available data.</summary>
		[field: NonSerialized]
		public bool HasBeenLoaded { get; set; }


		/// <summary>Gets the owning data set.</summary>
		public new CsDbDataSet DataSet => (CsDbDataSet) base.DataSet;


		/// <summary>Gets the owning data context.</summary>
		public CsDbDataContext DataContext => DataSet.DataContext;


		/// <summary>The default sql column selector used by the generated properties. (SELECT {DefaultSqlSelector} FROM ...</summary>
		protected virtual string DefaultSqlSelector => "*";

		/// <summary>Gets the relations for this table type.</summary>
		public CsDbRelation[] GetRelations()
		{
			CsDbRelation[] rv;
			if (DataSet.CsDbRelationsPerTableType.TryGetValue(GetType(), out rv))
				return rv;
			return new CsDbRelation[0];
		}

		/// <summary>Saves the changes of the table to the proxy</summary>
		public void SaveChanges(object tag = null)
		{
			var changes = GetChanges();
			if (changes == null)
				return;
			var table = new DataTable(TableName);
			table.Merge(this);
			DbProxy.SaveChanges(table, tag);
		}

		/// <summary>Loads only the schema of the table, no data. This method is secured from multiple invocations.</summary>
		public void LoadSchema()
		{
			if (_schemaLoaded)
				return;
			Merge(DataSet.SchemaSet.Tables[TableName]);
			_schemaLoaded = true;
		}

		/// <summary>Sets the property and calls on property changed if anything have changed.</summary>
		protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = "")
		{
			if (Equals(field, value))
				return false;
			field = value;
			OnPropertyChanged(propName);
			return true;
		}

		/// <summary>
		///     Invokes the property changed event for a value. IMPORTENT: Also sends property changed for all depending properties. Use
		///     <see cref="DependsOnAttribute" /> to specify properties that depends on other properties.
		/// </summary>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (_propertyChanged == null)
				return;
			if (propertyName == null)
				return;

			_propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

			string[] dependingPropertys;
			if (_propertyDependencys.TryGetValue(propertyName, out dependingPropertys))
				dependingPropertys.ForEach(x => _propertyChanged.Invoke(this, new PropertyChangedEventArgs(x)));
		}
		/// <summary>Creates a new native table with the schema and data from the current fixed table.</summary>
		public DataTable CloneTo_Native()
		{
			var legacyTable = new DataTable(TableName);
			legacyTable.Merge(this);
			return legacyTable;
		}


		/// <summary>Occurs whenever a property changes inside a row.</summary>
		protected internal virtual void RowInternalPropertyChanged(object sender, PropertyChangedEventArgs e)
		{

		}

		/// <summary>Converts a value to a valid sql param</summary>
		protected string SqlParam(string value)
		{
			return value.Replace("'", "''");
		}

		private void RegisterRowChangedEvent(CsDbRowBase row)
		{
			row.InternalPropertyChanged += RowInternalPropertyChanged;
		}

		private void UnregisterRowChangedEvent(CsDbRowBase row)
		{
			row.InternalPropertyChanged -= RowInternalPropertyChanged;
		}
	}
}