// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.IO;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






// ReSharper disable InconsistentNaming

namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_NewCashBookEntry : ConfigFileBase, IConfigNewCashBookEntrySettings
	{
		private static ConfigFile_NewCashBookEntry _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";


		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_NewCashBookEntry I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_NewCashBookEntry(CsGlobal.Storage.Private.GetFilePathByName("NewCashBookSettings")));
				}
			}
		}
		private string _belegText;
		private decimal _betragBrutto;
		private string _interneBeschreibung;
		private string _interneEmpfängerId;
		private string _internEmpfänger;
		private string _kassenOperator;
		private string _leistungsBeschreibung;
		private decimal _steuersatz;
		private string _typName;


		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewCashBookEntry(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewCashBookEntry(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>]</summary>
		public Guid Id
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>KassenId</c>]</summary>
		public int KassenId
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Datum</c>]</summary>
		public DateTime Datum
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>UmsatzZähler</c>]</summary>
		public decimal UmsatzZähler
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ZuletztGeändert</c>]</summary>
		public DateTime ZuletztGeändert
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegNummer</c>]</summary>
		public int BelegNummer
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}





		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TypName</c>]</summary>
		[Key]
		public string TypName
		{
			get { return _typName; }
			set
			{
				if (SetProperty(ref _typName, value))
					RaisePropertyChange(this, nameof(Typ));
			}
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BetragBrutto</c>]</summary>
		[Key]
		public decimal BetragBrutto
		{
			get { return _betragBrutto; }
			set
			{
				if (SetProperty(ref _betragBrutto, value))
					RaisePropertyChange(this, nameof(BetragNetto));
			}
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Steuersatz</c>]</summary>
		[Key]
		public decimal Steuersatz
		{
			get { return _steuersatz; }
			set
			{
				if (SetProperty(ref _steuersatz, value))
					RaisePropertyChange(this, nameof(BetragNetto));
			}
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LeistungsBeschreibung</c>]</summary>
		public string LeistungsBeschreibung
		{
			get { return _leistungsBeschreibung; }
			set { SetProperty(ref _leistungsBeschreibung, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegText</c>]</summary>
		[Key]
		public string BelegText
		{
			get { return _belegText; }
			set { SetProperty(ref _belegText, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternEmpfänger</c>]</summary>
		[Key]
		public string InternEmpfänger
		{
			get { return _internEmpfänger; }
			set { SetProperty(ref _internEmpfänger, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneEmpfängerId</c>]</summary>
		[Key]
		public string InterneEmpfängerId
		{
			get { return _interneEmpfängerId; }
			set { SetProperty(ref _interneEmpfängerId, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneBeschreibung</c>]</summary>
		[Key]
		public string InterneBeschreibung
		{
			get { return _interneBeschreibung; }
			set { SetProperty(ref _interneBeschreibung, value); }
		}


		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>KassenOperator</c>]</summary>
		[Key]
		public string KassenOperator
		{
			get { return _kassenOperator; }
			set { SetProperty(ref _kassenOperator, value); }
		}
		#endregion


		/// <summary>Gets or sets the BetragNetto.</summary>
		public decimal BetragNetto
		{
			get { return BetragBrutto/(1 + Steuersatz/100); }
			set { BetragBrutto = value*(1 + Steuersatz/100); }
		}
		/// <summary>The wrapper property for column property <see cref="TypName" />.</summary>
		public CashBookEntryTypes Typ
		{
			get
			{
				CashBookEntryTypes val;
				if (Enum.TryParse(TypName, true, out val))
					return val;
				return CashBookEntryTypes.Unknown;
			}
			set { TypName = value.ToString(); }
		}
	}
}