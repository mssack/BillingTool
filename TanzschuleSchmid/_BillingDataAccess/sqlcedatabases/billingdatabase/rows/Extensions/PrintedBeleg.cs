﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class PrintedBeleg : IOutputBeleg, IStoreComment
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

		/// <summary>$"[{nameof(PrintedBeleg)}, Beleg Nr = '{BelegData.Nummer}', State = '{ProcessingStateName}', Format = '{OutputFormatName}']"</summary>
		public override string ToString()
		{
			if (BelegData == null)
				return $"{nameof(PrintedBeleg)} [Hash = {GetHashCode()}]";
			return $"[{nameof(PrintedBeleg)}, Beleg Nr = '{BelegData.Nummer}', Status = '{ProcessingStateNumber}', Format = '{OutputFormatId}']";
		}



		/// <summary>The wrapper property for column property <see cref="ProcessingStateNumber" />.</summary>
		[DependsOn(nameof(ProcessingStateNumber))]
		public ProcessingStates ProcessingState
		{
			get { return EnumWrapper.Get(ProcessingStateNumber, ProcessingStates.Unknown); }
			set { EnumWrapper.Set(() => ProcessingStateNumber = (int)value); }
		}
		#endregion
	}
}