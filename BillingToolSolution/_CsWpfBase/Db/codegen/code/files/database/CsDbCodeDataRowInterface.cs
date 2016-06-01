// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.interfaceParts;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database
{
	/// <summary>Used for the interface creation for a row.</summary>
	internal class CsDbCodeDataRowInterface : FileTemplate
	{
		private CsDbcRowInterface_Member[] _members;

		internal CsDbCodeDataRowInterface(CsDbCodeDataRow row)
		{
			Row = row;
		}


		/// <summary>Gets the name of the interface.</summary>
		[Key]
		public string Name => $"I{Row.Name}";

		internal CsDbCodeDataRow Row { get; }
		private CsDbCodeBundleForDb CodeBundle => Row.CodeBundle;
		private CsDbArcTable Architecture => Row.Architecture;
		private CsDbCodeDataTable Table => Row.Table;
		[Key(IncludeInHash = false)]
		private string Attribute => new CsDbDataRowInterfaceAttribute {Database = Architecture.Owner.Name, Generated = DateTime.Now.ToString("yy.MM.dd HH:mm:ss"), TableName = Architecture.Name, Hash = GetHash()}.ToCode();
		[Key]
		private string DefaultDescription => $"Interface for <see cref=\"{Row.Name}\"/> can be used to create POCO object or other models.";
		[Key]
		private string Members => (_members ?? (_members = Row.Columns.Select(x => new CsDbcRowInterface_Member(x)).ToArray())).Select(x => x.GetString(1)).Join("\r\n\t");
	}
}