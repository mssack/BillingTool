// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.architecture.parts.bases
{
	/// <summary>This is the base class for views and tables.</summary>
	public abstract class CsDbArcTableViewBase : Base
	{
		private readonly List<CsDbArcColumn> _columns = new List<CsDbArcColumn>();

		private CsDbArcDatabase _owner;


		/// <summary>This initialize method should only be called by the <see cref="CsDbArcDatabase" /> class.</summary>
		protected CsDbArcTableViewBase(CsDbArcDatabase owner, string name)
		{
			Name = name;
			Owner = owner;
			Columns = _columns.AsReadOnly();
		}


		#region Abstract
		/// <summary>Removes the column from this table and removes all associated relations.</summary>
		public virtual void RemoveColumn(CsDbArcColumn column)
		{
			if (column.Owner != this)
				throw new InvalidOperationException("the column does not belong to the table.");
			column.SetRemoved();
			_columns.Remove(column);
		}
		#endregion


		/// <summary>Gets the columns defined for this table.</summary>
		public ReadOnlyCollection<CsDbArcColumn> Columns { get; }
		/// <summary>The architecture collection this table belongs to.</summary>
		public CsDbArcDatabase Owner
		{
			get { return _owner; }
			private set { SetProperty(ref _owner, value); }
		}

		/// <summary>Gets the native name of the table or view.</summary>
		public string Name { get; }



		/// <summary>Creates a new associated column.</summary>
		public CsDbArcColumn CreateColumn()
		{
			var rv = new CsDbArcColumn(this);
			_columns.Add(rv);
			return rv;
		}

		internal void SetRemoved()
		{
			foreach (var column in Columns)
			{
				column.SetRemoved();
			}
			Owner = null;
		}
	}
}