// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace CsWpfBase.Online.packets.v1.server
{
	/// <summary>Server Response, if an update is available.</summary>
	[Serializable]
	public class CsopServerUpdateAvailable : CsoPacket
	{
		private List<String> _autostartFiles;
		private string _changeset;
		private string _description;
		private string _downloadLink;
		private UInt64 _fileSize;
		private bool _isHiddenUpdate;
		private string _version;
		private string _windowTitle;


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ServerUpdateAvailable; }
		}
		/// <summary>
		///     The initial Value of the <see cref="CsoPacket.PacketVersion" />, this value will be applied to the <see cref="CsoPacket.PacketVersion" />
		///     property whenever the packet is created or no version is defined.
		/// </summary>
		protected override uint InitialVersion
		{
			get { return 1; }
		}

		/// <summary>Interprets a binary into this object.</summary>
		internal override void Parse(Reader reader, int length)
		{
			IsHiddenUpdate = reader.Byte() == 1;
			WindowTitle = reader.String();
			Description = reader.String();
			Changeset = reader.String();
			Version = reader.String();
			FileSize = reader.UInt64();
			DownloadLink = reader.String();

			for (var i = 0; i < reader.Int32(); i++)
			{
				AutostartFiles.Add(reader.String());
			}
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.Byte((byte) (IsHiddenUpdate ? 1 : 0));
			writer.String(WindowTitle);
			writer.String(Description);
			writer.String(Changeset);
			writer.String(Version);
			writer.UInt64(FileSize);
			writer.String(DownloadLink);

			writer.Int32(AutostartFiles.Count);
			foreach (var autostartFile in AutostartFiles)
			{
				writer.String(autostartFile);
			}
		}
		#endregion


		/// <summary>determine if user should be notified about the update process.</summary>
		public bool IsHiddenUpdate
		{
			get { return _isHiddenUpdate; }
			set { SetProperty(ref _isHiddenUpdate, value); }
		}
		/// <summary>returns the title which should be used by the window to present the update to the user.</summary>
		public string WindowTitle
		{
			get { return _windowTitle; }
			set { SetProperty(ref _windowTitle, value); }
		}
		/// <summary>Update description.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>Update changeset.</summary>
		public string Changeset
		{
			get { return _changeset; }
			set { SetProperty(ref _changeset, value); }
		}
		/// <summary>Update version.</summary>
		public string Version
		{
			get { return _version; }
			set { SetProperty(ref _version, value); }
		}
		/// <summary>Update file size.</summary>
		public UInt64 FileSize
		{
			get { return _fileSize; }
			set { SetProperty(ref _fileSize, value); }
		}
		/// <summary>Update download link.</summary>
		public string DownloadLink
		{
			get { return _downloadLink; }
			set { SetProperty(ref _downloadLink, value); }
		}
		/// <summary>Files which should be automatically executed after update installation.</summary>
		public List<String> AutostartFiles
		{
			get { return _autostartFiles ?? (_autostartFiles = new List<string>()); }
			set { SetProperty(ref _autostartFiles, value); }
		}

		/// <summary>Performs the update.</summary>
		public void Execute()
		{


			if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
				Application.Current.Dispatcher.BeginInvoke(new Action(Process));
			else
				Process();
		}

		private void Process()
		{
			var updateProcess = new UpdateProcess();
			updateProcess.Process(this);

		}
	}



	internal class UpdateProcess
	{
		private DirectoryInfo TmpFolder { get; set; }
		private DirectoryInfo ExtractionFolder { get; set; }
		private FileInfo CompressedFilePath { get; set; }
		private DirectoryInfo TargetFolder { get; set; }

		public void Process(CsopServerUpdateAvailable info)
		{
			if (ShouldBeExecuted(info) == false)
				return;

			Task t = new Task(() =>
			{
				TmpFolder = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tmp-" + Guid.NewGuid()));
				ExtractionFolder = new DirectoryInfo(Path.Combine(TmpFolder.FullName, "Extracted"));
				CompressedFilePath = new FileInfo(Path.Combine(TmpFolder.FullName, Guid.NewGuid().ToString()));
				// ReSharper disable once AssignNullToNotNullAttribute
				TargetFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
				try
				{
					DownloadFile(info.DownloadLink);
					DecompressFile();
					StartInstall(info.AutostartFiles, info.IsHiddenUpdate);
					CloseApp();
				}
				catch (Exception exc)
				{
					CsOnline.SendAsync.Exception(exc);
				}
			}, TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
				


		}

		private bool ShouldBeExecuted(CsopServerUpdateAvailable info)
		{
			if (info.IsHiddenUpdate)
				return true;
			if (CsGlobal.Message.Push(info, CsMessage.Types.Information, info.WindowTitle, CsMessage.MessageButtons.OkCancel) == CsMessage.MessageResults.Cancel)
				return false;
			return true;
		}

		private void DownloadFile(string downloadlink)
		{
			CompressedFilePath.CreateDirectory_IfNotExists();

			var wc = new WebClient();
			var linkAddress = new Uri(downloadlink);
			wc.DownloadFile(linkAddress, CompressedFilePath.FullName);
		}

		private void DecompressFile()
		{
			CsGlobal.Storage.Compression.Zip.Decompress(CompressedFilePath.FullName, ExtractionFolder.FullName);
		}

		private void StartInstall(List<string> autostartFiles, bool hidden)
		{
			var commands = new List<string>();
			commands.Add("xcopy \"" + ExtractionFolder + "\" \"" + TargetFolder.FullName + "\" /E /R /Y /J");
			commands.Add("rd /S /Q \"" + TmpFolder.FullName + "\"");

			foreach (var file in autostartFiles)
			{
				commands.Add("start \"\" \"" + Path.Combine(TargetFolder.FullName, file) + "\"");
			}

			if (NeedPrivileges())
			{
				if (hidden)
					CsGlobal.App.OnExit += args => CsGlobal.RunExternal.Cmd.Hidden.Timed.ElevatedCommand(commands, 6000);
				else
					CsGlobal.RunExternal.Cmd.Hidden.Timed.ElevatedCommand(commands, 6000);
			}
			else
			{
				if (hidden)
					CsGlobal.App.OnExit += args => CsGlobal.RunExternal.Cmd.Hidden.Timed.Command(commands, 6000); 
				else
					CsGlobal.RunExternal.Cmd.Hidden.Timed.Command(commands, 6000);
			}
		}

		private void CloseApp()
		{
			CsGlobal.App.Exit();
		}

		private bool NeedPrivileges()
		{
			try
			{
				using (File.Create(Path.Combine(TargetFolder.FullName, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
				{
				}
				return false;
			}
			catch
			{
				return true;
			}
		}
	}
}