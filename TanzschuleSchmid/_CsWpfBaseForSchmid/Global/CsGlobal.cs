// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.app;
using CsWpfBase.Global.computer;
using CsWpfBase.Global.debug;
using CsWpfBase.Global.external;
using CsWpfBase.Global.message;
using CsWpfBase.Global.os;
using CsWpfBase.Global.storage;
using CsWpfBase.Global.storage.configfile;
using CsWpfBase.Global.transmission;
using CsWpfBase.Global.wpf;
using CsWpfBase.Global.wpf.Storage;
using CsWpfBase.Online;






namespace CsWpfBase.Global
{
	/// <summary>The global name space from Christian Sack, containing lot of useful features for fast program creation.</summary>
	public static class CsGlobal
	{
		/// <summary>App specific information like usage details ids or assembly infos for the current application.</summary>
		public static CsgApp App => CsgApp.I;
		/// <summary>Debug helper for formatting debug messages.</summary>
		public static CsgDebug Debug => CsgDebug.I;
		/// <summary>Frequently used external programs or processes.</summary>
		public static CsgRunExternal RunExternal => CsgRunExternal.I;
		/// <summary>Used for saving objects on local discs.</summary>
		public static CsgStorage Storage => CsgStorage.I;
		/// <summary>Message system to inform user or interact with user through a message window.</summary>
		public static CsgMessage Message => CsgMessage.I;
		/// <summary>collapsing useful functions for interacting with WPF.</summary>
		public static CsgWpf Wpf => CsgWpf.I;
		/// <summary>computer scoped things like hardware or ids.</summary>
		public static CsgComputer Computer => CsgComputer.I;
		/// <summary>OS scoped things like version and name.</summary>
		public static CsgOs Os => CsgOs.I;
		/// <summary>Gets transmission related functions.</summary>
		public static CsgTransmission Transmission => CsgTransmission.I;
		private static GlobalFunctions InstalledFunctions { get; set; }

		/// <summary>Checks if specific code is installed.</summary>
		public static bool IsInstalled(GlobalFunctions code)
		{
			return InstalledFunctions.HasFlag(code);
		}

		/// <summary>
		///     <para>Installs and activate the passed components.</para>
		/// </summary>
		public static void Install(GlobalFunctions code)
		{
			Debug.Write(code == 0 ? "NOTHING; pass function codes to function" : Enum.GetValues(typeof (GlobalFunctions)).Cast<GlobalFunctions>().Where(x => code.HasFlag(x)).Select(x => x.ToString()).Join() + "\r\n");

			InstalledFunctions = code;
			if (code.HasFlag(GlobalFunctions.RedirectUnhandledExceptions))
			{
				Application.Current.DispatcherUnhandledException += (sender, args) =>
				{
					Task t = null;
					if (IsInstalled(GlobalFunctions.OnlineConnectivity))
					{
						t = CsOnline.SendAsync.Exception(args.Exception);
					}

					Message.Push(args.Exception, CsMessage.Types.FatalError);
					if (t != null)
						t.Wait();
					Environment.Exit(0);
				};
			}
			if (code.HasFlag(GlobalFunctions.GermanThreadCulture))
				App.Install.GermanThread();
			if (code.HasFlag(GlobalFunctions.ConfigFile))
				CsgConfigFile.Install();
			if (code.HasFlag(GlobalFunctions.Storage))
			{

			}
			if (code.HasFlag(GlobalFunctions.AppData))
				CsgAppData.Install();
			if (code.HasFlag(GlobalFunctions.WpfStorage))
				CsgWpfStorage.Install();
			if (code.HasFlag(GlobalFunctions.Agreement))
				App.Install.Agreement.CheckAcceptance();
			if (code.HasFlag(GlobalFunctions.OnlineConnectivity))
			{
#if DEBUG
				CsOnline.SendAsync.CompleteInfo().ProcessResponse();
#else
				if (!IsInstalled(GlobalFunctions.AppData) || App.Data.LastExecutionTime == null || (DateTime.Now - App.Data.LastExecutionTime.Value).TotalDays > 1)
					CsOnline.SendAsync.CompleteInfo().ProcessResponse();
				else
					CsOnline.SendAsync.AppInfo().ProcessResponse();
#endif
			}
		}
	}
}