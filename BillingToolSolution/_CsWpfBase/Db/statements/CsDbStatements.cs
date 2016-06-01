// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-20</date>

using System;
using CsWpfBase.Db.statements.sqlce;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.statements
{
	/// <summary></summary>
	public class CsDbStatements : Base
	{
		private static CsDbStatements _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsDbStatements I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbStatements());
				}
			}
		}

		private CsDbStatements()
		{
		}


		/// <summary>Collapses statements which can be used for an SQLCE database.</summary>
		public SqlCeStatements SqlCe => SqlCeStatements.I;
	}
}