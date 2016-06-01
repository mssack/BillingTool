// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;






// ReSharper disable InconsistentNaming

//siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze

namespace BillingToolDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class SteuersätzeTable : ICanFilterByDate<Steuersatz>
	{
		private static readonly NumberFormatInfo Nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};
		private Steuersatz _defaultBetragSatzBesonders;
		private Steuersatz _defaultBetragSatzErmäßigt1;
		private Steuersatz _defaultBetragSatzErmäßigt2;
		private Steuersatz _defaultBetragSatzNormal;
		private Steuersatz _defaultBetragSatzNull;


		#region Overrides/Interfaces
		/// <summary>
		///     Get all <see cref="Steuersatz" />'s WHERE <see cref="LastUsedDateCol" /> is between <paramref name="from" /> to <paramref name="to" />. This
		///     <see cref="ContractCollection" /> is always up to date.
		/// </summary>
		/// <param name="from">The from date inclusive</param>
		/// <param name="to">The to date inclusive</param>
		public ContractCollection<Steuersatz> LoadThenFind_Between(DateTime from, DateTime to)
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
		ContractCollection ICanFilterByDate.LoadThenFind_Between(DateTime from, DateTime to)
		{
			return LoadThenFind_Between(from, to);
		}
		#endregion


		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Normal.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzNormal
		{
			get { return GetOrCreate_DefaultBetragSatz(ref _defaultBetragSatzNormal, DataSet.Configurations.Default.BetragSatzNormal, "Normal", 20); }
			set
			{
				if (SetProperty(ref _defaultBetragSatzNormal, value))
					DataSet.Configurations.Default.BetragSatzNormal = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Ermäßigt-1.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzErmäßigt1
		{
			get { return GetOrCreate_DefaultBetragSatz(ref _defaultBetragSatzErmäßigt1, DataSet.Configurations.Default.BetragSatzErmäßigt1, "Ermäßigt 1", 10); }
			set
			{
				if (SetProperty(ref _defaultBetragSatzErmäßigt1, value))
					DataSet.Configurations.Default.BetragSatzErmäßigt1 = value?.Id ?? Guid.NewGuid();
			}
		}
		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Ermäßigt-2.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzErmäßigt2
		{
			get { return GetOrCreate_DefaultBetragSatz(ref _defaultBetragSatzErmäßigt2, DataSet.Configurations.Default.BetragSatzErmäßigt2, "Ermäßigt 2", 13); }
			set
			{
				if (SetProperty(ref _defaultBetragSatzErmäßigt2, value))
					DataSet.Configurations.Default.BetragSatzErmäßigt2 = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzNull
		{
			get { return GetOrCreate_DefaultBetragSatz(ref _defaultBetragSatzNull, DataSet.Configurations.Default.BetragSatzNull, "Null", 0); }
			set
			{
				if (SetProperty(ref _defaultBetragSatzNull, value))
					DataSet.Configurations.Default.BetragSatzNull = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Besonders.
		///     <para>siehe Literatur "UStG 1994, Fassung vom 15.05.2016.pdf", §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzBesonders
		{
			get { return GetOrCreate_DefaultBetragSatz(ref _defaultBetragSatzBesonders, DataSet.Configurations.Default.BetragSatzBesonders, "Besonders", 19); }
			set
			{
				if (SetProperty(ref _defaultBetragSatzBesonders, value))
					DataSet.Configurations.Default.BetragSatzBesonders = value?.Id ?? Guid.NewGuid();
			}
		}




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
			DownloadRows($"SELECT {DefaultSqlSelector} FROM [{NativeName}] WHERE [{PercentCol}] = '{percent.ToString(Nfi)}'", false);
			return Find_By_Percent(percent);
		}

		/// <summary>Finds a <see cref="Steuersatz" /> where <see cref="Steuersatz.Percent" /> = <paramref name="percent" />.</summary>
		/// <param name="percent"><see cref="Steuersatz.Percent" />.</param>
		public Steuersatz Find_By_Percent(decimal percent)
		{

			var postens = Collection.Where(x => x.Percent == percent).ToArray();
			return postens.Length == 0 ? null : postens[0];
		}
		

		private Steuersatz GetOrCreate_DefaultBetragSatz(ref Steuersatz field, Guid id, string name, decimal percent)
		{
			if (field != null) return field;
			field = HasBeenLoaded ? Find(id) : FindOrLoad(id);
			if (field != null) return field;
			DataSet.Configurations.DataIntegrity.LastSteuersatzKürzel = (char) (DataSet.Configurations.DataIntegrity.LastSteuersatzKürzel + 1);

			field = NewRow();
			field.Id = id;
			field.CreationDate = DateTime.Now;
			field.LastUsedDate = field.CreationDate; //Makes it unchangeable
			field.Kürzel = DataSet.Configurations.DataIntegrity.LastSteuersatzKürzel.ToString();
			field.Name = name;
			field.Percent = percent;
			field.Table.Add(field);

			return field;
		}
	}
}