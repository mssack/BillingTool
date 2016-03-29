// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.datasetParts;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>A file for a table class.</summary>
	[Serializable]
	internal class CsDbCodeDataSet : FileTemplate
	{
		private string _name;


		/// <summary>Creates a new code set.</summary>
		public CsDbCodeDataSet(CsDbArcDatabase architecture, CsDbCodeBundleForDb codeBundle)
		{
			CodeBundle = codeBundle;
			Architecture = architecture;
		}

		/// <summary>The underlaying architecture.</summary>
		public CsDbArcDatabase Architecture { get; }
		/// <summary>Gets the owning code bundle.</summary>
		public CsDbCodeBundleForDb CodeBundle { get; }


		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, true)); }
			set { SetProperty(ref _name, value); }
		}

		/// <summary>Gets the full qualified type name.</summary>
		public string FullQualifiedName => $"{CodeBundle.DataSetNameSpace}.{Name}";


		[Key]
		private string DatabaseName => Architecture.Name;


		[Key(IncludeInHash = false)]
		private string CsDbDataSetAttribute => new CsDbDataSetAttribute {Name = DatabaseName, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode();

		[Key]
		private string DebuggerDisplayAttribute => $"[DebuggerDisplay(\"DB[{Architecture.Name}]: Tables[{{Tables.Count}}]\")]";


		[Key]
		private string DataContextType => CodeBundle.Owner.Context.FullQualifiedName;
		[Key]
		internal string DataContextPropertyName => "DataContext";
		[Key]
		internal string DataContextNativeNameProperty => CodeBundle.Owner.Context.NativeNameConstant;
		[Key]
		internal string DataContextNativeName => CodeBundle.Owner.Context.Name;
		[Key]
		internal string DatabaseNameProperty => "NativeName";


		[Key(Name = "TableProperties")]
		private string TmpTableProperties => CodeBundle.Tables.OrderBy(x => x.Name).Select(x => new CsDbcSet_TableProperty(x)).Select(x => x.GetString(1)).Join("\r\n\r\n\t");
		[Key(Name = "ViewProperties")]
		private string TmpViewProperties => CodeBundle.Views.OrderBy(x => x.Name).Select(x => new CsDbcSet_ViewProperty(x)).Select(x => x.GetString(1)).Join("\r\n\r\n\t");



		[Key]
		private string SaveAnabolicBody => CodeBundle.Tables.GroupBy(x => x.Architecture.Level).OrderBy(x => x.Key).SelectMany(x => x.OrderBy(y => y.PropertyName)).Select(x => $"if(Tables.Contains(\"{x.NativeName}\")) AddAnabolicChanges(targetSet, {x.PropertyName});").Join("\r\n\t\t");

		[Key]
		private string SaveKatabolicBody => CodeBundle.Tables.GroupBy(x => x.Architecture.Level).OrderBy(x => x.Key).Reverse().SelectMany(x => x.OrderBy(y => y.PropertyName)).Select(x => $"if(Tables.Contains(\"{x.NativeName}\")) AddKatabolicChanges(targetSet, {x.PropertyName});").Join("\r\n\t\t");

		[Key]
		private string LoadRelations => CodeBundle.Tables.SelectMany(x => x.Relations).Distinct().Select(x => new CsDbcSet_RelationEntry(x).GetString()).Join("\r\n\t\t");

		[Key]
		private string GetTableByNameSwitch => CodeBundle.Tables.Select(x => $"case \"{x.NativeName}\":\r\n\t\t\t\treturn {x.PropertyName};").Concat(CodeBundle.Views.Select(x => $"case \"{x.NativeName}\":\r\n\t\t\t\treturn {x.PropertyName};")).Join("\r\n\t\t\t");


		[Key]
		private string TableNames => CodeBundle.Tables.Select(x => $"\"{x.NativeName}\"").Join(", ");

		[Key]
		private string RelationsForReflection =>
			!CodeBundle.Architecture.Relations.Any()?"":
			"_csDbRelations = new[]{" +
			CodeBundle.Tables.SelectMany(t => t.Relations).Distinct().Select(r =>
			$"new CsDbRelation(" +
			$"typeof({CodeBundle.TablesNameSpace}.{r.PkKey.Row.Table.Name}), " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.PkKey.Row.Name}), " +
			$"typeof({CodeBundle.TablesNameSpace}.{r.FkKey.Row.Table.Name}), " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.FkKey.Row.Name}), " +
			$"\"{r.PkKey.Row.Table.NativeName}\", " +
			$"\"{r.PkKey.NativeAttributes.Name}\", " +
			$"\"{r.FkKey.Row.Table.NativeName}\", " +
			$"\"{r.FkKey.NativeAttributes.Name}\", " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.PkKey.Row.Name}).GetProperty(\"{r.PkKey.Name}\"), " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.FkKey.Row.Name}).GetProperty(\"{r.FkKey.Name}\"), " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.FkKey.Row.Name}).GetProperty(\"{r.AssociatedProperty.Name}\"), " +
			$"typeof({CodeBundle.RowsNameSpace}.{r.PkKey.Row.Name}).GetProperty(\"{r.ReferencingProperty.Name}\")" +
			$"),").Join("\r\n\t\t\t\t")
			+ "\t\t\t};";
	}
}