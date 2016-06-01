// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;






namespace BillingOutput.TaskSchedulers
{
	/// <summary>Executes task only on STA threads.</summary>
	internal class StaTaskScheduler : TaskScheduler
	{
		private readonly List<Thread> _threads;
		private BlockingCollection<Task> _tasks;



		public StaTaskScheduler(int numberOfThreads)

		{
			if (numberOfThreads < 1)
				throw new ArgumentOutOfRangeException("concurrencyLevel");

			_tasks = new BlockingCollection<Task>();
			_threads = Enumerable.Range(0, numberOfThreads).Select(i =>
			{
				var thread = new Thread(() =>
				{
					foreach (var t in
						_tasks.GetConsumingEnumerable())
					{
						TryExecuteTask(t);
					}
				})
				{
					IsBackground = true
				};
				thread.SetApartmentState(ApartmentState.STA);
				return thread;
			}).ToList();

			_threads.ForEach(t => t.Start());
		}


		#region Overrides/Interfaces
		/// <summary>Queues a <see cref="T:System.Threading.Tasks.Task" /> to the scheduler.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be queued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		protected override void QueueTask(Task task)
		{

			_tasks.Add(task);
		}

		/// <summary>
		///     Determines whether the provided <see cref="T:System.Threading.Tasks.Task" /> can be executed synchronously in this call, and if it can,
		///     executes it.
		/// </summary>
		/// <returns>A Boolean value indicating whether the task was executed inline.</returns>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">
		///     A Boolean denoting whether or not task has previously been queued. If this parameter is True, then the task may have been previously queued
		///     (scheduled); if False, then the task is known not to have been queued, and this call is being made in order to execute the task inline without
		///     queuing it.
		/// </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> was already executed.</exception>
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA && TryExecuteTask(task);
		}

		/// <summary>Generates an enumerable of <see cref="T:System.Threading.Tasks.Task" /> instances currently queued to the scheduler waiting to be executed.</summary>
		/// <returns>An enumerable that allows traversal of tasks currently queued to this scheduler.</returns>
		/// <exception cref="T:System.NotSupportedException">This scheduler is unable to generate a list of queued tasks at this time.</exception>
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return _tasks.ToArray();
		}

		public override int MaximumConcurrencyLevel => _threads.Count;
		#endregion


		public void Dispose()
		{
			if (_tasks != null)
			{
				_tasks.CompleteAdding();
				foreach (var thread in _threads) thread.Join();
				_tasks.Dispose();
				_tasks = null;
			}
		}
	}
}