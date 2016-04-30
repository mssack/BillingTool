// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Db.models.helper
{
	/// <summary>
	///     A full bind able collection. This collection is bound to a table and a contract. The collection will be automatically filled with the items which
	///     meets the requirements of the contract.
	/// </summary>
	public abstract class ContractCollection : Base, INotifyCollectionChanged, IEnumerable
	{
		private object _tag;


		#region Abstract
		/// <summary>The items count.</summary>
		public abstract int Count { get; }

		protected internal abstract IEnumerator AbstractGetEnumerator();

		protected internal abstract object GetItemAtIndex(int index);
		#endregion


		#region Overrides/Interfaces
		/// <summary>Occurs whenever the collection changes</summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return AbstractGetEnumerator();
		}
		#endregion


		/// <summary>Gets the item at the specific position.</summary>
		[IndexerName("Item")]
		public object this[int index] => GetItemAtIndex(index);


		/// <summary>Gets or sets the Tag.</summary>
		public object Tag
		{
			get { return _tag; }
			set { SetProperty(ref _tag, value); }
		}
		/// <summary>The expression which is used to validate a row.</summary>
		public Expression ConditionExpression { get; protected set; }

		/// <summary>invokes the collection changed event</summary>
		/// <param name="e"></param>
		protected internal void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			CollectionChanged?.Invoke(this, e);
			OnPropertyChanged("Item[]");
			OnPropertyChanged("Count");
		}
	}



	/// <summary>
	///     A full bind able collection. This collection is bound to a table and a contract. The collection will be automatically filled with the items which
	///     meets the requirements of the contract.
	/// </summary>
	public sealed class ContractCollection<TRow> : ContractCollection, IEnumerable<TRow>
		where TRow : CsDbRowBase
	{
		private readonly HashSet<string> _dependingProperties;
		private readonly IEnumerable<TRow> _startingCollection;
		private HashSet<TRow> _hashList;
		private List<TRow> _list;
		private SortHandler _sortHandler;



		/// <summary>
		///     Creates a new collection which is based on a <paramref name="table" /> and a contract. The contract is used to validate a row inside the table.
		///     If the contract returns true the row will be added to the collection.
		/// </summary>
		/// <param name="table">The table this collection belongs to.</param>
		/// <param name="condition">
		///     If the result of this function is true the item will be included in this collection. Otherwise the item will be deleted or
		///     not included.
		/// </param>
		/// <param name="startingCollection">
		///     The collection to start with. If collection is null the whole table will be searched and validated with
		///     <paramref name="condition" />.
		/// </param>
		internal ContractCollection(CsDbTable<TRow> table, Expression<Func<TRow, bool>> condition, IEnumerable<TRow> startingCollection = null)
		{
			_dependingProperties = new HashSet<string>(condition.GetReferencedProperties().Select(x => x.Name));
			ConditionExpression = condition;
			Condition = condition.Compile();
			Table = table;
			table.ContractReferences.Add(new CsWeakReference<ContractCollection<TRow>>(this));
			_startingCollection = startingCollection;
		}


		#region Overrides/Interfaces
		/// <summary>The items count.</summary>
		public override int Count => List.Count;



#if DEBUG && DbTrace

		/// <summary>Destructor</summary>
		~ContractCollection()
		{
#if DEBUG && DbTrace
			Debug.WriteLine($"~ContractCollection<{typeof(TRow).Name}> ({Count}) --> garbage collected.");
#endif
		}

#endif

		protected internal override IEnumerator AbstractGetEnumerator()
		{
			return GetEnumerator();
		}

		protected internal override object GetItemAtIndex(int index)
		{
			return this[index];
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		public IEnumerator<TRow> GetEnumerator()
		{
			return List.GetEnumerator();
		}
		#endregion


		/// <summary>The contract is a function which validates an item whether it belongs to the collection or not.</summary>
		public Func<TRow, bool> Condition { get; }

		/// <summary>The owning table of this collection.</summary>
		public CsDbTable<TRow> Table { get; }


		/// <summary>Gets the item at the specific position.</summary>
		[IndexerName("Item")]
		public new TRow this[int index] => List[index];


		private List<TRow> List
		{
			get
			{
				if (_list == null)
					InitData();

				if (_sortHandler != null && _sortHandler.IsPending)
					_sortHandler.Execute();


				return _list;
			}
		}
		private HashSet<TRow> HashList
		{
			get
			{
				if (_hashList != null) return _hashList;
				InitData();

				return _hashList;
			}
		}


		/// <summary>Revalidates the contract on all items inside the owning table. This can be useful if contract condition changes.</summary>
		public void Evaluate()
		{
			foreach (var row in Table)
			{
				EvaluateRow(row, null, false);
			}


			if (_sortHandler == null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			else
				_sortHandler.Schedule();

		}

		/// <summary>Sorts the list.</summary>
		public void Sort(Expression<Func<TRow, object>> by)
		{

			if (by == null)
			{
				_sortHandler = null;
				return;
			}

			_sortHandler = new SortHandler(by, SortRequested, () => _list.Sort(new AnonComparer<TRow, object>(by.Compile())));
			_sortHandler.Schedule();
		}


		/// <summary>Sorts the list.</summary>
		public void SortDesc(Expression<Func<TRow, object>> by)
		{
			if (by == null)
			{
				_sortHandler = null;
				return;
			}

			_sortHandler = new SortHandler(by, SortRequested, () => _list = _list.OrderByDescending(by.Compile()).ToList());
			_sortHandler.Schedule();
		}


		private void InitData()
		{
			_hashList = new HashSet<TRow>();
			_list = new List<TRow>();

			if (_startingCollection == null)
			{
				foreach (var row in Table)
				{
					EvaluateRow(row, null, false);
				}
				return;
			}

			foreach (var row in _startingCollection)
			{
				EvaluateRow(row, null, false);
			}
		}



		internal void EvaluateRow(TRow row, string changedProperty = null, bool notify = true)
		{
			var needSort = _sortHandler?.Dependencys?.Contains(changedProperty);
			var needValidation = changedProperty == null || _dependingProperties.Contains(changedProperty);

			if (needValidation == false && needSort == false)
				return;

			var contained = HashList.Contains(row);
			if (needValidation == false && needSort == true)
			{
				_sortHandler.Schedule();
				return;
			}

			if (contained && (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached)) // Remove if deleted
			{
				var indexOf = List.IndexOf(row);
				HashList.Remove(row);
				List.RemoveAt(indexOf);
				if (notify)
					OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, row, indexOf));
				return;
			}
			if (!contained && row.RowState == DataRowState.Deleted)
			{
				return;
			}

			var valid = Condition(row);

			if (valid && contained)
			{
				if (needSort == true)
					_sortHandler.Schedule();
				return;
			}
			if (!valid && !contained)
				return;

			if (valid)
			{
				HashList.Add(row);
				List.Add(row);

				if (_sortHandler != null)
					_sortHandler.Schedule();
				else if (notify)
					OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, row));
			}
			else
			{
				var indexOf = List.IndexOf(row);
				HashList.Remove(row);
				List.RemoveAt(indexOf);
				if (notify)
					OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, row, indexOf));
			}
		}

		private void SortRequested()
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}





		private class SortHandler
		{
			public SortHandler(Expression<Func<TRow, object>> by, Action onScheduledAction, Action action)
			{
				Dependencys = new HashSet<string>(by.GetReferencedProperties().Select(x => x.Name));
				OnScheduledAction = onScheduledAction;
				Action = action;
			}

			public bool IsPending { get; private set; }
			public HashSet<string> Dependencys { get; private set; }
			private Action Action { get; }
			private Action OnScheduledAction { get; }

			public void Execute()
			{
				Action();
				IsPending = false;
			}

			public void Schedule()
			{
				IsPending = true;
				OnScheduledAction();
			}
		}
	}
}