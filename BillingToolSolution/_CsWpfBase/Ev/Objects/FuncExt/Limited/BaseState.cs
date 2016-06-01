// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Runtime.Serialization;





namespace CsWpfBase.Ev.Objects.FuncExt.Limited
{
#pragma warning disable 1591
	/// <summary>
	///     Provides a default state class.
	///     <para>
	///         ---->Override <see cref="ReleaseAllSubscriptions" /> to ensure that events in the base class are correctly
	///         released.
	///     </para>
	///     <para>---->Override methods beginning with Pre* to extend the state setters.</para>
	///     <para>---->Include descriptions for the properties <see cref="IsSucceeded" /> and <see cref="IsFaulted" />.</para>
	///     <para>
	///         ---->Don't forget to set owner after deserialization, which is not necessarily needed cause after
	///         deserialization no state changes or subscription are allowed, but maybe base class needs the owner. To avoid
	///         unpredictable behavior, implement it anyway.
	///     </para>
	///     <para>All new function or events should take car of <see cref="Lock" /> to ensure thread safety</para>
	/// </summary>
	/// <example>
	///     Use this as en example implementation for a new state. Don't forget to implement needed property's in result class
	///     <code>
	///  [field:NonSerialized] private EvDel _eventName;
	///  public event EvDel Online
	///  {
	///  	add { AutoRejectEvent_Add(ref _eventName, ref value, () => Condition); }
	///  	remove { AutoRejectEvent_Remove(ref _eventName, ref value); }
	///  }
	///  internal void SetOnline(BaseResult result)
	///  {
	///  	result.SetResultProp();
	///  	OnEventName();
	///  }
	///  private void OnEventName()
	///  {
	///  	AutoRejectEvent_Invoke(ref _eventName);
	///  }
	///  protected override void ReleaseAllSubscriptions()
	///  {
	///  	base.ReleaseAllSubscriptions();
	///  	_eventName = null;
	///  }
	///   </code>
	/// </example>
	/// <typeparam name="TOwner">Defines the owner object. The object is used by the events to give a reference to the sender.</typeparam>
	[Serializable]
	[DebuggerStepThrough]
	public abstract class BaseState<TOwner> : Base
	{
		[field: NonSerialized] private readonly object _lock = new object();
		private Exception _exception;
		private bool _isCanceled;
		private bool _isCanceling;
		private bool _isCompleted;
		private bool _isFaulted;
		private bool _isRunning;
		private bool _isSucceeded;
		private bool _isUnaltered = true;
		[field: NonSerialized] private TOwner _owner;

		/// <summary>Initializes a new <see cref="BaseState{TOwner}" /> and automatically sets its owner.</summary>
		protected BaseState(TOwner owner)
		{
			_owner = owner;
		}


		#region Overrides
		/// <summary>releases all subscriptions from all events.</summary>
		public override void Dispose()
		{
			base.Dispose();
			ReleaseAllSubscriptions();
		}
		public override string ToString()
		{
			if (IsUnaltered)
				return "Unaltered";
			if (IsRunning)
				return "Running";
			if (IsCanceling)
				return "Canceling";
			if (IsSucceeded)
				return "Succeeded";
			if (IsCanceled)
				return "Canceled";
			if (IsFaulted)
				return "Faulted";
			return "";
		}
		#endregion


		#region InternalSetters
		/// <summary>
		///     Sets the state to running state. If state is in running state everything must be initialized to ensure working
		///     cancel method. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is already started or completed.</exception>
		internal void SetRunning(BaseResult result, DateTime? startTime = null)
		{
			if (IsCompleted || IsRunning)
				throw new InvalidOperationException("The state is in the wrong mode to perform SetRunning.");

			IsUnaltered = false;
			if (result != null)
				result.SetStartTime(startTime);

			IsRunning = true;

			SendRunningEvents();
		}
		/// <summary>Sets the state to canceling state. Call <code>SetCanceled</code> to define cancellation completion. CAVE: Use
		///     <see cref="Lock" /> around setting the state to ensure thread safety!!</summary>
		/// <exception cref="InvalidOperationException">When the state is other then running.</exception>
		internal void SetCanceling()
		{
			if (!IsRunning)
				throw new InvalidOperationException("The state is in the wrong mode to perform SetCanceling.");

			IsCanceling = true;

			SendCancelingEvents();
		}
		/// <summary>
		///     Sets the state to canceled state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread
		///     safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then canceling.</exception>
		internal void SetCanceled(BaseResult result, TimeSpan? duration = null)
		{
			if (!IsCanceling)
				throw new InvalidOperationException("The state is in the wrong mode to perform SetCanceled.");

			if (result != null)
				result.SetCanceled(duration);

			IsCanceling = false;
			IsRunning = false;
			IsCanceled = true;
			IsCompleted = true;

			SendCanceledEvents();
		}
		/// <summary>
		///     Sets the state to faulted state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread
		///     safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then running or already canceling.</exception>
		internal void SetFaulted(BaseResult result, Exception exception = null, TimeSpan? duration = null)
		{
			if (!IsRunning || IsCanceling)
				throw new InvalidOperationException("The state is in the wrong mode to perform SetFaulted.");

			if (result != null)
				result.SetFaulted(exception, duration);

			Exception = exception;

			IsRunning = false;
			IsFaulted = true;
			IsCompleted = true;

			SendFaultedEvents();
		}
		/// <summary>
		///     Sets the state to succeeded state. CAVE: Use <see cref="Lock" /> around setting the state to ensure thread
		///     safety!!
		///     <para>Pass result parameter to automatically set result accordingly. </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">When the state is other then running or already canceling.</exception>
		internal void SetSucceeded(BaseResult result, TimeSpan? duration = null)
		{
			if (!IsRunning || IsCanceling)
				throw new InvalidOperationException("The state is in the wrong mode to perform SetSucceeded.");

			if (result != null)
				result.SetSucceeded(duration);

			IsRunning = false;
			IsSucceeded = true;
			IsCompleted = true;

			SendSucceededEvents();
		}
		#endregion


