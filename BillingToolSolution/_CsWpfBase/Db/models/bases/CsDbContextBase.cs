// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-05</date>

using System;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.models.bases
{
	/// <summary>The base class for all db context.</summary>
	[Serializable]
	public abstract class CsDbContextBase : Base
	{
		#region Abstract
		/// <summary>Gets the native database server name of the context.</summary>
		public abstract string Name { get; }

		/// <summary>Sets all db proxy's inside this context.</summary>
		public abstract void Set_DbProxy<T>() where T : IDbProxyAssociateable;

		/// <summary>Loads the constraints on all data sets inside this context.</summary>
		public abstract void LoadConstraints();

		/// <summary>Retrieves the database by database name.</summary>
		public abstract CsDbDataSet GetDatabaseByName(string name);
		#endregion
	}
}