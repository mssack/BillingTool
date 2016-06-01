// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingTool.btScope.versioning.buildData;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolGitControl._gen
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
			var source = releaseExecuteables ? Paths.Source.ReleaseExecuteables : Paths.Source.DebugExecuteables;


			foreach (var fileInfo in new DirectoryInfo(source).GetFiles("*.dll").Union(new DirectoryInfo(source).GetFiles("*.exe").Where(x => !x.Name.EndsWith(".vshost.exe"))).ToList())
			{
				var destFilePath = new FileInfo(Path.Combine(targetFolder, Paths.Arc.RelFolder_Executeable, fileInfo.Name));
				destFilePath.CreateDirectory_IfNotExists();
				destFilePath.DeleteFile_IfExists();
				fileInfo.CopyTo(destFilePath.FullName);
			}
			CopyEntireFolder(Paths.Source.IncludedContentFolder, targetFolder);
			CopyEntireFolder(Paths.Source.SqlCeScripts, Path.Combine(targetFolder, Paths.Arc.RelFolder_SqlCe));
			CopyEntireFolder(Paths.Source.BillingToolEnumerations, Path.Combine(targetFolder, Paths.Arc.RelFolder_Enumerations), Manipulate.Enumeration);

			CopyFile(Paths.Source.BelegDataTypes_EnumerationFile, Path.Combine(targetFolder, Paths.Arc.RelFolder_Enumerations), Manipulate.BelegDataTypes);

			CopyEntireFolder(Paths.Source.CodeSamples, Path.Combine(targetFolder, Paths.Arc.RelFolder_CodeBillingTool), Manipulate.Class);
			BatchFileCreator.CreateBatchFiles(targetFolder);
		}
		

		public static void CopyEntireFolder(string from, string to, Func<string, string> fileContentManipulationAction = null)
		{
			foreach (var dirPath in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
				Directory.CreateDirectory(dirPath.Replace(from, to));

			foreach (var newPath in Directory.GetFiles(from, "*.*", SearchOption.AllDirectories))
			{
				var targetFolder = Path.Combine(to, newPath.Replace(@from, "").Substring(1).Replace(new FileInfo(newPath).Name, ""));
				CopyFile(newPath, targetFolder, fileContentManipulationAction);
			}
		}

		public static void CopyFile(string from, string targetFolder, Func<string, string> fileContentManipulationAction = null)
		{
			var fileInfo = new FileInfo(@from);
			var targetFile = new FileInfo(Path.Combine(targetFolder, fileInfo.Name));
			targetFile.CreateDirectory_IfNotExists();
			File.Copy(from, targetFile.FullName, true);
			File.WriteAllText(targetFile.FullName, fileContentManipulationAction?.Invoke(File.ReadAllText(targetFile.FullName)));
		}



		/// <summary>Runs a list of CMD commands.</summary>
		/// <param name="commands">List of commands targetFolder execute by CMD window.</param>
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
	}


}