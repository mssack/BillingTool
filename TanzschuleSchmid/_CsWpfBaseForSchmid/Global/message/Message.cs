// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CsWpfBase.Ev.Objects;





namespace CsWpfBase.Global.message
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgMessage : Base
	{
		#region SINGLETON CLASS
		private static CsgMessage _instance;
		private static readonly object SingletonLock = new object();
		private CsgMessage()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgMessage I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgMessage());
				}
			}
		}
		#endregion


		/// <summary>Pushes a message on the users screen.</summary>
		public CsMessage.MessageResults Push(object content, CsMessage.Types type = CsMessage.Types.Information, string title = null, CsMessage.MessageButtons buttons = CsMessage.MessageButtons.Ok, [CallerMemberName] string methodName = null, [CallerFilePath] string classFilePath = null, [CallerLineNumber] int classLineNumber = 0)
		{
			if (Application.Current == null)
				return CsMessage.MessageResults.Undefined;

			if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
			{
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
				{
					var w1 = new CsMessageWindow(new CsMessage(type, content, title, buttons, methodName, classFilePath, classLineNumber));
					w1.ShowDialog();
				}));
				return CsMessage.MessageResults.Undefined;
			}
			var w = new CsMessageWindow(new CsMessage(type, content, title, buttons, methodName, classFilePath, classLineNumber));
			return w.ShowDialog();
		}
	}
}