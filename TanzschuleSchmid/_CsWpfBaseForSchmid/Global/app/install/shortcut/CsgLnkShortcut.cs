// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.app.install.shortcut
{
	/// <summary>Defining shortcut property's.</summary>
	public sealed class CsgLnkShortcut : Base
	{
		private string _description;
		private string _destinationDirectory;
		private string _name;
		private string _sourceFile;
		private string _sourceIconFile;


		/// <summary>Initializes a new Shortcut file with the standard properties retrieved from <see cref="CsgAppInfo"/>.</summary>
		public CsgLnkShortcut()
		{
			_name = CsGlobal.App.Info.Name;
			_description = CsGlobal.App.Info.Description;
			_sourceFile = CsGlobal.App.Info.ProcessFile.FullName;
			_sourceIconFile = CsGlobal.App.Info.ProcessFile.FullName;
		}


		/// <summary>This name will be displayed to the user</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>This description will be used to give the user a hint to the shortcuts destination.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}


		/// <summary>Defines the owning the directory of the shortcut.</summary>
		public string DestinationDirectory
		{
			get { return _destinationDirectory; }
			set { SetProperty(ref _destinationDirectory, value); }
		}
		/// <summary>Defines the file which will be opened by the shortcut.</summary>
		public string SourceFile
		{
			get { return _sourceFile; }
			set { SetProperty(ref _sourceFile, value); }
		}
		/// <summary>Defines a .ico file or another executable or DLL containing the icon for this shortcut.</summary>
		public string SourceIconFile
		{
			get { return _sourceIconFile; }
			set { SetProperty(ref _sourceIconFile, value); }
		}
	}
}