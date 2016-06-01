// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Linq;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables.belegDataCategories
{
	/// <summary>Used for categorization.</summary>
	public sealed class BelegDatenTableSampleDataFor : Base
	{
		private BelegData _printOrMail;
		private BelegData _storno;
		private BelegData _tagesBon;
		private BelegData _monatsBon;
		private BelegData _jahresBon;

		internal BelegDatenTableSampleDataFor(BelegDatenTable owner)
		{
			Owner = owner;
		}

		/// <summary>
		///     A <see cref="BelegData" /> which can be used as a sample for a <see cref="OutputFormat" /> with <see cref="BonLayoutTypes.Print" /> or
		///     <see cref="BonLayoutTypes.Mail" />.
		/// </summary>
		public BelegData PrintOrMail => GetSampleBelegDataFor(ref _printOrMail, BelegDataTypes.Bar, BelegDataTypes.Bankomat, BelegDataTypes.Kreditkarte);

		/// <summary>A <see cref="BelegData" /> which can be used as a sample for a <see cref="OutputFormat" /> with <see cref="BonLayoutTypes.Storno" />.</summary>
		public BelegData Storno => GetSampleBelegDataFor(ref _storno, BelegDataTypes.Storno);

		/// <summary>A <see cref="BelegData" /> which can be used as a sample for a <see cref="OutputFormat" /> with <see cref="BonLayoutTypes.TagesBon" />.</summary>
		public BelegData TagesBon => GetSampleBelegDataFor(ref _tagesBon, BelegDataTypes.TagesBon);

		/// <summary>A <see cref="BelegData" /> which can be used as a sample for a <see cref="OutputFormat" /> with <see cref="BonLayoutTypes.MonatsBon" />.</summary>
		public BelegData MonatsBon => GetSampleBelegDataFor(ref _monatsBon, BelegDataTypes.MonatsBon);

		/// <summary>A <see cref="BelegData" /> which can be used as a sample for a <see cref="OutputFormat" /> with <see cref="BonLayoutTypes.JahresBon" />.</summary>
		public BelegData JahresBon => GetSampleBelegDataFor(ref _jahresBon, BelegDataTypes.JahresBon);



		/// <summary>returns a <see cref="BelegData" /> which could be used as a sample for a specific Bonlayout.</summary>
		private BelegData GetSampleBelegDataFor(ref BelegData field, params BelegDataTypes[] bonLayouts)
		{
			if (field != null)
				return field;

			var belegDatas = Owner.DownloadRows($"SELECT TOP(1) {Owner.DefaultSqlSelector} FROM {BelegDatenTable.NativeName} WHERE {bonLayouts.Select(x => $"{BelegDatenTable.TypNumberCol} = '{((int)x).ToString()}'").Join(" OR ")}");
			field = belegDatas.Length == 0 ? null : belegDatas[0];
			return field;
		}

		/// <summary>Gets or sets the Owner.</summary>
		private BelegDatenTable Owner { get; }
	}
}