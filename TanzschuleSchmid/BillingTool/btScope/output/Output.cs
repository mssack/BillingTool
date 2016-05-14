// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Threading.Tasks;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingOutput.btOutputScope;






namespace BillingTool.btScope.output
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Output
	{
		private static Output _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Output I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Output());
				}
			}
		}

		private Output()
		{
		}


		/// <summary>Processes all <see cref="ProcessingStates.NotProcessed" /> items within the <see cref="BelegData" />.</summary>
		public Task<Task[]> DoOpenedExportsAsync(BelegData data)
		{
			return BtOutput.Process(data, Bt.Config.File.KassenEinstellung).ContinueWith(t =>
			{
				Bt.Data.SyncChanges();
				return t.Result;
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}