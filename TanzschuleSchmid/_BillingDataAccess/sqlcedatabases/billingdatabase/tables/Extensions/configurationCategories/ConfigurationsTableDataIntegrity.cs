// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Runtime.CompilerServices;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;
// ReSharper disable InconsistentNaming






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories
{
	/// <summary>Used for categorization.</summary>
	public sealed class ConfigurationsTableDataIntegrity : Base
	{
		private ConfigurationsTable _owner;

		internal ConfigurationsTableDataIntegrity(ConfigurationsTable owner)
		{
			_owner = owner;
		}


		/// <summary>
		///     The last applied <see cref="BelegData.Nummer" />. Increment this number by one each time a new <see cref="BelegData" /> is added to the
		///     database.
		/// </summary>
		public int LastBelegNummer
		{
			get { return GetValue(0); }
			set { SetValue(value); }
		}

		/// <summary>Each time a new <see cref="BelegData" /> is added the <see cref="BelegData.BetragBrutto" /> has to be added to the
		///     <see cref="Umsatzzähler" />.</summary>
		public decimal Umsatzzähler
		{
			get { return GetValue<decimal>(0); }
			set { SetValue(value); }
		}

		/// <summary>The last used <see cref="Steuersatz.Kürzel" />. Each time a new <see cref="Steuersatz" /> is added increment this value by one.</summary>
		public char LastSteuersatzKürzel
		{
			get { return GetValue((char)('A' - 1)); }
			set { SetValue(value); }
		}

		/// <summary>The last included <see cref="BelegData.Nummer"/> in a MonatsBon.</summary>
		public int MonatsBon_LastIncludedBon
		{
			get { return GetValue(0); }
			set { SetValue(value); }
		}

		/// <summary>Gets or sets the Owner.</summary>
		private ConfigurationsTable Owner
		{
			get { return _owner; }
			set { SetProperty(ref _owner, value); }
		}



		internal T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			return Owner.GetValue(defaultValue, $"INTEGRITY_{name}");
		}

		internal void SetValue(object value, [CallerMemberName] string name = null)
		{
			Owner.SetValue(value, $"INTEGRITY_{name}");
			OnPropertyChanged(name);
		}
	}
}