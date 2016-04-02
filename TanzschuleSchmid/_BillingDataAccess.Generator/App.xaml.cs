// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Windows;
using BillingDataAccessGenerator.DatabaseCreation;
using CsWpfBase.Db;
using CsWpfBase.Db.router;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






// ReSharper disable PossibleNullReferenceException

namespace BillingDataAccessGenerator
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		/// <summary>the relative path to the sample db inside project.</summary>
		public string SampleDatabaseFile => Path.Combine(new FileInfo("a").Directory.GoUpward_Until("_BillingDataAccess.Generator").FullName, "BillingDatabase.sdf");
		/// <summary>the relative path to the target project folder.</summary>
		public string TargetProjectFolder => Path.Combine(new FileInfo("a").Directory.GoUpward_Until("TanzschuleSchmid").FullName, "_BillingDataAccess");


		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			CreateDatabase();
			GenerateDatabaseClasses();
			CsGlobal.App.Exit();
		}

		private void CreateDatabase()
		{
			var installer = new DatabaseInstaller(SampleDatabaseFile);
			installer.Install(true);
		}


		private void GenerateDatabaseClasses()
		{
			var architecture = CsDb.CodeGen.Create.Architecture_From_SqlCe(new SqlCeRouter(SampleDatabaseFile));
			architecture.Databases[0].ReadTableNameConventions(
				"BelegDaten;BelegData;BelegDaten" + "\r\n" +
				"MailedBelege;MailedBeleg;MailedBelege" + "\r\n" +
				"PrintedBelege;PrintedBeleg;PrintedBelege" + "\r\n" +
				"BelegPostens;BelegPosten;BelegPostens" + "\r\n" +
				"Postens;Posten;Postens" + "\r\n" +
				"Steuersätze;Steuersatz;Steuersätze" + "\r\n" +
				"Logs;Log;Logs", ";"
				);

			architecture.Databases[0].ReadRelationNameConventions(
				"FK_BelegPostens_SteuersatzId;Steuersatz;Steuersätze" + "\r\n" +
				"FK_BelegData_StornoBelegId;StornoBeleg;StornierendeBelege" + "\r\n" +
				"FK_BelegPostens_BelegDataId;Data;Postens" + 
				"", ";");

			architecture.Name = "SqlCeDatabases";
			var codeBundle = architecture.GetCodeBundle();

			codeBundle.SetPaths(Path.Combine(TargetProjectFolder, ""), "BillingDataAccess", Path.Combine(TargetProjectFolder, "_BillingDataAccess.csproj"));
			codeBundle.Write();
		}
	}



	/// <summary>Used to connect to an sdf file.</summary>
	internal class SqlCeRouter : CsDbRouter
	{
		private readonly string _databaseFilePath;

		public SqlCeRouter(string databaseFilePath)
		{
			_databaseFilePath = databaseFilePath;
		}


		#region Overrides/Interfaces
		/// <summary>Creates a new <see cref="DbDataAdapter" /> of the specific type using the command. Do not change any settings of the data adapter.</summary>
		public override DbDataAdapter GetAdapter(DbCommand cmd)
		{
			return new SqlCeDataAdapter((SqlCeCommand) cmd);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommand" />.</summary>
		public override DbCommand GetCommand()
		{
			return new SqlCeCommand();
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommandBuilder" />. Use <paramref name="adapter" /> for initialization.</summary>
		public override DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter)
		{
			return new SqlCeCommandBuilder((SqlCeDataAdapter) adapter);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbConnection" />.</summary>
		public override DbConnection Initialize()
		{
			return new SqlCeConnection($"data source={_databaseFilePath}");
		}
		#endregion
	}
}