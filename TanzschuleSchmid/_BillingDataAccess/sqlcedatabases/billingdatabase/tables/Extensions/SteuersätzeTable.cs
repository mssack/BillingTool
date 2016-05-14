// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class SteuersätzeTable : ICanFilterByDate<Steuersatz>
	{


		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="Steuersatz" />'s WHERE <see cref="LastUsedDateCol" /> is between <paramref name="from" /> to <paramref name="to" />. This
		///     <see cref="ContractCollection" /> is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<Steuersatz> Get_Between(DateTime from, DateTime to)
		{
			from = from.Subtract(from.TimeOfDay);
			to = to.Add(new TimeSpan(0, 23 - to.Hour, 59 - to.Minute, 59 - to.Second, 999 - to.Millisecond));
			if (!HasBeenLoaded)
			{
				var timeBetweenSelector = CsDb.Statements.SqlCe.GetTimeBetweenSelector(LastUsedDateCol, from, to);
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {timeBetweenSelector} ORDER BY [{LastUsedDateCol}] DESC");
			}

			return CreateContractCollection(entry => entry.LastUsedDate >= from && entry.LastUsedDate <= to);
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
		/// <summary>Find or loads a <see cref="Steuersatz" /> where <see cref="Steuersatz.Percent" /> = <paramref name="percent" />.</summary>
		/// <param name="percent"><see cref="Steuersatz.Percent" />.</param>
		public Steuersatz FindOrLoad_By_Percent(decimal percent)
		{
			return Find_By_Percent(percent) ?? LoadThenFind_By_Percent(percent);
		}

		/// <summary>Load then finds a <see cref="Steuersatz" /> where <see cref="Steuersatz.Percent" /> = <paramref name="percent" />.</summary>
		/// <param name="percent"><see cref="Steuersatz.Percent" />.</param>
		public Steuersatz LoadThenFind_By_Percent(decimal percent)
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{PercentCol}] = '{percent}'", false);
			return Find_By_Percent(percent);
		}

		/// <summary>Finds a <see cref="Steuersatz" /> where <see cref="Steuersatz.Percent" /> = <paramref name="percent" />.</summary>
		/// <param name="percent"><see cref="Steuersatz.Percent" />.</param>
		public Steuersatz Find_By_Percent(decimal percent)
		{
			var postens = Select($"{PercentCol} = '{percent}'");
			return postens.Length == 0 ? null : postens[0];
		}
	}
}