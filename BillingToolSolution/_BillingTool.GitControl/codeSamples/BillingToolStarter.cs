// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-04</date>

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


		/// <summary>
		///     Verwendet um einen <see cref="BelegData" /> abzuwickeln. Liefert einen <see cref="ExitCodes" /> zurück der weiters interpretiert werden
		///     kann.
		/// </summary>
		public ExitCodes BelegDataApproval(BelegData data)
		{
			return OpenProcess(Cmd_BelegDataApproval(data));
		}

		/// <summary>Öffnet die Optionen.</summary>
		public ExitCodes Options()
		{
			return OpenProcess(Cmd_Options());
		}

		/// <summary>Erstellt im unsichtbaren einen neuen Monatsbon dieser wird an dem Standarddrucker ausgedruckt.</summary>
		public ExitCodes SilentMonatsBonPrint()
		{
			return OpenProcess(Cmd_SilentMonatsBonPrint());
		}

		/// <summary>Öffnet ein Fenster in dem manuell Rechnungen erstellt werden können, sowie alte Belege storniert, und bestehende angeschaut werden können.</summary>
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
			private IList<Mail> _mails;
			private IList<Posten> _postens;


			#region Overrides/Interfaces
			public override string ToString()
			{
				return Helper.Get_RootItem_String(this);
			}

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
			#endregion


			/// <summary>
			///     Die Art der Transaktion. Mögliche Werte: <see cref="BelegDataTypes.Bar" />, <see cref="BelegDataTypes.Bankomat" /> and
			///     <see cref="BelegDataTypes.Kreditkarte" />
			/// </summary>
			public BelegDataTypes TypNumber { get; set; }
			/// <summary>Der Empfänger des Bons. Dieser Wert wird nicht auf dem Bon aufgedruckt. Er ist lediglich für Dokumentationszwecke gedacht.</summary>
			public string Empfänger { get; set; }
			/// <summary>Die ID des Empfänger des Bons. Dieser Wert wird nicht auf dem Bon aufgedruckt. Er ist lediglich für Dokumentationszwecke gedacht.</summary>
			public string EmpfängerId { get; set; }
			/// <summary>Zusätzlicher Text der ans Ende des Bons angehängt wird. Kann auch Mehrzeilig sein.</summary>
			public string ZusatzText { get; set; }
			/// <summary>Die Zahlungsreferenz entspricht einer ID um Transaktion mit anderen Datenbanken in Beziehung zu stellen.</summary>
			public string ZahlungsReferenz { get; set; }
			/// <summary>Ein Kommentar der ausschließlich in der Datenbank und dem Kassenoperator sichtbar ist.</summary>
			public string Comment { get; set; }
			/// <summary>Wenn True, dann wird dieser Bon ausgedruckt.</summary>
			public bool PrintBeleg { get; set; }
			/// <summary>Die einzelnen Posten die auf dieser Rechnung erscheinen zum Beispiel Reinigungsgebühr oder Ähnliches.</summary>
			public IList<Posten> Postens
			{
				get
				{
					if (_postens == null)
						_postens = new List<Posten>();
					return _postens;
				}
				set { _postens = value; }
			}
			/// <summary>Die Mails die versendet werden sollen mit entsprechendem Bon im Anhang.</summary>
			public IList<Mail> Mails
			{
				get
				{
					if (_mails == null)
						_mails = new List<Mail>();
					return _mails;
				}
				set { _mails = value; }
			}
		}



		/// <summary>Der Typ eines Posten auf einem Bon.</summary>
		public class Posten : IValidateable
		{
			/// <summary>Erstellt einen neuen <see cref="Posten" />.</summary>
			public Posten()
			{

			}

			/// <summary>Erstellt einen Posten auf der zugehörigen Rechnung.</summary>
			/// <param name="name">[REQUIRED] Der Name des <see cref="Posten" /> z.B Reinigung.</param>
			/// <param name="betragBrutto">[REQUIRED] Die Kosten eines Stückes dieses <see cref="Posten" />s in Brutto.</param>
			/// <param name="steuer">[OPTIONAL] Die Steuer die angewendet wird auf folgende Rechnung (<see cref="BetragBrutto" />*<see cref="Anzahl" />).</param>
			/// <param name="anzahl">
			///     [REQUIRED] Die Stückanzahl dieses Postens die verkauft wird. Sie wird später mit dem Feld <see cref="BetragBrutto" /> multipliziert um auf die
			///     gesamt Kosten zu kommen.
			/// </param>
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


			/// <summary>[REQUIRED] Der Name des <see cref="Posten" /> z.B Reinigung.</summary>
			public string Name { get; set; }
			/// <summary>[REQUIRED] Die Kosten eines Stückes dieses <see cref="Posten" />s in Brutto.</summary>
			public decimal BetragBrutto { get; set; }
			/// <summary>[OPTIONAL] Die Steuer die angewendet wird auf folgende Rechnung (<see cref="BetragBrutto" />*<see cref="Anzahl" />).</summary>
			public decimal Steuer { get; set; }
			/// <summary>
			///     [REQUIRED] Die Stückanzahl dieses Postens die verkauft wird. Sie wird später mit dem Feld <see cref="BetragBrutto" /> multipliziert um auf die
			///     gesamt Kosten zu kommen.
			/// </summary>
			public int Anzahl { get; set; }
		}



		/// <summary>Der Typ einer Mail die versendet werden kann</summary>
		public class Mail : IValidateable
		{
			/// <summary>ctor</summary>
			public Mail()
			{

			}

			/// <summary>Erstellt eine neue Mail</summary>
			/// <param name="address">die Zieladresse dieser E-Mail.</param>
			/// <param name="betreff">[OPTIONAL] Der Betreff dieser E-Mail.</param>
			/// <param name="text">[OPTIONAL] Der Text dieser Mail.</param>
			/// <param name="bcc">[OPTIONAL] Die blind carbon copy Empfänger dieser Mail. Mehrere Mails werden durch einen Beistrich von einander separiert.</param>
			/// <param name="outputformat">
			///     [OPTIONAL] Der Name des output formats welches benutzt werden soll. Wenn das angegebene Format nicht existiert wird der Applikations default
			///     verwendet.
			/// </param>
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


			/// <summary>[REQUIRED] die Zieladresse dieser E-Mail.</summary>
			public string Address { get; set; }
			/// <summary>[OPTIONAL] Die blind carbon copy Empfänger dieser Mail. Mehrere Mails werden durch einen Beistrich von einander separiert.</summary>
			public string Bcc { get; set; }
			/// <summary>[OPTIONAL] Der Betreff dieser E-Mail.</summary>
			public string Betreff { get; set; }
			/// <summary>[OPTIONAL] Der Text dieser Mail.</summary>
			public string Text { get; set; }
			/// <summary>
			///     [OPTIONAL] Der Name des output formats welches benutzt werden soll. Wenn das angegebene Format nicht existiert wird der Applikations default
			///     verwendet.
			/// </summary>
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