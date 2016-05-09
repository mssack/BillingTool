// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.DataFunctions" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class OutputFormatFunctions : DataFunctionsBase<OutputFormat>
	{
		private static OutputFormatFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static OutputFormatFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new OutputFormatFunctions());
				}
			}
		}

		private OutputFormatFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid.</summary>
		protected override void FinalizeAction(OutputFormat item)
		{
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(OutputFormat item)
		{
		}
		#endregion


		/// <summary>Creates a new <see cref="OutputFormat" />.</summary>
		public OutputFormat New()
		{
			var newItem = Bt.Db.Billing.OutputFormats.NewRow();
			newItem.Id = Guid.NewGuid();
			newItem.CreationDate = DateTime.Now;
			newItem.BonLayout = BonLayouts.V1MailBon;
			newItem.Name = "Neues Layout";
			newItem.Table.Add(newItem);

			Notfinalized_Add(newItem);

			return newItem;
		}
	}
}