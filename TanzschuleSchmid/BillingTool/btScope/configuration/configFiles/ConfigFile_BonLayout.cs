using System;
using System.IO;
using System.Windows.Media.Imaging;
using BillingOutput.Interfaces;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.configuration.configFiles
{
	/// <summary>DO NOT USE THIS CLASS DIRECTLY. Use <see cref="Bt" /> Scope instead.</summary>
	// ReSharper disable once InconsistentNaming
	public sealed class ConfigFile_BonLayout : ConfigFileBase, IContainBonLayout
	{
		#region SINGLETON CLASS
		private static ConfigFile_BonLayout _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static ConfigFile_BonLayout I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new ConfigFile_BonLayout(CsGlobal.Storage.Private.GetFilePathByName("Bon-Layout")));
				}
			}
		}
		#endregion

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_BonLayout(FileInfo path) : base(path)
		{
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		private ConfigFile_BonLayout(Uri packUri) : base(packUri)
		{
		}


		#region Overrides/Interfaces
		/// <summary>The header of the Bon.</summary>
		public BitmapSource KassenBonHeader { get; }
		/// <summary>The Footer of the Bon.</summary>
		public BitmapSource KassenBonFooter { get; }
		/// <summary>The header of the Bon.</summary>
		public string KassenBonHeaderText { get; }
		/// <summary>The header of the Bon.</summary>
		public string KassenBonFooterText { get; }
		#endregion
	}
}