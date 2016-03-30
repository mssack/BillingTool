// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.dataviewparts.row.columns
{
	/// <summary>The code for a column inside <see cref="CsDbCodeDataRow" /> code.</summary>
	[Serializable]
	// ReSharper disable once InconsistentNaming
	internal class CsDbcViewRow_Column : FileTemplate
	{
		private CsDbNativeDataColumnAttribute _csDbNativeDataAttribute;
		private CsDbDataColumnAttribute _dotNetAttributes;
		private string _name;

		/// <summary>Creates a new column.</summary>
		public CsDbcViewRow_Column(CsDbArcColumn architecture, CsDbcViewRow row)
		{
			Row = row;
			Architecture = architecture;
			if (architecture.Name.StartsWith("Unsigned_"))
				UnsignedVersion = new CsDbcViewRow_UnsignedColumn(this);
		}


		/// <summary>Gets the owning code base.</summary>
		public CsDbCodeBundleForDb CodeBundle => Row.CodeBundle;
		/// <summary>Gets the owning row.</summary>
		public CsDbcViewRow Row { get; }
		/// <summary>Gets the underlaying architecture</summary>
		public CsDbArcColumn Architecture { get; }





		/// <summary>Gets or sets the ColumnAttribute.</summary>
		public CsDbNativeDataColumnAttribute NativeAttributes => _csDbNativeDataAttribute ?? (_csDbNativeDataAttribute = new CsDbNativeDataColumnAttribute()
		{
			Name = Architecture.Name,
			Type = Architecture.Type,
			MaxLength = Architecture.MaxLength,
			Description = Architecture.Description,
			Default = Architecture.DefaultValue,
			IsNullable = Architecture.Nullable,

		});
		/// <summary>Gets or sets the CsDbDotNetAttribute.</summary>
		public CsDbDataColumnAttribute DotNetAttributes => _dotNetAttributes ?? (_dotNetAttributes = new CsDbDataColumnAttribute()
		{
			Type = Architecture.DotNetType,
			MaxLength = Architecture.DotNetMaxLength,
			Default = Architecture.DotNetDefaultValue,
			IsNullable = Architecture.DotNetIsNullable,
		});





		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get { return _name ?? (_name = CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, false)); }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets the name of the constant which holds the native name of the column.</summary>
		[Key]
		public string NativeNameConstant => Name + "Col";




		private string TypeDescription => $"Type = <c>{Architecture.Type}</c>{(Architecture.DotNetIsNullable ? ", <c>NULLABLE</c>" : "")}{(string.IsNullOrEmpty(Architecture.DefaultValue) ? "" : $", Default = '<c>{Architecture.DefaultValue}</c>'")}{(string.IsNullOrEmpty(Architecture.MaxLength) ? "" : $", MaxLength = <c>{Architecture.MaxLength}</c>")}";

		[Key]
		internal string DefaultDescription => $"[<c>{CodeBundle.Architecture.Name}</c>].[<c>{Architecture.OwnerView.Name}</c>].[<c>{Architecture.Name}</c>] ({TypeDescription})";

		[Key]
		private string Attributes => $"{DotNetAttributes.ToCode()}{NativeAttributes.ToCode()}";

		[Key]
		private string Type => Architecture.DotNetType.IsValueType && Architecture.DotNetIsNullable ? $"{Architecture.DotNetType.Name}?" : Architecture.DotNetType.Name;

		[Key]
		private string ConstantSelector => $"{Row.Table.Name}.{NativeNameConstant}";





		internal CsDbcViewRow_UnsignedColumn UnsignedVersion { get; private set; }
	}
}