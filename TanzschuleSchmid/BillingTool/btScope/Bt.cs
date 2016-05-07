// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-15</date>

using System;
using System.IO;
using System.Windows;
using BillingDataAccess.DatabaseCreation;
using BillingTool.btScope.configuration;
using BillingTool.btScope.configuration._enums;
using BillingTool.btScope.db;
using BillingTool.btScope.functions;
using BillingTool.btScope.logging;
using BillingTool.Exceptions;
using BillingTool.Windows;
using CsWpfBase.Global;
using DeveloperWindow = BillingTool.Windows.privileged.DeveloperWindow;
using Window_DatabaseViewer = BillingTool.Windows.privileged.Window_DatabaseViewer;
using Window_KassenConfiguration = BillingTool.Windows.privileged.Window_KassenConfiguration;






namespace BillingTool.btScope
{
	/// <summary>
	///     The programming interface for all relevant functions inside the <see cref="BillingTool" /> name space. Use this static class whenever an
	///     interaction with this program is needed.
	/// </summary>
	public static class Bt
	{
		/// <summary>
		///     The configuration of the current application lifeline. This can be modified by simple modifying the configuration file or passing parameters to
		///     the program.
		/// </summary>
		public static Configuration Config => Configuration.I;
		/// <summary>Used to log events. Use this whenever there is need for logging.</summary>
		public static Logging Logging => Logging.I;
		/// <summary>Used to interact with the underlaying database. Use <see cref="Config" /> to set the current database.</summary>
		public static Db Db => Db.I;
		/// <summary>Default functions used to interact with the user.</summary>
		public static UiFunctions UiFunctions => UiFunctions.I;
		/// <summary>Default data functions used to interact with the database.</summary>
		public static DataFunctions DataFunctions => DataFunctions.I;
		/// <summary>Functions needed for the application.</summary>
		public static Functions Functions => Functions.I;




		/// <summary>Ensures that the configuration is valid and that a database is accessible and functional.</summary>
		public static void EnsureInitialization()
		{
			if (!Config.File.KassenEinstellung.Path.Exists || !Config.File.KassenEinstellung.IsValid)
			{
				var window = new Window_KassenConfiguration();
				window.ShowDialog();
				if (!Config.File.KassenEinstellung.IsValid)
				{
					throw new BillingToolException(BillingToolException.Types.No_ValidConfiguration, $"Es wurde keine gültige Datenbankkonfiguration erstellt.");
				}
			}


			var fi = new FileInfo(Config.File.KassenEinstellung.BillingDatabaseFilePath);
			if (fi.Exists)
			{
				InitDb();
				return;
			}


			var createDb = UiFunctions.CheckOperatorsTrustAbility("Datenbank fehlt", "Sie sind dabei eine komplett neue Datenbank zu erstellen. Sie müssen sich im Klaren sein das alle bisherigen Belegdaten, sollten denn welche vorhanden sein, durch diesen Schritt ungültig werden. Sie müssen sich absolut sicher sein, dass Sie das wollen.");
			if (createDb == false)
			{
				throw new BillingToolException(BillingToolException.Types.No_DatabaseAvailable, $"Die Datenbankinstallation wurde vom User abgebrochen.");
			}
			try
			{
				using (var installer = new DatabaseInstaller(Config.File.KassenEinstellung.BillingDatabaseFilePath))
				{
					installer.Install();
				}
			}
			catch (BadImageFormatException exc)
			{
				throw new BillingToolException(BillingToolException.Types.No_DatabaseAvailable, $"Die verwendete Architektur 64 Bit ist nicht erlaubt. Kompilieren Sie diese Applikation noch einmal als x86 (Environment.Is64BitProcess[{Environment.Is64BitProcess}])", exc);
			}
			catch (Exception exc)
			{
				throw new BillingToolException(BillingToolException.Types.No_DatabaseAvailable, $"Die Datenbank konnte nicht installiert werden.", exc);
			}

			InitDb();
		}

		private static void InitDb()
		{
			Db.EnsureConnectivity();
			if (Db.Billing.OutputFormats.HasBeenLoaded != true)
				Db.Billing.OutputFormats.DownloadRows();
			if (Db.Billing.Configurations.HasBeenLoaded != true)
				Db.Billing.Configurations.DownloadRows();
		}

		/// <summary>does the startup</summary>
		public static void Startup(string[] startupArgs)
		{
			Config.CommandLine.Interpret(startupArgs);

			var mode = Config.CommandLine.General.StartupMode;

			Window window;
			if (mode == StartupModes.Developer)
				window = new DeveloperWindow();
			else if (mode == StartupModes.BelegDataApprove)
				window = new Window_BelegData_Approve {Item = DataFunctions.New_BelegData_From_Configuration()};
			else if (mode == StartupModes.BelegDataViewer)
				window = new Window_BelegData_Viewer();
			else if (mode == StartupModes.Options)
				window = new Window_Options();
			else if (mode == StartupModes.Database)
				window = new Window_DatabaseViewer();
			else
				throw new BillingToolException(BillingToolException.Types.Invalid_StartupParam, $"Fehlender {nameof(StartupModes)} siehe enumeration[{nameof(StartupModes)}].");



			Application.Current.MainWindow = window;
			CsGlobal.Message.SetDefaultScaling(Bt.Config.File.KassenEinstellung.Scaling);
			window.Show();

		}

		/// <summary>gets an indicator whether the database is accessible.</summary>
		public static bool IsInitialized()
		{
			if (!Config.File.KassenEinstellung.Path.Exists || !Config.File.KassenEinstellung.IsValid)
				return false;
			var fi = new FileInfo(Config.File.KassenEinstellung.BillingDatabaseFilePath);
			if (!fi.Exists)
				return false;
			if (!Db.IsConnected())
				return false;
			return true;
		}
	}
}