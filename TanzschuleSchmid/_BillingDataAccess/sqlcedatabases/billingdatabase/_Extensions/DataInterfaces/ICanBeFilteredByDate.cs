// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using System.Data;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.models.helper;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces
{
	/// <summary>Used to share the ability of a table to be filtered by a date zone.</summary>
	public interface ICanFilterByDate
	{
		#region Abstract
		/// <summary>
		///     Get all <see cref="DataRow" />'s between <paramref name="from" /> to <paramref name="to" />. The <see cref="ContractCollection" /> is always up
		///     to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		ContractCollection LoadThenFind_Between(DateTime from, DateTime to);
		#endregion
	}
	/// <summary>Used to share the ability of a table to be filtered by a date zone.</summary>
	public interface ICanFilterByDate<TRow> : ICanFilterByDate
		where TRow : CsDbRowBase
	{
		#region Abstract
		/// <summary>
		///     Get all <see cref="DataRow" />'s between <paramref name="from" /> to <paramref name="to" />. The <see cref="ContractCollection" /> is always up
		///     to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		new ContractCollection<TRow> LoadThenFind_Between(DateTime from, DateTime to);
		#endregion
	}
}