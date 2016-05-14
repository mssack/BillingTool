﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
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
		private ContractCollection<OutputFormat> _nonStornoFormate;
		private ContractCollection<OutputFormat> _stornoFormate;

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for printing.</summary>
		public OutputFormat Default_PrintFormat
		{
			get
			{
				if (_defaultPrintFormat != null) return _defaultPrintFormat;

				_defaultPrintFormat = LoadThenFind(DataSet.Configurations.Default_PrintOutputFormat);
				if (_defaultPrintFormat != null) return _defaultPrintFormat;

				_defaultPrintFormat = NewRow();
				_defaultPrintFormat.Id = DataSet.Configurations.Default_PrintOutputFormat;
				_defaultPrintFormat.CreationDate = DateTime.Now;
				_defaultPrintFormat.Name = $"{BonLayouts.V1PrintBon.GetName()} Vorlage";
				_defaultPrintFormat.BonLayout = BonLayouts.V1PrintBon;
				_defaultPrintFormat.Table.Add(_defaultPrintFormat);
				return _defaultPrintFormat;
			}
			set
			{
				if (SetProperty(ref _defaultPrintFormat, value))
					DataSet.Configurations.Default_PrintOutputFormat = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for mailing.</summary>
		public OutputFormat Default_MailFormat
		{
			get
			{
				if (_defaultMailFormat != null) return _defaultMailFormat;

				_defaultMailFormat = LoadThenFind(DataSet.Configurations.Default_MailOutputFormat);
				if (_defaultMailFormat != null) return _defaultMailFormat;

				_defaultMailFormat = NewRow();
				_defaultMailFormat.Id = DataSet.Configurations.Default_MailOutputFormat;
				_defaultMailFormat.CreationDate = DateTime.Now;
				_defaultMailFormat.Name = $"{BonLayouts.V1MailBon.GetName()} Vorlage";
				_defaultMailFormat.BonLayout = BonLayouts.V1MailBon;
				_defaultMailFormat.Table.Add(_defaultMailFormat);
				return _defaultMailFormat;
			}
			set
			{
				if (SetProperty(ref _defaultMailFormat, value))
					DataSet.Configurations.Default_MailOutputFormat = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>Gets or sets the default <see cref="OutputFormat" /> which can be used for Storno.</summary>
		public OutputFormat Default_StornoFormat
		{
			get
			{
				if (_defaultStornoFormat != null) return _defaultStornoFormat;

				_defaultStornoFormat = LoadThenFind(DataSet.Configurations.Default_StornoOutputFormat);
				if (_defaultStornoFormat != null) return _defaultStornoFormat;

				_defaultStornoFormat = NewRow();
				_defaultStornoFormat.Id = DataSet.Configurations.Default_StornoOutputFormat;
				_defaultStornoFormat.CreationDate = DateTime.Now;
				_defaultStornoFormat.Name = $"{BonLayouts.V1StornoBon.GetName()} Vorlage";
				_defaultStornoFormat.BonLayout = BonLayouts.V1StornoBon;
				_defaultStornoFormat.Table.Add(_defaultStornoFormat);
				return _defaultStornoFormat;
			}
			set
			{
				if (SetProperty(ref _defaultStornoFormat, value))
					DataSet.Configurations.Default_StornoOutputFormat = value?.Id ?? Guid.NewGuid();
			}
		}

		/// <summary>returns a collection with only Storno formats</summary>
		public ContractCollection<OutputFormat> StornoFormate
		{
			get { return _stornoFormate ?? (_stornoFormate = CreateContractCollection(format => format.BonLayout.IsStornoLayout())); }
		}
		/// <summary>returns a collection with only non-Storno formats</summary>
		public ContractCollection<OutputFormat> NonStornoFormate
		{
			get { return _nonStornoFormate ?? (_nonStornoFormate = CreateContractCollection(format => !format.BonLayout.IsStornoLayout())); }
		}
	}
}