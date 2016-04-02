// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.IO;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope.configuration._interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






// ReSharper disable InconsistentNaming

namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_NewBelegData : ConfigFileBase, IConfig_NewBelegData
	{
		private static ConfigFile_NewBelegData _instance;
		private static readonly object SingletonLock = new object();
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";


		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_NewBelegData I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_NewBelegData(CsGlobal.Storage.Private.GetFilePathByName("NewBelegData")));
				}
			}
		}
		private string _zusatzText;
		private string _empfänger;
		private string _empfängerId;
		private string _kassenOperator;
		private string _kommentar;
		private bool _printBeleg;
		private bool _sendBeleg;
		private string _sendBelegTarget;
		private string _typName;


		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewBelegData(FileInfo path) : base(path)
		{
			Load();
			CsGlobal.App.OnExit += args => Save();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_NewBelegData(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Id</c>]</summary>
		public Guid Id
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
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Nummer</c>]</summary>
		public int Nummer
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZuletztGeändert</c>]</summary>
		public DateTime ZuletztGeändert
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
		/// <summary>!!!!NOT EDITABLE - Generated property!!!!     [<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragBrutto</c>]</summary>
		public decimal BetragBrutto
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
		public string TypName
		{
			get { return _typName; }
			set
			{
				if (SetProperty(ref _typName, value))
					OnPropertyChanged(nameof(Typ));
			}
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>]</summary>
		public string KassenOperator
		{
			get { return _kassenOperator; }
			set { SetProperty(ref _kassenOperator, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BelegText</c>]</summary>
		public string ZusatzText
		{
			get { return _zusatzText; }
			set { SetProperty(ref _zusatzText, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Empfänger</c>]</summary>
		public string Empfänger
		{
			get { return _empfänger; }
			set { SetProperty(ref _empfänger, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>EmpfängerId</c>]</summary>
		public string EmpfängerId
		{
			get { return _empfängerId; }
			set { SetProperty(ref _empfängerId, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Kommentar</c>]</summary>
		public string Kommentar
		{
			get { return _kommentar; }
			set { SetProperty(ref _kommentar, value); }
		}
		/// <summary>If true a new <see cref="PrintedBeleg" /> will be created and printed to a printer.</summary>
		public bool PrintBeleg
		{
			get { return _printBeleg; }
			set { SetProperty(ref _printBeleg, value); }
		}
		/// <summary>
		///     If true a new <see cref="MailedBeleg" /> will be created and sent per mail to the mail address specified in
		///     <see cref="IConfig_NewBelegData.SendBelegTarget" />.
		/// </summary>
		public bool SendBeleg
		{
			get { return _sendBeleg; }
			set { SetProperty(ref _sendBeleg, value); }
		}
		/// <summary>The mail target if <see cref="IConfig_NewBelegData.SendBeleg" /> is true.</summary>
		public string SendBelegTarget
		{
			get { return _sendBelegTarget; }
			set { SetProperty(ref _sendBelegTarget, value); }
		}
		#endregion


		/// <summary>The wrapper property for column property <see cref="TypName" />.</summary>
		public BelegDataTypes Typ
		{
			get
			{
				BelegDataTypes val;
				if (Enum.TryParse(TypName, true, out val))
					return val;
				return BelegDataTypes.Unknown;
			}
			set { TypName = value.ToString(); }
		}
	}
}