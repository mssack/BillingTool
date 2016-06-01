// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Ev.Objects.FuncExt
{
#pragma warning disable 1591
	/// <summary>
	///     Defines the standard layout for a option class. Use this class whenever there is a need for an option pool.
	///     <para>
	///         An option is per definition clone able. The <see cref="ToString" /> method should provide a list of the
	///         options.
	///     </para>
	/// </summary>
	[Serializable]
	public abstract class BaseOption : Base
	{
		#region Abstracts
		/// <summary>
		///     Copy all option relevant fields to the target object. Typically typecast the <paramref name="target" /> param
		///     to the current object type.
		/// </summary>
		public abstract void CopyTo<T>(T target) where T : BaseOption;
		#endregion


		#region Overrides
		public override string ToString()
		{
			return GetType().Name;
		}
		#endregion


		/// <summary>Clones actual object into a new object by using <see cref="CopyTo{T}" /> method.</summary>
		public virtual BaseOption Clone()
		{
			var newCreatedOption = (BaseOption) Activator.CreateInstance(GetType());
			CopyTo(newCreatedOption);
			return newCreatedOption;
		}
	}
}