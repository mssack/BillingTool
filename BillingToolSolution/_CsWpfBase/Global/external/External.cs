// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.external.cmd;






namespace CsWpfBase.Global.external
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgRunExternal : Base
	{
		private static CsgRunExternal _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgRunExternal I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgRunExternal());
				}
			}
		}

		private CsgRunExternal()
		{
		}

		/// <summary>Executes a cmd command</summary>
		public CsgreCmd Cmd
		{
			get { return CsgreCmd.I; }
		}

		/// <summary>Opens a google search for a specific term.</summary>
		public void Google(string term)
		{
			Process.Start("https://www.google.com/?#safe=off&q=" + term);
		}

		/// <summary>Opens a google maps search for a specific term.</summary>
		public void GoogleMaps(string term)
		{
			Process.Start("http://maps.google.com/maps?q=" + term);
		}

		/// <summary>Opens a Website</summary>
		public void OpenWebpage(string website)
		{
			var w = website.Trim().ToLower();
			if (!w.StartsWith("http"))
				w = "http://" + w;

			Process.Start(w);
		}

	}
}