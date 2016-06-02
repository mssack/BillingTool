// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-02</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using BillingTool.enumerations;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






//This class will be exported to the release candidate.

namespace BillingToolGitControl.codeSamples
{
	/// <summary>used to interact with the BillingTool.</summary>
	public class BillingToolStarter
	{

		private readonly string _filePath;
		private readonly string _kassenoperator;

		/// <summary>used for generation. Do not use in Production.</summary>
		public BillingToolStarter(string kassenoperator)
		{
			_kassenoperator = kassenoperator;
		}

		/// <summary>Creates a new BillingTool Interaction.</summary>
		/// <param name="filePath">The file path to the executable.</param>
		/// <param name="kassenoperator">The operator of the Kassa.</param>
		public BillingToolStarter(string filePath, string kassenoperator)
		{
			if (kassenoperator == null)
				throw new ArgumentException($"Es muss ein {nameof(kassenoperator)} angegeben werden.");
			if (!File.Exists(filePath))
				throw new ArgumentException($"Der übergebene File existiert nicht ['{filePath}']");

			_filePath = filePath;
			_kassenoperator = kassenoperator;
		}


		#region Abstract
		public interface IValidateable
		{
			#region Abstract
			bool Validate(bool throwError);
			#endregion
		}
		#endregion


		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public ExitCodes BelegDataApproval(BelegData data)
		{
			return OpenProcess(Cmd_BelegDataApproval(data));
		}

		/// <summary>Used to open the options.</summary>
		public ExitCodes Options()
		{
			return OpenProcess(Cmd_Options());
		}

		/// <summary>Used to create a Monatsbon.</summary>
		public ExitCodes SilentMonatsBonPrint()
		{
			return OpenProcess(Cmd_SilentMonatsBonPrint());
		}

		/// <summary>Used for Storno, viewing, manual creation and others.</summary>
		public ExitCodes BelegDataViewer()
		{
			return OpenProcess(Cmd_BelegDataViewer());
		}

		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public string Cmd_BelegDataApproval(BelegData data)
		{
			return GetCommand(StartupModes.BelegDataApprove, data.ToString());
		}

		/// <summary>Used to open the options.</summary>
		public string Cmd_Options()
		{
			return GetCommand(StartupModes.Options);
		}

		/// <summary>Used to create a Monatsbon.</summary>
		public string Cmd_SilentMonatsBonPrint()
		{
			return GetCommand(StartupModes.SilentMonatsBonPrint);
		}

		/// <summary>Used for Storno, viewing, manual creation and others.</summary>
		public string Cmd_BelegDataViewer()
		{
			return GetCommand(StartupModes.BelegDataViewer);
		}



		private string GetCommand(StartupModes mode, string args = null)
		{
			var fullArgument = $"/{mode} /NceKassenOperator {_kassenoperator}";
			if (!string.IsNullOrEmpty(args))
				fullArgument = fullArgument + " " + args;
			return fullArgument;
		}

		private ExitCodes OpenProcess(string args)
		{
			if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
				throw new ArgumentException($"Der angegebene file existiert nicht ['{_filePath}']");

			using (var process = Process.Start(_filePath, args))
			{
				process.WaitForExit();
				return (ExitCodes) process.ExitCode;
			}
		}



		/// <summary>used for the Bon approval.</summary>
		public class BelegData : IValidateable
		{
			#region Overrides/Interfaces
			public override string ToString()
			{
				return Helper.Get_RootItem_String(this);
			}
			#endregion


			/// <summary>The type of the transaction.</summary>
			public BelegDataTypes TypNumber { get; set; }
			/// <summary>The receiver of the Bon.</summary>
			public string Empfänger { get; set; }
			/// <summary>The ID of the receiver of the Bon.</summary>
			public string EmpfängerId { get; set; }
			/// <summary>Some text which is printed onto the Bon.</summary>
			public string ZusatzText { get; set; }
			/// <summary>A system internal string which could be a GUID for further references.</summary>
			public string ZahlungsReferenz { get; set; }
			/// <summary>A comment visible to the operator.</summary>
			public string Comment { get; set; }
			/// <summary>Should be printed to the default printer.</summary>
			public bool PrintBeleg { get; set; }
			/// <summary>The articles which are included onto the Bon.</summary>
			public IList<Posten> Postens { get; set; } = new List<Posten>();
			/// <summary>The mails which should be sended.</summary>
			public IList<Mail> Mails { get; set; } = new List<Mail>();

			public bool Validate(bool throwError = false)
			{
				var state = true;
				if (Postens != null)
					foreach (var posten in Postens)
					{
						if (posten.Validate(throwError) == false)
							state = false;
					}
				if (Mails != null)
					foreach (var mail in Mails)
					{
						if (mail.Validate(throwError) == false)
							state = false;
					}
				return state;
			}
		}



		/// <summary>Describes one item on a Bon.</summary>
		public class Posten : IValidateable
		{
			/// <summary>Creates a new <see cref="Posten" />.</summary>
			public Posten()
			{

			}

