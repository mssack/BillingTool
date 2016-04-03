// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.IO;
using System.Windows;
using BillingDataAccess.DatabaseCreation;
using BillingTool.btScope.configuration;
using BillingTool.btScope.configuration._enums;
using BillingTool.btScope.db;
using BillingTool.btScope.functions;
using BillingTool.btScope.logging;
using BillingTool.Windows;
using CsWpfBase.Global;






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
					Functions.SetExitCode(ExitCodes.No_ValidConfiguration);
					Environment.Exit(0);
				}
			}


			var fi = new FileInfo(Config.File.KassenEinstellung.BillingDatabaseFilePath);
			if (fi.Exists)
			{
				Db.EnsureConnectivity();
				return;
			}


			var createDb = UiFunctions.CheckOperatorsTrustAbility("Datenbank fehlt", "Sie sind dabei eine komplett neue Datenbank zu erstellen. Sie müssen sich im Klaren sein das alle bisherigen Belegdaten, sollten denn welche vorhanden sein, durch diesen Schritt ungültig werden. Sie müssen sich absolut sicher sein, dass Sie das wollen.");
			if (createDb == false)
			{
				Functions.SetExitCode(ExitCodes.No_DatabaseAvailable);
				Environment.Exit(0);
			}
			try
			{
				using (var installer = new DatabaseInstaller(Config.File.KassenEinstellung.BillingDatabaseFilePath))
				{
					installer.Install();
				}
			}
			catch (Exception)
			{
				Functions.SetExitCode(ExitCodes.No_DatabaseAvailable);
				Environment.Exit(0);
			}


			Db.EnsureConnectivity();
		}

		/// <summary>does the startup</summary>
		public static void Startup(string[] startupArgs)
		{
			Config.CommandLine.Interpret(startupArgs);

			var mode = Config.CommandLine.General.StartupMode;

			if (mode == StartupModes.Developer)
			{
				var w = new DeveloperWindow();
				Application.Current.MainWindow = w;

				w.Show();
			}
			else if (mode == StartupModes.ApproveBelegData)
			{
				var window = new Window_BelegData_Approve();
				Application.Current.MainWindow = window;

				window.Item = DataFunctions.New_BelegData_FromConfiguration();
				window.Show();
			}
			else if (mode == StartupModes.Database)
			{
				var window = new DatabaseWindow();
				Application.Current.MainWindow = window;

				window.Show();
			}

			if (Application.Current.MainWindow == null)
				Application.Current.MainWindow = Application.Current.Windows[0];
		}
	}
}