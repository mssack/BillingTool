// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.functions.data.basis;






namespace BillingTool.btScope.functions.data
{
	/// <summary>The <see cref="Bt.Data" /> scope. Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public class BelegPostenFunctions : DataFunctionsBase<BelegPosten>
	{
		private static BelegPostenFunctions _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static BelegPostenFunctions I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new BelegPostenFunctions());
				}
			}
		}

		private BelegPostenFunctions()
		{
		}


		#region Overrides/Interfaces
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected override void FinalizeAction(BelegPosten item)
		{
			item.Posten.AnzahlGekauft = item.Posten.AnzahlGekauft + item.Anzahl;
			if (item.Anzahl < 0)
				item.Posten.AnzahlStorniert = item.Posten.AnzahlStorniert + -item.Anzahl;

			item.Posten.LastUsedDate = DateTime.Now;
			item.Steuersatz.LastUsedDate = DateTime.Now;

			Bt.Data.Posten.TryFinalize(item.Posten);
			Bt.Data.Steuersatz.TryFinalize(item.Steuersatz);
		}

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected override void ValidationAction(BelegPosten item)
		{
			if (item.Anzahl == 0)
				throw new ArgumentException($"The {nameof(item)}.{nameof(item.Anzahl)} cannot be zero(0). This is an illegal operation.");
		}
		#endregion


		/// <summary>Creates a new <see cref="BelegPosten" /> and add it to the database.</summary>
		/// <param name="item">The owning <see cref="BelegData" />.</param>
		/// <param name="anzahl">...</param>
		/// <param name="posten">...</param>
		/// <param name="steuersatz">...</param>
		public BelegPosten New(BelegData item, int anzahl, Posten posten, Steuersatz steuersatz)
		{
			if (item == null)
				throw new ArgumentException($"The parameter {nameof(item)} can not be null.", nameof(item));
			if (anzahl == 0)
				throw new ArgumentException($"Die {nameof(anzahl)} can not be zero(0).", nameof(item));
			if (posten == null)
				throw new ArgumentException($"The parameter {nameof(posten)} can not be null.", nameof(item));
			if (steuersatz == null)
				throw new ArgumentException($"The parameter {nameof(steuersatz)} can not be null.", nameof(item));

			var newItem = item.DataSet.BelegPostens.NewRow();
			newItem.Id = Guid.NewGuid();
			newItem.Data = item;
			newItem.Anzahl = anzahl;
			newItem.Posten = posten;
			newItem.Steuersatz = steuersatz;
			newItem.Table.Add(newItem);

			Bt.Data.BelegData.UpdateBetragData(item);

			NonFinalized_Add(newItem);
			return newItem;
		}

		/// <summary>Deletes an existing <see cref="BelegPosten" />.</summary>
		/// <param name="item">The owning <see cref="BelegPosten" /> which need to be deleted.</param>
		public void Delete(BelegPosten item)
		{
			var belegData = item.Data;

			item.Delete();

			Bt.Data.BelegData.UpdateBetragData(belegData);
			NonFinalized_TryRemove(item);
		}


	}
}