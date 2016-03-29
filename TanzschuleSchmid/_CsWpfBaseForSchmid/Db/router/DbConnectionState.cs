// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.router
{
	/// <summary>The state of a connection.</summary>
	[Serializable]
	public sealed class CsDbRouterState : Base
	{
		private bool _isConnected;
		private bool _isConnecting;
		private Exception _lastException;

		internal CsDbRouterState()
		{

		}


		/// <summary>Gets if connection is currently building.</summary>
		public bool IsConnecting
		{
			get { return _isConnecting; }
			private set { SetProperty(ref _isConnecting, value); }
		}
		/// <summary>Gets if currently connected.</summary>
		public bool IsConnected
		{
			get { return _isConnected; }
			private set { SetProperty(ref _isConnected, value); }
		}
		/// <summary>Gets or sets the LastException.</summary>
		public Exception LastException
		{
			get { return _lastException; }
			private set { SetProperty(ref _lastException, value); }
		}


		internal void SetConnecting()
		{
			LastException = null;
			IsConnected = true;
		}

		internal void SetConnected()
		{
			LastException = null;
			IsConnecting = false;
			IsConnected = true;
		}

		internal void SetDisconnected(Exception lastException = null)
		{
			IsConnecting = false;
			IsConnected = false;
			LastException = lastException;
		}
	}
}