// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-02</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingToolGitControl.codeSamples;
using CsWpfBase.Ev.Public.Extensions;
// ReSharper disable InconsistentNaming






namespace BillingToolGitControl._gen
{
	public static class BatchFileCreator
	{
		private static readonly Random Rand = new Random();
		private static readonly string _sampleText = "Texte können mithilfe einer Schrift dargestellt deren Zeichen Phoneme Silben oder Wörter " +
													"Verschiedene Kulturen verwenden hierzu unterschiedliche Alphabete Durch die Einführung der " +
													"Schrift wurde eine Möglichkeit geschaffen wie zum Beispiel Erzählungen und Sagen für die Nachwelt " +
													"Ein großer Teil des geschichtlichen Wissens stammt aus schriftlichen die archiviert wurden oder " +
													"zufällig erhalten Texte aus Kulturen mit einer schriftlichen Überlieferungstradition unterscheiden" +
													" sich in ihrem Aufbau von Texten aus in denen die mündliche Überlieferung eine größere Rolle " +
													"spielt In den Geisteswissenschaften von denen keine schriftlichen Dokumente überliefert und " +
													"Frühgeschichte zugerechnet Somit wird eine aber dennoch sehr bedeutsame Definition des Gegenstandes" +
													" der Geschichtswissenschaft durch die Überlieferung von Texten gegeben";
		private static List<BillingToolStarter.Posten> _manyPostensArray;
		private static readonly BillingToolStarter _starter = new BillingToolStarter("", $"{Utils.Build.Version.Name}-SCRIPT");
		private static List<BillingToolStarter.Posten> _defaultPostensArray;

		private static readonly List<int> Steuersätze = new List<int>() {20, 13, 10, 0, 19};
		private static string TargetFolder;
		private static List<BillingToolStarter.Posten> ManyPostensArray
		{
			get { return _manyPostensArray ?? (_manyPostensArray = GetSeparatedWords(2).Select(x => new BillingToolStarter.Posten(x, Rand.Next(100, 999999)/(decimal) 100, 20, Rand.Next(1, 10))).ToList()); }
		}
		private static List<BillingToolStarter.Posten> DefaultPostensArray
		{
			get
			{
				return _defaultPostensArray ?? (_defaultPostensArray = new List<BillingToolStarter.Posten>
				{
					new BillingToolStarter.Posten("Kurs A", Rand.Next(100, 99999)/(decimal) 100, Steuersätze[Rand.Next(0, Steuersätze.Count)], Rand.Next(1, 5)),
					new BillingToolStarter.Posten("Kurs B", Rand.Next(100, 99999)/(decimal) 100, Steuersätze[Rand.Next(0, Steuersätze.Count)], Rand.Next(1, 5)),
					new BillingToolStarter.Posten("Gebühr Kurs C", Rand.Next(100, 99999)/(decimal) 100, Steuersätze[Rand.Next(0, Steuersätze.Count)], Rand.Next(1, 5)),
					new BillingToolStarter.Posten("Reinigungsaufschlag", Rand.Next(100, 99999)/(decimal) 100, Steuersätze[Rand.Next(0, Steuersätze.Count)], Rand.Next(1, 5)),
					new BillingToolStarter.Posten("Nächtigungsgebühr", Rand.Next(100, 99999)/(decimal) 100, Steuersätze[Rand.Next(0, Steuersätze.Count)], Rand.Next(1, 5)),
				});
			}
		}

		public static void CreateBatchFiles(string targetFolder)
		{
			TargetFolder = targetFolder;
			new Action[]
			{
				A_Print_ManyPosten,
				A_Print_DefaultPosten,
				B_Mail_ManyPosten,
				B_Mail_DefaultPosten,
				C_PrintMail_ManyPosten,
				C_PrintMail_DefaultPosten,
				D_Mail_FAILURE_ManyPosten,
				D_Mail_FAILURE_DefaultPosten,
				_Options,
				_Viewer,
				_SilentMonatsBonPrint,
			}.ForEach(x => x());
		}

		private static void A_Print_ManyPosten()
		{
			var data = GetDefaultBelegData("Eine ganze Menge Posten.", "Dieses Skript soll eine ganze Menge an Posten testen.", true, false);
			data.Postens = ManyPostensArray;
			WriteFile(data);
		}

		private static void B_Mail_ManyPosten()
		{
			var data = GetDefaultBelegData("Eine ganze Menge Posten.", "Dieses Skript soll eine ganze Menge an Posten testen.\r\nAllerdings fürs Mailing.", false, true);
			data.Postens = ManyPostensArray;
			WriteFile(data);
		}

