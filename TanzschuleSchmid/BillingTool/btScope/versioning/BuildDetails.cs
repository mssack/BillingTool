// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.IO;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.versioning
{
	/// <summary>Ctor</summary>
	public class BuildDetails : ConfigFileBase
	{
		private static BuildDetails _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BuildDetails I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BuildDetails());
				}
			}
		}
		/// <summary>Returns the singleton instance</summary>
		public static BuildDetails LoadFromFile(FileInfo fi)
		{
			return new BuildDetails(fi);
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private BuildDetails(FileInfo path) : base(path)
		{
			Load();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private BuildDetails() : base(new Uri(CsGlobal.Storage.Resource.Path.Get(nameof(BillingTool), $"{nameof(btScope)}/{nameof(versioning)}/{nameof(BuildDetails)}.txt"), UriKind.RelativeOrAbsolute))
		{
			Load();
		}

		/// <summary>Gets the current build time.</summary>
		[Key(StringFormat = "yyyy.MM.dd HH:mm:ss")]
		public DateTime Time { get; private set; }
		/// <summary>Gets the current build number.</summary>
		[Key]
		public int ActiveDevNumber { get; private set; }
		/// <summary>Gets the current build number2 used for Gold Branch manipulation.</summary>
		[Key]
		public int GoldNumber { get; private set; }
		/// <summary>Gets the user which invoked the build.</summary>
		[Key]
		public string User { get; private set; }
		/// <summary>Gets the computer which invoked the build.</summary>
		[Key]
		public string Computer { get; private set; }
	}
}