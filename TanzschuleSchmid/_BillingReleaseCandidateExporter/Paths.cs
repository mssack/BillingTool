// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.IO;
using System.Reflection;
using BillingTool.btScope.versioning;
using CsWpfBase.Ev.Public.Extensions;






namespace ReleaseCandidateExporter
{


	public static class Paths
	{
		private const string SolutionName = "TanzschuleSchmid";
		private const string ProjectName = "BillingTool";
		private const string ReadmeName = "README.md";
		private static string ExecuteableFolder { get; }


		private static string SolutionFolder => Path.Combine(GitRootFolder, SolutionName);
		private static string ProjectFolder => Path.Combine(SolutionFolder, ProjectName);
		private static string RcFolder => Path.Combine(SolutionFolder, "_Anhänge", "_ReleaseCandidates");
		private static string ArcFolder => Path.Combine(RcFolder, $"{Destination.BuildNumber} vom {BuildDetails.Time.ToString("yyyy.MM.dd HH.mm.ss")}");

		public static string GitRootFolder => new DirectoryInfo(ExecuteableFolder).GoUpward_Until(SolutionName).Parent.FullName;
		public static BuildDetails BuildDetails { get; set; }

		static Paths()
		{
			ExecuteableFolder = new DirectoryInfo(Assembly.GetEntryAssembly().Location).FullName;
		}



		public static class Destination
		{
			public static string RcFolder => Paths.RcFolder;
			public static string BuildNumber => $"RC{BuildDetails.ActiveDevNumber}{(BuildDetails.GoldNumber == 0 ? "" : $".{BuildDetails.GoldNumber}")}";
			public static string ZipFileName => $"BillingTool - {BuildNumber}.zip";
			public static string ZipFile => Path.Combine(RcFolder, ZipFileName);
		}
		public static class Arc
		{
			public static string Folder => ArcFolder;
			public static string CodeFolder => Path.Combine(Folder, "Code");
			public static string Executeable => Path.Combine(Folder, "Executeable");
			public static string SqlCeFolder => Path.Combine(CodeFolder, "SqlCE - Database Definition");
			public static string Enumerations => Path.Combine(CodeFolder, "Enumerations");
		}

		
		public static class Source
		{
			public static string ReadmeFile => Path.Combine(GitRootFolder, ReadmeName);
			public static string SqlCeScripts => Path.Combine(SolutionFolder, $"_BillingDataAccess", $"DatabaseCreation", "SqlCeScripts");
			public static string SharedEnumerations => Path.Combine(ProjectFolder, $"_SharedEnumerations");
			public static string IncludedContentFolder => Path.Combine(RcFolder, "_IncludedContent");
			public static string Executeables => Path.Combine(ProjectFolder, "bin", "Release");
			public static string BuildDetails => Path.Combine(ProjectFolder, nameof(BillingTool.btScope), nameof(BillingTool.btScope.versioning), nameof(BillingTool.btScope.versioning.BuildDetails) + ".txt");
		}
	}
}