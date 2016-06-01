// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.statements.sqlce
{
	/// <summary></summary>
	public class SqlCeStatements : Base
	{
		private static SqlCeStatements _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static SqlCeStatements I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new SqlCeStatements());
				}
			}
		}

		private SqlCeStatements()
		{
		}

		/// <summary>
		///     Returns a selector for searching time values in a datetime column for dates between <paramref name="from" /> and <paramref name="to" />. The
		///     return value can be used in WHERE clause.
		///     <code>
		/// SELECT * FROM WHERE {returnvalue} AND ... ORDER BY ...
		/// </code>
		/// </summary>
		/// <param name="column">The datetime column.</param>
		/// <param name="from">The datetime from</param>
		/// <param name="to">The datetime to</param>
		/// <param name="normalizeToDay">normalize to day means ignore time.</param>
		public string GetTimeBetweenSelector(string column, DateTime from, DateTime to, bool normalizeToDay = true)
		{
			//Currently not working if normalizeToDay = false.

			//yyyy-mm-dd hh:mi:ss (24h) =120
			//see https://technet.microsoft.com/en-us/library/ms174450%28v=sql.110%29.aspx
			if (normalizeToDay)
			{
				from = from.Subtract(from.TimeOfDay);
				to = to.Add(new TimeSpan(0, 23 - to.Hour, 59 - to.Minute, 59 - to.Second, 999 - to.Millisecond));
			}

			return $"(" +
					$"CONVERT(NVARCHAR(10), [{column}], 121)>=CONVERT(NVARCHAR(10), '{from.ToString("yyyy-MM-dd")}', 121) AND " +
					$"CONVERT(NVARCHAR(10), [{column}], 121)<=CONVERT(NVARCHAR(10), '{to.ToString("yyyy-MM-dd")}', 121)" +
					$")";
		}
	}
}