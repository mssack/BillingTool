// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.extensions;
using CsWpfBase.Db.codegen.code.files.database.datatableParts;
using CsWpfBase.Db.codegen.code.files.database.datatableParts.methods;
using CsWpfBase.Db.codegen.code.namingconventions;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>A file for a table class.</summary>
	[Serializable]
	internal class CsDbCodeDataTable : FileTemplate
	{
		private CsDbcTable_Methods _methods;
		private string _name;
		private string _pluralName;
		private List<CsDbCodeRelation> _relations;



		private string _singularName;

		/// <summary>Creates a new code set.</summary>
		public CsDbCodeDataTable(CsDbArcTable architecture, CsDbCodeBundleForDb codeBundle)
		{
			Architecture = architecture;
			CodeBundle = codeBundle;
			Row = new CsDbCodeDataRow(this);
		}

		/// <summary>Gets the Owner.</summary>
		public CsDbCodeBundleForDb CodeBundle { get; }
		/// <summary>Gets the underlaying architecture.</summary>
		public CsDbArcTable Architecture { get; }
		/// <summary>Gets the associated row.</summary>
		public CsDbCodeDataRow Row { get; }

		/// <summary>Gets the methods used by this table.</summary>
		public CsDbcTable_Methods Methods => _methods ?? (_methods = new CsDbcTable_Methods(this));

		/// <summary>The code base relations</summary>
		public List<CsDbCodeRelation> Relations => _relations ?? (_relations = new List<CsDbCodeRelation>());
		/// <summary>Gets or sets the SingularName.</summary>
		public string SingularName
		{
			get
			{
				if (_singularName == null)
				{
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
				}
				return _singularName;
			}
		}
		/// <summary>Gets or sets the PluralName.</summary>
		public string PluralName
		{
			get
			{
				if (_pluralName == null)
				{
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
				}
				return _pluralName;
			}
		}

		/// <summary>This is the name of the property inside the DataSet.</summary>
		public string PropertyName => PluralName;


		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = PluralName + "Table"); }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets the name of the constant which provides the native name of the table.</summary>
		[Key]
		public string NativeNameConstant => "NativeName";
		/// <summary>Gets or sets the NativeName.</summary>
		[Key]
		public string NativeName => Architecture.Name;





		[Key]
		private string DataContextType => CodeBundle.Owner.Context.FullQualifiedName;

		[Key]
		internal string DataContextPropertyName => CodeBundle.DataSet.DataContextPropertyName;

		[Key]
		private string DataSetType => CodeBundle.DataSet.Name;

		[Key]
		internal string DataSetPropertyName => "DataSet";

		[Key]
		private string DataRowType => Row.Name;





		[Key(IncludeInHash = false)]
		private string CsDbDataTableAttribute => new CsDbDataTableAttribute {Name = NativeName, Database = Architecture.Owner.Name, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), Hash = GetHash()}.ToCode();
		[Key]
		private string DebuggerDisplayAttribute => $"[DebuggerDisplay(\"DataTable({Architecture.Owner.Name}.{Architecture.Name}): Rows[{{Rows.Count}}]\")]";
		[Key]
		private string RelationAttributes => Relations.Select(x => x.Attribute.GetCode()).Join("\r\n");



		[Key]
		private string RelationsDescription => Relations.Select(x => x.PkKey.Row.Table == this ? x.Get_RelationDescription_PointingTowardFk() : x.Get_RelationDescription_PointingTowardPk()).Join("<para/>\r\n///\t\t");
		[Key]
		private string DefaultDescription => $"'[<c>{CodeBundle.Owner.Architecture.Name}</c>].[<c>#{CodeBundle.Architecture.Name}#</c>].[<c>{Architecture.Name}</c>]': A Table inside '[<c>{CodeBundle.Owner.Architecture.Name}</c>].[<c>#{CodeBundle.Architecture.Name}#</c>]' database with '<see cref=\"{Row.Name}\"/>' as <see cref=\"DataRow\"/>.";




		[Key]
		private string Constants
		{
			get
			{
				return Row.Columns.Select(x => $"///<summary>Holds native column name of <c>[{Architecture.Owner.Name}].[{NativeName}].[{x.NativeAttributes?.Name}]</c> column. Property = <see cref=\"{x.Row.Name}.{x.Name}\"/>.</summary>" +
													$"\r\n\tpublic const string {x.NativeNameConstant} = \"{x.NativeAttributes?.Name}\";").Join("\r\n\t");
			}
		}


		[Key(Name = "Methods")]
		private string TmpMethods => Methods.GetString(1);


		[Key]
		private string ColAttributes => Row.Columns.Where(x => x.DotNetAttributes.MaxLength != -1).Select(x => new CsDbcTable_ColAttribute(x, CsDbcTable_ColAttribute.Modes.MaxLength).GetString(2)).Concat(Row.Columns.Where(x => !string.IsNullOrEmpty(x.NativeAttributes.Description)).Select(x => new CsDbcTable_ColAttribute(x, CsDbcTable_ColAttribute.Modes.Description).GetString(2))).Join("\r\n\t\t");


	}
}