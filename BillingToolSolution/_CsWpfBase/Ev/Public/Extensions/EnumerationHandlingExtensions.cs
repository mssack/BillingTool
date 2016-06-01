// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of extension methods for <see cref="IEnumerable{T}" />.</summary>
	[DebuggerStepThrough]
	public static class EnumerationHandlingExtensions
	{
		/// <summary>Iterates over one item and yield returns it.</summary>
		public static void ForEach<T>(this IList<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}

		/// <summary>Extension method for <see cref="string.Join(string, IEnumerable{string})" />.</summary>
		public static string Join(this IEnumerable<string> items, string delimiter = ", ")
		{
			return items == null ? "" : string.Join(delimiter, items);
		}

		/// <summary>Finds the object where a specific value is the maximum in that list and returns the object itself.</summary>
		public static T MaxObject<T, TU>(this IEnumerable<T> source, Func<T, TU> selector) where TU : IComparable<TU>
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			var first = true;
			var maxObj = default(T);
			var maxKey = default(TU);
			foreach (var item in source)
			{
				if (first)
				{
					maxObj = item;
					maxKey = selector(maxObj);
					first = false;
				}
				else
				{
					var currentKey = selector(item);
					if (currentKey.CompareTo(maxKey) > 0)
					{
						maxKey = currentKey;
						maxObj = item;
					}
				}
			}
			return maxObj;
		}
		/// <summary>Finds the object where a specific value is the maximum in that list and returns the object itself.</summary>
		public static IEnumerable<T> DistinctBy<T, TU>(this IEnumerable<T> source, Func<T, TU> selector)
		{
			return source.Distinct(new AnonComparer<T, TU>(selector));
		}
	}
}