		#region ProtectedInvokes
		/// <summary>Sends the associated events (<see cref="Started" />) by calling the so called 'On' method.</summary>
		protected virtual void SendRunningEvents()
		{
			OnStarted();
		}
		/// <summary>Sends the associated events (<see cref="Canceling" />) by calling the so called 'On' method.</summary>
		protected virtual void SendCancelingEvents()
		{
			OnCanceling();
		}
		/// <summary>
		///     Sends the associated events (<see cref="Canceled" />, <see cref="Completed" />) by calling the so called 'On'
		///     method.
		/// </summary>
		protected virtual void SendCanceledEvents()
		{
			OnCanceled();
			OnCompleted();
		}
		/// <summary>
		///     Sends the associated events (<see cref="Faulted" />, <see cref="Completed" />) by calling the so called 'On'
		///     method.
		/// </summary>
		protected virtual void SendFaultedEvents()
		{
			OnFaulted();
			OnCompleted();
		}
		/// <summary>
		///     Sends the associated events (<see cref="Succeeded" />, <see cref="Completed" />) by calling the so called 'On'
		///     method.
		/// </summary>
		protected virtual void SendSucceededEvents()
		{
			OnSucceeded();
			OnCompleted();
		}
		#endregion


		#region Events
		[field: NonSerialized] private EvDel _canceled;
		[field: NonSerialized] private EvDel _canceling;
		[field: NonSerialized] private EvDel _completed;
		[field: NonSerialized] private EvDel _faulted;
		[field: NonSerialized] private EvDel _started;
		[field: NonSerialized] private EvDel _succeeded;


		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> and <see cref="IsCanceled" /> is false.
		///     Otherwise Event subscription is discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Canceled
		{
			add { AutoRejectEvent_Add(ref _canceled, ref value, () => IsCanceled); }
			remove { AutoRejectEvent_Remove(ref _canceled, ref value); }
		}
		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> and <see cref="IsCanceling" /> is false.
		///     Otherwise Event subscription is discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Canceling
		{
			add { AutoRejectEvent_Add(ref _canceling, ref value, () => IsCanceling); }
			remove { AutoRejectEvent_Remove(ref _canceling, ref value); }
		}
		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> is false. Otherwise Event subscription is
		///     discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Completed
		{
			add { AutoRejectEvent_Add(ref _completed, ref value); }
			remove { AutoRejectEvent_Remove(ref _completed, ref value); }
		}
		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> and <see cref="IsFaulted" /> is false.
		///     Otherwise Event subscription is discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Faulted
		{
			add { AutoRejectEvent_Add(ref _faulted, ref value, () => IsFaulted); }
			remove { AutoRejectEvent_Remove(ref _faulted, ref value); }
		}
		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> and <see cref="IsRunning" /> is false.
		///     Otherwise Event subscription is discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Started
		{
			add { AutoRejectEvent_Add(ref _started, ref value, () => IsRunning); }
			remove { AutoRejectEvent_Remove(ref _started, ref value); }
		}
		/// <summary>
		///     CAVE: AutoRejectEvent only subscribe if <see cref="IsCompleted" /> and <see cref="IsSucceeded" /> is false.
		///     Otherwise Event subscription is discarded. After event is fired all subscriptions are removed.
		/// </summary>
		/// <exception cref="SerializationException">On deserialized origin, no subscriptions are allowed</exception>
		public event EvDel Succeeded
		{
			add { AutoRejectEvent_Add(ref _succeeded, ref value, () => IsSucceeded); }
			remove { AutoRejectEvent_Remove(ref _succeeded, ref value); }
		}


