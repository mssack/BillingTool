// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of extension methods for <see cref="Exception" />.</summary>
	[DebuggerStepThrough]
	public static class ExceptionHandlingExtensions
	{
		/// <summary>Returns the most inner exception in a exception. Traversing the inner exception.</summary>
		public static Exception MostInner(this Exception e)
		{
			Exception ee = e;
			while (ee.InnerException != null)
				ee = ee.InnerException;
			return ee;
		}
		/// <summary>Returns the exception in a exception if it is of type <paramref name="T"/>. Traversing the inner exception.</summary>
		public static T CastOrFindInInnerExceptions<T>(this Exception e) where T: Exception
		{
			if (e is T)
				return (T) e;
			Exception ee = e;
			while (ee.InnerException != null)
			{
				ee = ee.InnerException;
				if (ee is T)
					return (T)ee;
			}
			return null;
		}
	}
}