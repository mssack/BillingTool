// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.methods
{
	/// <summary>This method is used to invalidate a complete row.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_BundledDataMethods : FileTemplate
	{
		internal CsDbcTableRow_BundledDataMethods(CsDbCodeDataRow owner)
		{
			Owner = owner;
		}
		
		[Key]
		private string DataSetName => Owner.CodeBundle.DataSet.Name;



		private CsDbCodeDataRow Owner { get; }
	}
}