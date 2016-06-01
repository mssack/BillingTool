// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class Steuersatz : IStoreComment
	{
		#region Overrides/Interfaces
		/// <summary>sets the value of a column and notify property changed.</summary>
		public override bool SetDbValue<T>(T m, string columnName, [CallerMemberName] string propName = "")
		{
			if (!base.SetDbValue(m, columnName, propName))
				return false;

			if (propName == nameof(Comment))
			{
				//change last changed date on comment change.
				CommentLastChanged = DateTime.Now;
			}

			return true;
		}
		#endregion


		/// <summary>returns true if this element has been used before.</summary>
		[DependsOn(nameof(LastUsedDate))]
		public bool HasBeenUsed => LastUsedDate != null;
	}
}