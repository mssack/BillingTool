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






namespace CsWpfBase.Global.external.cmd
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	public sealed class CsgreHiddenCmd : Base
	{
		private static CsgreHiddenCmd _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgreHiddenCmd I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgreHiddenCmd());
				}
			}
		}

		private CsgreHiddenCmd()
		{
		}

		/// <summary>Delayed command execution. Use this to open a CMD window then wait for a specific amount of time and then execute commands.</summary>
		public CsgreHiddenTimedCmd Timed
		{
			get { return CsgreHiddenTimedCmd.I; }
		}

		/// <summary>Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is opened.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <returns>The associated process.</returns>
		public Task<Process> ElevatedCommand(IEnumerable<string> commands)
		{
			var procStartInfo = new ProcessStartInfo
			{
				RedirectStandardError = false,
				RedirectStandardOutput = false,
				UseShellExecute = true,
				FileName = "cmd",
				Verb = "runas",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				Arguments = "/C " + commands.Join("&"),
			};


			var process = new Process();
			process.EnableRaisingEvents = true;
			process.StartInfo = procStartInfo;
			var tcs = new TaskCompletionSource<Process>();
			process.Exited += (sender, args) => tcs.SetResult(process);
			try
			{
				process.Start();
			}
			catch (Exception e)
			{
				tcs.SetException(e);
			}

			return tcs.Task;
		}

		/// <summary>Runs a CMD command in an elevated window. If current process is not elevated then UAC window is opened.</summary>
		/// <param name="command">The command to execute.</param>
		/// <returns>The associated process.</returns>
		public Task<Process> ElevatedCommand(string command)
		{
			return ElevatedCommand(new[] {command});
		}

		/// <summary>Runs a list of CMD commands.</summary>
		/// <param name="commands">List of commands to execute by CMD window.</param>
		/// <returns>The associated process.</returns>
		public Task<Process> Command(IEnumerable<string> commands)
		{
			var procStartInfo = new ProcessStartInfo
			{
				RedirectStandardError = false,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				FileName = "cmd",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				Arguments = "/c " + commands.Join("&"),
			};


			var process = new Process();
			process.EnableRaisingEvents = true;
			process.StartInfo = procStartInfo;
			var tcs = new TaskCompletionSource<Process>();
			process.Exited += (sender, args) => tcs.SetResult(process);
			try
			{
				process.Start();
			}
			catch (Exception e)
			{
				tcs.SetException(e);
			}


			return tcs.Task;
		}

		/// <summary>Runs a CMD command.</summary>
		/// <param name="command">The command to execute.</param>
		/// <returns>The associated process.</returns>
		public Task<Process> Command(string command)
		{
			return Command(new[] {command});
		}
	}
}