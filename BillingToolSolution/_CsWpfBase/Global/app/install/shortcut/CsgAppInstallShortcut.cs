// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.IO;
using CsWpfBase.Ev.Objects;
using IWshRuntimeLibrary;
using File = System.IO.File;






namespace CsWpfBase.Global.app.install.shortcut
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgAppInstallShortcut : Base
	{
		private static CsgAppInstallShortcut _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgAppInstallShortcut I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgAppInstallShortcut());
				}
			}
		}

		private CsgAppInstallShortcut()
		{
		}

		/// <summary>Creates a shortcut to the application on the desktop.</summary>
		public void CreateOnDesktop(string sourceFilePath = null)
		{
			var desktopfolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			CreateShortcut(new CsgLnkShortcut {DestinationDirectory = desktopfolder});
		}

		/// <summary>Creates a shortcut to the application in the start menu.</summary>
		public void CreateInStartmenu(string sourceFilePath = null)
		{
			var startmenufolder = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
			CreateShortcut(new CsgLnkShortcut {DestinationDirectory = startmenufolder});
		}

		/// <summary>Creates a shortcut to the application in the startup folder.</summary>
		public void CreateInStartupFolder(string sourceFilePath = null)
		{
			var startupfolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
			CreateShortcut(new CsgLnkShortcut {DestinationDirectory = startupfolder});
		}

		/// <summary>Creates a user defined shortcut.</summary>
		public void CreateShortcut(CsgLnkShortcut shortcut)
		{
			if (String.IsNullOrEmpty(shortcut.DestinationDirectory))
				throw new ArgumentException("a destination have to be defined.");
			if (!File.Exists(shortcut.SourceFile))
				throw new FileNotFoundException();
			if (!File.Exists(shortcut.SourceIconFile))
				throw new FileNotFoundException();
			if (String.IsNullOrEmpty(shortcut.Name))
				throw new ArgumentException();

			var sourceDirectory = (new FileInfo(shortcut.SourceFile)).Directory;
			if (sourceDirectory == null)
				throw new DirectoryNotFoundException();


			var wsh = new WshShell();
			var sc = (IWshShortcut) wsh.CreateShortcut(Path.Combine(shortcut.DestinationDirectory, shortcut.Name + ".lnk"));

			sc.TargetPath = shortcut.SourceFile;
			sc.WorkingDirectory = sourceDirectory.FullName;
			sc.IconLocation = shortcut.SourceIconFile;
			sc.Description = shortcut.Description;
			sc.WindowStyle = 1;
			sc.Save();
		}
	}
}