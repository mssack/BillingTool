// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;





namespace CsWpfBase.Ev.Objects
{
#pragma warning disable 1591
	/// <summary>
	///     A <code>RegisterTs</code> a so called thread safe register is used whenever a register is needed which
	///     automatically provides thread safety.
	///     <para />
	///     Define DEBUGLOCKS to see whenever a lock is applied.
	/// </summary>
	[Serializable]
	[DebuggerStepThrough]
	public class BaseRegisterTs<T> : BaseRegister<T>
	{
		#region MultiThreading
		[field: NonSerialized] private ReaderWriterLockSlim _lock;

		private ReaderWriterLockSlim Lock
		{
			get { return _lock ?? (_lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)); }
		}





		private class UseAbleLocker : IDisposable
		{
			private readonly Action _disposeAction;
			private readonly Action _enterAction;


			public UseAbleLocker(Action enterAction, Action disposeAction)
			{
				_disposeAction = disposeAction;
				_enterAction = enterAction;
			}
			public void Dispose()
			{
				_disposeAction();
			}
			public void Enter()
			{
				_enterAction();
			}
		}





#if(DEBUGLOCKS)
		
		public IDisposable AquireWriteLock([CallerMemberName]string method = null)
		{
			var locker = new UseAbleLocker(() =>
			{
				DebugLock(Type.Write, State.TryEnter, method);
				Lock.EnterWriteLock();
				DebugLock(Type.Write, State.Entered, method);
			}, () =>
			{
				Lock.ExitWriteLock();
				DebugLock(Type.Write, State.Exit, method);
			});
			locker.Enter();
			return locker;
		}
		public IDisposable AquireReadLock([CallerMemberName]string method = null)
		{
			var locker = new UseAbleLocker(() =>
			{
				DebugLock(Type.Read, State.TryEnter, method);
				Lock.EnterReadLock();
				DebugLock(Type.Read, State.Entered, method);
			}, () =>
			{
				Lock.ExitReadLock();
				DebugLock(Type.Read, State.Exit, method);
			});
			locker.Enter();
			return locker;
		}
		public IDisposable AquireUpgradeableReadLock([CallerMemberName]string method = null)
		{
			var locker = new UseAbleLocker(() =>
			{
				DebugLock(Type.UpgradeAbleRead, State.TryEnter, method);
				Lock.EnterUpgradeableReadLock();
				DebugLock(Type.UpgradeAbleRead, State.Entered, method);
			}, () =>
			{
				Lock.ExitUpgradeableReadLock();
				DebugLock(Type.UpgradeAbleRead, State.Exit, method);
			});
			locker.Enter();
			return locker;
		}


		private void EnterWriteLock([CallerMemberName] string method = null)
		{
			DebugLock(Type.Write, State.TryEnter, method);
			Lock.EnterWriteLock();
			DebugLock(Type.Write, State.Entered, method);
		}
		private void ExitWriteLock([CallerMemberName] string method = null)
		{
			Lock.ExitWriteLock();
			DebugLock(Type.Write, State.Exit, method);
		}
		private void EnterReadLock([CallerMemberName] string method = null)
		{
			DebugLock(Type.Read, State.TryEnter, method);
			Lock.EnterReadLock();
			DebugLock(Type.Read, State.Entered, method);
		}
		private void ExitReadLock([CallerMemberName] string method = null)
		{
			Lock.ExitReadLock();
			DebugLock(Type.Read, State.Exit, method);
		}
		private enum Type
		{
			Read, Write, UpgradeAbleRead
		}
		private enum State
		{
			TryEnter, Entered, Exit
		}
		private void DebugLock(Type typ, State state, string name)
		{
			var thread = "[" + String.Format("{0,5}", Thread.CurrentThread.ManagedThreadId) + "]";
			var lockType = "<" + String.Format("{0,5}",typ) + ">";
			var lockState = "(" + String.Format("{0,8}", state) + ")";
			Debug.WriteLine(thread + " " + this.GetHashCode() + " -->LOCK" + lockType + lockState + " " + name);
		}
#else
		[field: NonSerialized] private UseAbleLocker _upgradeableReadLock;
		[field: NonSerialized] private UseAbleLocker _writeLock;
		[field: NonSerialized] private UseAbleLocker _readLock;

		/// <summary>
		///     Use the returned object to ensure thread safety over code block.
		///     <code>
		///  using(<see cref="BaseRegisterTs{T}.AquireWriteLock" />)
		///  {
		/// 	thread safe operations;
		///  }</code>
		/// </summary>
		public IDisposable AquireWriteLock()
		{
			if (_writeLock == null)
				_writeLock = new UseAbleLocker(() => Lock.EnterWriteLock(), () => Lock.ExitWriteLock());
			_writeLock.Enter();
			return _writeLock;
		}
		/// <summary>
		///     Use the returned object to ensure thread safety over code block.
		///     <code>
		///  using(<see cref="BaseRegisterTs{T}.AquireReadLock" />)
		///  {
		/// 	thread safe operations;
		///  }</code>
		/// </summary>
		public IDisposable AquireReadLock()
		{
			if (_readLock == null)
				_readLock = new UseAbleLocker(() => Lock.EnterReadLock(), () => Lock.ExitReadLock());
			_readLock.Enter();
			return _readLock;
		}
		/// <summary>
		///     Use the returned object to ensure thread safety over code block.
		///     <code>
		///  using(<see cref="BaseRegisterTs{T}.AquireUpgradeableReadLock" />)
		///  {
		/// 	thread safe operations;
		///  }</code>
		/// </summary>
		public IDisposable AquireUpgradeableReadLock()
		{
			if (_upgradeableReadLock == null)
				_upgradeableReadLock = new UseAbleLocker(() => Lock.EnterUpgradeableReadLock(), () => Lock.ExitUpgradeableReadLock());
			_upgradeableReadLock.Enter();
			return _upgradeableReadLock;
		}


