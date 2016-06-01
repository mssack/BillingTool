// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using CsWpfBase.Utilitys.codingTestUtils.comparison;
using CsWpfBase.Utilitys.codingTestUtils.data;






namespace CsWpfBase.Utilitys.codingTestUtils
{
	/// <summary>Used on all Stuff for testing purpose</summary>
	public static class CsTest
	{
		/// <summary>All kind of data stuff for testing purpose.</summary>
		public static CsTestData Data => CsTestData.I;
		/// <summary>All kind of comparing tools.</summary>
		public static CsTestComparison Comparison => CsTestComparison.I;
	}



}