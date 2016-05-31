// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using System.Text.RegularExpressions;
using BillingTool.btScope.configuration;
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
		public BuildDetails BuildDetails => BuildDetails.I;

		/// <summary>Checks if update is necessary and updates the data portion.</summary>
		public bool DoUpdates()
		{
			var currentDataVersion = GetCurrentDataRc();
			if (currentDataVersion == null)
				return false;
			if (currentDataVersion.Equals(BuildDetails.ActiveDevNumber, BuildDetails.GoldNumber))
				return false;

			var updateForRc = UpdateBase.GetUpdateForRc(currentDataVersion);
			if (updateForRc == null)
				return false;

			updateForRc.Run();
			DoUpdates();
			return true;
		}


		private ReleaseCandidate GetCurrentDataRc()
		{
			ConfigFile_LocalSettings.FileName.Refresh();
			if (!ConfigFile_LocalSettings.FileName.Exists)
				return null;

			var match = Regex.Match(ConfigFile_LocalSettings.FileName.LoadAs_UTF8String(), "DataVersion\\s*?=\\s*?(.*)");
			if (!match.Success || match.Groups.Count != 2)
				throw new BillingToolException(BillingToolException.Types.No_DataVersionFound, $"Es fehlt der Parameter 'DataVersion = RC??' im File [{ConfigFile_LocalSettings.FileName.FullName}].");
			try
			{
				return new ReleaseCandidate(match.Groups[1].Value);
			}
			catch (Exception exc)
			{
				throw new BillingToolException(BillingToolException.Types.No_DataVersionFound, exc.MostInner().Message);
			}

		}
	}
}