// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows

{
	partial class CashBookEntry
	{
		#region Overrides/Interfaces
		/// <summary>On row creation this method will be executed. So a new row will always have the highest reference number</summary>
		public override void ApplyExtendedDefaults()
		{
			var highestReferenceNumber = DbProxy.ExecuteCommand($"SELECT MAX({CashBookTable.ReferenceNumberCol}) FROM {CashBookTable.NativeName}").Rows[0][0];
			ReferenceNumber = (highestReferenceNumber == DBNull.Value ? 0 : (int) highestReferenceNumber) + 1;
		}
		#endregion
	}
}