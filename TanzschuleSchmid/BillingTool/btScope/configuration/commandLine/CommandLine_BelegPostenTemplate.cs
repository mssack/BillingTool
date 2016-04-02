// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.configuration.commandLine
{
	/// <summary>Used for the command line interpretation of the different <see cref="BelegPosten" /> for an <see cref="BelegData" />.</summary>
	// ReSharper disable once InconsistentNaming
	public class CommandLine_BelegPostenTemplate : Base
	{
		/// <summary>Parsing <see cref="Regex"/>.</summary>
		private static readonly Regex ParsingRegex = new Regex("([a-zA-Z].*?)[ \\r\\n]*=[ \\r\\n]*(.*?)[ \\r\\n]*?([,}]|$)");
		private static Dictionary<string, PropertyInfo> _reflectedProperties;
		private static Dictionary<string, PropertyInfo> ReflectedProperties
		{
			get
			{
				if (_reflectedProperties != null)
					return _reflectedProperties;
				_reflectedProperties = typeof (CommandLine_BelegPostenTemplate).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(x => x.Name, x => x);
				return _reflectedProperties;
			}
		}
		private int _anzahl;
		private string _name;
		private string _command;
		private decimal _betragBrutto;
		private decimal _steuer;


		/// <summary>ctor</summary>
		public CommandLine_BelegPostenTemplate(string command)
		{
			InterpretFrom(command);
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
				throw new Exception($"{nameof(Name)} cannot be empty. Please validate '{_command}'");
		}

		private void SetProperty(string name, string value)
		{
			PropertyInfo target;
			if (!ReflectedProperties.TryGetValue(name, out target))
				throw new KeyNotFoundException($"The parameter '{name}' is not recognizable. Please validate argument '{_command}'");

			object typedValue;
			try
			{
				typedValue = Convert.ChangeType(value, target.PropertyType);
			}
			catch (Exception)
			{
				throw new Exception($"The parameter '{name}' has to be of type {target.PropertyType.Name}. Please validate argument '{_command}'");
			}
			target.SetValue(this, typedValue, null);
		}
	}
}