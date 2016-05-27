// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using BillingTool.btScope.versioning;
using CsWpfBase.Ev.Public.Extensions;






namespace ReleaseCandidateExporter
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App : Application
	{
		#region Overrides/Interfaces
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			new ExportRuntime().Run();

			Environment.Exit(0);
		}
		#endregion
	}



	public class ExportRuntime
	{

		public ExportRuntime()
		{
			var currentDirectory = new DirectoryInfo(Assembly.GetEntryAssembly().Location);
			SolutionFolder = currentDirectory.GoUpward_Until("TanzschuleSchmid").FullName;
			RcFolder = Path.Combine(SolutionFolder, "_Anhänge", "_ReleaseCandidates");
		}

		public string SolutionFolder { get; }
		public string RcFolder { get; }
		public string ProjectFolder => Path.Combine(SolutionFolder, "BillingTool");
		public string ReleaseFolder => Path.Combine(ProjectFolder, "bin", "Release");
		public string IncludedContentFolder => Path.Combine(RcFolder, "_IncludedContent");
		public string ActualReleaseCandidateFolder { get; set; }
		public string ActualReleaseCandidateCodeFolder => Path.Combine(ActualReleaseCandidateFolder, "Code");
		
		private BuildDetails BuildDetails { get; set; }


		public void Run()
		{
			DeleteAllActualFolders();
			CollectBuildDetails();
			CopyExecuteables();
			CopyIncludedContent();
			CopySqlCeScripts();
			CopyEnums();
			ZipToFile();
		}


		private void DeleteAllActualFolders()
		{
			new DirectoryInfo(RcFolder).GetDirectories("Actual*").ForEach(di => di.Delete(true));
		}

		private void CollectBuildDetails()
		{
			BuildDetails = BuildDetails.LoadFromFile(new FileInfo(Path.Combine(ProjectFolder, nameof(BillingTool.btScope), nameof(BillingTool.btScope.versioning), nameof(BillingTool.btScope.versioning.BuildDetails) + ".txt")));
			ActualReleaseCandidateFolder = Path.Combine(RcFolder, $"Actual RC.{BuildDetails.Number}");
		}

		private void CopyExecuteables()
		{
			foreach (var fileInfo in new DirectoryInfo(ReleaseFolder).GetFiles("*.dll").Union(new DirectoryInfo(ReleaseFolder).GetFiles("*.exe").Where(x => !x.Name.EndsWith(".vshost.exe"))).ToList())
			{
				var destFilePath = new FileInfo(Path.Combine(ActualReleaseCandidateFolder, "Executeable", fileInfo.Name));
				destFilePath.CreateDirectory_IfNotExists();
				fileInfo.CopyTo(destFilePath.FullName);
			}
		}

		private void CopyIncludedContent()
		{
			CopyEntireFolder(IncludedContentFolder, ActualReleaseCandidateFolder);
		}

		private void CopySqlCeScripts()
		{
			CopyEntireFolder
				(
					Path.Combine
						(
							SolutionFolder,
							$"_BillingDataAccess",
							$"DatabaseCreation",
							"SqlCeScripts"
						),
					Path.Combine
						(
							ActualReleaseCandidateCodeFolder,
							"SqlCeScripts"
						)
				);
		}

		private void CopyEnums()
		{
			CopyEntireFolder
				(
					Path.Combine
						(
							ProjectFolder,
							$"_SharedEnumerations"
						),
					Path.Combine
						(
							ActualReleaseCandidateCodeFolder,
							"Enumerations"
						)
				);
		}

		private void ZipToFile()
		{
			ZipFile.CreateFromDirectory(ActualReleaseCandidateFolder, Path.Combine(RcFolder, $"Billingtool RC.{BuildDetails.Number}.zip"), CompressionLevel.Optimal,false, Encoding.UTF8);
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