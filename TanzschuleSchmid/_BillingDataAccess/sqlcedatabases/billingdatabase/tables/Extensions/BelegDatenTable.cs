// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using System.Data;
using System.Linq.Expressions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class BelegDatenTable : ICanFilterByDate<BelegData>
	{


		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="BelegData" />'s between <paramref name="from" /> to <paramref name="to" />. This <see cref="ContractCollection{TRow}" />
		///     is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<BelegData> Get_Between(DateTime from, DateTime to)
		{
			if (!HasBeenLoaded)
			{
				var timeBetweenSelector = CsDb.Statements.SqlCe.GetTimeBetweenSelector(DatumCol, @from, to);
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {timeBetweenSelector} ORDER BY [{DatumCol}] DESC");
			}

			return CreateContractCollection(entry => entry.Datum >= @from && entry.Datum <= to);
		}
		/// <summary>
		///     Get all <see cref="BelegData" />'s between <paramref name="from" /> to <paramref name="to" />. This <see cref="ContractCollection{TRow}" />
		///     is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<BelegData> Get_Between(DateTime from, DateTime to, Expression<Func<BelegData, bool>>  expr)
		{
			if (!HasBeenLoaded)
			{
				var timeBetweenSelector = CsDb.Statements.SqlCe.GetTimeBetweenSelector(DatumCol, @from, to);
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {timeBetweenSelector} ORDER BY [{DatumCol}] DESC");
			}
			
			return CreateContractCollection(entry => entry.Datum >= @from && entry.Datum <= to && expr.Compile()(entry));
		}

		/// <summary>
		///     Get all <see cref="DataRow" />'s between <paramref name="from" /> to <paramref name="to" />. The <see cref="ContractCollection" /> is always up
		///     to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		ContractCollection ICanFilterByDate.Get_Between(DateTime from, DateTime to)
		{
			return Get_Between(from, to);
		}
		#endregion
	}
}