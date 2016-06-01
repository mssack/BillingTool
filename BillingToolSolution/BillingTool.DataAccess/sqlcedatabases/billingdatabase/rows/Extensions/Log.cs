// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class Log : IStoreComment
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


		/// <summary>[<c>BillingDatabase</c>].[<c>Logs</c>].[<c>TypeName</c>] (Type = <c>nvarchar</c>, Default = '<c>('Information')</c>', MaxLength = <c>100</c>
		///     )</summary>
		[DependsOn("TypeName")]
		public LogTypes Type
		{
			get
			{
				LogTypes result;
				return Enum.TryParse(TypeName, true, out result) ? result : LogTypes.Error;
			}
			set { TypeName = value.ToString(); }
		}
	}
}