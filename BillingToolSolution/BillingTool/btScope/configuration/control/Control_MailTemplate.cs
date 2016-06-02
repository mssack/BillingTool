// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.configuration.control
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	// ReSharper disable once InconsistentNaming
	public class Control_MailTemplate : Base
	{
		/// <summary>Parsing <see cref="Regex" />.</summary>
		private static readonly Regex ParsingRegex = new Regex("([a-zA-Z].*?)[ \\r\\n]*=[ \\r\\n]*(.*?)[ \\r\\n]*?([;}]|$)", RegexOptions.Singleline);
		private static Dictionary<string, PropertyInfo> _reflectedProperties;
		private static Dictionary<string, PropertyInfo> ReflectedProperties
		{
			get
			{
				if (_reflectedProperties != null)
					return _reflectedProperties;
				_reflectedProperties = typeof(Control_MailTemplate).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(x => x.Name.ToLower(), x => x);
				return _reflectedProperties;
			}
		}

		private string _address;
		private string _betreff;
		private string _command;
		private string _text;
		private string _outputFormat;
		private string _bcc;


		/// <summary>ctor</summary>
		public Control_MailTemplate(string controlCommand)
		{
			InterpretFrom(controlCommand);
		}

		/// <summary>Gets or sets the Address.</summary>
		public string Address
		{
			get { return _address; }
			set { SetProperty(ref _address, value); }
		}
		///<summary>Gets or sets the Bcc.</summary>
		public string Bcc
		{
			get { return _bcc; }
			set { SetProperty(ref _bcc, value); }
		}
		/// <summary>Gets or sets the Betreff.</summary>
		public string Betreff
		{
			get { return _betreff; }
			set { SetProperty(ref _betreff, value); }
		}
		/// <summary>Gets or sets the Text.</summary>
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}
		/// <summary>Gets or sets the OutputFormat.</summary>
		public string OutputFormat
		{
			get { return _outputFormat; }
			set { SetProperty(ref _outputFormat, value); }
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
			if (string.IsNullOrEmpty(Address))
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Mails)}] muss eine Mail zumindest [{nameof(Address)}] enthalten. Überprüfen Sie '{_command}'");
		}

		private void SetProperty(string name, string value)
		{
			PropertyInfo target;
			if (!ReflectedProperties.TryGetValue(name.ToLower(), out target))
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Mails)}] enthält eine Mail ein ungültiges Argument ({name}). Überprüfen Sie '{_command}'");

			object typedValue;
			try
			{
				if (target.PropertyType == typeof(decimal))
					value = value.Replace('.', ',');
				typedValue = Convert.ChangeType(value, target.PropertyType);
			}
			catch (Exception exc)
			{
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Bei dem Parameter[{nameof(Control_NewBelegData.Mails)}] enthält eine Mail ein ungültigen Typ bei dem Argument [{name}]. Der Typ des Arguments muss [{target.PropertyType.Name}] sein. Überprüfen Sie '{_command}'", exc);
			}
			target.SetValue(this, typedValue, null);
		}
	}
}