		private void EnterWriteLock()
		{
			Lock.EnterWriteLock();
		}
		private void ExitWriteLock()
		{
			Lock.ExitWriteLock();
		}
		private void EnterReadLock()
		{
			Lock.EnterReadLock();
		}
		private void ExitReadLock()
		{
			Lock.ExitReadLock();
		}
#endif
		#endregion


		#region Enumeration
		protected override IEnumerator<T> GetEnumerator()
		{
			EnterReadLock();
			return new MuliThreadingEnumerator(base.GetEnumerator(), () => ExitReadLock());
		}





		private class MuliThreadingEnumerator : IEnumerator<T>
		{
			private readonly IEnumerator<T> _mInner;
			private readonly Action _ondisposedAction;

			public MuliThreadingEnumerator(IEnumerator<T> inner, Action ondisposedAction)
			{
				_mInner = inner;
				_ondisposedAction = ondisposedAction;
			}

			void IDisposable.Dispose()
			{
				Dispose();
			}
			public bool MoveNext()
			{
				return _mInner.MoveNext();
			}
			public void Reset()
			{
				_mInner.Reset();
			}
			public T Current
			{
				get { return _mInner.Current; }
			}
			object IEnumerator.Current
			{
				get { return Current; }
			}
			public void Dispose()
			{
				_ondisposedAction();
			}
		}
		#endregion


		#region ChangedEvents
		protected override void InvokeCollectionChangedDelegates(Delegate[] dels, NotifyCollectionChangedEventArgs args)
		{
			Lock.EnterReadLock();
			base.InvokeCollectionChangedDelegates(dels, args);
			Lock.ExitReadLock();
		}
		#endregion


		/// <summary>
		///     Encapsulates <see cref="BaseRegister{T}.Count" />. CAVE: When using, think of thread safety and use locks to
		///     prevent race conditions!!!
		/// </summary>
		public override int Count
		{
			get { return base.Count; }
		}
		/// <summary>
		///     Encapsulates <see cref="BaseRegister{T}.this" />. CAVE: When using, think of thread safety and use locks to
		///     prevent race conditions!!!
		/// </summary>
		public override T this[int index]
		{
			get { return base[index]; }
			set { base[index] = value; }
		}
		/// <summary>
		///     Encapsulates <see cref="BaseRegister{T}.Contains" />. CAVE: When using, think of thread safety and use locks
		///     to prevent race conditions!!!
		/// </summary>
		public override bool Contains(T item)
		{
			return base.Contains(item);
		}
		/// <summary>
		///     Encapsulates <see cref="BaseRegister{T}.IndexOf" />. CAVE: When using, think of thread safety and use locks to
		///     prevent race conditions!!!
		/// </summary>
		public override int IndexOf(T item)
		{
			return base.IndexOf(item);
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Replace" /> with an write lock.</summary>
		protected override void Replace(IEnumerable<T> items)
		{
			EnterWriteLock();
			try
			{
				base.Replace(items);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Add(T)" /> with an write lock.</summary>
		protected override void Add(T item)
		{
			EnterWriteLock();
			try
			{
				base.Add(item);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Add(IEnumerable{T})" /> with an write lock.</summary>
		protected override void Add(IEnumerable<T> items)
		{
			EnterWriteLock();
			try
			{
				base.Add(items);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Insert" /> with an write lock.</summary>
		protected override void Insert(int index, T item)
		{
			EnterWriteLock();
			try
			{
				base.Insert(index, item);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Remove(T)" /> with an write lock.</summary>
		protected override bool Remove(T item)
		{
			EnterWriteLock();
			try
			{
				return base.Remove(item);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Remove(IEnumerable{T})" /> with an write lock.</summary>
		protected override void Remove(IEnumerable<T> items)
		{
			EnterWriteLock();
			try
			{
				base.Remove(items);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.RemoveAt" /> with an write lock.</summary>
		protected override void RemoveAt(int index)
		{
			EnterWriteLock();
			try
			{
				base.RemoveAt(index);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Clear" /> with an write lock.</summary>
		protected override void Clear()
		{
			EnterWriteLock();
			try
			{
				base.Clear();
			}
			finally
			{
				ExitWriteLock();
			}
		}


		/// <summary>Encapsulate <see cref="BaseRegister{T}.Move" /> with an write lock.</summary>
		protected override void Move(int oldindex, int newindex)
		{
			EnterWriteLock();
			try
			{
				base.Move(oldindex, newindex);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.CopyTo" /> with an write lock.</summary>
		protected override void CopyTo(T[] array, int arrayIndex)
		{
			EnterWriteLock();
			try
			{
				base.CopyTo(array, arrayIndex);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Sort" /> with an write lock.</summary>
		protected override void Sort(Func<T, object> by)
		{
			EnterWriteLock();
			try
			{
				base.Sort(by);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Assimilate" /> with an write lock.</summary>
		protected override void Assimilate(IEnumerable<T> model, Assimilator assimilator)
		{
			EnterWriteLock();
			try
			{
				base.Assimilate(model, assimilator);
			}
			finally
			{
				ExitWriteLock();
			}
		}
		/// <summary>Encapsulate <see cref="BaseRegister{T}.Assimilate{TN}" /> with an write lock.</summary>
		protected override void Assimilate<TN>(IEnumerable<TN> model, Assimilator<TN> assimilator)
		{
			EnterWriteLock();
			try
			{
				base.Assimilate(model, assimilator);
			}
			finally
			{
				ExitWriteLock();
			}
		}
	}
}