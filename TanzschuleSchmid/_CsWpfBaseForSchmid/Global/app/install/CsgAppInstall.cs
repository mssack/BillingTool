// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.app.install.agreement;
using CsWpfBase.Global.app.install.shortcut;






namespace CsWpfBase.Global.app.install
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgAppInstall : Base
	{
		private static CsgAppInstall _instance;
		private static readonly object SingletonLock = new object();
		internal static CsgAppInstall I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgAppInstall());
				}
			}
		}

		private CsgAppInstall()
		{
		}

		/// <summary>Subset  of methods for shortcut installation.</summary>
		public CsgAppInstallShortcut Shortcut
		{
			get { return CsgAppInstallShortcut.I; }
		}
		/// <summary>Provides informations for the current program license agreements.</summary>
		public CsgAppInstallAgreement Agreement
		{
			get { return CsgAppInstallAgreement.I; }
		}

		/// <summary>
		///     <para>Set current thread culture to German</para>
		///     <para>includes WPF based date time formating --> change it to <code>dd.MM.yyyy HH:mm:ss</code>
		///     </para>
		/// </summary>
		public void GermanThread()
		{
			var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			culture.DateTimeFormat.FullDateTimePattern = "dd.MM.yyyy HH:mm:ss";
			culture.DateTimeFormat.LongDatePattern = "dd.MM.yyyy HH:mm:ss";
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
			FrameworkElement.LanguageProperty.OverrideMetadata(typeof (FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
		}
	}
}