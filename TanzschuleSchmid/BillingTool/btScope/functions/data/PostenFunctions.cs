// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.configuration.commandLine;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class PostenFunctions : DataFunctionsBase<Posten>
	{
		private static PostenFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static PostenFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new PostenFunctions());
				}
			}
		}

		private PostenFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected override void FinalizeAction(Posten item)
		{

		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(Posten item)
		{

		}
		#endregion


		/// <summary>Get or creates a <see cref="Posten" /> from the database specified by a template.</summary>
		public Posten GetOrNew_FromTemplate(CommandLine_BelegPostenTemplate template)
		{
			var newItem = Bt.Db.Billing.Postens.FindOrLoad_By_NameAndPreis(template.Name, template.BetragBrutto);
			if (newItem == null)
			{
				newItem = Bt.Db.Billing.Postens.NewRow();
				newItem.CreationDate = DateTime.Now;
				newItem.Name = template.Name;
				newItem.PreisBrutto = template.BetragBrutto;
				newItem.Table.Add(newItem);

				NonFinalized_Add(newItem);
			}
			return newItem;
		}

	}
}