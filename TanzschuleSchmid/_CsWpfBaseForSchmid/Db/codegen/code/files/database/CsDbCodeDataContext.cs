// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture;
using CsWpfBase.Db.codegen.code.files.database.datacontextParts;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>A file for a DbContext class.</summary>
	[Serializable]
	internal class CsDbCodeDataContext : FileTemplate
	{
		private CsDbcContext_DataSetProperty[] _dataSetProperties;
		private CsDbcContext_SetDbProxyMethod _connectDirectMethod;
		private CsDbcContext_GetDatabaseByNameMethod _getDatabaseByNameMethod;
		private CsDbcContext_LoadConstraintsMethod _loadConstraintsMethod;

		/// <summary>Creates a new db context code file.</summary>
		public CsDbCodeDataContext(CsDbCodeBundle codeBundle)
		{
			CodeBundle = codeBundle;
		}

		public CsDbArchitecture Architecture => CodeBundle.Architecture;
		public CsDbCodeBundle CodeBundle { get; }


		public CsDbcContext_DataSetProperty[] DataSetProperties => _dataSetProperties ?? (_dataSetProperties = CodeBundle.Databases.Select(x => new CsDbcContext_DataSetProperty(CodeBundle, x.DataSet)).ToArray());
		public CsDbcContext_SetDbProxyMethod SetDbProxyMethod => _connectDirectMethod ?? (_connectDirectMethod = new CsDbcContext_SetDbProxyMethod(CodeBundle));
		public CsDbcContext_GetDatabaseByNameMethod GetDatabaseByNameMethod => _getDatabaseByNameMethod ?? (_getDatabaseByNameMethod = new CsDbcContext_GetDatabaseByNameMethod(CodeBundle));
		public CsDbcContext_LoadConstraintsMethod LoadConstraintsMethod => _loadConstraintsMethod ?? (_loadConstraintsMethod = new CsDbcContext_LoadConstraintsMethod(CodeBundle));

		[Key]
		public string Name => $"{CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, false)}Context";

		[Key]
		public string FullQualifiedName => $"{CodeBundle.NameSpace}.{Name}";

		[Key]
		public string NativeNameConstant => "Name";

		[Key]
		private string NativeName => Architecture.Name;

		[Key(IncludeInHash = false)]
		private string CsDbDataContextAttribute => new CsDbDataContextAttribute {Name = NativeName, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode();

		[Key]
		private string DebuggerDisplayAttribute => $"[DebuggerDisplay(\"DBContext[{Architecture.Name}]\")]";


		[Key(Name = "DataSetProperties")]
		private string TmpDataSetProperties => DataSetProperties.Select(x => x.GetString(1)).Join("\r\n\t");

		[Key(Name = "SetDbProxyMethod")]
		private string TmpSetDbProxyMethod => SetDbProxyMethod.GetString(1);
		[Key(Name = "LoadConstraintsMethod")]
		private string TmpLoadConstraintMethod => LoadConstraintsMethod.GetString(1);

		[Key(Name = "GetDatabaseByNameMethod")]
		private string TmpGetDatabaseByNameMethod => GetDatabaseByNameMethod.GetString(1);

		[Key]
		private string DataSetPropertyFields => DataSetProperties.Select(x => x.FieldDecleration).Join("\r\n\t");
	}
}