// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Collections.Generic;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class CommandLine_NewCashBookEntrySetting : Base, IConfigNewCashBookEntrySettings
	{
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";
		private const string ParamPrefix = "NCE";
		private static CommandLine_NewCashBookEntrySetting _instance;
		private static readonly object SingletonLock = new object();





		/// <summary>Returns the singleton instance</summary>
		internal static CommandLine_NewCashBookEntrySetting I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CommandLine_NewCashBookEntrySetting());
				}
			}
		}
		private string _belegAussteller;
		private string _belegText;
		private decimal _betragBrutto;
		private string _interneBeschreibung;
		private string _interneEmpfängerId;
		private string _internEmpfänger;
		private string _leistungsBeschreibung;
		private decimal _steuersatz;
		private string _steuersatzArt;

		private CommandLine_NewCashBookEntrySetting()
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


		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegAusteller</c>]</summary>
		public string BelegAussteller
		{
			get { return _belegAussteller; }
			set { SetProperty(ref _belegAussteller, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BetragBrutto</c>]</summary>
		public decimal BetragBrutto
		{
			get { return _betragBrutto; }
			set { SetProperty(ref _betragBrutto, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Steuersatz</c>]</summary>
		public decimal Steuersatz
		{
			get { return _steuersatz; }
			set { SetProperty(ref _steuersatz, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>SteuersatzArt</c>]</summary>
		public string SteuersatzArt
		{
			get { return _steuersatzArt; }
			set { SetProperty(ref _steuersatzArt, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LeistungsBeschreibung</c>]</summary>
		public string LeistungsBeschreibung
		{
			get { return _leistungsBeschreibung; }
			set { SetProperty(ref _leistungsBeschreibung, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegText</c>]</summary>
		public string BelegText
		{
			get { return _belegText; }
			set { SetProperty(ref _belegText, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternEmpfänger</c>]</summary>
		public string InternEmpfänger
		{
			get { return _internEmpfänger; }
			set { SetProperty(ref _internEmpfänger, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneEmpfängerId</c>]</summary>
		public string InterneEmpfängerId
		{
			get { return _interneEmpfängerId; }
			set { SetProperty(ref _interneEmpfängerId, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneBeschreibung</c>]</summary>
		public string InterneBeschreibung
		{
			get { return _interneBeschreibung; }
			set { SetProperty(ref _interneBeschreibung, value); }
		}
		#endregion


		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		public void Interpret(List<string> commands)
		{
			foreach (var value in commands.ToArray())
			{
				var found = false;
				if (TryParse(value, nameof(BelegAussteller), cont => BelegAussteller = cont))
					found = true;
				else if (TryParse(value, nameof(BetragBrutto), cont => BetragBrutto = Convert.ToDecimal(cont)))
					found = true;
				else if (TryParse(value, nameof(Steuersatz), cont => Steuersatz = Convert.ToDecimal(cont)))
					found = true;
				else if (TryParse(value, nameof(SteuersatzArt), cont => SteuersatzArt = cont))
					found = true;
				else if (TryParse(value, nameof(LeistungsBeschreibung), cont => LeistungsBeschreibung = cont))
					found = true;
				else if (TryParse(value, nameof(BelegText), cont => BelegText = cont))
					found = true;
				else if (TryParse(value, nameof(InternEmpfänger), cont => InternEmpfänger = cont))
					found = true;
				else if (TryParse(value, nameof(InterneEmpfängerId), cont => InterneEmpfängerId = cont))
					found = true;
				else if (TryParse(value, nameof(InterneBeschreibung), cont => InterneBeschreibung = cont))
					found = true;
				else if (TryParse(value, nameof(InterneBeschreibung), cont => InterneBeschreibung = cont))
					found = true;

				if (found)
					commands.Remove(value);
			}
		}

		private bool TryParse(string value, string name, Action<string> set)
		{
			if (!IsParameter(value, name))
				return false;
			set(ParseValue(value, name));
			return true;
		}

		private bool IsParameter(string value, string name)
		{
			return value.StartsWith(ParamPrefix + name, StringComparison.OrdinalIgnoreCase);
		}

		private string ParseValue(string value, string name)
		{
			return value.Substring((ParamPrefix + name).Length + 1);
		}
	}
}