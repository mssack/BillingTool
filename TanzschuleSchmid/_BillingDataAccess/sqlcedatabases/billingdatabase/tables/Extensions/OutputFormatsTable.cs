// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class OutputFormatsTable
	{
		private OutputFormat _defaultJahresBonFormat;
		private OutputFormat _defaultMailFormat;
		private OutputFormat _defaultMonatsBonFormat;
		private OutputFormat _defaultPrintFormat;
		private OutputFormat _defaultStornoFormat;
		private OutputFormat _defaultTagesBonFormat;
		private ContractCollection<OutputFormat> _jahresBonFormate;
		private ContractCollection<OutputFormat> _mailFormate;
		private ContractCollection<OutputFormat> _monatsBonFormate;
		private ContractCollection<OutputFormat> _printFormate;
		private ContractCollection<OutputFormat> _stornoFormate;
		private ContractCollection<OutputFormat> _tagesBonFormate;

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for printing.</summary>
		public OutputFormat Default_PrintFormat
		{
			get { return GetDefaultFormat(ref _defaultPrintFormat, DataSet.Configurations.Default.PrintFormatId, BonLayouts.Unknown.Default_Print()); }
			set { SetDefaultFormat(ref _defaultPrintFormat, value, (confDefaults, id) => confDefaults.PrintFormatId = id); }
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for mailing.</summary>
		public OutputFormat Default_MailFormat
		{
			get { return GetDefaultFormat(ref _defaultMailFormat, DataSet.Configurations.Default.MailFormatId, BonLayouts.Unknown.Default_Mail()); }
			set { SetDefaultFormat(ref _defaultMailFormat, value, (confDefaults, id) => confDefaults.MailFormatId = id); }
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for Storno.</summary>
		public OutputFormat Default_StornoFormat
		{
			get { return GetDefaultFormat(ref _defaultStornoFormat, DataSet.Configurations.Default.StornoFormatId, BonLayouts.Unknown.Default_Storno(), true); }
			set { SetDefaultFormat(ref _defaultStornoFormat, value, (confDefaults, id) => confDefaults.StornoFormatId = id); }
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Tagesbon.</summary>
		public OutputFormat Default_TagesBonFormat
		{
			get { return GetDefaultFormat(ref _defaultTagesBonFormat, DataSet.Configurations.Default.TagesBonFormatId, BonLayouts.Unknown.Default_Tag(), true); }
			set { SetDefaultFormat(ref _defaultTagesBonFormat, value, (confDefaults, id) => confDefaults.TagesBonFormatId = id); }
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Monatsbon.</summary>
		public OutputFormat Default_MonatsBonFormat
		{
			get { return GetDefaultFormat(ref _defaultMonatsBonFormat, DataSet.Configurations.Default.MonatsBonFormatId, BonLayouts.Unknown.Default_Monat(), true); }
			set { SetDefaultFormat(ref _defaultMonatsBonFormat, value, (confDefaults, id) => confDefaults.MonatsBonFormatId = id); }
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Jahresbon.</summary>
		public OutputFormat Default_JahresBonFormat
		{
			get { return GetDefaultFormat(ref _defaultJahresBonFormat, DataSet.Configurations.Default.JahresBonFormatId, BonLayouts.Unknown.Default_Jahr(), true); }
			set { SetDefaultFormat(ref _defaultJahresBonFormat, value, (confDefaults, id) => confDefaults.JahresBonFormatId = id); }
		}


		/// <summary>returns a collection with only print formats</summary>
		public ContractCollection<OutputFormat> PrintFormate => _printFormate ?? (_printFormate = CreateContractCollection(format => format.BonLayout.IsPrintLayout()));
		/// <summary>returns a collection with only mail formats</summary>
		public ContractCollection<OutputFormat> MailFormate => _mailFormate ?? (_mailFormate = CreateContractCollection(format => format.BonLayout.IsMailLayout()));
		/// <summary>returns a collection with only Storno formats</summary>
		public ContractCollection<OutputFormat> StornoFormate => _stornoFormate ?? (_stornoFormate = CreateContractCollection(format => format.BonLayout.IsStornoLayout()));
		/// <summary>returns a collection with only Tagesbon formats</summary>
		public ContractCollection<OutputFormat> TagesBonFormate => _tagesBonFormate ?? (_tagesBonFormate = CreateContractCollection(format => format.BonLayout.IsTagesBonLayout()));
		/// <summary>returns a collection with only Monatsbon formats</summary>
		public ContractCollection<OutputFormat> MonatsBonFormate => _monatsBonFormate ?? (_monatsBonFormate = CreateContractCollection(format => format.BonLayout.IsMonatsBonLayout()));
		/// <summary>returns a collection with only Jahresbon formats</summary>
		public ContractCollection<OutputFormat> JahresBonFormate => _jahresBonFormate ?? (_jahresBonFormate = CreateContractCollection(format => format.BonLayout.IsJahresBonLayout()));


		private OutputFormat GetDefaultFormat(ref OutputFormat field, Guid id, BonLayouts layout, bool makeInchangeable = false)
		{
			if (field != null && field.Id == id) return field;
			field = HasBeenLoaded ? Find(id) : FindOrLoad(id);
			if (field != null) return field;

			field = NewRow();
			field.Id = id;
			field.CreationDate = DateTime.Now;
			if (makeInchangeable)
				field.LastUsedDate = field.CreationDate; //Makes it unchangeable
			field.Name = $"{layout.GetName()} Vorlage";
			field.BonLayout = layout;
			field.Table.Add(field);
			return field;
		}

		private bool SetDefaultFormat(ref OutputFormat field, OutputFormat value, Action<ConfigurationsTableDefaults, Guid> setDbConfigAction)
		{
			if (Equals(field, value))
				return false;

			var oldValue = field;
			field = value;
			setDbConfigAction(DataSet.Configurations.Default, field?.Id ?? Guid.NewGuid());
			oldValue?.RaisePropertyChanged(nameof(OutputFormat.IsDefault));
			field?.RaisePropertyChanged(nameof(OutputFormat.IsDefault));
			return true;
		}
	}
}