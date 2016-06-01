// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Ev.Objects.FuncExt.Limited
{
	/// <summary>
	///     Defines the standard layout for a result class. Use this class whenever something like a function needs a result
	///     wrapper.
	///     <para>
	///         Inherited objects should not have references to other objects. The results should stay in a 'box', so don't
	///         include any references to other bigger objects to reduce needed ram space. The result class should be able to
	///         provide all needed objects on its own.
	///     </para>
	///     <para>
	///         Do not include any events, cause events contains references to other objects. All events should be included
	///         into a separate state classes. The state classes should be of type <see cref="BaseState{TOwner}" />
	///     </para>
	///     <para>
	///         The result class is not thread safe be careful when using it in a threaded environment. Normally a result is
	///         combined with a state class which in combination provides thread safety if state is correctly handled by state
	///         lock.
	///     </para>
	/// </summary>
	[Serializable]
	public abstract class BaseResult : Base
	{
		private TimeSpan? _duration;
		private Exception _exception;
		private bool _isCanceled;
		private bool _isDefined;
		private bool _isFaulted;
		private bool _isSucceeded;
		private DateTime? _startTime;


		#region Overrides
		/// <summary>Returns not the type.</summary>
		public override string ToString()
		{
			if (!IsDefined)
				return "No results yet." + (StartTime == null ? "" : " started: " + StartTime);
			if (IsSucceeded)
				return "Succeeded" + (Duration == null ? "" : " in " + Duration.Value.TotalMilliseconds.ToString("0") + " ms");
			if (IsFaulted)
				return "Faulted" + (Duration == null ? "" : " in " + Duration.Value.TotalMilliseconds.ToString("0") + " ms") + (Exception == null ? "" : " *" + Exception.Message);
			if (IsCanceled)
				return "Canceled" + (Duration == null ? "" : " in " + Duration.Value.TotalMilliseconds.ToString("0") + " ms");
			return base.ToString();
		}
		#endregion


		/// <summary>Determines if function was faulted. If null then the function has not finished yet.</summary>
		public virtual bool IsFaulted
		{
			get { return _isFaulted; }
			private set { SetProperty(ref _isFaulted, value); }
		}
		/// <summary>Determines if function was canceled. If null then the function has not finished yet.</summary>
		public virtual bool IsCanceled
		{
			get { return _isCanceled; }
			private set { SetProperty(ref _isCanceled, value); }
		}
		/// <summary>Determines if function was succeeded. If null then the function has not finished yet.</summary>
		public virtual bool IsSucceeded
		{
			get { return _isSucceeded; }
			private set { SetProperty(ref _isSucceeded, value); }
		}
		/// <summary>If function is faulted gets the associated exception.</summary>
		public virtual Exception Exception
		{
			get { return _exception; }
			private set { SetProperty(ref _exception, value); }
		}
		/// <summary>Gets the execution duration. If null then the function has not finished yet.</summary>
		public virtual TimeSpan? Duration
		{
			get { return _duration; }
			private set { SetProperty(ref _duration, value); }
		}
		/// <summary>Determines if function is already completed and result values are defined.</summary>
		public bool IsDefined
		{
			get { return _isDefined; }
			private set { SetProperty(ref _isDefined, value); }
		}
		/// <summary>Gets the start time of the function. If null then the function has not started yet or no start time exists.</summary>
		public DateTime? StartTime
		{
			get { return _startTime; }
			private set { SetProperty(ref _startTime, value); }
		}

		/// <summary>Sets the results start time.
		///     <para>CAVE: No thread safety.</para>
		/// </summary>
		internal void SetStartTime(DateTime? startTime = null)
		{
			StartTime = startTime ?? DateTime.Now;
		}
		/// <summary>Sets the result to succeeded.
		///     <para>CAVE: No thread safety.</para>
		/// </summary>
		protected internal virtual void SetSucceeded(TimeSpan? duration = null)
		{
			if (IsDefined)
				throw new InvalidOperationException("The result is already specified!");

			Duration = duration ?? (StartTime == null ? null : DateTime.Now - StartTime);

			IsFaulted = false;
			IsCanceled = false;
			IsSucceeded = true;

			IsDefined = true;
		}
		/// <summary>Sets the result to faulted.
		///     <para>CAVE: No thread safety.</para>
		/// </summary>
		/// <param name="exc"></param>
		/// <param name="duration"></param>
		protected internal virtual void SetFaulted(Exception exc, TimeSpan? duration = null)
		{
			if (IsDefined)
				throw new InvalidOperationException("The result is already specified!");
			Duration = duration ?? (StartTime == null ? null : DateTime.Now - StartTime);
			Exception = exc;

			IsCanceled = false;
			IsSucceeded = false;
			IsFaulted = true;

			IsDefined = true;
		}
		/// <summary>Sets the result to canceled.
		///     <para>CAVE: No thread safety.</para>
		/// </summary>
		/// <param name="duration"></param>
		protected internal virtual void SetCanceled(TimeSpan? duration = null)
		{
			if (IsDefined)
				throw new InvalidOperationException("The result is already specified!");
			Duration = duration ?? (StartTime == null ? null : DateTime.Now - StartTime);

			IsFaulted = false;
			IsSucceeded = false;
			IsCanceled = true;

			IsDefined = true;
		}
	}
}