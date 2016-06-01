// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Ev.Objects.FuncExt.Endless
{
	/// <summary>Provides a State for an endless but pause able state.</summary>
	[Serializable]
	public class BaseEndlessState<TOwner> : Base
	{
		[field: NonSerialized] private readonly object _lock = new object();
		[field: NonSerialized] private EvDel _continued;
		private bool _isPaused;
		private bool _isPausing;
		private bool _isRunning;
		private bool _isUnaltered = true;
		[field: NonSerialized] private TOwner _owner;
		[field: NonSerialized] private EvDel _paused;
		[field: NonSerialized] private EvDel _pausing;
		[field: NonSerialized] private EvDel _started;

		/// <summary>Initializes a new <see cref="BaseEndlessState{TOwner}" /> and automatically sets its owner.</summary>
		protected BaseEndlessState(TOwner owner)
		{
			_owner = owner;
		}


		#region Abstract
		/// <summary>Invokes the corresponding Event.</summary>
		protected virtual void OnContinued()
		{
			Event_Invoke(ref _continued);
		}

		/// <summary>Invokes the corresponding Event.</summary>
		protected virtual void OnPausing()
		{
			Event_Invoke(ref _pausing);
		}

		/// <summary>Invokes the corresponding Event.</summary>
		protected virtual void OnPaused()
		{
			Event_Invoke(ref _paused);
		}

		/// <summary>Invokes the corresponding Event.</summary>
		protected virtual void OnStarted()
		{
			Event_Invoke(ref _started);
		}

		/// <summary>
		///     Sets the state to Started state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then Unaltered.</exception>
		internal virtual void SetStarted(BaseEndlessResult result)
		{
			if (!IsUnaltered)
				throw new InvalidOperationException("The state is in the wrong mode.");
			if (result != null)
				result.SetStarted();
			IsUnaltered = false;
			IsRunning = true;
			OnStarted();
		}

		/// <summary>
		///     Sets the state to pausing state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then running.</exception>
		internal virtual void SetPausing(BaseEndlessResult result)
		{
			if (!IsRunning && !IsUnaltered)
				throw new InvalidOperationException("The state is in the wrong mode.");
			if (result != null)
				result.SetPausing();
			IsPausing = true;
			OnPausing();
		}

		/// <summary>
		///     Sets the state to paused state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then pausing.</exception>
		internal virtual void SetPaused(BaseEndlessResult result)
		{
			if (!IsPausing)
				throw new InvalidOperationException("The state is in the wrong mode.");
			if (result != null)
				result.SetPaused();

			IsPausing = false;
			IsRunning = false;
			IsPaused = true;
			OnPaused();
		}

		/// <summary>
		///     Sets the state to continued state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then Paused.</exception>
		internal virtual void SetContinued(BaseEndlessResult result)
		{
			if (IsRunning)
				throw new InvalidOperationException("The state is in the wrong mode.");
			if (result != null)
				result.SetContinued();

			IsPaused = false;
			IsRunning = true;
			OnContinued();
		}

		/// <summary>Releases all subscribers to the events hold by this class.</summary>
		protected virtual void ReleaseAllSubscriptions()
		{
			_continued = null;
			_pausing = null;
			_paused = null;
			_started = null;
		}
		#endregion


		#region Overrides/Interfaces
		/// <summary>releases all subscriptions from all events.</summary>
		public override void Dispose()
		{
			base.Dispose();
			ReleaseAllSubscriptions();
		}

		/// <summary>Returns not the type but the current state of this class.</summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (IsUnaltered)
				return "Unaltered";
			if (IsRunning && IsPausing)
				return "Running but waiting to be paused.";
			if (IsRunning)
				return "Running";
			return "Paused";
		}
		#endregion


		/// <summary>
		///     State lock. Locks concurrent threads from event subscription. CAVE: Don't use this lock if you are not the owner of that state. To prevent dead
		///     locks use this lock ONLY in logically owner!!!
		/// </summary>
		public object Lock
		{
			get
			{
				if (_lock == null)
					throw new InvalidOperationException("After a state is deserialized no changes to the object are allowed.");
				return _lock;
			}
		}
		/// <summary>Determines if the operation is currently not started.</summary>
		public bool IsUnaltered
		{
			get { return _isUnaltered; }
			private set { SetProperty(ref _isUnaltered, value); }
		}
		/// <summary>Determines if the operation is currently entering the pause state.</summary>
		public bool IsPausing
		{
			get { return _isPausing; }
			private set { SetProperty(ref _isPausing, value); }
		}
		/// <summary>Determines if the operation is paused.</summary>
		public bool IsPaused
		{
			get { return _isPaused; }
			private set { SetProperty(ref _isPaused, value); }
		}
		/// <summary>Determines if the operation is currently running.</summary>
		public bool IsRunning
		{
			get { return _isRunning; }
			private set { SetProperty(ref _isRunning, value); }
		}
		internal TOwner Owner
		{
			get { return _owner; }
			set { SetProperty(ref _owner, value); }
		}

		/// <summary>Occurs when the operation is being continued from pause.</summary>
		public event EvDel Continued
		{
			add { Event_Add(ref _continued, value); }
			remove { Event_Remove(ref _continued, value); }
		}
		/// <summary>Occurs when the operation is paused.</summary>
		public event EvDel Paused
		{
			add { Event_Add(ref _paused, value); }
			remove { Event_Remove(ref _paused, value); }
		}
		/// <summary>Occurs when the operation is currently pausing.</summary>
		public event EvDel Pausing
		{
			add { Event_Add(ref _pausing, value); }
			remove { Event_Remove(ref _pausing, value); }
		}
		/// <summary>Occurs when the operation is started.</summary>
		public event EvDel Started
		{
			add { Event_Add(ref _started, value); }
			remove { Event_Remove(ref _started, value); }
		}


		/// <summary>Gets whether state is in a mode where cancellation is possible or reasonable.</summary>
		public bool IsPauseable()
		{
			return IsRunning && IsPausing == false;
		}


		/// <summary>Thread safe adds an delegate to an delegate field.</summary>
		protected void Event_Add(ref EvDel field, EvDel value, Func<bool> discard = null)
		{
			lock (Lock)
			{
				if (discard != null && discard())
					return;
				field = (EvDel) Delegate.Combine(field, value);
			}
		}

		/// <summary>Thread safe removes an delegate from an delegate field.</summary>
		protected void Event_Remove(ref EvDel field, EvDel value)
		{
			lock (Lock)
			{
				field = (EvDel) Delegate.Remove(field, value);
			}
		}

		/// <summary>Invokes an event.</summary>
		protected void Event_Invoke(ref EvDel field)
		{
			if (field != null)
				field(Owner);
		}



		/// <summary>Standard Delegate</summary>
		public delegate void EvDel(TOwner sender);
	}
}