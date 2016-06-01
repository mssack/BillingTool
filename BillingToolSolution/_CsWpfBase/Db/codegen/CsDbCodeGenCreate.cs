// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using CsWpfBase.Db.codegen.architecture;
using CsWpfBase.Db.codegen.architecture.generators;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Db.router;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen
{
	/// <summary>see <see cref="CsDb" />.</summary>
	public class CsDbCodeGenCreate : Base
	{
		private static CsDbCodeGenCreate _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsDbCodeGenCreate I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbCodeGenCreate());
				}
			}
		}

		private CsDbCodeGenCreate()
		{
		}


		/// <summary>Creates an architecture from SqlCe routers belonging to one context.</summary>
		public CsDbArchitecture Architecture_From_SqlCe(params CsDbRouter[] routers)
		{
			return new CsDbArcGen_SqlCe(routers).Generate();
		}

		/// <summary>
		///     Creates the architecture from an new connection created with the following connection string:
		///     <para>Data Source={server};User id={user};Password={password}</para>
		///     The architecture contains all tables which are accessible by the specified user.
		/// </summary>
		/// <param name="server">The target server name</param>
		/// <param name="user">The user name for the connection</param>
		/// <param name="password">The password</param>
		public CsDbArchitecture Architecture_From_Sql(string server, string user, string password)
		{
			return new CsDbArcGen_Sql(new CsDbRouter_SqlDirect() {DataSource = server, UserName = user, Password = password}).Generate();
		}
	}
}