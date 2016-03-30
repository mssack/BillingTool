// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;





namespace CsWpfBase.Ev.Objects
{
	/// <summary>
	///     A register is used whenever a List is needed but the list functions should be hidden from outer side. Thread
	///     comprehensive <see cref="INotifyCollectionChanged" /> mechanism, to ensure that updates gets correctly dispatched
	///     to Ui if needed.
	///     <para>
	///         CAVE: When using this list with Ui don't use it in combination with <see cref="CollectionView" />
	///         this could result in deadlocks or unpredictable behaviors. Instead use <see cref="ObservableCollection{T}" />
	///         and dispatch all changes by yourself.
	///     </para>
	/// </summary>
	[DebuggerStepThrough]
	[Serializable]
	public class BaseRegister<T> : Base, INotifyCollectionChanged, IEnumerable<T>
	{
		#region InternalListHandling
		private List<T> _internalList;


		private List<T> InternalList
		{
			get { return _internalList ?? (_internalList = new List<T>()); }
		}

		private void InternalReplace(List<T> items)
		{
			_internalList = items;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void InternalAdd(T item)
		{
			InternalList.Add(item);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, InternalList.Count));
		}
		private void InternalInsert(int index, T item)
		{
			InternalList.Insert(index, item);

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
		}
		private void InternalSet(int index, T item)
		{
			var old = InternalList[index];
			InternalList[index] = item;

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, old, index));
		}

		private void InternalRemove(int index)
		{
			var removedItem = InternalList[index];
			InternalList.RemoveAt(index);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItem, index));
		}
		private void InternalRemove(IEnumerable<T> items)
		{
			foreach (var index in items.Select(item => InternalList.IndexOf(item)))
			{
				InternalRemove(index);
			}
		}
		private void InternalClear()
		{
			if (InternalList.Count == 0)
				return;

			InternalList.Clear();

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
		private void InternalMove(int oldIndex, int newIndex)
		{
			var item = InternalList[oldIndex];
			InternalList.RemoveAt(oldIndex);
			InternalList.Insert(newIndex, item);

			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex));
		}

		private void InternalSort(Func<T, object> by)
		{
			var orderBy = InternalList.OrderBy(by).ToList();
			//TODO faster sort method should be implemented, multiple enumerations

			for (int newIndex = 0; newIndex < orderBy.Count; newIndex++)
			{
				var toOrderItem = orderBy[newIndex];
				var oldIndex = InternalList.IndexOf(toOrderItem);
				if (oldIndex != newIndex)
				{
					InternalMove(oldIndex, newIndex);
				}
			}
		}
		/// <summary>
		///     Checks whether the register is currently firing CollectionChanged event and is modified while invoking. The
		///     List allows changes only on the last invocation in the CollectionChanged event. This method should be called before
		///     any changes are applied to the source list. This prevents restricted modifications which could lead to
		///     unpredictable behaviors.
		/// </summary>
		private void CheckReentrancy()
		{
			if (_isInCollectionChangeMode)
				throw new AccessViolationException("While the CollectionChanged event is invoked no changes to the collection are allowed while the target invocation is not the last item in the invocation list.");
		}
		/// <summary>Sends a property changed event for the count property</summary>
		private void OnCountChanged()
		{
			OnPropertyChanged("Count");
		}
		/// <summary>Sends a property changed event for the current List indexer [].</summary>
		private void OnIndexerChanged()
		{
			OnPropertyChanged(Binding.IndexerName);
		}
		#endregion


		#region CollectionChangedHandling
		/// <summary>Defines the invocation list for the <see cref="CollectionChanged" /> event.</summary>
		[field: NonSerialized] protected NotifyCollectionChangedEventHandler CollectionChangedInvocationList;
		/// <summary>This bool is used to prevent reentrant while firing the <see cref="CollectionChanged" /> event.</summary>
		[field: NonSerialized] private bool _isInCollectionChangeMode;
		/// <summary>
		///     Invokes automatically on UI.
		///     <para>
		///         CAVE: When the event is fired there could already been a lot of other changes to the list. This unpredictable
		///         behavior comes from the async dispatch mechanism. This event shouldn't be used by anyone, using this event in
		///         combination with enumeration of the list could result in unpredictable behavior.
		///     </para>
		///     <para>There is no problem with this event when used in web context.</para>
		/// </summary>
		public virtual event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add { CollectionChangedInvocationList = Delegate.Combine(CollectionChangedInvocationList, value) as NotifyCollectionChangedEventHandler; }
			remove { CollectionChangedInvocationList = Delegate.Remove(CollectionChangedInvocationList, value) as NotifyCollectionChangedEventHandler; }
		}
		/// <summary>
		///     Automatically dispatches all changes to the ui dispatcher if one is available (only available in WPF
		///     applications). By calling <see cref="InvokeCollectionChangedDelegates" /> it also avoids changes while
		///     <see cref="CollectionChanged" /> is firing.
		/// </summary>
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			var handler = CollectionChangedInvocationList;
			if (handler == null)
				return;

			if (Application.Current != null && Application.Current.Dispatcher != null)
			{
				if (Application.Current.Dispatcher != Dispatcher.CurrentDispatcher)
				{
					Application.Current.Dispatcher.BeginInvoke(new Action(() => InvokeCollectionChangedDelegates(handler.GetInvocationList(), args)), DispatcherPriority.DataBind);
					return;
				}
			}
			InvokeCollectionChangedDelegates(handler.GetInvocationList(), args);
		}
		/// <summary>
		///     Invokes the invocation list (<see cref="CollectionChangedInvocationList" />) and prevents changes while firing.
		///     <para>
		///         Include 'DEBUGLOCKS' flag as build attribute to see whenever this method is called-> look forward to NCCEA in
		///         Debug window.
		///     </para>
		/// </summary>
		protected virtual void InvokeCollectionChangedDelegates(Delegate[] dels, NotifyCollectionChangedEventArgs args)
		{
			_isInCollectionChangeMode = true;
#if (DEBUGLOCKS)

			var thread = "[" + String.Format("{0,5}", Thread.CurrentThread.ManagedThreadId) + "]";
			Debug.WriteLine(thread + " " + GetHashCode() + " NCCEA --> " + args.Action + (args.NewItems == null ? "" : "New(" + args.NewItems.Count + ") ") + (args.OldItems == null ? "" : "Old(" + args.OldItems.Count + ") "));
#endif
			for (int i = 0; i < dels.Length; i++)
			{
				if (i == dels.Length - 1)
					_isInCollectionChangeMode = false;


				((NotifyCollectionChangedEventHandler) dels[i])(this, args);
			}
		}
		#endregion


		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		#region Virtuals
		/// <summary>Use this method to override all enumerators accessed through this class.</summary>
		protected virtual IEnumerator<T> GetEnumerator()
		{
			return InternalList.GetEnumerator();
		}
		#endregion


		#region Publics
		/// <summary>Gets or sets an item in the list.</summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		public virtual T this[int index]
		{
			get
			{
				var item = InternalList[index];
				return item;
			}
			set
			{
				CheckReentrancy();

				InternalSet(index, value);
				OnIndexerChanged();
			}
		}
		/// <summary>Gets the amount of items in the register. Internal calls <see cref="List{T}.Count" />.</summary>
		public virtual int Count
		{
			get { return InternalList.Count; }
		}
		/// <summary>Determines whether an element is in the Register. Internal calls <see cref="List{T}.Contains" />.</summary>
		public virtual bool Contains(T item)
		{
			return InternalList.Contains(item);
		}
		/// <summary>Determines the index of a specific item in the IList. Internal calls <see cref="List{T}.IndexOf(T)" />.</summary>
		public virtual int IndexOf(T item)
		{
			return InternalList.IndexOf(item);
		}
		/// <summary>Iterates through register.</summary>
		public virtual void ForEach(Action<T> action)
		{
			foreach (var item in this.AsEnumerable())
			{
				action(item);
			}
		}
		#endregion


		#region Protected's
		/// <summary>Replaces the internal List (<see cref="InternalList" />) with an <see cref="IEnumerable{T}" />.</summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Replace(IEnumerable<T> items)
		{
			CheckReentrancy();

			InternalReplace(items.ToList());

			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then adds an item to the collection. <see cref="List{T}.Add" /></summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Add(T item)
		{
			CheckReentrancy();

			InternalAdd(item);

			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then adds items to the collection.</summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Add(IEnumerable<T> items)
		{
			//TODO This method (Add list) is not checked at all
			CheckReentrancy();

			foreach (var item in items)
			{
				InternalAdd(item);
			}

			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then inserts an item to the collection. <see cref="List{T}.Insert" /></summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Insert(int index, T item)
		{
			CheckReentrancy();

			InternalInsert(index, item);

			OnCountChanged();
			OnIndexerChanged();
		}


		/// <summary>First re entrance is checked and then removes an item from the collection. <see cref="List{T}.Remove(T)" /></summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual bool Remove(T item)
		{
			CheckReentrancy();

			int index = InternalList.IndexOf(item);
			if (index == -1)
				return false;


			InternalRemove(index);
			OnCountChanged();
			OnIndexerChanged();


			return true;
		}
		/// <summary>First re entrance is checked and then removes items from the collection.</summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Remove(IEnumerable<T> items)
		{
			CheckReentrancy();

			InternalRemove(items);

			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then removes an item from the collection. <see cref="List{T}.RemoveAt" /></summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void RemoveAt(int index)
		{
			CheckReentrancy();

			InternalRemove(index);

			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then clears the collection. <see cref="List{T}.Clear" /></summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Clear()
		{
			CheckReentrancy();

			InternalClear();

			OnCountChanged();
			OnIndexerChanged();
		}


		/// <summary>First re entrance is checked and then moves an item in the collection. </summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Move(int oldindex, int newindex)
		{
			CheckReentrancy();

			InternalMove(oldindex, newindex);

			OnIndexerChanged();
		}
		/// <summary>First re entrance is checked and then copy's the collection to an array.
		///     <see cref="List{T}.CopyTo(T[], int)" />
		/// </summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void CopyTo(T[] array, int arrayIndex)
		{
			InternalList.CopyTo(array, arrayIndex);
		}
		/// <summary>First re entrance is checked and then collection is sorted.</summary>
		/// <exception cref="AccessViolationException">Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.</exception>
		protected virtual void Sort(Func<T, object> by)
		{
			CheckReentrancy();

			InternalSort(by);

			OnIndexerChanged();
		}
		#endregion


		#region Assimilation
		/// <summary>
		///     Change this collection to look like a model. Uses an <see cref="Assimilator{TN}" /> to compare and convert
		///     items.
		/// </summary>
		/// <typeparam name="TN"></typeparam>
		/// <param name="model">The model which is used to modify this collection</param>
		/// <param name="assimilator">
		///     An specific <see cref="Assimilator{TN}" /> which is capable to convert and compare items from
		///     model to this collection.
		/// </param>
		/// <exception cref="InvalidDataException">Throws when the list consists of value types. Value types are not allowed</exception>
		/// <exception cref="InvalidOperationException">
		///     Throws when no convert mechanism is defined in the
		///     <paramref name="assimilator" />.
		/// </exception>
		/// <exception cref="AccessViolationException">
		///     Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.
		/// </exception>
		protected virtual void Assimilate<TN>(IEnumerable<TN> model, Assimilator<TN> assimilator)
		{
			if (typeof (T) == typeof (ValueType))
				throw new InvalidDataException("This method works only with reference types. Value types are not allowed");
			if (assimilator.ConvertFunc == null)
				throw new InvalidOperationException("This method needs an convert mechanism!");
			CheckReentrancy();


			var startinglist = InternalList.ToList();
			if (model == null)
			{
				if (startinglist.Count == 0)
					return;
				InternalClear();
				OnCountChanged();
				OnIndexerChanged();
				if (assimilator.OnRemoved != null)
					startinglist.ForEach(assimilator.OnRemoved);
				return;
			}

			var externalList = model as TN[] ?? model.ToArray();


			if (!externalList.Any()) //No Items in List Clear Collection
			{
				if (startinglist.Count == 0)
					return;
				InternalClear();
				OnCountChanged();
				OnIndexerChanged();
				if (assimilator.OnRemoved != null)
					startinglist.ForEach(assimilator.OnRemoved);
				return;
			}

			for (int i = 0; i < externalList.Length; i++)
			{
				var externalItem = externalList[i];
				T internalItem = default(T);
				int currentIndex;

				for (currentIndex = 0; currentIndex < InternalList.Count; currentIndex++)
				{
					var ci = InternalList[currentIndex];
					if (assimilator.EqualFunc == null ? ci.Equals(externalItem) : assimilator.EqualFunc(ci, externalItem))
					{
						internalItem = ci;
						break;
					}
				}

				// ReSharper disable once CompareNonConstrainedGenericWithNull
				if (internalItem != null)
				{
					startinglist.Remove(internalItem);
					if (currentIndex != i)
						InternalMove(currentIndex, i);

					if (assimilator.OnPairFound != null)
						assimilator.OnPairFound(internalItem, externalItem);
				}
				else
				{
					InternalInsert(i, assimilator.ConvertFunc(externalItem));
				}
			}
			foreach (var removeItem in startinglist)
			{
				InternalRemove(InternalList.IndexOf(removeItem));
			}
			OnCountChanged();
			OnIndexerChanged();
		}
		/// <summary>Change this collection to look like a model. Uses an <see cref="Assimilator" /> to compare and convert items.</summary>
		/// <param name="model">The model which is used to modify this collection</param>
		/// <param name="assimilator">
		///     An specific <see cref="Assimilator" /> which is capable to convert and compare items from
		///     model to this collection.
		/// </param>
		/// <exception cref="InvalidDataException">Throws when the list consists of value types. Value types are not allowed</exception>
		/// <exception cref="AccessViolationException">
		///     Throws when change is made while firing <see cref="CollectionChanged" />
		///     event.
		/// </exception>
		protected virtual void Assimilate(IEnumerable<T> model, Assimilator assimilator)
		{
			if (typeof (T) == typeof (ValueType))
				throw new InvalidDataException("This method works only with reference types. Value types are not allowed");
			CheckReentrancy();


			var startinglist = InternalList.ToList();
			if (model == null)
			{
				if (startinglist.Count == 0)
					return;
				InternalClear();
				OnCountChanged();
				OnIndexerChanged();
				if (assimilator.OnRemoved != null)
					startinglist.ForEach(assimilator.OnRemoved);
				return;
			}

			var externalList = model as T[] ?? model.ToArray();


			if (!externalList.Any()) //No Items in List Clear Collection
			{
				if (startinglist.Count == 0)
					return;
				InternalClear();
				OnCountChanged();
				OnIndexerChanged();
				if (assimilator.OnRemoved != null)
					startinglist.ForEach(assimilator.OnRemoved);
				return;
			}

			for (int i = 0; i < externalList.Length; i++)
			{
				var externalItem = externalList[i];
				T internalItem = default(T);
				int currentIndex;

				for (currentIndex = 0; currentIndex < InternalList.Count; currentIndex++)
				{
					var ci = InternalList[currentIndex];
					if (assimilator.EqualFunc == null ? ci.Equals(externalItem) : assimilator.EqualFunc(ci, externalItem))
					{
						internalItem = ci;
						break;
					}
				}

				// ReSharper disable once CompareNonConstrainedGenericWithNull
				if (internalItem != null)
				{
					startinglist.Remove(internalItem);
					if (currentIndex != i)
						InternalMove(currentIndex, i);

					if (assimilator.OnPairFound != null)
						assimilator.OnPairFound(internalItem, externalItem);
				}
				else
				{
					InternalInsert(i, externalItem);
					if (assimilator.OnAdded != null)
						assimilator.OnAdded(externalItem);
				}
			}
			foreach (var removeItem in startinglist)
			{
				InternalRemove(InternalList.IndexOf(removeItem));
			}
			OnCountChanged();
			OnIndexerChanged();
		}





		/// <summary>
		///     Used to assimilate an existing list to mirror an model. Extend and withdraws items and after order it like the
		///     model.
		/// </summary>
		public class Assimilator
		{
			/// <summary>This function is used to compare two instances instead of using the Equality comparer.</summary>
			public Func<T, T, bool> EqualFunc { get; set; }
			/// <summary>Invokes when an item was added to the collection. As a example: Use this method to invoke an OnAdded event</summary>
			public Action<T> OnAdded { get; set; }
			/// <summary>Invokes when a pair is found by the EqualFunc. As a example: Use this method to update an existing item.</summary>
			public Action<T, T> OnPairFound { get; set; }
			/// <summary>Invokes when an existing item was removed. As a example: Use this method to invoke an OnRemoved event.</summary>
			public Action<T> OnRemoved { get; set; }
		}





		/// <summary>
		///     Used to assimilate an existing list to mirror an model. Extend and withdraws items and order it like the
		///     model.
		/// </summary>
		public class Assimilator<TN>
		{
			/// <summary>This function is used to compare two objects.</summary>
			public Func<T, TN, bool> EqualFunc { get; set; }
			/// <summary>
			///     Invokes when an item need to be added to the collection. Convert the item an as a example: Use this method to
			///     invoke an OnAdded event
			/// </summary>
			public Func<TN, T> ConvertFunc { get; set; }
			/// <summary>Invokes when a pair is found by the EqualFunc. As a example: Use this method to update an existing item.</summary>
			public Action<T, TN> OnPairFound { get; set; }
			/// <summary>Invokes when an existing item was removed. As a example: Use this method to invoke an OnRemoved event.</summary>
			public Action<T> OnRemoved { get; set; }
		}
		#endregion
	}
}