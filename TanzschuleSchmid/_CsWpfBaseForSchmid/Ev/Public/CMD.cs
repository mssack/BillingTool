// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CsWpfBase.Ev.Public.Extensions;





namespace CsWpfBase.Ev.Public
{
	/// <summary>Allows different methods to delegate CMD commands to a CMD command window.</summary>
	public static class Cmd
	{
		/// <summary>Execute CMD command in a hidden CMD window.</summary>
		public static class Hidden
		{
			/// <summary>
			///     Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is
			///     opened.
			/// </summary>
			/// <param name="commands">List of commands to execute by CMD window.</param>
			/// <returns>The associated process.</returns>
			public static Task<Process> RunElevated(IEnumerable<string> commands)
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
			public static Task<Process> RunElevated(string command)
			{
				return RunElevated(new[] {command});
			}
			/// <summary>Runs a list of CMD commands.</summary>
			/// <param name="commands">List of commands to execute by CMD window.</param>
			/// <returns>The associated process.</returns>
			public static Task<Process> Run(IEnumerable<string> commands)
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
			public static Task<Process> Run(string command)
			{
				return Run(new[] {command});
			}





			/// <summary>
			///     Delayed command execution. Use this Wrapper to open a CMD window then wait for a specific amount of time and
			///     then execute commands.
			/// </summary>
			public static class Timed
			{
				/// <summary>
				///     Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is
				///     opened.
				/// </summary>
				/// <param name="commands">List of commands to execute by CMD window.</param>
				/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
				/// <returns>The associated process.</returns>
				public static Task<Process> RunElevated(List<string> commands, int millisecounds = 500)
				{
					commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
					return Hidden.RunElevated(commands);
				}
				/// <summary>Runs a list of CMD commands.</summary>
				/// <param name="commands">List of commands to execute by CMD window.</param>
				/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
				/// <returns>The associated process.</returns>
				public static Task<Process> Run(List<string> commands, int millisecounds = 500)
				{
					commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
					return Hidden.Run(commands);
				}
			}
		}





		/// <summary>Execute CMD command in a visible CMD window.</summary>
		public static class Visible
		{
			/// <summary>
			///     Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is
			///     opened.
			/// </summary>
			/// <param name="commands">List of commands to execute by CMD window.</param>
			/// <returns>The associated process.</returns>
			public static Task<Process> RunElevated(IEnumerable<string> commands)
			{
				var procStartInfo = new ProcessStartInfo
									{
										RedirectStandardError = false,
										RedirectStandardOutput = false,
										FileName = "cmd",
										Verb = "runas",
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
			public static Task<Process> RunElevated(string command)
			{
				return RunElevated(new[] {command});
			}
			/// <summary>Runs a list of CMD commands.</summary>
			/// <param name="commands">List of commands to execute by CMD window.</param>
			/// <returns>The associated process.</returns>
			public static Task<Process> Run(IEnumerable<string> commands)
			{
				var procStartInfo = new ProcessStartInfo
									{
										RedirectStandardError = false,
										RedirectStandardOutput = false,
										FileName = "cmd",
										Arguments = "/C " + commands.Join("&"),
									};

				var process = new Process {EnableRaisingEvents = true, StartInfo = procStartInfo};
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
			public static Task<Process> Run(string command)
			{
				return Run(new[] {command});
			}





			/// <summary>
			///     Delayed command execution. Use this Wrapper to open a CMD window then wait for a specific amount of time and
			///     then execute commands.
			/// </summary>
			public static class Timed
			{
				/// <summary>
				///     Runs a list of CMD commands in an elevated window. If current process is not elevated then UAC window is
				///     opened.
				/// </summary>
				/// <param name="commands">List of commands to execute by CMD window.</param>
				/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
				/// <returns>The associated process.</returns>
				public static Task<Process> RunElevated(List<string> commands, int millisecounds = 500)
				{
					commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
					return Visible.RunElevated(commands);
				}
				/// <summary>Runs a list of CMD commands.</summary>
				/// <param name="commands">List of commands to execute by CMD window.</param>
				/// <param name="millisecounds">Milliseconds to wait in external process before commands will be executed</param>
				/// <returns>The associated process.</returns>
				public static Task<Process> Run(List<string> commands, int millisecounds = 500)
				{
					commands.Insert(0, "ping 192.0.2.2 -n 1 -w " + millisecounds + " > nul");
					return Visible.Run(commands);
				}
			}
		}
	}
}