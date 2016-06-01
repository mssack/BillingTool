// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BillingToolGitControl._gen;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolGitControl.NewRc
{
	public class RcCreatorRuntime
	{
		private readonly string _messageList;

		public RcCreatorRuntime(string messageList)
		{
			_messageList = messageList;
		}


		public void Run()
		{
			DeleteAllActualFolders();

			Utils.CreateTestEnvironment(Paths.Arc.Folder, true);

			Zipping();
			ChangeAnhängeReadme();
			ChangeStartseiteReadme();

			CommitViaCommandline();
		}

		private void DeleteAllActualFolders()
		{
			new DirectoryInfo(Paths.Destination.RcFolder).GetDirectories("RC*").ForEach(di => di.Delete(true));
		}
		
		private void Zipping()
		{
			new FileInfo(Paths.Destination.ZipFile).DeleteFile_IfExists();
			ZipFile.CreateFromDirectory(Paths.Arc.Folder, Paths.Destination.ZipFile, CompressionLevel.Optimal, false, Encoding.UTF8);
		}

		private void ChangeAnhängeReadme()
		{
			var txtLines = File.ReadAllLines(Paths.Source.AnhängeReadmeFile).ToList(); //Fill a list with the lines from the text file.
			txtLines.Insert(txtLines.IndexOf("#####Release Candidates") + 1, $"* [{Utils.Build.NameWithDate}](_ReleaseCandidates/{Paths.Destination.ZipFileName}?raw=true)" +
																			$" (Computer: {Utils.Build.Machine}, User: {Utils.Build.User})"
																			+ (string.IsNullOrEmpty(_messageList) ? "" : "\n\t* " + Regex.Split(_messageList.Replace("\r\n", "\n"), "\n").Join("\n\t* ")));
			File.WriteAllLines(Paths.Source.AnhängeReadmeFile, txtLines);
		}

		private void ChangeStartseiteReadme()
		{
			var txtLines = File.ReadAllLines(Paths.Source.StartseiteReadmeFile).ToList(); //Fill a list with the lines from the text file.
			txtLines[2] = $"Aktuell [{Utils.Build.NameWithDate}](BillingToolSolution/_Anhänge/_ReleaseCandidates/{Paths.Destination.ZipFileName}?raw=true).";
			File.WriteAllLines(Paths.Source.StartseiteReadmeFile, txtLines);
		}

		private void CommitViaCommandline()
		{
			Utils.CommitFiles($"New Release Candidate {Utils.Build.Version.Name}", Paths.Source.BuildDetails, Paths.Source.AnhängeReadmeFile, Paths.Source.StartseiteReadmeFile, Paths.Destination.ZipFile);
		}


	}
}