// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Db.codegen;






namespace CsWpfBase.Db
{
	/// <summary>Database toolkit for connections with different data types.</summary>
	public static class CsDb
	{
		/// <summary>Code generating</summary>
		public static CsDbCodeGen CodeGen => CsDbCodeGen.I;
	}
}