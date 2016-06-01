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
	/// <summary>Collapses methods for the data row.</summary>
	// ReSharper disable once InconsistentNaming
	internal class CsDbcTableRow_Methods : FileTemplate
	{
		internal CsDbcTableRow_Methods(CsDbCodeDataRow owner)
		{
			Owner = owner;
			Reload = owner.PkColumn == null ? null : new CsDbcTableRow_Reload(owner.PkColumn);
			SetDefaultValues = new CsDbcTableRow_ApplyDefaults(owner);
			Invalidate = new CsDbcTableRow_Invalidate(owner);
			CopyMethods = new CsDbcTableRow_CopyMethods(owner);
			BundledDataMethods = new CsDbcTableRow_BundledDataMethods(owner);
		}

		/// <summary>The Method used to reload a single datarow.</summary>
		public CsDbcTableRow_Reload Reload { get; }
		/// <summary>The method used to set default values.</summary>
		public CsDbcTableRow_ApplyDefaults SetDefaultValues { get; }
		/// <summary>The method used to invalidate the complete row.</summary>
		public CsDbcTableRow_Invalidate Invalidate { get; }
		/// <summary>The copy methods.</summary>
		public CsDbcTableRow_CopyMethods CopyMethods { get; }
		/// <summary>The bundled Data methods.</summary>
		public CsDbcTableRow_BundledDataMethods BundledDataMethods { get; }


		[Key]
		private string InvalidateMethod => Invalidate.GetString();
		[Key(Name = "CopyMethods")]
		private string TmpCopyMethods => CopyMethods.GetString();
		[Key(Name = "BundledDataMethods")]
		private string TmpBundledDataMethods => BundledDataMethods.GetString();
		[Key]
		private string SetDefaultValuesMethod => SetDefaultValues.GetString();
		[Key]
		private string ReloadMethod => Reload?.GetString();




		[Key]
		private string DataFields => Owner.AssociatedProperties.Select(x => x.DataField).Concat(Owner.ReferencingProperties.Select(x => x.DataField)).Join("\r\n");
		[Key]
		private string LoadedPropertys => Owner.AssociatedProperties.Select(x => x.LoadedProperty).Join("\r\n");



		[Key]
		private string AssociatedProperties => Owner.AssociatedProperties.Select(x => x.GetString()).Join("\r\n");



		[Key]
		private string ReferencingProperties => Owner.ReferencingProperties.Select(x => x.GetString()).Join("\r\n");





		private CsDbCodeDataRow Owner { get; }
	}
}