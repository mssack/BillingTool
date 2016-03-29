// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-02</date>

using System;
using System.Diagnostics;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.codegen
{
	internal class CsDbCodeGenTracing
	{
		private static CsDbCodeGenTracing _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsDbCodeGenTracing I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbCodeGenTracing());
				}
			}
		}

		private CsDbCodeGenTracing()
		{
		}


		public void Trace(string message, int level = 0)
		{
			var prefix = "CsDb.CodeGen => ";
			if (level != 0)
				prefix = "".Expand(level*3 + prefix.Length) + ">";
			Debug.WriteLine(prefix + message.Replace("\r\n", "\n").Replace("\n", "\n".Expand(prefix.Length)));
		}
	}
}