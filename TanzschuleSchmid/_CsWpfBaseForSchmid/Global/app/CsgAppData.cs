// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using CsWpfBase.Ev.Exceptions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.app
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgAppData : Base
	{
		private static CsgAppData _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgAppData I
		{
			get
			{
				if (_instance == null)
					throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.AppData);

				return _instance;
			}
		}
		private readonly Guid _iD;
		private DateTime _firstExecutionTime;
		private DateTime? _lastExecutionTime;
		private UInt32 _startupCount = 1;
		private TimeSpan _useageTime;

		internal static void Install()
		{

			if (!CsGlobal.IsInstalled(GlobalFunctions.Storage))
				throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.Storage);
			_instance = CsGlobal.Storage.Private.Handle("AppData", () => new CsgAppData());
		}
		private CsgAppData()
		{
			_iD = Guid.NewGuid();
			FirstExecutionTime = DateTime.Now;
		}

		/// <summary>Application unique ID. This ID is created whenever an Instance of this is created. This ID is not unique for a computer or its hardware.</summary>
		public Guid InstanceId
		{
			get { return _iD; }
		}
		/// <summary>Whenever this instance is serialized the startup count is incremented by one starting with one.</summary>
		public UInt32 StartupCount
		{
			get { return _startupCount; }
			private set { SetProperty(ref _startupCount, value); }
		}
		/// <summary>
		///     Whenever this instance is serialized the usage time will be incremented by the time this program was running. The time will not be updated when
		///     application close unexpected.
		/// </summary>
		public TimeSpan UseageTime
		{
			get { return _useageTime.Add(DateTime.Now - Process.GetCurrentProcess().StartTime); }
			private set { SetProperty(ref _useageTime, value); }
		}
		/// <summary>The first execution time of the program.</summary>
		public DateTime FirstExecutionTime
		{
			get { return _firstExecutionTime; }
			private set { SetProperty(ref _firstExecutionTime, value); }
		}
		/// <summary>Gets the time when the program was last time executed.</summary>
		public DateTime? LastExecutionTime
		{
			get { return _lastExecutionTime; }
			private set { SetProperty(ref _lastExecutionTime, value); }
		}


		[OnSerializing]
		private void OnSerializing(StreamingContext sc)
		{
			UseageTime = UseageTime;
			LastExecutionTime = Process.GetCurrentProcess().StartTime;
			StartupCount++;
		}
	}
}