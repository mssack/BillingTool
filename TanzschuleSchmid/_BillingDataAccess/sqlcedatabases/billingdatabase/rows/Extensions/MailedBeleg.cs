// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class MailedBeleg : IOutputBeleg
	{
		#region Overrides/Interfaces
		/// <summary>$"[{nameof(PrintedBeleg)}, Beleg Nr = '{BelegData.Nummer}', State = '{ProcessingStateName}', Format = '{OutputFormatName}']"</summary>
		public override string ToString()
		{
			if (BelegData == null)
				return $"{nameof(MailedBeleg)} [Hash = {GetHashCode()}]";
			return $"[{nameof(MailedBeleg)}, Beleg Nr = '{BelegData.Nummer}', State = '{ProcessingStateName}', Format = '{OutputFormatName}']";
		}

		/// <summary>Applys the database extended default values, described by developer, to the row.</summary>
		public override void ApplyExtendedDefaults()
		{
			base.ApplyExtendedDefaults();
			OutputFormat = OutputFormats.StandardBonV1;
		}
		#endregion


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
	}
}