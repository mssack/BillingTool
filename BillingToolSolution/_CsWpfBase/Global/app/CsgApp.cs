// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.app.install;






namespace CsWpfBase.Global.app
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgApp : Base
	{
		private static CsgApp _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgApp I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgApp());
				}
			}
		}

		private CsgApp()
		{
			if (Application.Current != null)
				Application.Current.Exit += (sender, args) =>
				{
					if (OnExit != null)
						OnExit(args);
				};
		}

		/// <summary>Gets runtime entry assembly informations about the current application. This scope is only available on applications not in web.</summary>
		public CsgAppInfo Info
		{
			get { return CsgAppInfo.I; }
		}
		/// <summary>Gets application data which will also be saved to disc.</summary>
		public CsgAppData Data
		{
			get { return CsgAppData.I; }
		}
		/// <summary>Gets the application installation methods.</summary>
		public CsgAppInstall Install
		{
			get { return CsgAppInstall.I; }
		}
		/// <summary>Invokes when the application exits. Use this event whenever it is possible.</summary>
		public event Action<ExitEventArgs> OnExit;

		/// <summary>Do an ordered exit of the application. Use this function to exit the application appropriate.</summary>
		public void Exit()
		{
			if (Application.Current == null || Application.Current.MainWindow == null)
			{
				Environment.Exit(0);
			}
			if (Application.Current.MainWindow.IsInitialized)
				Application.Current.Shutdown();
			else
			{
				Application.Current.MainWindow.Initialized += (sender, args) =>
				{
					var window = (Window) sender;
					window.Visibility = Visibility.Collapsed;
					window.ShowInTaskbar = false;
					Application.Current.Shutdown();
				};
			}
		}
	}
}