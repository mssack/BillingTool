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
using System.Threading.Tasks;
using BillingTool.btScope.versioning;
using CsWpfBase.Ev.Public.Extensions;






namespace ReleaseCandidateExporter
{
	public class ExportRuntime
	{

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

			CommitViaCommandline();
		}


		private void DeleteAllActualFolders()
		{
			new DirectoryInfo(Paths.Destination.ZipFile).GetDirectories("Actual*").ForEach(di => di.Delete(true));
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
				fileInfo.CopyTo(destFilePath.FullName);
			}
		}


		private void Zipping()
		{
			ZipFile.CreateFromDirectory(Paths.Arc.Folder, Paths.Destination.ZipFile, CompressionLevel.Optimal, false, Encoding.UTF8);
		}

		private void ChangeReadme()
		{
			var writer = new StreamWriter(Paths.Source.ReadmeFile);
			writer.WriteLine(BuildDetails.Number);
			writer.Close();
			writer.Dispose();
		}

		private void CommitViaCommandline()
		{
			Command
				(
					$"cd {Paths.GitRootFolder}",
					$"git commit \"{Paths.Source.BuildDetails}\" \"{Paths.Destination.ZipFile}\" \"{Paths.Source.ReadmeFile}\" -m 'New Relase Candidate Number = {BuildDetails.Number}'"
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