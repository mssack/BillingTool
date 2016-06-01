// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;






namespace CsWpfBase.Utilitys.codingTestUtils.data
{
	/// <summary>See at <see cref="CsTest" />.</summary>
	public sealed class CsTestData
	{
		private static CsTestData _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsTestData I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsTestData());
				}
			}
		}
		/// <summary>All kind of generating stuff for testing purpose.</summary>
		public CsTestDataGenerator Generate => CsTestDataGenerator.I;

		private CsTestData()
		{
		}
	}
}