// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.merged
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class Merged_NewCashBookEntry : Base, IConfigNewCashBookEntrySettings
	{
		private static Merged_NewCashBookEntry _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this merged property.";
		private const string SetErrorMessage = "You cannot set merged property's.";

		/// <summary>Returns the singleton instance</summary>
		internal static Merged_NewCashBookEntry I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Merged_NewCashBookEntry());
				}
			}
		}

		private Merged_NewCashBookEntry()
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
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegNummer</c>]</summary>
		public int BelegNummer
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


		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Typ</c>]</summary>
		public string TypName
		{
			get { return GetMergedValue(setting => setting.TypName); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>KassenOperator</c>]</summary>
		public string KassenOperator
		{
			get { return GetMergedValue(setting => setting.KassenOperator); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BetragBrutto</c>]</summary>
		public decimal BetragBrutto
		{
			get { return GetMergedValue(setting => setting.BetragBrutto); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Steuersatz</c>]</summary>
		public decimal Steuersatz
		{
			get { return GetMergedValue(setting => setting.Steuersatz); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LeistungsBeschreibung</c>]</summary>
		public string LeistungsBeschreibung
		{
			get { return GetMergedValue(setting => setting.LeistungsBeschreibung); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegText</c>]</summary>
		public string BelegText
		{
			get { return GetMergedValue(setting => setting.BelegText); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternEmpfänger</c>]</summary>
		public string InternEmpfänger
		{
			get { return GetMergedValue(setting => setting.InternEmpfänger); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneEmpfängerId</c>]</summary>
		public string InterneEmpfängerId
		{
			get { return GetMergedValue(setting => setting.InterneEmpfängerId); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneBeschreibung</c>]</summary>
		public string InterneBeschreibung
		{
			get { return GetMergedValue(setting => setting.InterneBeschreibung); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		#endregion


		/// <summary>Try to get the value from command line configuration. If command line configuration is default use the value from configuration file.</summary>
		private T GetMergedValue<T>(Func<IConfigNewCashBookEntrySettings, T> get)
		{
			var value = get(Bt.Config.CommandLine.NewCashBookEntry);
			if (value != null && !Equals(default(T), value))
				return value;
			return get(Bt.Config.File.NewCashBookEntry);
		}
	}
}