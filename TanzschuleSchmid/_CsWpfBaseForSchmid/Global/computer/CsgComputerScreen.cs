// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerScreen : Base
	{
		private static CsgComputerScreen _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerScreen I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerScreen());
				}
			}
		}
		private bool _isCollected;
		private double _totalHeight;
		private double _totalWidth;

		private CsgComputerScreen()
		{
		}

		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return TotalWidth + " * " + TotalHeight;
		}

		/// <summary>Gets the total screen width.</summary>
		public double TotalWidth
		{
			get
			{
				Collect(true);
				return _totalWidth;
			}
			private set { SetProperty(ref _totalWidth, value); }
		}
		/// <summary>Gets the total screen height.</summary>
		public double TotalHeight
		{
			get
			{
				Collect(true);
				return _totalHeight;
			}
			private set { SetProperty(ref _totalHeight, value); }
		}

		private void Collect(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			TotalWidth = SystemParameters.VirtualScreenWidth;
			TotalHeight = SystemParameters.VirtualScreenHeight;


			_isCollected = true;
		}
	}
}