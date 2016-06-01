// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of DateTime extensions</summary>
	public static class DateTimeExtensions
	{
		/// <summary>Gets the age for an birthday.</summary>
		public static int Age(this DateTime time)
		{
			DateTime today = DateTime.Today;
			int age = today.Year - time.Year;
			if (time > today.AddYears(-age))
				age--;
			return age;
		}
		/// <summary>Gets the current date with the time adjusted to the end of day.</summary>
		public static DateTime EndOfDay(this DateTime time)
		{
			return new DateTime(time.Year, time.Month, time.Day, 23, 59, 59, 999);
		}
		/// <summary>Gets the current date with the time adjusted to the start of the day.</summary>
		public static DateTime StartOfDay(this DateTime time)
		{
			return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0, 0);
		}
		/// <summary>Gets the current date with the time adjusted to the end of day.</summary>
		public static DateTime? EndOfDay(this DateTime? time)
		{
			if (time == null)
				return null;
			return new DateTime(time.Value.Year, time.Value.Month, time.Value.Day, 23, 59, 59, 999);
		}
		/// <summary>Gets the current date with the time adjusted to the start of the day.</summary>
		public static DateTime? StartOfDay(this DateTime? time)
		{
			if (time == null)
				return null;
			return new DateTime(time.Value.Year, time.Value.Month, time.Value.Day, 0, 0, 0, 0);
		}
	}
}