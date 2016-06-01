// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using System.IO;
using System.Text.RegularExpressions;
using BillingTool.btScope.configuration;
using BillingTool.btScope.versioning.buildData;
using BillingTool.btScope.versioning.updates;
using BillingTool.Exceptions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.versioning
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class BtVersioning : Base
	{
		private static BtVersioning _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BtVersioning I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BtVersioning());
				}
			}
		}

		private BtVersioning()
		{
		}

		/// <summary>Gets current build details.</summary>
		public BuildData Build => BuildData.I;

		/// <summary>Checks if update is necessary and updates the data portion.</summary>
		public bool DoUpdates()
		{

			if (ConfigFile_LocalSettings.FileName.Exists && Build.Version < new BuildVersion(Bt.Config.LocalSettings.DataVersion))
				throw new BillingToolException(BillingToolException.Types.Invalid_DataVersion, $"Sie verwenden eine alte program version! Um die Daten lesen zu können benutzen sie bitte eine Version gleich oder größer {Bt.Config.LocalSettings.DataVersion}.");

			return DoRecursiveUpdates();
		}

		private bool DoRecursiveUpdates()
		{
			var currentDataVersion = GetCurrentDataRc();
			if (currentDataVersion == null || Build.Version.Equals(currentDataVersion))
				return false;

			var updateForRc = UpdateBase.GetUpdateForBuildVersion(currentDataVersion);
			if (updateForRc == null)
				return false;

			updateForRc.Run();
			DoRecursiveUpdates();
			return true;
		}

		private BuildVersion GetCurrentDataRc()
		{
			if (File.Exists(RC66_To_Next_Updater.KasseneinstellungenFilePath))
				return new BuildVersion("RC66");

			ConfigFile_LocalSettings.FileName.Refresh();
			if (!ConfigFile_LocalSettings.FileName.Exists)
				return null;

			var match = Regex.Match(ConfigFile_LocalSettings.FileName.LoadAs_UTF8String(), "DataVersion\\s*?=\\s*?(.*)");
			if (!match.Success || match.Groups.Count != 2)
				throw new BillingToolException(BillingToolException.Types.No_DataVersionFound, $"Es fehlt der Parameter 'DataVersion = RC??' im File [{ConfigFile_LocalSettings.FileName.FullName}].");
			try
			{
				return new BuildVersion(match.Groups[1].Value);
			}
			catch (Exception exc)
			{
				throw new BillingToolException(BillingToolException.Types.No_DataVersionFound, exc.MostInner().Message);
			}

		}
	}
}