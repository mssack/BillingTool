// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using BillingTool.Exceptions;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration.control
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class Control_NewBelegData : Base, IBelegData
	{
		private const string GetErrorMessage = "You cannot get this property. This property is not use able.";
		private const string SetErrorMessage = "You cannot set this property. This property is not use able.";
		/// <summary>The prefix which will be used for each configuration part.</summary>
		private const string ParamPrefix = "NCE";
		private static Control_NewBelegData _instance;
		private static readonly object SingletonLock = new object();





		/// <summary>Returns the singleton instance</summary>
		internal static Control_NewBelegData I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Control_NewBelegData());
				}
			}
		}
		private string _comment;
		private string _empfänger;
		private string _empfängerId;
		private string _kassenOperator;
		private Control_BelegPostenTemplate[] _postens;
		private bool _printBeleg;
		private int _typNumber;
		private string _zahlungsReferenz;
		private string _zusatzText;

		private Control_NewBelegData()
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
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerVon</c>]</summary>
		public int? BonNummerVon
		{
			get { throw new InvalidOperationException(GetErrorMessage); }
			set { throw new InvalidOperationException(SetErrorMessage); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerBis</c>]</summary>
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


		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>TypNumber</c>]</summary>
		public int TypNumber
		{
			get { return _typNumber; }
			set { SetProperty(ref _typNumber, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>]</summary>
		public string KassenOperator
		{
			get { return _kassenOperator; }
			set { SetProperty(ref _kassenOperator, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZusatzText</c>]</summary>
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
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZahlungsReferenz</c>]</summary>
		public string ZahlungsReferenz
		{
			get { return _zahlungsReferenz; }
			set { SetProperty(ref _zahlungsReferenz, value); }
		}
		/// <summary>[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Comment</c>]</summary>
		public string Comment
		{
			get { return _comment; }
			set { SetProperty(ref _comment, value); }
		}
		#endregion


		/// <summary>If true a new <see cref="PrintedBeleg" /> will be created.</summary>
		public bool PrintBeleg
		{
			get { return _printBeleg; }
			private set { SetProperty(ref _printBeleg, value); }
		}


		/// <summary>The <see cref="Posten"/> are directly correlated to the created <see cref="BelegPosten"/>.</summary>
		public Control_BelegPostenTemplate[] Postens
		{
			get { return _postens; }
			private set { SetProperty(ref _postens, value); }
		}
		private Control_MailTemplate[] _mails;
		///<summary>Gets or sets the Mails.</summary>
		public Control_MailTemplate[] Mails
		{
			get { return _mails; }
			set { SetProperty(ref _mails, value); }
		}

		/// <summary>DO NOT USE THIS METHOD. This method is used to interpret the commands into the current properties.</summary>
		internal void Interpret(List<string> commands)
		{
			var compareableDictionary = BelegData.NativeColumnName_To_Property.ToDictionary(x => ParamPrefix.ToLower() + x.Key.Replace(" ", "").ToLower(), x => x.Value);

			foreach (var command in commands.ToArray())
			{
				if (string.IsNullOrEmpty(command))
					continue;

				var found = false;

				var indexOfFirtsLeerzeichen = command.IndexOf(" ", StringComparison.Ordinal);
				if (indexOfFirtsLeerzeichen == -1)
					continue;

				PropertyInfo foundProperty;
				var param = command.Substring(0, indexOfFirtsLeerzeichen).ToLower();
				var value = command.Substring(indexOfFirtsLeerzeichen + 1).Trim();

				if (compareableDictionary.TryGetValue(param, out foundProperty))
				{
					if (foundProperty.PropertyType == typeof(string))
						foundProperty.SetValue(this, value, null);
					else
						foundProperty.SetValue(this, Convert.ChangeType(value, foundProperty.PropertyType), null);
					found = true;
				}
				else if (param == $"{ParamPrefix}{nameof(PrintBeleg)}".ToLower())
					PrintBeleg = Convert.ToBoolean(value);
				else if (param == $"{ParamPrefix}{nameof(Mails)}".ToLower())
					ParseMails(nameof(Mails), value);
				else if (param == $"{ParamPrefix}{nameof(Postens)}".ToLower())
					ParsePosten(nameof(Postens), value);

				if (found)
					commands.Remove(command);
			}
		}

		private void ParseMails(string parameterName, string value)
		{
			if (value.Length < 2 || value[0] != '{' || value[value.Length - 1] != '}')
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Der parameter[{parameterName}] ist ungültig weil der Wert[{value}] falsch ist.");

			value = value.Substring(1, value.Length - 2);
			// find all {...} in {...}, {...}, {...} => if ... contains '}' escape it with '\}'
			var first = new Regex("\\{(.*?[^\\\\])\\}", RegexOptions.Singleline);

			var enumerable = first.Matches(value).OfType<Match>().Select(x => x.Groups[1].Value);
			Mails = enumerable.Select(x=> new Control_MailTemplate(x)).Where(x=>IsValidMailAddress(x.Address)).ToArray();
		}

		private void ParsePosten(string parameterName, string value)
		{
			// FOR TESTING see http://www.regextester.com/
			if (value.Length < 2 || value[0] != '{' || value[value.Length - 1] != '}')
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Der parameter[{parameterName}] ist ungültig weil der Wert[{value}] falsch ist.");

			value = value.Substring(1, value.Length - 2);
			// find all {...} in {...}, {...}, {...} => if ... contains '}' escape it with '\}'
			var first = new Regex("\\{(.*?[^\\\\])\\}", RegexOptions.Singleline) ;

			Postens = first.Matches(value).OfType<Match>().Select(x => x.Groups[1].Value).Select(x => new Control_BelegPostenTemplate(x)).ToArray();
		}
		private bool IsValidMailAddress(string emailaddress)
		{
			try
			{
				// ReSharper disable once ObjectCreationAsStatement
				new MailAddress(emailaddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

	}



}