			/// <summary>Creates a new <see cref="Posten" />.</summary>
			public Posten(string name, decimal betragBrutto, decimal steuer, int anzahl)
			{
				Name = name;
				BetragBrutto = betragBrutto;
				Steuer = steuer;
				Anzahl = anzahl;
			}


			#region Overrides/Interfaces
			public override string ToString()
			{
				return Helper.Get_ListItem_String(this);
			}


			public bool Validate(bool throwError = false)
			{
				string message = null;
				if (Anzahl == 0)
					message = $"Die {nameof(Anzahl)} eines {nameof(Posten)} kann nicht 0 sein.";
				else if (BetragBrutto == 0)
					message = $"Die {nameof(BetragBrutto)} eines {nameof(Posten)} kann nicht 0 sein.";
				else if (string.IsNullOrEmpty(Name))
					message = $"Der {nameof(Name)} des {nameof(Posten)} kann nicht leer sein.";

				if (throwError && message != null)
					throw new ArgumentException(message);

				return message == null;
			}
			#endregion


			/// <summary>The name of the <see cref="Posten" />.</summary>
			public string Name { get; set; }
			/// <summary>The cost of one entity of this <see cref="Posten" />.</summary>
			public decimal BetragBrutto { get; set; }
			/// <summary>The tax which is applied to the (<see cref="BetragBrutto" />*<see cref="Anzahl" />) calculation.</summary>
			public decimal Steuer { get; set; }
			/// <summary>
			///     the quantity of this type. The quantity will be multiplied with the <see cref="BetragBrutto" /> field to get the total cost of this
			///     <see cref="Posten" />.
			/// </summary>
			public int Anzahl { get; set; }
		}



		/// <summary>Describes one mail, which will be sent on approval.</summary>
		public class Mail : IValidateable
		{
			/// <summary>ctor</summary>
			public Mail()
			{

			}

			/// <summary>ctor</summary>
			public Mail(string address, string betreff = null, string text = null, string bcc = null, string outputformat = null)
			{
				Address = address;
				Bcc = bcc;
				Betreff = betreff;
				Text = text;
				OutputFormat = outputformat;
			}


			#region Overrides/Interfaces
			public override string ToString()
			{
				return Helper.Get_ListItem_String(this);
			}


			public bool Validate(bool throwError = false)
			{
				string message = null;
				if (string.IsNullOrEmpty(Address))
					message = $"{nameof(Address)} kann nicht null sein in {nameof(Mail)}.";

				if (throwError && message != null)
					throw new ArgumentException(message);

				return message == null;
			}
			#endregion


			/// <summary>The mail address of the receiver of the mail.</summary>
			public string Address { get; set; }
			/// <summary>the blind carbon copy addresses, used to send the mail to other recipients. Different mails have to be comma separated.</summary>
			public string Bcc { get; set; }
			/// <summary>The mail subject.</summary>
			public string Betreff { get; set; }
			/// <summary>the mail text.</summary>
			public string Text { get; set; }
			/// <summary>the name of the layout which should be used for the mail, if the format does not exist the default will be chosen.</summary>
			public string OutputFormat { get; set; }
		}



		private static class Helper
		{
			public static string Get_RootItem_String(object o)
			{
				var arguments = "";
				foreach (var property in o.GetType().GetProperties())
				{
					var value = property.GetValue(o, null);
					if ((property.PropertyType.IsValueType && Equals(value, Activator.CreateInstance(property.PropertyType))) || value == null)
						continue;

					var valueText = GetTextRepresentation(property, value);
					if (valueText == null)
						continue;

					arguments = arguments + $"/Nce{property.Name} {valueText} ";
				}
				return arguments.Substring(0, arguments.Length - 1);
			}

			public static string Get_ListItem_String(object o)
			{
				var arguments = "";
				foreach (var property in o.GetType().GetProperties())
				{
					var value = property.GetValue(o, null);
					if ((property.PropertyType.IsValueType && value == Activator.CreateInstance(property.PropertyType)) || value == null)
						continue;

					var valueText = GetTextRepresentation(property, value);
					if (valueText == null)
						continue;

					arguments = arguments + $"{property.Name} = {valueText}; ";
				}

				return "{" + arguments.Substring(0, arguments.Length - 2) + "}";
			}

			private static string GetTextRepresentation(PropertyInfo property, object value)
			{
				if ((property.PropertyType.IsValueType && value == Activator.CreateInstance(property.PropertyType)) || value == null)
					return null;
				if (property.PropertyType == typeof(decimal))
					return ((decimal) value).ToString("0.00");

				if (property.PropertyType.IsEnum)
					return ((int) value).ToString();

				if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
				{
					var count = 0;
					var valueText = "{ ";
					foreach (var obj in (IEnumerable) value)
					{
						if (obj is IValidateable && ((IValidateable) obj).Validate(false) == false)
							continue;

						count++;
						valueText = valueText + obj + ", ";
					}
					if (count == 0)
						return null;

					return valueText.Substring(0, valueText.Length - 2) + " }";

				}
				return value.ToString();
			}
		}
	}
}