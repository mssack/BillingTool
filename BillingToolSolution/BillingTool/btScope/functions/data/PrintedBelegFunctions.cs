// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class PrintedBelegFunctions : DataFunctionsBase<PrintedBeleg>
	{
		private static PrintedBelegFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static PrintedBelegFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new PrintedBelegFunctions());
				}
			}
		}

		private PrintedBelegFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected override void FinalizeAction(PrintedBeleg item)
		{
			Bt.Data.OutputFormat.TryFinalize(item.OutputFormat);
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(PrintedBeleg item)
		{
		}
		#endregion


		/// <summary>Appends a new <see cref="PrintedBeleg" /> to the <paramref name="data" />.</summary>
		public PrintedBeleg New(BelegData data)
		{
			var newItem = data.DataSet.PrintedBelege.NewRow();
			newItem.ProcessingState = ProcessingStates.NotProcessed;
			newItem.BelegData = data;
			newItem.PrinterDevice = Bt.Config.LocalSettings.DefaultPrinter;
			newItem.OutputFormat = newItem.DataSet.OutputFormats.Default_PrintFormat;
			newItem.Table.Add(newItem);

			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>deletes the <see cref="PrintedBeleg" />.</summary>
		public void Delete(PrintedBeleg item)
		{
			if (item.ProcessingState != ProcessingStates.NotProcessed)
				throw new InvalidOperationException($"The {nameof(PrintedBeleg)} has already been printed and therefore cannot be deleted.");

			NonFinalized_TryRemove(item);
			item.Delete();
		}
	}
}