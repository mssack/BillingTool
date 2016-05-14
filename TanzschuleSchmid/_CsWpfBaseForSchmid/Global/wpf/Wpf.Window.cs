// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-05</date>

using System;
using System.Linq;
using System.Windows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Themes.Controls.Containers;






namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpfWindow : Base
	{
		private static CsgWpfWindow _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		public static CsgWpfWindow I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpfWindow());
				}
			}
		}

		private CsgWpfWindow()
		{
		}


		/// <summary>Grays out all <see cref="CsWindow" /> instances.</summary>
		public IDisposable GrayOutAllWindows()
		{
			return new GrayOutClass();
		}



		private class GrayOutClass : IDisposable
		{
			public GrayOutClass()
			{
				Application.Current.Windows.OfType<Window>().Where(x => x is CsWindow).OfType<CsWindow>().ToArray().ForEach(x => x.IsGrayedOut = true);
			}


			#region Overrides/Interfaces
			public void Dispose()
			{
				Application.Current.Windows.OfType<Window>().Where(x => x is CsWindow).OfType<CsWindow>().ToArray().ForEach(x => x.IsGrayedOut = false);
			}
			#endregion
		}
	}
}