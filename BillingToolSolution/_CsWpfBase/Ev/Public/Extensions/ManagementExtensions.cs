// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-27</date>

using System;
using System.Management;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of functions for Management.</summary>
	public static class ManagementExtensions
	{
		/// <summary>Try's to get the specific value.</summary>
		public static TValueType TryGet<TValueType>(this ManagementObject mo, string propName)
		{
			try
			{
				object o = mo[propName];
				if (o == null)
					return default(TValueType);
				if (o is string && typeof (TValueType) == typeof (DateTime))
					return (TValueType)(object)ManagementDateTimeConverter.ToDateTime((string) o);
				if (o is string && typeof (TValueType) == typeof (TimeSpan))
					return (TValueType)(object)ManagementDateTimeConverter.ToTimeSpan((string) o);
				return (TValueType) o;
			}
			catch (Exception e)
			{
				if (e is InvalidCastException)
					throw e;
				return default(TValueType);
			}
		}
	}
}