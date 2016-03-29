// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Db.models.bases;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class CashBookTable
	{
		#region Overrides/Interfaces
		/// <summary>
		///     Creates a new row but do not add it to the collection. This does automatically apply's the default values to the rows specified by the
		///     <see cref="CsDbRowBase.ApplyDefaults" /> method. It sets the <see cref="CashBookEntry.ReferenceNumber" /> field to the maximum value in database.
		/// </summary>
		public override CashBookEntry NewRow()
		{
			var highestReferenceNumber = DbProxy.ExecuteCommand($"SELECT MAX({ReferenceNumberCol}) FROM {NativeName}").Rows[0][0];


			var entry = base.NewRow();
			entry.Id = Guid.NewGuid();
			entry.ReferenceNumber = (highestReferenceNumber == DBNull.Value ? 0 : (int) highestReferenceNumber) + 1;
			return entry;
		}
		#endregion
	}
}