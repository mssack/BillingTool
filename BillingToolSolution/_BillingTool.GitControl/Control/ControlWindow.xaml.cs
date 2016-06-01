// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using BillingToolGitControl._gen;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingToolGitControl.Control
{
	/// <summary>Interaction logic for ControlWindow.xaml</summary>
	public partial class ControlWindow : CsWindow
	{
		public ControlWindow()
		{
			InitializeComponent();
		}

		private string ChangelogText => ChangelogTextBox.Text.Replace("\r\n", "\n").Split("\n").Join("  \r\n  ");

		private void GenerateReleaseTestingEnvironment(object sender, RoutedEventArgs e)
		{
			var targetFolder = Path.Combine(Paths.Destination.RcFolder, $"{Utils.Build.Version.Name} - TestEnvironment");
			Utils.CreateTestEnvironment(targetFolder, true);
			Process.Start(Path.Combine(targetFolder));
			Close();
		}

		private void GenerateDebugTestingEnvironment(object sender, RoutedEventArgs e)
		{
			var targetFolder = Path.Combine(Paths.Destination.RcFolder, $"{Utils.Build.Version.Name} - TestEnvironment");
			Utils.CreateTestEnvironment(targetFolder, false);
			Process.Start(Path.Combine(targetFolder));
			Close();
		}

		private void AppendToChangelog(object sender, RoutedEventArgs e)
		{
			AppendToChangelog(Paths.Source.StartseiteReadmeFile);

			Utils.CommitFiles($"Readme adapted ('{ChangelogText}')", Paths.Source.StartseiteReadmeFile);
			Close();
		}

		private void AppendToChangelog(string filename)
		{
			var txtLines = File.ReadAllLines(filename).ToList(); //Fill a list with the lines from the text file.
			txtLines.Insert(txtLines.IndexOf("[](CHANGELOGEND)"), "* " + ChangelogText);
			File.WriteAllLines(filename, txtLines);
		}
	}
}