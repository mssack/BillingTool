// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;






// ReSharper disable once CheckNamespace





namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal class CallerMemberNameAttribute : Attribute
	{
	}





	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal class CallerFilePathAttribute : Attribute
	{
	}





	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	internal class CallerLineNumberAttribute : Attribute
	{
	}
}