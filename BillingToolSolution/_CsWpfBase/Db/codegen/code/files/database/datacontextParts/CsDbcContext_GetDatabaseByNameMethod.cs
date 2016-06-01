using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datacontextParts
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbcContext_GetDatabaseByNameMethod : FileTemplate
	{
		public CsDbcContext_GetDatabaseByNameMethod(CsDbCodeBundle codeBundle)
		{
			CodeBundle = codeBundle;
		}

		private CsDbCodeBundle CodeBundle { get; }

		[Key]
		private string Items => CodeBundle.Context.DataSetProperties.Select(x => $"case {x.DataSet.Name}.{x.DataSet.DatabaseNameProperty}:\r\n\t\t\treturn {x.Name};").Join("\r\n\t\t");
		[Key]
		private string DbContextNativeName => CodeBundle.Architecture.Name;
	}
}
