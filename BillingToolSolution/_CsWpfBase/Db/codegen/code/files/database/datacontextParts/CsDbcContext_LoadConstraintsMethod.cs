using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datacontextParts
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbcContext_LoadConstraintsMethod : FileTemplate
	{
		public CsDbcContext_LoadConstraintsMethod(CsDbCodeBundle codeBundle)
		{
			CodeBundle = codeBundle;
		}

		private CsDbCodeBundle CodeBundle { get; }

		[Key]
		private string Items => CodeBundle.Databases.Select(x => $"{x.DataSet.Name}.LoadConstraints();").Join("\r\n\t");
		[Key]
		private string DbContextNativeName => CodeBundle.Architecture.Name;
	}
}
