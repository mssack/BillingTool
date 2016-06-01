// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps threading methods.</summary>
	[DebuggerStepThrough]
	public static class ThreadingExtensions
	{
		/// <summary>Dispatches an Action async to the UI Thread</summary>
		public static void DispatchOnUi(this Action action)
		{
			Application.Current.Dispatcher.BeginInvoke(action, DispatcherPriority.Normal);
		}
	}
}