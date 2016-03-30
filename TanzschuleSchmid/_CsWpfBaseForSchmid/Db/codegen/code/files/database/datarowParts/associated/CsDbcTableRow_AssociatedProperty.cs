// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.codegen.code.extensions;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.associated
{
	/// <summary>Used to load and store another a relation based instance of another row.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_AssociatedProperty : CsDbCodeRelationFileTemplate
	{
		internal CsDbcTableRow_AssociatedProperty(CsDbCodeRelation relation) : base(relation)
		{
		}



		/// <summary>The name of the property.</summary>
		[Key]
		public string Name => Relation.Convention?.Singular ?? PkTable.SingularName;





		[Key]
		internal string DataFieldName => $"_{Name.ToLowerName()}";
		[Key]
		internal string IsLoadedPropertyName => $"Is{Name}Loaded";


		/// <summary>The name of the field which says if property is loaded or not.</summary>
		internal string LoadedProperty => $"public bool {IsLoadedPropertyName} => Equals({DataFieldName}?.{PkPropertyName}, {FkColumn.Name});";
		/// <summary>The name of the backing store field of the property.</summary>
		internal string DataField => $"private {RowType} {DataFieldName};";



		[Key]
		private string SelectDescription => Relation.Get_PkSelectDescription();
		[Key]
		private string RelationDescription => Relation.Get_RelationDescription_PointingTowardPk();
		[Key]
		private string Attribute => Relation.Attribute.GetCode() + $"[DependsOn(\"{Relation.FkKey.Name}\")]";


		[Key]
		private string CheckNull => FkColumn.DotNetAttributes.Type.IsValueType && FkColumn.DotNetAttributes.IsNullable ? $"if ({FkColumn.Name} == null) {DataFieldName} = null; else" : null;



		//Pk is the other end
		[Key]
		private string TableProperty => PkTable.PropertyName;
		[Key]
		private string RowType => PkRow.Name;
		[Key]
		private string PkPropertyName => PkColumn.Name;
		[Key]
		private string FindOrLoadMethodName => Relation.PkMethods.FindOrLoadName;


		//FK means "this" pointer
		[Key]
		private string FkPropertyName => FkColumn.Name;
		[Key]
		private string FkPropertyNameSelector => FkColumn.DotNetAttributes.Type.IsValueType && FkColumn.DotNetAttributes.IsNullable ? $"{FkColumn.Name}.Value" : FkColumn.Name;
		[Key]
		private string FkNativeColumnNameConstant => $"{FkColumn.Row.Table.Name}.{FkColumn.NativeNameConstant}";
		[Key]
		private string FkRealType => FkColumn.DotNetAttributes.Type.IsValueType && FkColumn.DotNetAttributes.IsNullable ? $"{FkColumn.DotNetAttributes.Type.Name}?" : FkColumn.DotNetAttributes.Type.Name;
		[Key]
		private string ForbidNulls => FkColumn.DotNetAttributes.IsNullable == true ? null : $"if (value == null) throw new InvalidOperationException(\"The value cannot be null (FK: {Relation.Architecture.Name})\");";
	}
}