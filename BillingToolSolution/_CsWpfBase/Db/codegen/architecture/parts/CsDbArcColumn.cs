// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using CsWpfBase.Db.codegen.architecture.parts.bases;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.architecture.parts
{
	/// <summary>Contains architecture information about a column inside a table.</summary>
	[Serializable]
	public class CsDbArcColumn : Base
	{
		private string _defaultValue;
		private string _description;
		private object _dotNetDefaultValue;
		private bool _dotNetIsNullable;
		private int _dotNetMaxLength;
		private Type _dotNetType;
		private string _maxLength;
		private string _name;
		private string _nullable;
		private CsDbArcTableViewBase _owner;
		private string _type;

		/// <summary>Creates a new ArchitectureColumn and associate</summary>
		internal CsDbArcColumn(CsDbArcTableViewBase owner)
		{
			_owner = owner;
		}

		/// <summary>Gets or sets the Owner.</summary>
		public CsDbArcTable Owner => (CsDbArcTable) _owner;
		/// <summary>Gets or sets the Owner.</summary>
		public CsDbArcView OwnerView => (CsDbArcView) _owner;
		/// <summary>The native name of the db column</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>The native description of the db column.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>The native type of the db column.</summary>
		public string Type
		{
			get { return _type; }
			set { SetProperty(ref _type, value); }
		}
		/// <summary>The native nullable string of the db column</summary>
		public string Nullable
		{
			get { return _nullable; }
			set { SetProperty(ref _nullable, value); }
		}
		/// <summary>The native max length of the db column.</summary>
		public string MaxLength
		{
			get { return _maxLength; }
			set { SetProperty(ref _maxLength, value); }
		}
		/// <summary>The native default value of the db column.</summary>
		public string DefaultValue
		{
			get { return _defaultValue; }
			set { SetProperty(ref _defaultValue, value); }
		}


		/// <summary>The associated .Net value of the <see cref="Type" /> property. if it is a value type this will not return nullable bool.</summary>
		public Type DotNetType
		{
			get { return _dotNetType; }
			set { SetProperty(ref _dotNetType, value); }
		}
		/// <summary>The associated .Net value of the <see cref="Nullable" /> property.</summary>
		public bool DotNetIsNullable
		{
			get { return _dotNetIsNullable; }
			set { SetProperty(ref _dotNetIsNullable, value); }
		}
		/// <summary>The associated .Net value of the <see cref="MaxLength" /> property.</summary>
		public int DotNetMaxLength
		{
			get { return _dotNetMaxLength; }
			set { SetProperty(ref _dotNetMaxLength, value); }
		}
		/// <summary>The associated .Net value of the <see cref="DefaultValue" /> property.</summary>
		public object DotNetDefaultValue
		{
			get { return _dotNetDefaultValue; }
			set { SetProperty(ref _dotNetDefaultValue, value); }
		}


		internal void SetRemoved()
		{
			_owner = null;
		}
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"Column[{Name}]";
		}
	}
}