// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Data;
using System.Globalization;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using CsWpfBase.Db;
using CsWpfBase.Db.models.helper;






// ReSharper disable InconsistentNaming

//siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class SteuersätzeTable : ICanFilterByDate<Steuersatz>
	{
		private Steuersatz _defaultBetragSatzErmäßigt1;
		private Steuersatz _defaultBetragSatzErmäßigt2;
		private Steuersatz _defaultBetragSatzNormal;
		private Steuersatz _defaultBetragSatzNull;
		private Steuersatz _defaultBetragSatzBesonders;


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


		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Normal.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzNormal
		{
			get
			{
				if (_defaultBetragSatzNormal != null) return _defaultBetragSatzNormal;
				var id = DataSet.Configurations.Default_BetragSatzNormal;

				_defaultBetragSatzNormal = LoadThenFind(id);
				if (_defaultBetragSatzNormal != null) return _defaultBetragSatzNormal;


				DataSet.Configurations.LastSteuersatzKürzel = (char) (DataSet.Configurations.LastSteuersatzKürzel + 1);

				_defaultBetragSatzNormal = NewRow();
				_defaultBetragSatzNormal.Id = id;
				_defaultBetragSatzNormal.CreationDate = DateTime.Now;
				_defaultBetragSatzNormal.Kürzel = DataSet.Configurations.LastSteuersatzKürzel.ToString();
				_defaultBetragSatzNormal.Name = "Normal";
				_defaultBetragSatzNormal.Percent = 20;
				_defaultBetragSatzNormal.Table.Add(_defaultBetragSatzNormal);
				return _defaultBetragSatzNormal;
			}
			set
			{
				if (SetProperty(ref _defaultBetragSatzNormal, value))
					DataSet.Configurations.Default_BetragSatzNormal = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Ermäßigt-1.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzErmäßigt1
		{
			get
			{
				if (_defaultBetragSatzErmäßigt1 != null) return _defaultBetragSatzErmäßigt1;
				var id = DataSet.Configurations.Default_BetragSatzErmäßigt1;

				_defaultBetragSatzErmäßigt1 = LoadThenFind(id);
				if (_defaultBetragSatzErmäßigt1 != null) return _defaultBetragSatzErmäßigt1;

				DataSet.Configurations.LastSteuersatzKürzel = (char)(DataSet.Configurations.LastSteuersatzKürzel + 1);

				_defaultBetragSatzErmäßigt1 = NewRow();
				_defaultBetragSatzErmäßigt1.Id = id;
				_defaultBetragSatzErmäßigt1.CreationDate = DateTime.Now;
				_defaultBetragSatzErmäßigt1.Kürzel = DataSet.Configurations.LastSteuersatzKürzel.ToString();
				_defaultBetragSatzErmäßigt1.Name = "Ermäßigt 1";
				_defaultBetragSatzErmäßigt1.Percent = 10;
				_defaultBetragSatzErmäßigt1.Table.Add(_defaultBetragSatzErmäßigt1);
				return _defaultBetragSatzErmäßigt1;
			}
			set
			{
				if (SetProperty(ref _defaultBetragSatzErmäßigt1, value))
					DataSet.Configurations.Default_BetragSatzErmäßigt1 = value?.Id ?? Guid.NewGuid();
			}
		}
		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Ermäßigt-2.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzErmäßigt2
		{
			get
			{
				if (_defaultBetragSatzErmäßigt2 != null) return _defaultBetragSatzErmäßigt2;
				var id = DataSet.Configurations.Default_BetragSatzErmäßigt2;

				_defaultBetragSatzErmäßigt2 = LoadThenFind(id);
				if (_defaultBetragSatzErmäßigt2 != null) return _defaultBetragSatzErmäßigt2;

				DataSet.Configurations.LastSteuersatzKürzel = (char)(DataSet.Configurations.LastSteuersatzKürzel + 1);


				_defaultBetragSatzErmäßigt2 = NewRow();
				_defaultBetragSatzErmäßigt2.Id = id;
				_defaultBetragSatzErmäßigt2.CreationDate = DateTime.Now;
				_defaultBetragSatzErmäßigt2.Kürzel = DataSet.Configurations.LastSteuersatzKürzel.ToString();
				_defaultBetragSatzErmäßigt2.Name = "Ermäßigt 2";
				_defaultBetragSatzErmäßigt2.Percent = 13;
				_defaultBetragSatzErmäßigt2.Table.Add(_defaultBetragSatzErmäßigt2);
				return _defaultBetragSatzErmäßigt2;
			}
			set
			{
				if (SetProperty(ref _defaultBetragSatzErmäßigt2, value))
					DataSet.Configurations.Default_BetragSatzErmäßigt2 = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzNull
		{
			get
			{
				if (_defaultBetragSatzNull != null) return _defaultBetragSatzNull;
				var id = DataSet.Configurations.Default_BetragSatzNull;

				_defaultBetragSatzNull = LoadThenFind(id);
				if (_defaultBetragSatzNull != null) return _defaultBetragSatzNull;

				DataSet.Configurations.LastSteuersatzKürzel = (char)(DataSet.Configurations.LastSteuersatzKürzel + 1);


				_defaultBetragSatzNull = NewRow();
				_defaultBetragSatzNull.Id = id;
				_defaultBetragSatzNull.CreationDate = DateTime.Now;
				_defaultBetragSatzNull.Kürzel = DataSet.Configurations.LastSteuersatzKürzel.ToString();
				_defaultBetragSatzNull.Name = "Null";
				_defaultBetragSatzNull.Percent = 0;
				_defaultBetragSatzNull.Table.Add(_defaultBetragSatzNull);
				return _defaultBetragSatzNull;
			}
			set
			{
				if (SetProperty(ref _defaultBetragSatzNull, value))
					DataSet.Configurations.Default_BetragSatzNull = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>
		///     Gets or sets the default <see cref="Steuersatz" /> which can be used as Betrag-Satz-Besonders.
		///     <para>siehe Literatur "UStG 1994, Fassung vom 15.05.2016.pdf", §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Steuersatz Default_BetragSatzBesonders
		{
			get
			{
				if (_defaultBetragSatzBesonders != null) return _defaultBetragSatzBesonders;
				var id = DataSet.Configurations.Default_BetragSatzBesonders;

				_defaultBetragSatzBesonders = LoadThenFind(id);
				if (_defaultBetragSatzBesonders != null) return _defaultBetragSatzBesonders;

				DataSet.Configurations.LastSteuersatzKürzel = (char)(DataSet.Configurations.LastSteuersatzKürzel + 1);


				_defaultBetragSatzBesonders = NewRow();
				_defaultBetragSatzBesonders.Id = id;
				_defaultBetragSatzBesonders.CreationDate = DateTime.Now;
				_defaultBetragSatzBesonders.Kürzel = DataSet.Configurations.LastSteuersatzKürzel.ToString();
				_defaultBetragSatzBesonders.Name = "Besonders";
				_defaultBetragSatzBesonders.Percent = 19;
				_defaultBetragSatzBesonders.Table.Add(_defaultBetragSatzBesonders);
				return _defaultBetragSatzBesonders;
			}
			set
			{
				if (SetProperty(ref _defaultBetragSatzBesonders, value))
					DataSet.Configurations.Default_BetragSatzBesonders = value?.Id ?? Guid.NewGuid();
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
			var postens = Select($"{PercentCol} = '{percent.ToString(Nfi)}'");
			return postens.Length == 0 ? null : postens[0];
		}

		/// <summary>Invokes the creation of the default <see cref="Steuersatz" /> rows.</summary>
		public void EnsureDefaults()
		{
			// ReSharper disable once NotAccessedVariable
			// ReSharper disable RedundantAssignment
			var name = Default_BetragSatzNormal.Name;
			name = Default_BetragSatzErmäßigt1.Name;
			name = Default_BetragSatzErmäßigt2.Name;
			name = Default_BetragSatzNull.Name;
			// ReSharper restore RedundantAssignment
		}
		private static NumberFormatInfo Nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };
	}
}