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
using System.Linq;
using System.Linq.Expressions;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.models.helper;






namespace CsWpfBase.Db.models
{

	/// <summary>The base for each row inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbTable : CsDbTableBase
	{
		
	}
	/// <summary>The base for each row inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbTable<TRow> : CsDbTable, IEnumerable<TRow>
		where TRow : CsDbRowBase
	{
		/// <summary>All contracts handled by this table.</summary>
		[field: NonSerialized] internal readonly List<CsWeakReference<ContractCollection<TRow>>> ContractReferences = new List<CsWeakReference<ContractCollection<TRow>>>();
		[field: NonSerialized] private CsDbTableRowCollection<TRow> _collection;


		#region Overrides
		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.</summary>
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);

			var row = (TRow) e.Row;
			if (e.Action == DataRowAction.Add)
			{
				Collection.Add(row);
				foreach (var contract in Contracts)
				{
					contract.EvaluateRow(row);
				}
			}
			else if (e.Action == DataRowAction.Delete)
			{
				Collection.Remove(row);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			var row = (TRow) e.Row;
			Collection.Remove(row);
			foreach (var contract in Contracts)
			{
				contract.EvaluateRow(row);
			}
		}

		/// <summary>This is the generic version of the Collection property.</summary>
		public override IEnumerable Generic_Collection => Collection;

		/// <summary>Occurs whenever a property changes inside a row.</summary>
		protected internal override void RowInternalPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.RowInternalPropertyChanged(sender, e);

			var row = (TRow) sender;
			Collection.APropertyChanged(row, e.PropertyName);
			foreach (var contract in Contracts)
			{
				contract.EvaluateRow(row, e.PropertyName);
			}
		}


		/// <summary>Gets the generic row type.</summary>
		protected override Type GetRowType()
		{
			return typeof (TRow);
		}

		/// <summary>Creates a specified generic type as <see cref="DataRow" /> for this <see cref="DataSet" />.</summary>
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			var row = (CsDbRowBase) Activator.CreateInstance(typeof (TRow), builder);
			return row;
		}
		#endregion


		#region Interfaces
		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		public IEnumerator<TRow> GetEnumerator()
		{
			return Rows.OfType<TRow>().Where(x => x.RowState != DataRowState.Detached && x.RowState != DataRowState.Deleted).GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion


		/// <summary>Gets a bind able collection which can be sorted.</summary>
		public CsDbTableRowCollection<TRow> Collection
		{
			get
			{
				if (_collection != null) return _collection;

				_collection = new CsDbTableRowCollection<TRow>(this);

				return _collection;
			}
		}
		/// <summary>Gets the row at the specific position.</summary>
		public TRow this[int index] => (TRow) Rows[index];
		/// <summary>Returns all active contracts</summary>
		protected IEnumerable<ContractCollection<TRow>> Contracts
		{
			get
			{
				var gcRefs = new List<CsWeakReference<ContractCollection<TRow>>>();
				for (var index = 0; index < ContractReferences.Count; index++)
				{
					var weakReference = ContractReferences[index];
					ContractCollection<TRow> contract;
					if (weakReference.TryGetTarget(out contract))
						yield return contract;
					else
						gcRefs.Add(weakReference);
				}

				while (gcRefs.Count != 0)
				{
					gcRefs.Remove(gcRefs[0]);
				}
			}
		}





		/// <summary>Creates a new contract by defining a condition. The <see cref="ContractCollection{TRow}" /> itself takes care of the refreshing.</summary>
		/// <param name="condition">The condition is used to check new rows against that condition and add it to the collection if needed.</param>
		/// <param name="initialCatalog">
		///     The initial catalog is the list of rows which should be checked at the initialization process. Subsequent filtering will be processed over the
		///     whole table.
		/// </param>
		public ContractCollection<TRow> CreateContractCollection(Expression<Func<TRow, bool>> condition, IEnumerable<TRow> initialCatalog = null)
		{
			return new ContractCollection<TRow>(this, condition, initialCatalog ?? this);
		}


		/// <summary>Selects specified rows. local data only</summary>
		public virtual new TRow[] Select(string selectStatement)
		{
			return base.Select(selectStatement).Cast<TRow>().ToArray();
		}

		/// <summary>Selects specified rows and order by <paramref name="sort" />. local data only</summary>
		public virtual new TRow[] Select(string selectStatement, string sort)
		{
			return base.Select(selectStatement, sort).Cast<TRow>().ToArray();
		}

		/// <summary>Selects specified rows and order by <paramref name="sort" />. local data only</summary>
		public virtual new TRow[] Select(string selectStatement, string sort, DataViewRowState recordStates)
		{
			return base.Select(selectStatement, sort, recordStates).Cast<TRow>().ToArray();
		}

		/// <summary>
		///     Creates a new row but do not add it to the collection. This does automatically apply's the default values to the rows specified by the
		///     <see cref="CsDbRowBase.ApplyDefaults" /> method.
		/// </summary>
		public virtual new TRow NewRow()
		{
			var row = (TRow) base.NewRow();
			row.ApplyDefaults();
			row.ApplyExtendedDefaults();
			return row;
		}

		/// <summary>Adds a row by adding it to the rows collection</summary>
		public virtual TRow Add(TRow item)
		{
			Rows.Add(item);
			return item;
		}


		/// <summary>
		///     Loads the rows from the data base by passing the <paramref name="sqlcommand" /> to the <see cref="CsDbTableBase.DbProxy" /> instance. The Result
		///     will be merged into the 'loading table' (=Caching).
		/// </summary>
		/// <param name="sqlcommand">The sql command which should be processed</param>
		/// <param name="createCollection">
		///     Specify whether the result should be analyzed and packed to a collection, which will be returned from this method. If you do not use the return
		///     value set this param to false.
		/// </param>
		/// <param name="preserveChanges">Specify whether the current changes should be preserved. If you specify false, all changes will be overwritten.</param>
		protected virtual internal TRow[] DownloadRows(string sqlcommand, bool createCollection = true, bool preserveChanges = true)
		{
			var table = DbProxy.ExecuteCommand(sqlcommand);

			Merge(table, preserveChanges);

			return !createCollection ? null : table.Rows.OfType<DataRow>().Select(x => x[table.PrimaryKey[0]]).Select(x => Rows.Find(x)).OfType<TRow>().ToArray();
		}
	}
}