		private static void C_PrintMail_ManyPosten()
		{
			var data = GetDefaultBelegData("Eine ganze Menge Posten.", "Dieses Skript soll eine ganze Menge an Posten testen.\r\nAllerdings fürs Drucken und Mailen.", true, true);
			data.Postens = ManyPostensArray;
			WriteFile(data);
		}

		private static void D_Mail_FAILURE_ManyPosten()
		{
			var data = GetDefaultBelegData("Eine ganze Menge Posten.", "Dieses Skript soll eine ganze Menge an Posten testen.\r\nAllerdings mit invaliden Mail Adressen.", false, false);
			data.Mails = new List<BillingToolStarter.Mail> {new BillingToolStarter.Mail("failure.at"), new BillingToolStarter.Mail("failure2.at")};
			data.Postens = ManyPostensArray;
			WriteFile(data);
		}




		private static void A_Print_DefaultPosten()
		{
			var data = GetDefaultBelegData("Eine ganz normale Rechnung wenn man so will", "Dieses Skript soll eine übliche Rechnung testen.", true, false);
			data.Postens = DefaultPostensArray;
			WriteFile(data);
		}

		private static void B_Mail_DefaultPosten()
		{
			var data = GetDefaultBelegData("Eine ganz normale Rechnung wenn man so will", "Dieses Skript soll eine übliche Rechnung testen.", false, true);
			data.Postens = DefaultPostensArray;
			WriteFile(data);
		}

		private static void C_PrintMail_DefaultPosten()
		{
			var data = GetDefaultBelegData("Eine ganz normale Rechnung wenn man so will", "Dieses Skript soll eine übliche Rechnung testen.", true, true);
			data.Postens = DefaultPostensArray;
			WriteFile(data);
		}

		private static void D_Mail_FAILURE_DefaultPosten()
		{
			var data = GetDefaultBelegData("Eine ganz normale Rechnung wenn man so will", "Dieses Skript soll eine übliche Rechnung testen.", false, false);
			data.Mails = new List<BillingToolStarter.Mail> {new BillingToolStarter.Mail("failure.at"), new BillingToolStarter.Mail("failure2.at")};
			data.Postens = DefaultPostensArray;
			WriteFile(data);
		}

		private static void _Options()
		{
			WriteFile(_starter.Cmd_Options());
		}

		private static void _Viewer()
		{
			WriteFile(_starter.Cmd_BelegDataViewer());
		}

		private static void _SilentMonatsBonPrint()
		{
			WriteFile(_starter.Cmd_SilentMonatsBonPrint());
		}



		private static void WriteFile(BillingToolStarter.BelegData data, [CallerMemberName] string fileName = null)
		{
			WriteFile(_starter.Cmd_BelegDataApproval(data), fileName);
		}

		private static void WriteFile(string argument, [CallerMemberName] string fileName = null)
		{
			argument = "chcp 1252\r\nsetlocal EnableDelayedExpansion\r\nStart ..\\Executeable\\Billingtool.exe " + argument.Replace("\r\n", "^\r\n\r\n");
			var targetFilePath = Path.Combine(TargetFolder, Paths.Arc.RelFolder_Startup, fileName + ".bat");
			new FileInfo(targetFilePath).CreateDirectory_IfNotExists();
			File.WriteAllText(targetFilePath, argument, Encoding.Default); //
		}

		private static BillingToolStarter.BelegData GetDefaultBelegData(string description, string zusatzText, bool print, bool mail)
		{
			var defaultBelegData = new BillingToolStarter.BelegData()
			{
				Comment = description,
				ZusatzText = $"{zusatzText}\r\nCreated @ {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} \r\nfor {Utils.Build.Version.Name}.",
				TypNumber = BelegDataTypes.Bar,
				PrintBeleg = print,
				ZahlungsReferenz = "TEST REFERENZ",
			};
			if (mail)
				defaultBelegData.Mails = new List<BillingToolStarter.Mail>
				{
					new BillingToolStarter.Mail("bttool@mail.com"),
					new BillingToolStarter.Mail("other@mail.com", "Mein eigener Betreff"),
					new BillingToolStarter.Mail("other@mail.com", "Mein eigener Betreff 2", "Mein eigener \r\nText mit \r\nLinebreaks.")
				};
			return defaultBelegData;
		}

		private static string[] GetSeparatedWords(int wordCount = 3)
		{
			var word = " \\w+";
			var regex = word.Expand(wordCount*word.Length, word).Trim();

			return Regex.Matches(_sampleText, regex).OfType<Match>().Select(x => x.Value).ToArray();
		}
	}
}