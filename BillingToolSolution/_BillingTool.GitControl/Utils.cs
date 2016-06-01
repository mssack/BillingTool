// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BillingTool.btScope.versioning.buildData;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolGitControl
{

	public static class Utils
	{
		private static BuildData _buildData;
		public static BuildData Build
		{
			get
			{
				if (_buildData == null)
					_buildData = BuildData.LoadFromFile(new FileInfo(Paths.Source.BuildDetails));
				return _buildData;
			}
		}

		public static void CommitFiles(string message, params string[] files)
		{
			Command(
				new[]
				{
					"echo off",
					$"cd {Paths.GitRootFolder}",
				}
					.Concat(files.Select(x => $"git add \"{x}\""))
					.Concat(new[]
					{
						$"git commit {files.Select(x => $"\"{x}\"").Join(" ")} -m \"{message}\""
					}).ToArray()
				).Wait();
		}

		public static void CreateTestEnvironment(string targetFolder, bool releaseExecuteables)
		{
			string source = releaseExecuteables ? Paths.Source.ReleaseExecuteables : Paths.Source.DebugExecuteables;


			foreach (var fileInfo in new DirectoryInfo(source).GetFiles("*.dll").Union(new DirectoryInfo(source).GetFiles("*.exe").Where(x => !x.Name.EndsWith(".vshost.exe"))).ToList())
			{
				var destFilePath = new FileInfo(Path.Combine(targetFolder, Paths.Arc.RelFolder_Executeable, fileInfo.Name));
				destFilePath.CreateDirectory_IfNotExists();
				destFilePath.DeleteFile_IfExists();
				fileInfo.CopyTo(destFilePath.FullName);
			}
			CopyEntireFolder(Paths.Source.IncludedContentFolder, targetFolder);
			CopyEntireFolder(Paths.Source.SqlCeScripts, Path.Combine(targetFolder, Paths.Arc.RelFolder_SqlCe));
			CopyEntireFolder(Paths.Source.SharedEnumerations, Path.Combine(targetFolder, Paths.Arc.RelFolder_Enumerations));
		}

		private static void CopyEntireFolder(string from, string to)
		{
			foreach (var dirPath in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
				Directory.CreateDirectory(dirPath.Replace(from, to));

			foreach (var newPath in Directory.GetFiles(from, "*.*", SearchOption.AllDirectories))
			{
				var destFileName = new FileInfo(newPath.Replace(@from, to));
				destFileName.CreateDirectory_IfNotExists();
				File.Copy(newPath, destFileName.FullName, true);
			}
		}

		/// <summary>Runs a list of CMD commands.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <returns>The associated process.</returns>
		private static Task<Process> Command(params string[] commands)
		{
			var procStartInfo = new ProcessStartInfo
			{
				RedirectStandardError = false,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				FileName = "cmd",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				Arguments = "/c " + commands.Join("&"),
			};
			Console.WriteLine(commands.Join("\r\n"));

			var process = new Process();
			process.EnableRaisingEvents = true;
			process.StartInfo = procStartInfo;
			var tcs = new TaskCompletionSource<Process>();
			process.Exited += (sender, args) => tcs.SetResult(process);
			try
			{
				process.Start();
			}
			catch (Exception e)
			{
				tcs.SetException(e);
			}


			return tcs.Task;
		}



		public static class Paths
		{
			private const string SolutionName = "BillingToolSolution";
			private const string ProjectName = "BillingTool";
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
				public static string ZipFileName => $"BillingTool - {Build.Version.Name}.zip";
				public static string ZipFile => Path.Combine(RcFolder, ZipFileName);
			}



			public static class Arc
			{
				public static string Folder => Path.Combine(RcFolder, $"{Build.NameWithDate}");

				public static string RelFolder_Code_ => "Code";
				public static string RelFolder_Executeable => "Executeable";
				public static string RelFolder_SqlCe => Path.Combine(RelFolder_Code_, "SqlCE - Database Definition");
				public static string RelFolder_Enumerations => Path.Combine(RelFolder_Code_, "Enumerations");
			}



			public static class Source
			{
				public static string StartseiteReadmeFile => Path.Combine(GitRootFolder, ReadmeName);
				public static string AnhängeReadmeFile => Path.Combine(SolutionFolder, "_Anhänge", ReadmeName);
				public static string SqlCeScripts => Path.Combine(SolutionFolder, $"_BillingDataAccess", $"DatabaseCreation", "SqlCeScripts");
				public static string SharedEnumerations => Path.Combine(ProjectFolder, $"_SharedEnumerations");
				public static string IncludedContentFolder => Path.Combine(RcFolder, "_IncludedContent");
				public static string ReleaseExecuteables => Path.Combine(ProjectFolder, "bin", "Release");
				public static string DebugExecuteables => Path.Combine(ProjectFolder, "bin", "Debug");
				public static string BuildDetails => Path.Combine(ProjectFolder, nameof(BillingTool.btScope), nameof(BillingTool.btScope.versioning), nameof(BillingTool.btScope.versioning.buildData), nameof(BillingTool.btScope.versioning.buildData.BuildData) + ".txt");
			}
		}
	}
}