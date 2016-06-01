// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Media;
using CsWpfBase.Ev.Objects;





namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpfVisualTree : Base
	{
		#region SINGLETON CLASS
		private static CsgWpfVisualTree _instance;
		private static readonly object SingletonLock = new object();
		private CsgWpfVisualTree()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpfVisualTree I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpfVisualTree());
				}
			}
		}
		#endregion


		/// <summary>find a child in a container which fits into a predicate.</summary>
		public FrameworkElement FindChild(FrameworkElement container, Predicate<FrameworkElement> condition)
		{
			int childrenCount = VisualTreeHelper.GetChildrenCount(container);
			var children = new FrameworkElement[childrenCount];

			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(container, i) as FrameworkElement;
				children[i] = child;
				if (condition(child))
					return child;
			}

			for (int i = 0; i < childrenCount; i++)
				if (children[i] != null)
				{
					var subChild = FindChild(children[i], condition);
					if (subChild != null)
						return subChild;
				}

			return null;
		}
	}
}