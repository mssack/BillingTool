// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class PostensTable
	{
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