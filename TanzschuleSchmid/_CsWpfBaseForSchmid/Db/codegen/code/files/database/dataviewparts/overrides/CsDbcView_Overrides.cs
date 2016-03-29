// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.dataviewparts.overrides
{
	/// <summary>Collapses the override function inside a view.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcView_Overrides : FileTemplate
	{
		internal CsDbcView_Overrides(CsDbCodeDataView view)
		{
			View = view;
		}

		private CsDbCodeDataView View { get; }
		[Key]
		private string NativeName => View.NativeName;
		[Key]
		private string NativeNameConstant => View.NativeNameConstant;
	}
}