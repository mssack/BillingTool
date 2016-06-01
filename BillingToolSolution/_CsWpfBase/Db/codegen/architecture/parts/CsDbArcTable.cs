// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CsWpfBase.Db.codegen.architecture.parts.bases;






namespace CsWpfBase.Db.codegen.architecture.parts
{
	/// <summary>Contains architecture information about a db table.</summary>
	[Serializable]
	public class CsDbArcTable : CsDbArcTableViewBase
	{
		private readonly List<CsDbArcRelation> _relations = new List<CsDbArcRelation>();
		private int _level;
		private CsDbArcColumn _primaryColumn;


		private CsDbArcTable(CsDbArcDatabase owner, string name) : base(owner, name)
		{
			Relations = _relations.AsReadOnly();
		}


		#region Overrides/Interfaces
		/// <summary>Removes the column from this table and removes all associated relations.</summary>
		public override void RemoveColumn(CsDbArcColumn column)
		{
			if (column.Owner != this)
				throw new InvalidOperationException("the column does not belong to the table.");

			foreach (var relation in Relations.Where(x => x.PrimaryKey == column || x.ForeignKey == column))
			{
				relation.Remove();
			}
			base.RemoveColumn(column);

		}
		#endregion


		/// <summary>Gets the relations associated with this table</summary>
		public ReadOnlyCollection<CsDbArcRelation> Relations { get; }




		/// <summary>Gets or sets the PrimaryColumn.</summary>
		public CsDbArcColumn PrimaryColumn
		{
			get { return _primaryColumn; }
			set { SetProperty(ref _primaryColumn, value); }
		}

		/// <summary>
		///     Gets a value which describes the position of the table inside the whole database. This can be used to determine the dependency's for this table.
		///     Level 0 means that this table depends on no other table inside the database.
		/// </summary>
		public int Level
		{
			get { return _level; }
			set { SetProperty(ref _level, value); }
		}

		internal void AddRelation(CsDbArcRelation relation)
		{
			_relations.Add(relation);
		}

		internal void RemoveRelation(CsDbArcRelation relation)
		{
			_relations.Remove(relation);
		}


		/// <summary>This initialize method should only be called by the <see cref="CsDbArcDatabase" /> class.</summary>
		internal static CsDbArcTable Create(CsDbArcDatabase owner, string name)
		{
			return new CsDbArcTable(owner, name);
		}


		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"Table[{Name}]";
		}
	}



}