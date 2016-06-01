// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CsWpfBase.Db.codegen.architecture.parts;
using CsWpfBase.Db.codegen.code;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.architecture
{
	/// <summary>A database context, is a subset of databases inside a database server.</summary>
	[Serializable]
	public class CsDbArchitecture : Base
	{
		private readonly List<CsDbArcDatabase> _databases = new List<CsDbArcDatabase>();
		private string _name;

		/// <summary>The database context name could be a server or something similar.</summary>
		public CsDbArchitecture()
		{
			Databases = _databases.AsReadOnly();
		}

		/// <summary>The context name.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>A list of all Databases inside this architecture.</summary>
		public ReadOnlyCollection<CsDbArcDatabase> Databases { get; }

		/// <summary>Write an code bundle for the db architecture.</summary>
		public CsDbCodeBundle GetCodeBundle()
		{
			var codeBundle = CsDbCodeBundle.FromArchitecture(this);
			return codeBundle;
		}

		internal CsDbArcDatabase NewDatabase(string name)
		{
			var database = CsDbArcDatabase.New(this, name);
			_databases.Add(database);
			return database;
		}
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return $"Architecture[{Name}]";
		}
	}
}