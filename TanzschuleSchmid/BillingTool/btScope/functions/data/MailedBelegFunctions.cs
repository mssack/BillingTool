// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class MailedBelegFunctions : DataFunctionsBase<MailedBeleg>
	{
		private static MailedBelegFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static MailedBelegFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new MailedBelegFunctions());
				}
			}
		}

		private MailedBelegFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected override void FinalizeAction(MailedBeleg item)
		{
			Bt.Data.OutputFormat.TryFinalize(item.OutputFormat);
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(MailedBeleg item)
		{

		}
		#endregion


		/// <summary>Appends a new <see cref="MailedBeleg" /> to the <paramref name="data" />.</summary>
		public MailedBeleg New(BelegData data, string targetMailAddress)
		{
			var newItem = data.DataSet.MailedBelege.NewRow();
			newItem.ProcessingState = ProcessingStates.NotProcessed;
			newItem.BelegData = data;
			newItem.TargetMailAddress = targetMailAddress;
			newItem.Betreff = data.DataSet.Configurations.Default.MailBetreff;
			newItem.Text = data.DataSet.Configurations.Default.MailText;
			newItem.OutputFormat = newItem.DataSet.OutputFormats.Default_MailFormat;
			newItem.Table.Add(newItem);
			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>deletes the <see cref="MailedBeleg" />.</summary>
		public void Delete(MailedBeleg item)
		{
			if (item.ProcessingState != ProcessingStates.NotProcessed)
				throw new InvalidOperationException($"The {nameof(MailedBeleg)} has already been printed and therefore cannot be deleted.");

			NonFinalized_TryRemove(item);
			item.Delete();
		}
	}



}