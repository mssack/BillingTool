// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BillingTool.btScope.versioning;
using CsWpfBase.Ev.Public.Extensions;






namespace ReleaseCandidateExporter
{
	public class ExportRuntime
	{
		private readonly string _messageList;

		public ExportRuntime(string messageList)
		{
			_messageList = messageList;
		}

		private BuildDetails BuildDetails { get; set; }

		public void Run()
		{
			DeleteAllActualFolders();
			CollectBuildDetails();
			CopyExecuteables();


			CopyEntireFolder(Paths.Source.IncludedContentFolder, Paths.Arc.Folder);
			CopyEntireFolder(Paths.Source.SqlCeScripts, Paths.Arc.SqlCeFolder);
			CopyEntireFolder(Paths.Source.SharedEnumerations, Paths.Arc.Enumerations);


			Zipping();
			ChangeAnh�ngeReadme();
			ChangeStartseiteReadme();

			CommitViaCommandline();
		}

		private void DeleteAllActualFolders()
		{
			new DirectoryInfo(Paths.Destination.RcFolder).GetDirectories("RC*").ForEach(di => di.Delete(true));
		}

		private void CollectBuildDetails()
		{
			Paths.BuildDetails = BuildDetails = BuildDetails.LoadFromFile(new FileInfo(Paths.Source.BuildDetails));
		}

		private void CopyExecuteables()
		{
			foreach (var fileInfo in new DirectoryInfo(Paths.Source.Executeables).GetFiles("*.dll").Union(new DirectoryInfo(Paths.Source.Executeables).GetFiles("*.exe").Where(x => !x.Name.EndsWith(".vshost.exe"))).ToList())
			{
				var destFilePath = new FileInfo(Path.Combine(Paths.Arc.Executeable, fileInfo.Name));
				destFilePath.CreateDirectory_IfNotExists();
				destFilePath.DeleteFile_IfExists();
				fileInfo.CopyTo(destFilePath.FullName);
			}
		}


		private void Zipping()
		{
			new FileInfo(Paths.Destination.ZipFile).DeleteFile_IfExists();
			ZipFile.CreateFromDirectory(Paths.Arc.Folder, Paths.Destination.ZipFile, CompressionLevel.Optimal, false, Encoding.UTF8);
		}

		private void ChangeAnh�ngeReadme()
		{
			var txtLines = File.ReadAllLines(Paths.Source.Anh�ngeReadmeFile).ToList(); //Fill a list with the lines from the text file.
			txtLines.Insert(txtLines.IndexOf("####Release Candidates") + 1, $"* [{BuildDetails.NameWithDate}](_ReleaseCandidates/{Paths.Destination.ZipFileName}?raw=true)" +
																			$" (Computer: {BuildDetails.Computer}, User: {BuildDetails.User})"
																	+ (string.IsNullOrEmpty(_messageList) ? "" : "\n\t* " + Regex.Split(_messageList.Replace("\r\n", "\n"), "\n").Join("\n\t* ")));
			File.WriteAllLines(Paths.Source.Anh�ngeReadmeFile, txtLines);
		}
		private void ChangeStartseiteReadme()
		{
			var txtLines = File.ReadAllLines(Paths.Source.StartseiteReadmeFile).ToList(); //Fill a list with the lines from the text file.
			txtLines[2] = $"Aktuell im Betrieb [{BuildDetails.NameWithDate}](TanzschuleSchmid/_Anh�nge/_ReleaseCandidates/{Paths.Destination.ZipFileName}?raw=true).";
			File.WriteAllLines(Paths.Source.StartseiteReadmeFile, txtLines);
		}

		private void CommitViaCommandline()
		{
			Command
				(
					"echo off",
					$"cd {Paths.GitRootFolder}",
					$"git add \"{Paths.Source.BuildDetails}\"",
					$"git add \"{Paths.Destination.ZipFile}\"",
					$"git add \"{Paths.Source.Anh�ngeReadmeFile}\"",
					$"git add \"{Paths.Source.StartseiteReadmeFile}\"",
					$"git commit \"{Paths.Source.BuildDetails}\" \"{Paths.Destination.ZipFile}\" \"{Paths.Source.Anh�ngeReadmeFile}\" \"{Paths.Source.StartseiteReadmeFile}\" -m \"New Release Candidate {BuildDetails.Name}\""
				).Wait();
		}

		/// <summary>Runs a list of CMD commands.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <returns>The associated process.</returns>
		private Task<Process> Command(params string[] commands)
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

		private void CopyEntireFolder(string from, string to)
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
	}
}