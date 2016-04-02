// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class SteuersätzeTable
	{
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