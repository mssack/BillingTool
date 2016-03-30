// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Resources;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.resource
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgResourceFile : Base
	{
		#region SINGLETON CLASS
		private static CsgResourceFile _instance;
		private static readonly object SingletonLock = new object();
		private CsgResourceFile()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgResourceFile I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgResourceFile());
				}
			}
		}
		#endregion


		/// <summary>opens a stream to an embedded resource.</summary>
		public Stream OpenStream(string source)
		{
			return OpenStream(new Uri(source, UriKind.RelativeOrAbsolute));
		}
		/// <summary>opens a stream to an embedded resource.</summary>
		public Stream OpenStream(Uri source)
		{


			var streamResourceInfo = Application.GetResourceStream(source);
			return streamResourceInfo != null ? streamResourceInfo.Stream : null;
		}

		/// <summary>Reading the text of a file by a 'pack <see cref="Uri" />'.</summary>
		public string Read(Uri source, Encoding encoding = null)
		{
			using (var stream = OpenStream(source))
			{
				using (var reader = new StreamReader(stream, encoding ?? new UTF8Encoding()))
				{
					return reader.ReadToEnd();
				}
			}
		}
		/// <summary>Reading the text of a file by a 'pack <see cref="Uri" />'.</summary>
		public string Read(string source, Encoding encoding = null)
		{
			return Read(new Uri(source, UriKind.RelativeOrAbsolute), encoding);
		}
		/// <summary>Reading the text of a file by a 'pack <see cref="Uri" />'.</summary>
		public string Read(string assemblyName, string path, Encoding encoding = null)
		{
			if (Application.Current == null)
			{
				StreamResourceInfo sr1 = Application.GetResourceStream(new Uri(assemblyName + ";component/" + path, UriKind.Relative));
				var file = new StreamReader(sr1.Stream);
				return file.ReadToEnd();
			}
			return Read(new Uri(CsGlobal.Storage.Resource.Path.Get(assemblyName, path), UriKind.RelativeOrAbsolute), encoding);
		}
	}
}