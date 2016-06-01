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






namespace CsWpfBase.Global.external.cmd
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	public sealed class CsgreVisibleTimedCmd : Base
	{
		private static CsgreVisibleTimedCmd _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgreVisibleTimedCmd I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgreVisibleTimedCmd());
				}
			}
		}

		private CsgreVisibleTimedCmd()
		{
		}

		/// <summary>Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is opened.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
		/// <returns>The associated process.</returns>
		public static Task<Process> RunElevated(List<string> commands, int millisecounds = 500)
		{
			commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
			return CsGlobal.RunExternal.Cmd.Visible.RunElevated(commands);
		}

		/// <summary>Runs a list of CMD commands.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
		/// <returns>The associated process.</returns>
		public static Task<Process> Run(List<string> commands, int millisecounds = 500)
		{
			commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
			return CsGlobal.RunExternal.Cmd.Visible.Run(commands);
		}
	}
}