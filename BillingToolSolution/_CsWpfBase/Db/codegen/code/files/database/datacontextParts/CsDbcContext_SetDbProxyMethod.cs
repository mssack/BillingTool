using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datacontextParts
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbcContext_SetDbProxyMethod : FileTemplate
	{
		public CsDbcContext_SetDbProxyMethod(CsDbCodeBundle codeBundle)
		{
			CodeBundle = codeBundle;
		}

		private CsDbCodeBundle CodeBundle { get; }

		[Key]
		private string Items => CodeBundle.Databases.Select(x => $"{x.DataSet.Name}.Set_DbProxy<T>();").Join("\r\n\t");
		[Key]
		private string DbContextNativeName => CodeBundle.Architecture.Name;
	}
}
