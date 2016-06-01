// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Ev.Exceptions
{
	/// <summary>Used for all exceptions thrown by this application.</summary>
	[Serializable]
	public class CsGlobalException : Exception
	{
		internal CsGlobalException()
		{
		}
		internal CsGlobalException(string message)
			: base(message)
		{
		}
		internal CsGlobalException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}