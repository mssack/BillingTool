// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.wpf.Storage;





namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpf : Base
	{
		#region SINGLETON CLASS
		private static CsgWpf _instance;
		private static readonly object SingletonLock = new object();
		private CsgWpf()
		{
		}
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
		#endregion


		/// <summary>Provides a way to save states of user controls in wpf.</summary>
		public CsgWpfStorage Storage
		{
			get { return CsgWpfStorage.I; }
		}
		/// <summary>Collapsing animation helpers for code behind animations.</summary>
		public CsgWpfAnimation Animation
		{
			get { return CsgWpfAnimation.I; }
		}
		/// <summary>Provides a singleton time class which is bind able and provides automated updates when time changes.</summary>
		public CsgWpfTime Time
		{
			get { return CsgWpfTime.I; }
		}
		/// <summary>Visual tree helper methods.</summary>
		public CsgWpfVisualTree VisualTree
		{
			get { return CsgWpfVisualTree.I; }
		}
		/// <summary>Dragging helper methods.</summary>
		public CsgWpfDragging Dragging
		{
			get { return CsgWpfDragging.I; }
		}
	}
}