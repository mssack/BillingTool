// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen
{
	/// <summary>see CsDb property</summary>
	public class CsDbCodeGenStatics : Base
	{
		#region SINGLETON CLASS
		private static CsDbCodeGenStatics _instance;
		private static readonly object SingletonLock = new object();
		private Func<DateTime> _dateTimeNowFunction;
		private Func<Guid> _newGuidFunction;

		private CsDbCodeGenStatics()
		{
		}

		/// <summary>Returns the singleton instance</summary>
		internal static CsDbCodeGenStatics I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsDbCodeGenStatics());
				}
			}
		}
		#endregion



		/// <summary>This is the value which will be used if the database default value is a function for creating a new guid.</summary>
		public Func<Guid> NewGuidFunction => _newGuidFunction ?? (_newGuidFunction = Guid.NewGuid);
		/// <summary>This is the value which will be used if the database default value is a function for creating the current date and time.</summary>
		public Func<DateTime> DateTimeNowFunction => _dateTimeNowFunction??(_dateTimeNowFunction = () => DateTime.Now);
	}
}