// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class OutputFormatsTable
	{
		private OutputFormat _defaultMailFormat;
		private OutputFormat _defaultPrintFormat;
		private OutputFormat _defaultStornoFormat;
		private OutputFormat _defaultTagesBonFormat;
		private OutputFormat _defaultMonatsBonFormat;
		private OutputFormat _defaultJahresBonFormat;
		private ContractCollection<OutputFormat> _mailOrPrintFormate;
		private ContractCollection<OutputFormat> _stornoFormate;
		private ContractCollection<OutputFormat> _monatsBonFormate;
		private ContractCollection<OutputFormat> _tagesBonFormate;
		private ContractCollection<OutputFormat> _jahresBonFormate;

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for printing.</summary>
		public OutputFormat Default_PrintFormat
		{
			get { return GetDefaultFormat(ref _defaultPrintFormat, DataSet.Configurations.Default.PrintOutputFormatId, BonLayouts.Unknown.Default_Print()); }
			set
			{
				if (SetProperty(ref _defaultPrintFormat, value))
					DataSet.Configurations.Default.PrintOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for mailing.</summary>
		public OutputFormat Default_MailFormat
		{
			get { return GetDefaultFormat(ref _defaultMailFormat, DataSet.Configurations.Default.MailOutputFormatId, BonLayouts.Unknown.Default_Mail()); }
			set
			{
				if (SetProperty(ref _defaultMailFormat, value))
					DataSet.Configurations.Default.MailOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for Storno.</summary>
		public OutputFormat Default_StornoFormat
		{
			get { return GetDefaultFormat(ref _defaultStornoFormat, DataSet.Configurations.Default.StornoOutputFormatId, BonLayouts.Unknown.Default_Storno()); }
			set
			{
				if (SetProperty(ref _defaultStornoFormat, value))
					DataSet.Configurations.Default.StornoOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Tagesbon.</summary>
		public OutputFormat Default_TagesBonFormat
		{
			get { return GetDefaultFormat(ref _defaultTagesBonFormat, DataSet.Configurations.Default.TagesBonOutputFormatId, BonLayouts.Unknown.Default_Tag()); }
			set
			{
				if (SetProperty(ref _defaultTagesBonFormat, value))
					DataSet.Configurations.Default.TagesBonOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Monatsbon.</summary>
		public OutputFormat Default_MonatsBonFormat
		{
			get { return GetDefaultFormat(ref _defaultMonatsBonFormat, DataSet.Configurations.Default.MonatsBonOutputFormatId, BonLayouts.Unknown.Default_Monat()); }
			set
			{
				if (SetProperty(ref _defaultMonatsBonFormat, value))
					DataSet.Configurations.Default.MonatsBonOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for the Jahresbon.</summary>
		public OutputFormat Default_JahresBonFormat
		{
			get { return GetDefaultFormat(ref _defaultJahresBonFormat, DataSet.Configurations.Default.JahresBonOutputFormatId, BonLayouts.Unknown.Default_Jahr()); }
			set
			{
				if (SetProperty(ref _defaultJahresBonFormat, value))
					DataSet.Configurations.Default.JahresBonOutputFormatId = value?.Id ?? Guid.NewGuid();
			}
		}


		/// <summary>returns a collection with only Storno formats</summary>
		public ContractCollection<OutputFormat> StornoFormate
		{
			get { return _stornoFormate ?? (_stornoFormate = CreateContractCollection(format => format.BonLayout.IsStornoLayout())); }
		}
		/// <summary>returns a collection with only Tagesbon formats</summary>
		public ContractCollection<OutputFormat> TagesBonFormate
		{
			get { return _tagesBonFormate ?? (_tagesBonFormate = CreateContractCollection(format => format.BonLayout.IsTagesBonLayout())); }
		}
		/// <summary>returns a collection with only Monatsbon formats</summary>
		public ContractCollection<OutputFormat> MonatsBonFormate
		{
			get { return _monatsBonFormate ?? (_monatsBonFormate = CreateContractCollection(format => format.BonLayout.IsMonatsBonLayout())); }
		}
		/// <summary>returns a collection with only Jahresbon formats</summary>
		public ContractCollection<OutputFormat> JahresBonFormate
		{
			get { return _jahresBonFormate ?? (_jahresBonFormate = CreateContractCollection(format => format.BonLayout.IsJahresBonLayout())); }
		}
		/// <summary>returns a collection with only non-Storno formats</summary>
		public ContractCollection<OutputFormat> MailOrPrintFormate
		{
			get { return _mailOrPrintFormate ?? (_mailOrPrintFormate = CreateContractCollection(format => format.BonLayout.IsPrintLayout() || format.BonLayout.IsMailLayout())); }
		}
		

		private OutputFormat GetDefaultFormat(ref OutputFormat field, Guid id, BonLayouts layout)
		{
			if (field != null) return field;
			field = HasBeenLoaded ? Find(id) : FindOrLoad(id);
			if (field != null) return field;

			field = NewRow();
			field.Id = id;
			field.CreationDate = DateTime.Now;
			field.LastUsedDate = field.CreationDate; //Makes it unchangeable
			field.Name = $"{layout.GetName()} Vorlage";
			field.BonLayout = layout;
			field.Table.Add(field);
			return field;
		}
	}
}