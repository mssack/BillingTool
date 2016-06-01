// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.IO;
using System.Reflection;
using BillingTool.btScope.versioning.buildData;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolGitControl._gen
{
	public static class Paths
	{
		private const string SolutionName = "BillingToolSolution";
		private const string ProjectName = "BillingTool";
		private const string DataAccessProjectName = "BillingTool.DataAccess";
		private const string ReadmeName = "readme.md";
		private static string ExecuteableFolder { get; }


		private static string SolutionFolder => Path.Combine(GitRootFolder, SolutionName);
		private static string ProjectFolder => Path.Combine(SolutionFolder, ProjectName);
		private static string RcFolder => Path.Combine(SolutionFolder, "_Anhänge", "_ReleaseCandidates");

		public static string GitRootFolder => new DirectoryInfo(ExecuteableFolder).GoUpward_Until(SolutionName).Parent.FullName;

		static Paths()
		{
			ExecuteableFolder = new DirectoryInfo(Assembly.GetEntryAssembly().Location).FullName;
		}



		public static class Destination
		{
			public static string RcFolder => Paths.RcFolder;
			public static string ZipFileName => $"BillingTool - {Utils.Build.Version.Name}.zip";
			public static string ZipFile => Path.Combine(RcFolder, ZipFileName);
		}



		public static class Arc
		{
			public static string Folder => Path.Combine(RcFolder, $"{Utils.Build.NameWithDateForIO}");

			public static string RelFolder_Code => "Code";
			public static string RelFolder_CodeBillingTool => Path.Combine(RelFolder_Code, "BillingTool");
			public static string RelFolder_Executeable => "Executeable";
			public static string RelFolder_SqlCe => Path.Combine(RelFolder_Code, "SqlCE - Database Definition");
			public static string RelFolder_Enumerations => Path.Combine(RelFolder_CodeBillingTool, "enumerations");
		}



		public static class Source
		{
			public static string StartseiteReadmeFile => Path.Combine(GitRootFolder, ReadmeName);
			public static string AnhängeReadmeFile => Path.Combine(SolutionFolder, "_Anhänge", ReadmeName);
			public static string SqlCeScripts => Path.Combine(SolutionFolder, DataAccessProjectName, $"DatabaseCreation", "SqlCeScripts");
			public static string BillingToolEnumerations => Path.Combine(ProjectFolder, $"enumerations");
			public static string BelegDataTypes_EnumerationFile => Path.Combine(SolutionFolder, DataAccessProjectName, "sqlcedatabases", "billingdatabase", "_Extensions", "enumerations", "BelegDataTypes.cs");
			public static string BillingToolCodeSamples => Path.Combine(ProjectFolder, $"codeSamples");
			public static string IncludedContentFolder => Path.Combine(RcFolder, "_IncludedContent");
			public static string ReleaseExecuteables => Path.Combine(ProjectFolder, "bin", "Release");
			public static string DebugExecuteables => Path.Combine(ProjectFolder, "bin", "Debug");
			public static string BuildDetails => Path.Combine(ProjectFolder, nameof(BillingTool.btScope), nameof(BillingTool.btScope.versioning), nameof(BillingTool.btScope.versioning.buildData), nameof(BuildData) + ".txt");
		}
	}
}