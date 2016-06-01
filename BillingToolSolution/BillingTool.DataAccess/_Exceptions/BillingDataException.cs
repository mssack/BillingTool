// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-15</date>

using System;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingToolDataAccess._Exceptions
{
	/// <summary>The exception message which is used whenever the billing data access module throws an exception.</summary>
	internal class BillingDataException : Exception
	{
		/// <summary>Creates a new billing data exception.</summary>
		/// <param name="type">The type of the exception.</param>
		/// <param name="message">The message of the exception.</param>
		public BillingDataException(Types type, string message) : base($"Fehler [{type.GetDescription()}] -> '{message}'")
		{

		}



		/// <summary>Billing data exception types</summary>
		[Serializable]
		public enum Types
		{

		}
	}
}