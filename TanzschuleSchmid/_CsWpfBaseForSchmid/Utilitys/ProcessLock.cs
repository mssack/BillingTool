// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Utilitys
{
	/// <summary>The process lock allows to build a construct which is capable of ignoring when some other activity is running.</summary>
	public class ProcessLock : IDisposable
	{
		#region Overrides/Interfaces
		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			Locks--;
		}
		#endregion


		/// <summary>Gets if some activity has currently locked the process.</summary>
		public bool Active => Locks != 0;
		private int Locks { get; set; }

		/// <summary>Activate the lock.</summary>
		public ProcessLock Activate()
		{
			Locks++;
			return this;
		}
	}
}