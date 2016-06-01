// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;





namespace CsWpfBase.Ev.Objects.FuncExt.Endless
{
	/// <summary>Provides a Result for an endless but pause able operation.</summary>
	[Serializable]
	public class BaseEndlessResult : Base
	{
		internal void SetPaused()
		{
		}
		internal void SetContinued()
		{
		}
		internal void SetStarted()
		{
		}
		internal void SetPausing()
		{
		}
	}
}