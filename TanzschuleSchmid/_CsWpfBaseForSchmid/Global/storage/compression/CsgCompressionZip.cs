// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.IO;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.storage.compression.zip;






namespace CsWpfBase.Global.storage.compression
{
	/// <summary>
	///     <see cref="CsGlobal" />
	/// </summary>
	public class CsgCompressionZip
	{
		private static CsgCompressionZip _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgCompressionZip I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgCompressionZip());
				}
			}
		}

		private CsgCompressionZip()
		{
		}

		/// <summary>Decompresses a zip file into the specified target directory.</summary>
		public void Decompress(string filename, string targetDirectory)
		{
			using (var zipper = ZipStorer.Open(filename, FileAccess.Read))
			{
				var files = zipper.ReadCentralDir();
				foreach (var file in files)
				{
					var targetFile = new FileInfo(Path.Combine(targetDirectory, file.FilenameInZip));
					targetFile.CreateDirectory_IfNotExists();
					targetFile.DeleteFile_IfExists();
					zipper.ExtractFile(file, targetFile.FullName);
				}
			}
		}
	}
}