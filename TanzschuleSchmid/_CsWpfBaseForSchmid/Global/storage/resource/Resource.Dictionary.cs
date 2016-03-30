// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.resource
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgResourceDictionary : Base
	{
		#region SINGLETON CLASS
		private static CsgResourceDictionary _instance;
		private static readonly object SingletonLock = new object();
		private CsgResourceDictionary()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsgResourceDictionary I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgResourceDictionary());
				}
			}
		}
		#endregion


		private ResourceDictionary _stylesStandard;


		/// <summary>Gets the standard resource dictionary.</summary>
		public ResourceDictionary Standard
		{
			get { return _stylesStandard ?? (_stylesStandard = Load(CsGlobal.Storage.Resource.Path.Get("CsWpfBase", "Themes/Standard.xaml"))); }
		}


		private static ResourceDictionary Load(string filepath)
		{
			var dict = new ResourceDictionary();
			dict.BeginInit();
			dict.Source = new Uri(filepath, UriKind.RelativeOrAbsolute);
			dict.EndInit();
			return dict;
		}
	}
}