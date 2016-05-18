// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.dataset
{
	partial class BillingDatabase
	{
		/// <summary>Initializes the database an generates all default values.</summary>
		public void Init()
		{
			if (!OutputFormats.HasBeenLoaded)
				OutputFormats.DownloadRows();
			if (!Steuersätze.HasBeenLoaded)
				Steuersätze.DownloadRows();
			if (!Configurations.HasBeenLoaded)
				Configurations.DownloadRows();


			if (!Configurations.IsInstalled)
			{

				// ReSharper disable once NotAccessedVariable

				// ReSharper disable RedundantAssignment
				var name = Steuersätze.Default_BetragSatzNormal.Name;
				name = Steuersätze.Default_BetragSatzErmäßigt1.Name;
				name = Steuersätze.Default_BetragSatzErmäßigt2.Name;
				name = Steuersätze.Default_BetragSatzNull.Name;
				name = Steuersätze.Default_BetragSatzBesonders.Name;

				name = OutputFormats.Default_PrintFormat.Name;
				name = OutputFormats.Default_MailFormat.Name;
				name = OutputFormats.Default_StornoFormat.Name;
				name = OutputFormats.Default_TagesBonFormat.Name;
				name = OutputFormats.Default_MonatsBonFormat.Name;
				name = OutputFormats.Default_JahresBonFormat.Name;
				// ReSharper restore RedundantAssignment

				Configurations.IsInstalled = true;
				SaveAnabolic();
				AcceptChanges();
			}

		}
	}
}