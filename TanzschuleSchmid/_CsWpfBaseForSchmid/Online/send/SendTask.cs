// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Threading;
using System.Threading.Tasks;
using CsWpfBase.Online.packets;






namespace CsWpfBase.Online.send
{
	/// <summary>
	///     A task created to give the user the possibility to make a call chain after sending data to the remote host. The user can append specialized Task
	///     before ending in own functions.
	/// </summary>
	public class SendTask : Task<CsoPacket>
	{
		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified function.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<CsoPacket> function) : base(function)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified function.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to be assigned to this task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<CsoPacket> function, CancellationToken cancellationToken) : base(function, cancellationToken)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified function and creation options.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for
		///     <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<CsoPacket> function, TaskCreationOptions creationOptions) : base(function, creationOptions)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified function and creation options.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for
		///     <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<CsoPacket> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(function, cancellationToken, creationOptions)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified function and state.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<object, CsoPacket> function, object state) : base(function, state)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified action, state, and options.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="state">An object representing data to be used by the function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to be assigned to the new task.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<object, CsoPacket> function, object state, CancellationToken cancellationToken) : base(function, state, cancellationToken)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified action, state, and options.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="state">An object representing data to be used by the function.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for
		///     <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<object, CsoPacket> function, object state, TaskCreationOptions creationOptions) : base(function, state, creationOptions)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task`1" /> with the specified action, state, and options.</summary>
		/// <param name="function">
		///     The delegate that represents the code to execute in the task. When the function has completed, the task's
		///     <see cref="P:System.Threading.Tasks.Task`1.Result" /> property will be set to return the result value of the function.
		/// </param>
		/// <param name="state">An object representing data to be used by the function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to be assigned to the new task.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for
		///     <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is null.</exception>
		public SendTask(Func<object, CsoPacket> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : base(function, state, cancellationToken, creationOptions)
		{
		}

		/// <summary>Calls the cs online system and processes the server response.</summary>
		public Task<CsoPacket> ProcessResponse()
		{
			return ContinueWith(t =>
			{
				if (t.Exception != null)
					throw t.Exception;

				CsOnline.Response.Process.Complete(t.Result);
				return t.Result;
			});
		}
	}
}