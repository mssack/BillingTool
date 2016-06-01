// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Runtime.Serialization;






namespace CsWpfBase.Db.models.helper
{
	/// <summary>Represents a weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <typeparam name="T">The type of the object that is referenced.</typeparam>
	[Serializable]
	public class CsWeakReference<T>
		: WeakReference
		where T : class
	{
		/// <summary>Initializes a new instance of the CsWeakReference{T} class, referencing the specified object.</summary>
		/// <param name="target">The object to reference.</param>
		public CsWeakReference(T target)
			: base(target)
		{
		}

		/// <summary>Initializes a new instance of the CsWeakReference{T} class, referencing the specified object and using the specified resurrection tracking.</summary>
		/// <param name="target">An object to track.</param>
		/// <param name="trackResurrection">
		///     Indicates when to stop tracking the object. If true, the object is tracked after finalization; if false, the object is only tracked until
		///     finalization.
		/// </param>
		public CsWeakReference(T target, bool trackResurrection)
			: base(target, trackResurrection)
		{
		}

		/// <summary>Initializes a new instance of the CsWeakReference{T} class, referencing the specified object and using the specified resurrection tracking.</summary>
		protected CsWeakReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets or sets the object (the target) referenced by the current CsWeakReference{T} object.</summary>
		public new T Target
		{
			get { return (T) base.Target; }
			set { base.Target = value; }
		}

		/// <summary>Try to get the value</summary>
		public bool TryGetTarget(out T target)
		{
			target = Target;
			return target != null;
		}

		/// <summary>Sets the target</summary>
		public void SetTarget(T target)
		{
			Target = target;
		}
	}
}