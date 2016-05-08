// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Linq;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.Windows;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions
{
	/// <summary>The <see cref="Bt.UiFunctions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class UiFunctions : Base
	{
		private static UiFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static UiFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new UiFunctions());
				}
			}
		}

		private UiFunctions()
		{
		}


		/// <summary>
		///     Opens a window with an <paramref name="title" /> and a specific <paramref name="text" />. This window ensures that the user knows what he does.
		///     The method returns true if the user passed all verification mechanism.
		/// </summary>
		public bool CheckOperatorsTrustAbility(string title, string text)
		{
			var wind = new CheckTrustAbilityWindow(title, text);
			wind.ShowDialog();
			return wind.HasBeenValidated;
		}

		/// <summary>Processes all unprocessed <see cref="MailedBeleg" /> or <see cref="PrintedBeleg" /> for a specific <paramref name="data" /> object.</summary>
		public void ProcessAllUnprocessed(BelegData data)
		{
			var outputWindow = new Window_BelegData_ProcessOutput(data) {Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive)};
			outputWindow.ShowDialog();
		}
	}
}