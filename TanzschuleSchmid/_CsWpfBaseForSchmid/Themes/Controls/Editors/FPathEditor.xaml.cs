// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using CsWpfBase.Themes.Controls.Editors.Base;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;





namespace CsWpfBase.Themes.Controls.Editors
{
#pragma warning disable 1591
	public class FPathEditor : PathEditor<FileInfo>
	{
		static FPathEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FPathEditor), new FrameworkPropertyMetadata(typeof (FPathEditor)));
		}


		#region Overrides
		public override string Convert(FileInfo path)
		{
			return path.FullName;
		}
		public override FileInfo Convert(string path)
		{
			return new FileInfo(path);
		}
		public override bool Exists(FileInfo path)
		{
			return path.Exists;
		}
		public override string OpenDialog(IWin32Window owner, string initialPath, out bool canceled)
		{
			var ofd = new OpenFileDialog
					{
						InitialDirectory = initialPath,
						CheckFileExists = !AllowNonExistingPath,
					};
			var result = ofd.ShowDialog(Window.GetWindow(this));

			if (result != true)
			{
				canceled = true;
				return null;
			}
			canceled = false;
			return ofd.FileName;
		}
		#endregion
	}
}