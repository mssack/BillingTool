// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-11</date>

using System;






namespace CsWpfBase.Db.interfaces
{
	/// <summary>Contains a connection, or a database proxy or something else.</summary>
	public interface IContainDbProxy
	{
		#region Abstract
		/// <summary>The db proxy all commands should be routed through this.</summary>
		IDbProxy DbProxy { get; }
		#endregion
	}
}