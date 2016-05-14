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
	partial class PostensTable : ICanFilterByDate<Posten>
	{
		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="Posten" />'s WHERE <see cref="LastUsedDateCol" /> is between <paramref name="from" /> to <paramref name="to" />. This
		///     <see cref="ContractCollection" /> is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<Posten> Get_Between(DateTime from, DateTime to)
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


		/// <summary>
		///     Find or loads a <see cref="Posten" /> where <see cref="Posten.Name" /> = <paramref name="name" /> AND <see cref="Posten.PreisBrutto" /> =
		///     <paramref name="preis" />.
		/// </summary>
		/// <param name="name"><see cref="Posten.Name" />.</param>
		/// <param name="preis"><see cref="Posten.PreisBrutto" />.</param>
		public Posten FindOrLoad_By_NameAndPreis(string name, decimal preis)
		{
			return Find_By_NameAndPreis(name, preis) ?? LoadThenFind_By_NameAndPreis(name, preis);
		}


		/// <summary>
		///     Load then finds a <see cref="Posten" /> where <see cref="Posten.Name" /> = <paramref name="name" /> AND <see cref="Posten.PreisBrutto" /> =
		///     <paramref name="preis" />.
		/// </summary>
		/// <param name="name"><see cref="Posten.Name" />.</param>
		/// <param name="preis"><see cref="Posten.PreisBrutto" />.</param>
		public Posten LoadThenFind_By_NameAndPreis(string name, decimal preis)
		{
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{NameCol}] LIKE '{name}' AND [{PreisBruttoCol}] = {preis}", false);
			return Find_By_NameAndPreis(name, preis);
		}


		/// <summary>
		///     Finds a <see cref="Posten" /> where <see cref="Posten.Name" /> = <paramref name="name" /> AND <see cref="Posten.PreisBrutto" /> =
		///     <paramref name="preis" />.
		/// </summary>
		/// <param name="name"><see cref="Posten.Name" />.</param>
		/// <param name="preis"><see cref="Posten.PreisBrutto" />.</param>
		public Posten Find_By_NameAndPreis(string name, decimal preis)
		{
			var postens = Select($"{NameCol} = '{name}' AND {PreisBruttoCol} = '{preis}'");
			return postens.Length == 0 ? null : postens[0];
		}
	}
}