// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CsWpfBase.Db.codegen.code.namingconventions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.architecture.parts
{



	/// <summary>Creates a new database architecture.</summary>
	[Serializable]
	public class CsDbArcDatabase : Base
	{
		private readonly List<CsDbArcTable> _tables = new List<CsDbArcTable>();
		private readonly List<CsDbArcView> _views = new List<CsDbArcView>();
		private string _name;

		private CsDbArchitecture _owner;

		private CsDbArcDatabase(CsDbArchitecture owner, string name)
		{
			Name = name;
			Owner = owner;
			Tables = _tables.AsReadOnly();
			Views = _views.AsReadOnly();
		}

		/// <summary>Gets the Owner.</summary>
		public CsDbArchitecture Owner
		{
			get { return _owner; }
			private set { SetProperty(ref _owner, value); }
		}
		/// <summary>The Database name.</summary>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}


		/// <summary>Naming conventions to replace the default naming of a table, row and table property.</summary>
		public Dictionary<string, NamingConvention> TableNameConventions { get; private set; } = new Dictionary<string, NamingConvention>();
		/// <summary>Naming conventions to replace the default naming of a referencing property or associated property.</summary>
		public Dictionary<string, NamingConvention> RelationNameConventions { get; private set; } = new Dictionary<string, NamingConvention>();

		/// <summary>A list of all tables inside this architecture.</summary>
		public ReadOnlyCollection<CsDbArcTable> Tables { get; }

		/// <summary>A list of all views inside this architecture.</summary>
		public ReadOnlyCollection<CsDbArcView> Views { get; }

		/// <summary>A list of all relations in this architecture.</summary>
		public IEnumerable<CsDbArcRelation> Relations => Tables.SelectMany(x => x.Relations).Distinct();

		/// <summary>Reads the conventions from a string where each line represent one convention.</summary>
		public void ReadTableNameConventions(string lines, string delimiter)
		{
			var splittedLines = lines.Replace("\r\n", "\n").Split('\n');
			TableNameConventions = splittedLines.Select(x => NamingConvention.ParseFromLine(x, delimiter)).ToDictionary(x => x.NativeName, x => x);
		}

		/// <summary>Reads the conventions from a string where each line represent one convention.</summary>
		public void ReadRelationNameConventions(string lines, string delimiter)
		{
			var splittedLines = lines.Replace("\r\n", "\n").Split('\n');
			RelationNameConventions = splittedLines.Select(x => NamingConvention.ParseFromLine(x, delimiter)).ToDictionary(x => x.NativeName, x => x);
		}

		/// <summary>Removes the table. All associated Relation will be removed either.</summary>
		public void RemoveTable(CsDbArcTable table)
		{
			if (table.Owner != this)
				throw new InvalidOperationException("This table belongs to another Architecture");

			foreach (var relation in table.Relations.ToArray())
			{
				relation.Remove();
			}
			_tables.Remove(table);
			table.SetRemoved();
		}

		/// <summary>Removes the view.</summary>
		public void RemoveView(CsDbArcView view)
		{
			if (view.Owner != this)
				throw new InvalidOperationException("This view belongs to another Architecture");

			_views.Remove(view);
			view.SetRemoved();
		}

		internal static CsDbArcDatabase New(CsDbArchitecture owner, string name)
		{
			return new CsDbArcDatabase(owner, name);
		}

		/// <summary>Creates a new table and checks for already existing tables. If table already exists an exception will be thrown.</summary>
		internal CsDbArcTable CreateTable(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new InvalidOperationException("the table have to have a name.");
			if (Tables.Any(x => x.Name == name))
				throw new InvalidOperationException("A table with the same name already exists.");

			var rv = CsDbArcTable.Create(this, name);
			_tables.Add(rv);
			return rv;
		}

		/// <summary>Creates a new view and checks for already existing views. If view already exists an exception will be thrown.</summary>
		internal CsDbArcView CreateView(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new InvalidOperationException("the view have to have a name.");
			if (Views.Any(x => x.Name == name))
				throw new InvalidOperationException("A view with the same name already exists.");

			var rv = CsDbArcView.Create(this, name);
			_views.Add(rv);
			return rv;
		}
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"Database[{Name}]";
		}
	}
}