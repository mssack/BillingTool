// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using CsWpfBase.Ev.Exceptions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.storage.compression;
using CsWpfBase.Global.storage.configfile;
using CsWpfBase.Global.storage.resource;
using CsWpfBase.Global.storage.scopes;






namespace CsWpfBase.Global.storage
{
	/// <summary>see <see cref="CsGlobal" />.</summary>
	public sealed class CsgStorage : Base
	{
		private static CsgStorage _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgStorage I => _instance ?? (_instance = new CsgStorage());
		private readonly string _entryAssemblyName;
		private CsgStorageScope _private;
		private CsgStorageScope _public;

		private CsgStorage()
		{
			if (Application.Current != null)
				_entryAssemblyName = Assembly.GetEntryAssembly().GetName().Name;
		}

		/// <summary>A place for files where to user have access to this can help to backup or transfer application data.</summary>
		public CsgStorageScope Public
		{
			get
			{
				if (_public != null)
					return _public;

				if (!CsGlobal.IsInstalled(GlobalFunctions.Storage))
					throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.Storage);
				_public = new CsgStorageScope(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _entryAssemblyName)), ".txt");
				return _public;
			}
		}
		/// <summary>The private scope is meant to be as a storage place for files which the user can not change easily.</summary>
		public CsgStorageScope Private
		{
			get
			{
				if (_private != null)
					return _private;

				if (!CsGlobal.IsInstalled(GlobalFunctions.Storage))
					throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.Storage);

				_private = new CsgStorageScope(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _entryAssemblyName)), ".txt");
				return _private;
			}
		}
		/// <summary>config.txt in application folder path wrapper. Must be activated through <see cref="GlobalFunctions" />.</summary>
		public CsgConfigFile ConfigFile => CsgConfigFile.I;
		/// <summary>Embedded resource addressing and loading.</summary>
		public CsgResource Resource => CsgResource.I;
		/// <summary>Compression methods for storage.</summary>
		public CsgCompression Compression => CsgCompression.I;
	}
}