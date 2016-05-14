// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-15</date>

using System;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.Exceptions
{

	// ReSharper disable InconsistentNaming



	/// <summary>The exception message which is used whenever the billing tool throws an exception.</summary>
	internal class BillingToolException : Exception
	{
		public Types Type { get; private set; }


		/// <summary>Creates a new billing tool exception.</summary>
		/// <param name="type">The type of the exception.</param>
		/// <param name="message">The message of the exception.</param>
		/// <param name="inner">inner exception</param>
		public BillingToolException(Types type, string message, Exception inner = null) : base($"Fehler [{type.GetDescription()}] -> '{message}'", inner)
		{
			Type = type;
		}



		/// <summary>Billing tool exception types</summary>
		[Serializable]
		public enum Types
		{
			Undefined,
			No_DatabaseAvailable = ExitCodes.No_DatabaseAvailable,
			No_ValidConfiguration = ExitCodes.No_ValidConfiguration,
			No_DatabaseConnectionPossible = ExitCodes.No_DatabaseConnectionPossible,
			Invalid_StartupParam = ExitCodes.Invalid_StartupParam,
			No_KassenOperator = ExitCodes.No_KassenOperator,
		}
	}
}