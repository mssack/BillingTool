// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-15</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.configuration.control
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	// ReSharper disable once InconsistentNaming
	public class Control_BelegPostenTemplate : Base
	{
		/// <summary>Parsing <see cref="Regex" />.</summary>
		private static readonly Regex ParsingRegex = new Regex("([a-zA-Z].*?)[ \\r\\n]*=[ \\r\\n]*(.*?)[ \\r\\n]*?([;}]|$)");
		private static Dictionary<string, PropertyInfo> _reflectedProperties;
		private static Dictionary<string, PropertyInfo> ReflectedProperties
		{
			get
			{
				if (_reflectedProperties != null)
					return _reflectedProperties;
				_reflectedProperties = typeof (Control_BelegPostenTemplate).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(x => x.Name, x => x);
				return _reflectedProperties;
			}
		}
		private int _anzahl;
		private decimal _betragBrutto;
		private string _command;
		private string _name;
		private decimal _steuer;


		/// <summary>ctor</summary>
		public Control_BelegPostenTemplate(string controlCommand)
		{
			InterpretFrom(controlCommand);
		}

		/// <summary>Name for the <see cref="Posten.Name" />.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Amount for the <see cref="Posten.PreisBrutto" />.</summary>
		public decimal BetragBrutto
		{
			get { return _betragBrutto; }
			set { SetProperty(ref _betragBrutto, value); }
		}
		/// <summary>Value for the <see cref="Steuersatz.Percent" />.</summary>
		public decimal Steuer
		{
			get { return _steuer; }
			set { SetProperty(ref _steuer, value); }
		}
		/// <summary>Value for the <see cref="BelegPosten.Anzahl" />.</summary>
		public int Anzahl
		{
			get { return _anzahl; }
			set { SetProperty(ref _anzahl, value); }
		}


		private void InterpretFrom(string command)
		{
			_command = command;
			// find all parameter inside {...}: Parameters look like: 'Name' = 'Value',
			ParsingRegex.Matches(command).OfType<Match>().ToArray().ForEach(x => SetProperty(x.Groups[1].Value, x.Groups[2].Value));
			SanityCheck();
		}

		private void SanityCheck()
		{
			if (string.IsNullOrEmpty(Name))
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Postens)}] muss ein Posten einen [{nameof(Name)}] enthalten. Überprüfen Sie '{_command}'");
		}

		private void SetProperty(string name, string value)
		{
			PropertyInfo target;
			if (!ReflectedProperties.TryGetValue(name, out target))
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Postens)}] enthält ein Posten ein ungültiges Argument ({name}). Überprüfen Sie '{_command}'");

			object typedValue;
			try
			{
				if (target.PropertyType == typeof(decimal))
					value = value.Replace('.', ',');
				typedValue = Convert.ChangeType(value, target.PropertyType);
			}
			catch (Exception exc)
			{
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Postens)}] enthält ein Posten ein ungültigen Typ bei dem Argument [{name}]. Der Typ des Arguments muss [{target.PropertyType.Name}] sein. Überprüfen Sie '{_command}'", exc);
			}
			target.SetValue(this, typedValue, null);
		}
	}
}