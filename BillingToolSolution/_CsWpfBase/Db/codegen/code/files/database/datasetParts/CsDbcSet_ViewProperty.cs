// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datasetParts
{
	/// <summary>Template for a view property inside a data set.</summary>
	[Serializable]
	// ReSharper disable once InconsistentNaming
	internal class CsDbcSet_ViewProperty : FileTemplate
	{
		public CsDbcSet_ViewProperty(CsDbCodeDataView @base)
		{
			Base = @base;
		}

		public CsDbCodeDataView Base { get; }


		[Key]
		private string Name => Base.PropertyName;


		[Key]
		private string Type => Base.Name;


		[Key]
		private string NativeViewName => Base.NativeName;


		[Key]
		private string DatabaseName => Base.Architecture.Owner.Name;
	}
}