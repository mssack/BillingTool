// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.dataviewparts.constants;
using CsWpfBase.Db.codegen.code.files.database.dataviewparts.overrides;
using CsWpfBase.Db.codegen.code.files.database.dataviewparts.row;
using CsWpfBase.Db.codegen.code.namingconventions;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>A file for a view class.</summary>
	[Serializable]
	internal class CsDbCodeDataView : FileTemplate
	{
		private CsDbcView_Constant[] _constants;
		private string _name;
		private string _pluralName;
		private string _singularName;
		private CsDbcView_Overrides _overrides;


		/// <summary>Creates a new code set.</summary>
		public CsDbCodeDataView(CsDbArcView architecture, CsDbCodeBundleForDb codebundle)
		{
			Architecture = architecture;
			CodeBundle = codebundle;
			Row = new CsDbcViewRow(this);
		}

		/// <summary>Gets the Owner.</summary>
		public CsDbCodeBundleForDb CodeBundle { get; }
		/// <summary>Gets the underlaying architecture.</summary>
		public CsDbArcView Architecture { get; }
		/// <summary>Gets the constants used to point to a native column name.</summary>
		public CsDbcView_Constant[] Constants => _constants ?? (_constants = Row.Columns.Select(x => new CsDbcView_Constant(x)).ToArray());
		/// <summary>Override functions</summary>
		public CsDbcView_Overrides Overrides => _overrides ?? (_overrides = new CsDbcView_Overrides(this));


		/// <summary>Gets or sets the SingularName.</summary>
		public string SingularName
		{
			get
			{
				if (_singularName != null) return _singularName;
				NamingConvention convention;
				if (Architecture.Owner.TableNameConventions.TryGetValue(NativeName, out convention))
				{
					_singularName = convention.Singular;
					_pluralName = convention.Plural;
				}
				else
				{
					_singularName = CsDb.CodeGen.Convert.ToMemberName(NativeName, true);
					_pluralName = CsDb.CodeGen.Convert.ToMemberName(NativeName, false);
				}
				return _singularName;
			}
		}
		/// <summary>Gets or sets the PluralName.</summary>
		public string PluralName
		{
			get
			{
				if (_pluralName != null) return _pluralName;
				NamingConvention convention;
				if (Architecture.Owner.TableNameConventions.TryGetValue(NativeName, out convention))
				{
					_singularName = convention.Singular;
					_pluralName = convention.Plural;
				}
				else
				{
					_singularName = CsDb.CodeGen.Convert.ToMemberName(NativeName, true);
					_pluralName = CsDb.CodeGen.Convert.ToMemberName(NativeName, false);
				}
				return _pluralName;
			}
		}



		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = PluralName + "View"); }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the NativeName.</summary>
		[Key]
		public string NativeName => Architecture.Name;
		/// <summary>Gets the name of the constant which provides the native name of the table.</summary>
		[Key]
		public string NativeNameConstant => "NativeName";
		/// <summary>This is the name of the property inside the DataSet.</summary>
		public string PropertyName => PluralName;




		/// <summary>The view row from the table.</summary>
		public CsDbcViewRow Row { get; }



		[Key]
		private string DefaultDescription => $"'[<c>{CodeBundle.Owner.Architecture.Name}</c>].[<c>#{CodeBundle.Architecture.Name}#</c>].[<c>{Architecture.Name}</c>]': A VIEW inside '[<c>{CodeBundle.Owner.Architecture.Name}</c>].[<c>#{CodeBundle.Architecture.Name}#</c>]' database with '<see cref=\"{DataRowType}\"/>' as <see cref=\"DataRow\"/>.";
		[Key]
		private string DebuggerDisplayAttribute => $"[DebuggerDisplay(\"DataVIEW({Architecture.Owner.Name}.{Architecture.Name}): Rows[{{Rows.Count}}]\")]";
		[Key(IncludeInHash = false)]
		private string CsDbDataViewAttribute => new CsDbDataViewAttribute() {Database = Architecture.Owner.Name, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode();

		[Key]
		private string DataSetType => CodeBundle.DataSet.Name;
		[Key]
		private string DataContextType => CodeBundle.Owner.Context.FullQualifiedName;
		[Key]
		private string DataSetPropertyName => "DataSet";
		[Key]
		private string DataContextPropertyName => CodeBundle.DataSet.DataContextPropertyName;
		[Key]
		private string DataRowType => Row.Name;



		[Key(Name = "Constants")]
		private string TmpConstants => Constants.Select(x => x.GetString(1)).Join("\r\n\r\n\t");



		[Key(Name = "Overrides")]
		private string TmpOverrides => Overrides.GetString(1);
	}
}