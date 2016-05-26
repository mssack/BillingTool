// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Data;
using System.Linq;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables.belegDataCategories;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class BelegDatenTable : ICanFilterByDate<BelegData>
	{
		private BelegDatenTableSampleDataFor _sampleFor;


		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="BelegData" />'s between <paramref name="from" /> to <paramref name="to" />. This <see cref="ContractCollection{TRow}" />
		///     is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<BelegData> LoadThenFind_Between(DateTime from, DateTime to)
		{
			from = from.Subtract(from.TimeOfDay);
			to = to.Add(new TimeSpan(0, 23 - to.Hour, 59 - to.Minute, 59 - to.Second, 999 - to.Millisecond));
			if (!HasBeenLoaded)
			{
				var timeBetweenSelector = CsDb.Statements.SqlCe.GetTimeBetweenSelector(DatumCol, @from, to);
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {timeBetweenSelector} ORDER BY [{DatumCol}] DESC", false);
			}

			return CreateContractCollection(entry => entry.Datum >= @from && entry.Datum <= to);
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

		/// <summary>
		/// Contains sample beleg datas.
		/// </summary>
		public BelegDatenTableSampleDataFor SampleFor => _sampleFor??(_sampleFor = new BelegDatenTableSampleDataFor(this));

		/// <summary>Gets the latest <see cref="BelegData" />'s by the <paramref name="number" />.</summary>
		public BelegData[] LoadThenFind_Latest(int number)
		{
			if (!HasBeenLoaded)
			{
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {NummerCol}>{DataSet.Configurations.DataIntegrity.LastBelegNummer - number}", false);
			}

			return this.Where(x => x.Nummer > DataSet.Configurations.DataIntegrity.LastBelegNummer - number).ToArray();
		}

		/// <summary>Gets the <see cref="BelegData" />'s by the <see cref="BelegData.Nummer" />.</summary>
		public BelegData[] LoadThenFind_Between(int from, int to)
		{
			if (!HasBeenLoaded)
			{
				DownloadRows($"SELECT * FROM [{NativeName}] WHERE {NummerCol}>={from} AND {NummerCol}<={to}", false);
			}

			return this.Where(x => x.Nummer >= from && x.Nummer <= to).ToArray();
		}

	}
}