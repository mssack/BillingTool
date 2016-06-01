// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using BillingTool.enumerations;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






//This class will be exported to the release candidate.

namespace BillingTool.codeSamples
{
	/// <summary>used to interact with the BillingTool.</summary>
	public class BillingToolStarter
	{

		private readonly string _filePath;
		private readonly string _kassenoperator;

		/// <summary>Creates a new BillingTool Interaction.</summary>
		/// <param name="filePath">The file path to the executable.</param>
		/// <param name="kassenoperator">The operator of the Kassa.</param>
		public BillingToolStarter(string filePath, string kassenoperator)
		{
			_filePath = filePath;
			_kassenoperator = kassenoperator;
		}

		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public ExitCodes BelegDataApproval(BelegData data)
		{
			return OpenProcess(StartupModes.BelegDataApprove, data.ToString());
		}

		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public ExitCodes Options()
		{
			return OpenProcess(StartupModes.Options);
		}

		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public ExitCodes SilentMonatsBonPrint()
		{
			return OpenProcess(StartupModes.SilentMonatsBonPrint);
		}

		/// <summary>Used to approve one instance of <see cref="BelegData" />.</summary>
		public ExitCodes BelegDataViewer()
		{
			return OpenProcess(StartupModes.BelegDataViewer);
		}



		private ExitCodes OpenProcess(StartupModes mode, string commandlineArgs = null)
		{
			var fullArgument = "/" + mode + " /NceKassenOperator " + _kassenoperator;
			if (!string.IsNullOrEmpty(commandlineArgs))
				fullArgument = fullArgument + " " + commandlineArgs;

			using (var process = Process.Start(_filePath, fullArgument))
			{
				process.WaitForExit();
				return (ExitCodes) process.ExitCode;
			}
		}



		/// <summary>used for the Bon approval.</summary>
		public class BelegData
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
			public IList<Posten> Postens { get; set; }
			/// <summary>The mails which should be sended.</summary>
			public IList<Mail> Mails { get; set; }
		}



		/// <summary>Describes one item on a Bon.</summary>
		public class Posten
		{
			/// <summary>Creates a new article.</summary>
			public Posten(string name, decimal betragBrutto, decimal steuer, int anzahl)
			{
				if (anzahl == 0)
					throw new ArgumentException("anzahl = 0", "anzahl");
				if (betragBrutto == 0)
					throw new ArgumentException("betragBrutto = 0", "betragBrutto");
				if (string.IsNullOrEmpty(name))
					throw new ArgumentException("name = ''", "name");


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
			#endregion


			/// <summary>The name of the article.</summary>
			public string Name { get; }
			/// <summary>The cost of one entity of this article.</summary>
			public decimal BetragBrutto { get; }
			/// <summary>The tax which is applied to the article*amount</summary>
			public decimal Steuer { get; }
			/// <summary>the number of articles of this type.</summary>
			public int Anzahl { get; }
		}



		/// <summary>Describes one mail.</summary>
		public class Mail
		{
			/// <summary>Creates a new article.</summary>
			public Mail(string address, string betreff = null, string text = null, string outputformat = null)
			{
				if (string.IsNullOrEmpty(address))
					throw new ArgumentException("address = null", "address");

				Address = address;
				Betreff = betreff;
				Text = text;
				OutputFormat = outputformat;
			}


			#region Overrides/Interfaces
			public override string ToString()
			{
				return Helper.Get_ListItem_String(this);
			}
			#endregion


			/// <summary>The mail address of the receiver.</summary>
			public string Address { get; }
			/// <summary>The subject of the mail</summary>
			public string Betreff { get; }
			/// <summary>the text of the mail message</summary>
			public string Text { get; }
			/// <summary>the layout name which should be used for the mail.</summary>
			public string OutputFormat { get; }
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

					string valueText = GetTextRepresentation(property, value);
					if (valueText == null)
						continue;

					arguments = arguments + "/Nce" + property.Name + " " + valueText + " ";
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

					string valueText = GetTextRepresentation(property, value);
					if (valueText == null)
						continue;

					arguments = arguments + property.Name + " = " + valueText + "; ";
				}

				return "{" + arguments.Substring(0, arguments.Length - 2) + "}";
			}

			private static string GetTextRepresentation(PropertyInfo property, object value)
			{
				if ((property.PropertyType.IsValueType && value == Activator.CreateInstance(property.PropertyType)) || value == null)
					return null;
				if (property.PropertyType == typeof(decimal))
					return ((decimal)value).ToString("0.00");

				if (property.PropertyType.IsEnum)
					return ((int)value).ToString();

				if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
				{
					var count = 0;
					string valueText = "{ ";
					foreach (var obj in (IEnumerable)value)
					{
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