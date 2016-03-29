// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections;
using System.Collections.Generic;






namespace CsWpfBase.Utilitys
{
	/// <summary>An anonymous comparer for a default comparison with generic types.</summary>
	public class AnonComparer<TObject, TType> : IComparer<TObject>, IEqualityComparer<TObject>
	{
		/// <summary>ctor</summary>
		public AnonComparer(Func<TObject, TType> selector)
		{
			Selector = selector;
		}


		#region Overrides/Interfaces
		/// <summary>Compares two objects with the default comparer.</summary>
		public int Compare(TObject x, TObject y)
		{
			if (x == null && y == null)
				return 0;
			if (Selector == null)
				return 1;
			return Comparer.Default.Compare(Selector(x), Selector(y));
		}
		#endregion


		private Func<TObject, TType> Selector { get; }

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		/// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
		public bool Equals(TObject x, TObject y)
		{
			if (x == null && y == null)
				return true;
			if (x == null)
				return false;
			if (y == null)
				return false;

			return Equals(Selector(x), Selector(y));
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		/// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
		public int GetHashCode(TObject obj)
		{
			return Selector(obj).GetHashCode();
		}
	}
}