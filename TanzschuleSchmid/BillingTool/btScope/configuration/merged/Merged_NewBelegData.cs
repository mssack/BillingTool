// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.merged
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class Merged_NewBelegData : Base, IConfig_NewBelegData
	{
		private static Merged_NewBelegData _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this merged property.";
		private const string SetErrorMessage = "You cannot set merged property's.";

		/// <summary>Returns the singleton instance</summary>
		internal static Merged_NewBelegData I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Merged_NewBelegData());
				}
			}
		}


		/// <summary>Try to get the value from command line configuration. If command line configuration is default use the value from configuration file.</summary>
		private static T GetMergedValue<T>(Func<IConfig_NewBelegData, T> get)
		{
			var value = get(Bt.Config.CommandLine.NewBelegData);
			if (value != null && !Equals(default(T), value))
				return value;
			return get(Bt.Config.File.NewBelegData);
		}

		private Merged_NewBelegData()
		{
		}


		#region Overrides/Interfaces
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Id</c>]</summary>
		public Guid Id
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StateNumber</c>]</summary>
		public int StateNumber
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenId</c>]</summary>
		public string KassenId
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Datum</c>]</summary>
		public DateTime Datum
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>UmsatzZähler</c>]</summary>
		public decimal UmsatzZähler
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StornoBelegId</c>]</summary>
		public Guid? StornoBelegId
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerVon</c>]</summary>
		public int? BonNummerVon
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerBis</c>]</summary>
		public int? BonNummerBis
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Nummer</c>]</summary>
		public int Nummer
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragBrutto</c>]</summary>
		public decimal BetragBrutto
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZuletztGeändert</c>]</summary>
		public DateTime? CommentLastChanged
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>PrintCount</c>]</summary>
		public int PrintCount
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>MailCount</c>]</summary>
		public int MailCount
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragNetto</c>]</summary>
		public decimal BetragNetto
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}



		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>TypName</c>]</summary>
		public int TypNumber
		{
			get { return GetMergedValue(setting => setting.TypNumber); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>]</summary>
		public string KassenOperator
		{
			get { return GetMergedValue(setting => setting.KassenOperator); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		///	<summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZahlungsReferenz</c>]</summary>
		public string ZahlungsReferenz
		{
			get { return GetMergedValue(setting => setting.ZahlungsReferenz); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Comment</c>]</summary>
		public string Comment
		{
			get { return GetMergedValue(setting => setting.Comment); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZusatzText</c>]</summary>
		public string ZusatzText
		{
			get { return GetMergedValue(setting => setting.ZusatzText); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Empfänger</c>]</summary>
		public string Empfänger
		{
			get { return GetMergedValue(setting => setting.Empfänger); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>EmpfängerId</c>]</summary>
		public string EmpfängerId
		{
			get { return GetMergedValue(setting => setting.EmpfängerId); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>If true a new <see cref="PrintedBeleg" /> will be created and printed to a printer.</summary>
		public bool PrintBeleg
		{
			get { return GetMergedValue(setting => setting.PrintBeleg); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>The mail targets.</summary>
		public string[] SendBelegTargets
		{
			get { return GetMergedValue(setting => setting.SendBelegTargets); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		#endregion
	}
}