// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using System.IO;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;
// ReSharper disable UnusedAutoPropertyAccessor.Local






namespace BillingTool.btScope.versioning.buildData
{
	/// <summary>Ctor</summary>
	public class BuildData : ConfigFileBase
	{
		private static BuildData _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BuildData I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BuildData());
				}
			}
		}

		/// <summary>Returns the singleton instance</summary>
		public static BuildData LoadFromFile(FileInfo fi)
		{
			return new BuildData(fi);
		}

		private BuildVersion _version;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private BuildData(FileInfo path) : base(path)
		{
			Load();
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private BuildData() : base(new Uri(CsGlobal.Storage.Resource.Path.Get(nameof(BillingTool), $"{nameof(btScope)}/{nameof(versioning)}/{nameof(buildData)}/{nameof(BuildData)}.txt"), UriKind.RelativeOrAbsolute))
		{
			Load();
		}

		/// <summary>Gets the current build time.</summary>
		[Key(StringFormat = "yyyy.MM.dd HH:mm:ss")]
		public DateTime Time { get; private set; }
		/// <summary>Gets the user which invoked the build.</summary>
		[Key]
		public string User { get; private set; }
		/// <summary>Gets the computer which invoked the build.</summary>
		[Key]
		public string Machine { get; private set; }

		/// <summary>The version of the build.</summary>
		public BuildVersion Version => _version ?? (_version = new BuildVersion(ActiveDevNumber, GoldNumber));

		/// <summary>Gets the name of the current RC.</summary>
		public string NameWithDate => $"{Version.Name} vom {Time.ToString("dd.MM.yyyy u\\m HH:mm")}";
		/// <summary>Gets the name of the current RC.</summary>
		public string NameWithDateForIO => $"{Version.Name} vom {Time.ToString("dd.MM.yyyy u\\m HH.mm")}";


		/// <summary>Gets the current build number.</summary>
		[Key]
		private int ActiveDevNumber { get; set; }
		/// <summary>Gets the current build number2 used for Gold Branch manipulation.</summary>
		[Key]
		private int GoldNumber { get; set; }


	}
}