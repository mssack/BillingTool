// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen
{
	/// <summary>For further infos see <see cref="CsDb" />.</summary>
	[Serializable]
	public class CsDbCodeGen : Base
	{
		private static CsDbCodeGen _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsDbCodeGen I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbCodeGen());
				}
			}
		}

		private CsDbCodeGen()
		{
		}

		/// <summary>Convert functions.</summary>
		public CsDbCodeGenConvert Convert => CsDbCodeGenConvert.I;
		/// <summary>Static values needed for db functions.</summary>
		public CsDbCodeGenStatics Statics => CsDbCodeGenStatics.I;
		/// <summary>Creates architectures or code.</summary>
		public CsDbCodeGenCreate Create => CsDbCodeGenCreate.I;
		/// <summary>Used for tracing messages while code is generating.</summary>
		internal CsDbCodeGenTracing Tracing => CsDbCodeGenTracing.I;
	}



}