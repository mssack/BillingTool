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





namespace CsWpfBase.Themes.Controls.Editors
{
#pragma warning disable 1591
	public class DPathEditor : PathEditor<DirectoryInfo>
	{
		static DPathEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DPathEditor), new FrameworkPropertyMetadata(typeof (DPathEditor)));
		}


		#region Overrides
		public override string Convert(DirectoryInfo path)
		{
			return path.FullName;
		}
		public override DirectoryInfo Convert(string path)
		{
			return new DirectoryInfo(path);
		}
		public override bool Exists(DirectoryInfo path)
		{
			return path.Exists;
		}
		public override string OpenDialog(IWin32Window owner, string initialPath, out bool canceled)
		{
			var fbd = new FolderBrowserDialog
					{
						SelectedPath = initialPath,
						ShowNewFolderButton = true,
					};
			var result = fbd.ShowDialog(owner);

			if (result != DialogResult.OK)
			{
				canceled = true;
				return null;
			}
			canceled = false;
			return fbd.SelectedPath;
		}
		#endregion
	}
}