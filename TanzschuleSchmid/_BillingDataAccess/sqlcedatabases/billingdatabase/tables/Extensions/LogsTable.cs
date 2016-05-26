// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using System.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class LogsTable : ICanFilterByDate<Log>
	{


		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="Log" />'s WHERE <see cref="DateCol" /> is between <paramref name="from" /> to <paramref name="to" />. This
		///     <see cref="ContractCollection{TRow}" /> is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<Log> LoadThenFind_Between(DateTime from, DateTime to)
		{
			from = from.Subtract(from.TimeOfDay);
			to = to.Add(new TimeSpan(0, 23 - to.Hour, 59 - to.Minute, 59 - to.Second, 999 - to.Millisecond));
			if (!HasBeenLoaded)
			{
				var timeBetweenSelector = CsDb.Statements.SqlCe.GetTimeBetweenSelector(DateCol, from, to);
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {timeBetweenSelector} ORDER BY [{DateCol}] DESC");
			}

			return CreateContractCollection(entry => entry.Date >= from && entry.Date <= to);
		}

		/// <summary>
		///     Get all <see cref="DataRow" />'s between <paramref name="from" /> to <paramref name="to" />. The <see cref="ContractCollection" /> is always up
		///     to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		ContractCollection ICanFilterByDate.LoadThenFind_Between(DateTime from, DateTime to)
		{
			return LoadThenFind_Between(from, to);
		}
		#endregion
	}
}