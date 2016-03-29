// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code.files.database.datarowParts.methods
{
	/// <summary>Used to set the default database values on this row.</summary>

	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_ApplyDefaults : FileTemplate
	{
		internal CsDbcTableRow_ApplyDefaults(CsDbCodeDataRow owner)
		{
			Owner = owner;
		}

		/// <summary>Get the name of the method.</summary>
		[Key]
		public string Name => "ApplyDefaults";
		[Key]
		private string TableType => Owner.Table.Name;
		[Key]
		private string Content
		{
			get
			{
				return Owner.Columns.Where(x => x.DotNetAttributes.Default != null && !(x.DotNetAttributes.Default.GetType().IsValueType && x.DotNetAttributes.Default == Activator.CreateInstance(x.DotNetAttributes.Default.GetType())))
							.Select(x => x.Name + " = " + GetDafaultValueCode(x.DotNetAttributes.Default, x.DotNetAttributes.Type) + ";").Join("\r\n\t\t");
			}
		}

		private CsDbCodeDataRow Owner { get; }

		private static string GetDafaultValueCode(object def, Type targetType)
		{
			if (def.Equals(CsDb.CodeGen.Statics.DateTimeNowFunction))
				return "DateTime.Now";
			if (def.Equals(CsDb.CodeGen.Statics.NewGuidFunction))
				return "Guid.NewGuid()";



			if (targetType.IsNumericType())
				return def.ToString();
			if (targetType == typeof (DateTime))
				return $"new DateTime({((DateTime) def).Ticks})";
			if (targetType == typeof (bool))
				return def.ToString().ToLower();
			if (targetType == typeof (string))
				return $"\"{def}\"";



			throw new Exception("Unknown data format");
		}
	}
}