// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;






namespace CsWpfBase.Ev.Exceptions
{
	/// <summary>Occurs whenever a function is not installed prior to usage or is currently not supported by application type.</summary>
	[Serializable]
	public class CsGlobalFunctionNotConfiguredException : CsGlobalException
	{
		internal CsGlobalFunctionNotConfiguredException(GlobalFunctions function)
			: base($"The CsGlobal function '{function}' is not installed, please install functions first!")
		{
		}
	}
}