		/// <summary>
		///     CAVE: Event subscription only if <see cref="IsCompleted" /> is false and <paramref name="discard" /> is false.
		///     Adds an delegate(<paramref name="value" />) to an event(<paramref name="event" />).
		///     <para>Method is working thread safe. Subscriptions are only allowed while lock is free.</para>
		/// </summary>
		/// <param name="event">A reference to a delegate to which <paramref name="value" /> is added</param>
		/// <param name="value">A reference to a delegate which will be added to <paramref name="event" />.</param>
		/// <param name="discard">When true, event subscription is not performed.</param>
		/// <remarks>
		///     Param <paramref name="discard" /> can not be an boolean cause value should be determined in lock. And not
		///     before!! So do not remove the 'Func' implementation.
		/// </remarks>
		protected void AutoRejectEvent_Add(ref EvDel @event, ref EvDel value, Func<bool> discard = null)
		{
			lock (Lock)
			{
				if (IsCompleted || (discard != null && discard()))
					return;
				@event = Delegate.Combine(@event, value) as EvDel;
			}
		}
		/// <summary>Removes an AutoResetEvent delegate.</summary>
		protected void AutoRejectEvent_Remove(ref EvDel @event, ref EvDel value)
		{
			lock (Lock)
			{
				@event = Delegate.Remove(@event, value) as EvDel;
			}
		}
		/// <summary>Invokes an AutoResetEvent and removes all subscribers.</summary>
		protected void AutoRejectEvent_Invoke(ref EvDel @event)
		{
			if (@event == null)
				return;
			@event(Owner);
			@event = null;
		}
		/// <summary>Releases all subscribers to the events hold by this class.</summary>
		protected virtual void ReleaseAllSubscriptions()
		{
			_started = null;
			_canceling = null;
			_canceled = null;
			_faulted = null;
			_succeeded = null;
			_completed = null;
		}
		#endregion


		#region Invokers
		private void OnStarted()
		{
			AutoRejectEvent_Invoke(ref _started);
		}
		private void OnCanceling()
		{
			AutoRejectEvent_Invoke(ref _canceling);
		}
		private void OnCanceled()
		{
			AutoRejectEvent_Invoke(ref _canceled);
		}
		private void OnFaulted()
		{
			AutoRejectEvent_Invoke(ref _faulted);
		}
		private void OnSucceeded()
		{
			AutoRejectEvent_Invoke(ref _succeeded);
		}
		private void OnCompleted()
		{
			AutoRejectEvent_Invoke(ref _completed);
			ReleaseAllSubscriptions();
		}
		#endregion


		/// <summary>
		///     State lock. Locks concurrent threads from event subscription. CAVE: Don't use this lock if you are not the
		///     owner of that state. To prevent dead locks use this lock ONLY in logically owner!!!
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

		/// <summary>
		///     Determine if state was already set to running. Use this boolean to determine if already some state change
		///     happened.
		/// </summary>
		public bool IsUnaltered
		{
			get { return _isUnaltered; }
			private set { SetProperty(ref _isUnaltered, value); }
		}
		/// <summary>Gets whether the operation is currently running.</summary>
		public bool IsRunning
		{
			get { return _isRunning; }
			private set { SetProperty(ref _isRunning, value); }
		}
		/// <summary>Gets whether the operation is in cancellation state and waits for completion.</summary>
		public bool IsCanceling
		{
			get { return _isCanceling; }
			private set { SetProperty(ref _isCanceling, value); }
		}
		/// <summary>Gets whether the operation was completed by cancellation.</summary>
		public bool IsCanceled
		{
			get { return _isCanceled; }
			private set { SetProperty(ref _isCanceled, value); }
		}
		/// <summary>
		///     Gets whether the operation was completed with an exception. Obtain base class documentation for more precise
		///     description.
		/// </summary>
		public bool IsFaulted
		{
			get { return _isFaulted; }
			private set { SetProperty(ref _isFaulted, value); }
		}
		/// <summary>
		///     Gets whether the operation was completed successfully. Obtain base class documentation for more precise
		///     description.
		/// </summary>
		public bool IsSucceeded
		{
			get { return _isSucceeded; }
			private set { SetProperty(ref _isSucceeded, value); }
		}
		/// <summary>Gets whether the operation was completed.</summary>
		public bool IsCompleted
		{
			get { return _isCompleted; }
			private set { SetProperty(ref _isCompleted, value); }
		}
		/// <summary>Gets the assigned exception. Obtain <see cref="IsFaulted" /> to check whether operation was faulted.</summary>
		public Exception Exception
		{
			get { return _exception; }
			private set { SetProperty(ref _exception, value); }
		}
		internal TOwner Owner
		{
			get { return _owner; }
			set { SetProperty(ref _owner, value); }
		}

		/// <summary>Gets whether state is in a mode where cancellation is possible or reasonable.</summary>
		public bool IsCancleAble()
		{
			return IsRunning && IsCanceling == false;
		}





		/// <summary>Generic delegate is used by the standard auto reset events.</summary>
		public delegate void EvDel(TOwner sender);
	}
}