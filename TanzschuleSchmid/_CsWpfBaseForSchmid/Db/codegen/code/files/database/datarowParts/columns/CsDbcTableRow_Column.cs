// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.columns
{
	/// <summary>The code for a column inside <see cref="CsDbCodeDataRow" /> code.</summary>
	[Serializable]
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_Column : FileTemplate
	{
		private CsDbNativeDataColumnAttribute _csDbNativeDataAttribute;
		private CsDbDataColumnAttribute _dotNetAttributes;
		private string _name;
		private CsDbcTableRow_UnsignedColumn _unsignedVersion;

		/// <summary>Creates a new column.</summary>
		public CsDbcTableRow_Column(CsDbArcColumn architecture, CsDbCodeDataRow row)
		{
			Row = row;
			Architecture = architecture;
		}


		/// <summary>Gets the underlaying architecture</summary>
		public CsDbArcColumn Architecture { get; }
		/// <summary>Gets the owning code base.</summary>
		public CsDbCodeBundleForDb CodeBundle => Row.CodeBundle;
		/// <summary>Gets the owning row.</summary>
		public CsDbCodeDataRow Row { get; }


		/// <summary>Gets or sets the Name.</summary>
		[Key]
		public string Name
		{
			get
			{
				if (_name != null) return _name;

				_name = CsDb.CodeGen.Convert.ToMemberName(Architecture.Name, false);
				if (_name == Row.Name)
					_name = _name + "Column";

				return _name;
			}
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the ColumnAttribute.</summary>
		public CsDbNativeDataColumnAttribute NativeAttributes => _csDbNativeDataAttribute ?? (_csDbNativeDataAttribute = new CsDbNativeDataColumnAttribute()
		{
			Table = Architecture.Owner.Name,
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




		/// <summary>Gets the name of the constant which holds the native name of the column.</summary>
		[Key]
		public string NativeNameConstant => Name + "Col";


		internal CsDbcTableRow_UnsignedColumn UnsignedVersion
		{
			get
			{
				if (!Architecture.Name.StartsWith("Unsigned_"))
					return null;
				return _unsignedVersion ?? (_unsignedVersion = new CsDbcTableRow_UnsignedColumn(this));
			}
		}


		[Key]
		private string DatabaseName => Architecture.Owner.Owner.Name;


		[Key]
		private string TableName => Row.Table.Name;


		[Key]
		private string NativeTableName => Row.Table.NativeName;


		[Key]
		private string NativeName => Architecture.Name;


		[Key]
		internal string TypeDescription => $"Type = <c>{Architecture.Type}</c>{(Architecture.DotNetIsNullable ? ", <c>NULLABLE</c>" : "")}{(string.IsNullOrEmpty(Architecture.DefaultValue) ? "" : $", Default = '<c>{Architecture.DefaultValue}</c>'")}{(string.IsNullOrEmpty(Architecture.MaxLength) ? "" : $", MaxLength = <c>{Architecture.MaxLength}</c>")}";


		[Key(ValuePrefix = "<para/>\r\n///		")]
		internal string DbDescription => Architecture.Description?.Replace("\r\n", "\n").Replace("\n", "<para/>");


		[Key]
		internal string Type => Architecture.DotNetType.IsValueType && Architecture.DotNetIsNullable ? $"{Architecture.DotNetType.Name}?" : Architecture.DotNetType.Name;


		[Key(ValueSuffix = "\r\n")]
		private string Attributes => $"{DotNetAttributes.ToCode()}\r\n{NativeAttributes.To_Attribute_Code()}";
	}
}