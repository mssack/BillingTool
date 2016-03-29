// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.resource
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgResourcesPath : Base
	{
		#region SINGLETON CLASS
		private static CsgResourcesPath _instance;
		private static readonly object SingletonLock = new object();
		private CsgResourcesPath()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgResourcesPath I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgResourcesPath());
				}
			}
		}
		#endregion


		/// <summary>returns the application internal address of a embedded resource.</summary>
		public string Get(string assemblyname, string path)
		{
			if (assemblyname == null)
				throw new InvalidOperationException();
			if (String.IsNullOrEmpty(path))
				throw new InvalidOperationException();
			if (path.StartsWith("/"))
				path = path.Substring(1);

			return "pack://application:,,,/" + assemblyname + ";component/" + path;
		}
	}
}