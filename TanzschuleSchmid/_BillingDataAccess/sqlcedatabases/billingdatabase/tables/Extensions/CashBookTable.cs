// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Db.models.helper;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class CashBookTable
	{
		/// <summary>
		///     Get all <see cref="CashBookEntry" />'s between <paramref name="from" /> to <paramref name="to" />. This <see cref="ContractCollection{TRow}" />
		///     is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<CashBookEntry> Get_Between(DateTime from, DateTime to)
		{
			//yyyy-mm-dd hh:mi:ss (24h) =120
			//see https://technet.microsoft.com/en-us/library/ms174450%28v=sql.110%29.aspx
			from = from.Subtract(from.TimeOfDay);
			to = to.Add(new TimeSpan(0, 23 - to.Hour, 59 - to.Minute, 59 - to.Second, 999 - to.Millisecond));



			return CreateContractCollection(entry => entry.Datum >= from && entry.Datum <= to, DownloadRows($"SELECT * FROM [{NativeName}] WHERE " +
																											$"CONVERT(NVARCHAR(10), [{DatumCol}], 121)>=CONVERT(NVARCHAR(10), '{from.ToString("yyyy-MM-dd")}', 121) AND " +
																											$"CONVERT(NVARCHAR(10), [{DatumCol}], 121)<=CONVERT(NVARCHAR(10), '{to.ToString("yyyy-MM-dd")}', 121) ORDER BY [{DatumCol}] DESC"));
		}
	}
}