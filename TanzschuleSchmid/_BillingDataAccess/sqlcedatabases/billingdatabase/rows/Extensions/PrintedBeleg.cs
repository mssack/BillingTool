// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;






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
			return $"[{nameof(PrintedBeleg)}, Beleg Nr = '{BelegData.Nummer}', State = '{ProcessingStateName}', Format = '{OutputFormatName}']";
		}

		/// <summary>Applys the database extended default values, described by developer, to the row.</summary>
		public override void ApplyExtendedDefaults()
		{
			base.ApplyExtendedDefaults();
			OutputFormat = OutputFormats.StandardBonV1;
		}


		/// <summary>The wrapper property for column property <see cref="ProcessingStateName" />.</summary>
		[DependsOn(nameof(ProcessingStateName))]
		public ProcessingStates ProcessingState
		{
			get
			{
				ProcessingStates val;
				return Enum.TryParse(ProcessingStateName, true, out val) ? val : ProcessingStates.Unknown;
			}
			set { ProcessingStateName = value.ToString(); }
		}



		/// <summary>The wrapper property for column property <see cref="OutputFormatName" />.</summary>
		[DependsOn(nameof(OutputFormatName))]
		public OutputFormats OutputFormat
		{
			get
			{
				OutputFormats val;
				return Enum.TryParse(OutputFormatName, true, out val) ? val : OutputFormats.Unknown;
			}
			set { OutputFormatName = value.ToString(); }
		}
		#endregion
	}
}