// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Data;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.architecture.parts
{
	/// <summary>Contains architecture information about a relation between two tables.</summary>
	[Serializable]
	public class CsDbArcRelation : Base
	{
		private Rule _deleteRule;
		private CsDbArcColumn _foreignKey;
		private string _name;
		private CsDbArcColumn _primaryKey;
		private Rule _updateRule;

		private CsDbArcRelation()
		{
		}

		/// <summary>Gets or sets the Name of the relation. Usually it is the FK_Name</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the PrimaryKey.</summary>
		public CsDbArcColumn PrimaryKey
		{
			get { return _primaryKey; }
			private set { SetProperty(ref _primaryKey, value); }
		}
		/// <summary>Gets or sets the ForeignKey.</summary>
		public CsDbArcColumn ForeignKey
		{
			get { return _foreignKey; }
			private set { SetProperty(ref _foreignKey, value); }
		}
		/// <summary>Gets or sets the DeleteRule.</summary>
		public Rule DeleteRule
		{
			get { return _deleteRule; }
			set { SetProperty(ref _deleteRule, value); }
		}
		/// <summary>Gets or sets the UpdateRule.</summary>
		public Rule UpdateRule
		{
			get { return _updateRule; }
			set { SetProperty(ref _updateRule, value); }
		}

		/// <summary>Associates two tables with each other. Automatically adds this relation to the associated table relation collection.</summary>
		public static CsDbArcRelation Create(string name, CsDbArcColumn primary, CsDbArcColumn foreign)
		{
			var rv = new CsDbArcRelation {Name = name, PrimaryKey = primary, ForeignKey = foreign};
			primary.Owner.AddRelation(rv);
			foreign.Owner.AddRelation(rv);
			return rv;
		}

		/// <summary>Removes this relation from primary key and foreign key table.</summary>
		public void Remove()
		{
			PrimaryKey.Owner.RemoveRelation(this);
			ForeignKey.Owner.RemoveRelation(this);
		}

		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"Relation[{Name}]";
		}
	}
}