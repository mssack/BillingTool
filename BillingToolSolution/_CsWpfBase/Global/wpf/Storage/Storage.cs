// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Exceptions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.wpf.Storage
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpfStorage : Base
	{
		private static CsgWpfStorage _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpfStorage I
		{
			get
			{
				if (_instance == null)
					throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.WpfStorage);

				return _instance;
			}
		}

		private CsgWpfStorage()
		{
		}
		internal static void Install()
		{
			if (!CsGlobal.IsInstalled(GlobalFunctions.Storage))
				throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.Storage);
			_instance = new CsgWpfStorage();
		}

		/// <summary>
		///     With the window handler you can save the position of windows and handle size. The data will be saved to disc. To use this you have to initialize
		///     the window handler.
		/// </summary>
		public CsgWpfStorageWindow Window
		{
			get { return CsgWpfStorageWindow.I; }
		}
		/// <summary>
		///     With the ListView handler you can save the column widths and handle size. The data will be saved to disc. To use this you have to initialize the
		///     ListViewHandler.
		/// </summary>
		public CsgWpfStorageListView ListView
		{
			get { return CsgWpfStorageListView.I; }
		}
	}
}