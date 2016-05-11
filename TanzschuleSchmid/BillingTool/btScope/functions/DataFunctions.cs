// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using BillingTool.btScope.functions.data;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class DataFunctions : Base
	{
		private static DataFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static DataFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new DataFunctions());
				}
			}
		}

		private DataFunctions()
		{
		}

		private bool NonFinalizedChangesExists()
		{
			return
				BelegData.HasNonFinalizedRows ||
				OutputFormat.HasNonFinalizedRows ||
				BelegPosten.HasNonFinalizedRows ||
				Posten.HasNonFinalizedRows ||
				Steuersatz.HasNonFinalizedRows ||
				MailedBeleg.HasNonFinalizedRows ||
				PrintedBeleg.HasNonFinalizedRows;

		}

		/// <summary>collapses functions for the specified row object.</summary>
		public BelegDataFunctions BelegData => BelegDataFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public OutputFormatFunctions OutputFormat => OutputFormatFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public BelegPostenFunctions BelegPosten => BelegPostenFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public PostenFunctions Posten => PostenFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public SteuersatzFunctions Steuersatz => SteuersatzFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public MailedBelegFunctions MailedBeleg => MailedBelegFunctions.I;
		/// <summary>collapses functions for the specified row object.</summary>
		public PrintedBelegFunctions PrintedBeleg => PrintedBelegFunctions.I;





		/// <summary>Ensures synchronization with file.</summary>
		public void RejectAllChanges()
		{
			Bt.Db.Billing.RejectChanges();
		}
		/// <summary>Ensures synchronization with file.</summary>
		public void SyncAnabolicChanges()
		{
			if (NonFinalizedChangesExists())
				throw new InvalidOperationException("There are not finalized changes pending");

			Bt.Db.Billing.SaveAnabolic();
			Bt.Db.Billing.AcceptChanges();
		}
		/// <summary>Ensures synchronization with file.</summary>
		public void SyncKatabolicChanges()
		{
			if (NonFinalizedChangesExists())
				throw new InvalidOperationException("There are unfinalized changes pending");

			Bt.Db.Billing.SaveKatabolic();
			Bt.Db.Billing.AcceptChanges();
		}
		/// <summary>Ensures synchronization with file.</summary>
		public void SyncChanges()
		{
			if (NonFinalizedChangesExists())
				throw new InvalidOperationException("There are not finalized changes pending");

			Bt.Db.Billing.SaveUnspecific();
			Bt.Db.Billing.AcceptChanges();
		}
	}
}