// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.associated;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.columns;
using CsWpfBase.Db.codegen.code.files.database.datarowParts.referencing;
using CsWpfBase.Db.codegen.code.files.database.datatableParts.methods;
using CsWpfBase.Db.codegen.code.files.database.datatableParts.methods.foreignkeybundle;
using CsWpfBase.Db.codegen.code.namingconventions;






namespace CsWpfBase.Db.codegen.code.extensions
{
	/// <summary>Maps the architecture to the code base.</summary>
	internal class CsDbCodeRelation
	{
		private CsDbResolvesRelationAttribute _attribute;
		private NamingConvention _convention;
		private bool _conventionLoaded;

		/// <summary>ctor</summary>
		private CsDbCodeRelation(CsDbArcRelation architecture, CsDbcTableRow_Column pkKey, CsDbcTableRow_Column fkKey)
		{
			Architecture = architecture;
			PkKey = pkKey;
			FkKey = fkKey;
		}

		/// <summary>underlaying architecture.</summary>
		public CsDbArcRelation Architecture { get; }
		/// <summary>primary Key</summary>
		public CsDbcTableRow_Column PkKey { get; }
		/// <summary>foreign key</summary>
		public CsDbcTableRow_Column FkKey { get; }
		/// <summary>Gets the associated naming convention if one exist.</summary>
		public NamingConvention Convention
		{
			get
			{

				if (_conventionLoaded == false)
				{
					PkKey.Architecture.Owner.Owner.RelationNameConventions.TryGetValue(Architecture.Name, out _convention);
					_conventionLoaded = true;
				}
				return _convention;
			}
		}

		/// <summary>The attribute for methods or object which are related to this relation.</summary>
		public CsDbResolvesRelationAttribute Attribute => _attribute ?? (_attribute = new CsDbResolvesRelationAttribute(Architecture.Name) {PkTable = Architecture.PrimaryKey.Owner.Name, PkColumn = Architecture.PrimaryKey.Name, FkTable = Architecture.ForeignKey.Owner.Name, FkColumn = Architecture.ForeignKey.Name});


		/// <summary>Methods for selecting a single item.</summary>
		public CsDbcTable_PrimaryKeyMethods PkMethods { get; set; }
		/// <summary>Methods for selecting many items.</summary>
		public CsDbcTable_ForeignKeyMethodBundle FkMethods { get; set; }


		/// <summary>The associated property inside the data row. This links to one item of Pk</summary>
		internal CsDbcTableRow_AssociatedProperty AssociatedProperty { get; set; }
		/// <summary>The referencing property inside the data row. This references to multiple instances of fk.</summary>
		internal CsDbcTableRow_ReferencingProperty ReferencingProperty { get; set; }





		/// <summary>Creates the code base relations and maps it to the associated tables. </summary>
		public static CsDbCodeRelation Create(CsDbArcRelation architecture, CsDbcTableRow_Column pkKey, CsDbcTableRow_Column fkKey)
		{
			var item = new CsDbCodeRelation(architecture, pkKey, fkKey);
			if (pkKey.Row == fkKey.Row)
				pkKey.Row.Table.Relations.Add(item);
			else
			{
				pkKey.Row.Table.Relations.Add(item);
				fkKey.Row.Table.Relations.Add(item);
			}
			return item;
		}



		/// <summary>Get the select description for summarys. Use this if you want to point to one item.</summary>
		public string Get_PkSelectDescription()
		{
			return $"SELECT * FROM {PkKey.Architecture.Owner.Name} WHERE [{PkKey.Architecture.Name}] = '<paramref name=\"{FkKey.Name}\"/>'";
		}

		/// <summary>Get the select description for summarys. Use this if you want to point to many items.</summary>
		public string Get_FkSelectDescription()
		{
			return $"SELECT * FROM {FkKey.Architecture.Owner.Name} WHERE [{FkKey.Architecture.Name}] = '<paramref name=\"{PkKey.Name}\"/>'";
		}

		/// <summary>Get the relation description for summarys. Use this if you want to point to one item.</summary>
		public string Get_RelationDescription_PointingTowardPk()
		{
			return $"[<c>{FkKey.Row.Table.NativeName}</c>].[<c>{FkKey.NativeAttributes.Name}</c>] &#62;&#62;&#62;&#62; [<c>{PkKey.Row.Table.NativeName}</c>].[<c>{PkKey.NativeAttributes.Name}</c>]";
		}

		/// <summary>Get the relation description for summarys. Use this if you want to point to many items.</summary>
		public string Get_RelationDescription_PointingTowardFk()
		{
			return $"[<c>{PkKey.Row.Table.NativeName}</c>].[<c>{PkKey.NativeAttributes.Name}</c>] &#60;&#60;&#60;&#60; [<c>{FkKey.Row.Table.NativeName}</c>].[<c>{FkKey.NativeAttributes.Name}</c>]";
		}
	}



}