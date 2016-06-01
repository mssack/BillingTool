// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using BillingTool.btScope.configuration.control;
using BillingTool.btScope.functions.data.basis;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class SteuersatzFunctions : DataFunctionsBase<Steuersatz>
	{
		private static SteuersatzFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static SteuersatzFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new SteuersatzFunctions());
				}
			}
		}

		private SteuersatzFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected override void FinalizeAction(Steuersatz item)
		{
			Bt.Db.Billing.Configurations.DataIntegrity.LastSteuersatzKürzel = Convert.ToChar(item.Kürzel);
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(Steuersatz item)
		{

		}
		#endregion


		/// <summary>Get or creates a <see cref="Steuersatz" />.</summary>
		public Steuersatz New()
		{
			if (HasNonFinalizedRows)
				throw new NotFinalizedInstanceException();


			var newItem = Bt.Db.Billing.Steuersätze.NewRow();
			newItem.CreationDate = DateTime.Now;
			newItem.Kürzel = ((char) (Bt.Db.Billing.Configurations.DataIntegrity.LastSteuersatzKürzel + 1)).ToString();
			newItem.Table.Add(newItem);


			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>Get or creates a <see cref="Steuersatz" /> from the database specified by a template.</summary>
		public Steuersatz GetOrNew_FromTemplate(Control_BelegPostenTemplate template)
		{
			var newItem = Bt.Db.Billing.Steuersätze.FindOrLoad_By_Percent(template.Steuer);
			if (newItem == null)
			{
				if (HasNonFinalizedRows)
					throw new NotFinalizedInstanceException();

				newItem = Bt.Db.Billing.Steuersätze.NewRow();
				newItem.CreationDate = DateTime.Now;
				newItem.Percent = template.Steuer;
				newItem.Kürzel = ((char) (Bt.Db.Billing.Configurations.DataIntegrity.LastSteuersatzKürzel + 1)).ToString();
				newItem.Table.Add(newItem);

				NonFinalized_Add(newItem);
			}
			return newItem;
		}
	}
}