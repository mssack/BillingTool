// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Attributes;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions
{
	/// <summary>The reasons why an row of type <see cref="BelegData" /> can be invalid.</summary>
	public enum BelegDataInvalidReasons
	{
		/// <summary>The <see cref="BelegData" /> is not invalid.</summary>
		[EnumDescription("Gültig", "Der Beleg ist gültig.")] Valid = 0,


		/// <summary>The <see cref="BelegData.Typ" /> is not entered.</summary>
		[EnumDescription("Fehlender Typ", "Der Typ des Beleges muss ausgefüllt sein.")] Missing_BelegDataType = 1,


		/// <summary>The <see cref="BelegData.KassenOperator" /> is not entered.</summary>
		[EnumDescription("Fehlender Kassenoperator", "Der zuständige Kassenoperator ist nicht eingetragen.")] Missing_Kassenoperator = 2,


		/// <summary>The <see cref="BelegData.StornoBeleg" /> is not entered.</summary>
		[EnumDescription("Fehlender Stornobeleg", "Bei dem Storno eines Beleges muss der entsprechende Beleg angeführt sein")] Missing_StornoBeleg = 3,
	}
}