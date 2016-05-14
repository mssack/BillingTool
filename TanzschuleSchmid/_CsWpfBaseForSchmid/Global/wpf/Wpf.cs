// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-05</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.wpf.Storage;






namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpf : Base
	{
		private static CsgWpf _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpf I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpf());
				}
			}
		}

		private CsgWpf()
		{
		}


		/// <summary>Provides a way to save states of user controls in wpf.</summary>
		public CsgWpfStorage Storage => CsgWpfStorage.I;
		/// <summary>Collapsing animation helpers for code behind animations.</summary>
		public CsgWpfAnimation Animation => CsgWpfAnimation.I;
		/// <summary>Provides a singleton time class which is bind able and provides automated updates when time changes.</summary>
		public CsgWpfTime Time => CsgWpfTime.I;
		/// <summary>Visual tree helper methods.</summary>
		public CsgWpfVisualTree VisualTree => CsgWpfVisualTree.I;
		/// <summary>Dragging helper methods.</summary>
		public CsgWpfDragging Dragging => CsgWpfDragging.I;
		/// <summary>Window helper methods.</summary>
		public CsgWpfWindow Window => CsgWpfWindow.I;
	}
}