// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
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

		private static CsgMessage _instance;
		private static readonly object SingletonLock = new object();
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

		private CsgMessage()
		{
		}


		/// <summary>Pushes a message on the users screen.</summary>
		public CsMessage.MessageResults Push(object content, CsMessage.Types type = CsMessage.Types.Information, string title = null, CsMessage.MessageButtons buttons = CsMessage.MessageButtons.Ok, [CallerMemberName] string methodName = null, [CallerFilePath] string classFilePath = null, [CallerLineNumber] int classLineNumber = 0)
		{
			if (Application.Current == null)
				return CsMessage.MessageResults.Undefined;

			if (Application.Current.Dispatcher.Thread != Thread.CurrentThread)
			{
				Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => { GetWindow(content, type, title, buttons, methodName, classFilePath, classLineNumber).ShowDialog(); }));
				return CsMessage.MessageResults.Undefined;
			}
			return GetWindow(content, type, title, buttons, methodName, classFilePath, classLineNumber).ShowDialog();
		}

		/// <summary>Pushes a message on the users screen.</summary>
		public CsMessageWindow GetWindow(object content, CsMessage.Types type = CsMessage.Types.Information, string title = null, CsMessage.MessageButtons buttons = CsMessage.MessageButtons.Ok, [CallerMemberName] string methodName = null, [CallerFilePath] string classFilePath = null, [CallerLineNumber] int classLineNumber = 0)
		{
			if (Application.Current == null)
				return null;
			var w1 = new CsMessageWindow(new CsMessage(type, content, title, buttons, methodName, classFilePath, classLineNumber));
			return w1;
		}

		/// <summary>Sets the default message scaling.</summary>
		public void SetDefaultScaling(double scaling)
		{
			CsMessageWindow.DefaultContentScaling = scaling;
		}
	}
}