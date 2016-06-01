// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Threading.Tasks;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Online.packets.v1.client;






namespace CsWpfBase.Online.send
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoSendAsync : Base
	{
		private static CsoSendAsync _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsoSendAsync I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoSendAsync());
				}
			}
		}

		private CsoSendAsync()
		{
		}

		/// <summary>Sends an empty request. checks for updates.</summary>
		public SendTask Ping()
		{
			var t = new SendTask(() => CsOnline.Send.Ping(), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}

		/// <summary>Sends an request with client informations.</summary>
		public SendTask AppInfo()
		{
			var t = new SendTask(() => CsOnline.Send.AppInfo(), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}

		/// <summary>Sends an request with the complete client info.</summary>
		public SendTask CompleteInfo()
		{
			var t = new SendTask(() => CsOnline.Send.CompleteInfo(), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}

		/// <summary>Sends an request with the complete client info.</summary>
		public SendTask Feedback(CsopClientFeedback feedback)
		{
			var t = new SendTask(() => CsOnline.Send.Feedback(feedback), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}

		/// <summary>Sends an exception to the server for logging purpose.</summary>
		public SendTask Exception(Exception exception)
		{
			var t = new SendTask(() => CsOnline.Send.Exception(exception), TaskCreationOptions.LongRunning);
			t.Start(TaskScheduler.Default);
			return t;
		}
	}
}