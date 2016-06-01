// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;






namespace CsWpfBase.Db.codegen.architecture.generators.bases
{
	internal interface IGenerateCsDbArcContext
	{
		#region Abstract
		/// <summary>Generates the architecture.</summary>
		CsDbArchitecture Generate();
		#endregion
	}
}