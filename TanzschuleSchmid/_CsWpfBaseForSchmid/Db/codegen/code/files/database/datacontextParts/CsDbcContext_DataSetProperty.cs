// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datacontextParts
{
	// ReSharper disable once InconsistentNaming
	internal class CsDbcContext_DataSetProperty : FileTemplate
	{
		public CsDbcContext_DataSetProperty(CsDbCodeBundle codeBundle, CsDbCodeDataSet dataSet)
		{
			DataSet = dataSet;
			CodeBundle = codeBundle;
		}

		public string FieldDecleration => $"private {DataSetType} {FieldName};";

		private CsDbCodeBundle CodeBundle { get; }
		internal CsDbCodeDataSet DataSet { get; }

		[Key]
		private string DataSetType => DataSet.Name;
		[Key]
		public string Name => DataSetType;
		[Key]
		private string FieldName => $"_{Name.ToLowerName()}";
		[Key]
		private string DataContextPropertyName => DataSet.DataContextPropertyName;
		[Key]
		private string Description => $"References the '[{CodeBundle.Architecture.Name}].[{DataSet.Architecture.Name}]' database.";
    }
}