// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace BillingTool.btScope.administrator.licence
{
	/// <summary>contains the EULA.</summary>
	// ReSharper disable once InconsistentNaming
	public class EULAGerman : FileTemplate
	{
		[Key(FullName = "{PRODUKTNAME}")]
		private string ProductName => CsGlobal.App.Info.ProductTitle;
		[Key(FullName = "{Firmenname}")]
		private string FirmenName => CsGlobal.App.Info.Company;
	}
}