// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Db.models.helper
{
	/// <summary>A bind able collection extension for the <see cref="CsDbTable{T}" />.</summary>
	public class CsDbTableRowCollection<TRow> : INotifyPropertyChanged, INotifyCollectionChanged, IEnumerable<TRow>
		where TRow : CsDbRowBase
	{
		private List<TRow> _items;
		private HashSet<TRow> _itemsSet;
		private readonly CsDbTable<TRow> _table;

		internal CsDbTableRowCollection(CsDbTable<TRow> table)
		{
			_table = table;
		}


		#region Overrides/Interfaces
		/// <summary>Occurs whenever a property changes.</summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>Occurs whenever th collection changes.</summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;


		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
		public IEnumerator<TRow> GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion


		/// <summary>The items count.</summary>
		public int Count => Items.Count;

		/// <summary>Gets the item at the specific position.</summary>
		[IndexerName("Item")]
		public TRow this[int index] => Items[index];


		private List<TRow> Items
		{
			get
			{
				if (_items == null)
					_items = _table.ToList();

				if (Sorter != null && Sorter.IsPending)
					Sorter.Execute();

				return _items;
			}
			set { _items = value; }
		}
		private HashSet<TRow> ItemsSet
		{
			get
			{
				if (_items == null)
					_items = _table.ToList();
				if (_itemsSet == null)
					_itemsSet = new HashSet<TRow>(_items);
				return _itemsSet;
			}
		}
		private SortHandler Sorter { get; set; }


		/// <summary>
		///     Sorts the collection by a given property. The sort algorithm checks the properties which are accessed by the <paramref name="by" /> method and
		///     listens to the associated property changed event on the rows. If <paramref name="by" /> equals null the sort engine stops to sort.
		/// </summary>
		/// <param name="by">The method to select a property on the row</param>
		/// <param name="autosort">if activated the collection takes care of resorting the list if needed.</param>
		public void Sort(Expression<Func<TRow, object>> by, bool autosort = true)
		{
			if (by == null)
			{
				Sorter = null;
				return;
			}
			if (autosort)
			{
				Sorter = new SortHandler(by, () => _items.Sort(new AnonComparer<TRow, object>(by.Compile())), SortRequested);
				Sorter.Schedule();
			}
			else if (_items != null)
				_items.Sort(new AnonComparer<TRow, object>(by.Compile()));
		}

		/// <summary>
		///     Sorts the collection by a given property descending. The sort algorithm checks the properties which are accessed by the <paramref name="by" />
		///     method and listens to the associated property changed event on the rows. If <paramref name="by" /> equals null the sort engine stops to sort.
		/// </summary>
		/// <param name="by">The method to select a property on the row</param>
		/// <param name="autosort">if activated the collection takes care of resorting the list if needed.</param>
		public void SortDes(Expression<Func<TRow, object>> by, bool autosort = true)
		{
			if (by == null)
			{
				Sorter = null;
				return;
			}
			if (autosort)
			{
				Sorter = new SortHandler(by, () => _items = _items.OrderByDescending(by.Compile()).ToList(), SortRequested);
				Sorter.Schedule();
			}
			else if (_items != null)
				_items = _items.OrderByDescending(by.Compile()).ToList();
		}

		internal void APropertyChanged(TRow item, string property)
		{
			if (Sorter == null)
				return;
			if (!Sorter.DependingProperties.Contains(property))
				return;

			Sorter.Schedule();
		}

		internal void Add(TRow row)
		{
			if (_items == null)
				_items = _table.ToList();
			if (ItemsSet.Contains(row))
				return;

			ItemsSet.Add(row);
            _items.Add(row);

			if (Sorter != null)
				Sorter.Schedule();
			else
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, row));

		}

		internal void Remove(TRow row)
		{
			if (_items == null)
				return;

			var indexOf = _items.IndexOf(row);
			if (indexOf == -1)
				return;

			_items.Remove(row);
			ItemsSet.Remove(row);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, row, indexOf));
		}

		private void SortRequested()
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			CollectionChanged?.Invoke(this, e);
			OnPropertyChanged("Item[]");
			OnPropertyChanged("Count");
		}



		private class SortHandler
		{
			public SortHandler(Expression<Func<TRow, object>> sortSelector, Action action, Action onSortRequested)
			{
				DependingProperties = new HashSet<string>(sortSelector.GetReferencedProperties().Select(x => x.Name));
				Action = action;
				OnSortRequested = onSortRequested;
			}

			public bool IsPending { get; private set; }
			public HashSet<string> DependingProperties { get; }
			public Action Action { get; }
			public Action OnSortRequested { get; }


			public void Execute()
			{
				IsPending = false;
				Action();
			}

			public void Schedule()
			{
				IsPending = true;
				OnSortRequested();
			}
		}
	}
}