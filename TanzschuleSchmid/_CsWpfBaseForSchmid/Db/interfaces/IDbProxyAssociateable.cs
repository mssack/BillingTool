// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-05</date>

using System;






namespace CsWpfBase.Db.interfaces
{
	/// <summary>Used <see cref="IDbProxy" /> which can be initialized by the <see cref="Associate" /> method.</summary>
	public interface IDbProxyAssociateable : IDbProxy
	{
		#region Abstract
		/// <summary>Associates the <see cref="IDbProxy" /> with a database name.</summary>
		/// <param name="catalogName">the name of the target data base.</param>
		void Associate(string catalogName);
		#endregion
	}
}