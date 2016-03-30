// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.ComponentModel;
using System.Windows.Threading;
using CsWpfBase.Ev.Objects;





namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpfTime : Base
	{
		#region Singleton
		private static CsgWpfTime _instance;
		private static readonly object SingletonLock = new object();
		private CsgWpfTime()
		{
			_timer.Tick += _timer_Tick;
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
		}
		internal static CsgWpfTime I
		{
			get
			{
				if (_instance != null)
					return _instance;
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpfTime());
				}
			}
		}
		#endregion


		private readonly DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
		private bool _isRunning;
		private int _subscriptions;


		#region Overrides
		/// <summary>
		///     standard <see cref="INotifyPropertyChanged" /> implementation. When subscribing automatically starts the
		///     automated process.
		/// </summary>
		public override event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				base.PropertyChanged += value;
				Subscriptions++;
			}
			remove
			{
				base.PropertyChanged -= value;
				Subscriptions--;
			}
		}
		#endregion


		/// <summary>Gets the current Time</summary>
		public DateTime Value
		{
			get { return DateTime.Now; }
		}
		/// <summary>Current property changed subscriptions</summary>
		public int Subscriptions
		{
			get { return _subscriptions; }
			private set
			{
				if (SetProperty(ref _subscriptions, value))
				{
					IsRunning = _subscriptions != 0;
				}
			}
		}
		/// <summary>Determining whether automated notifications are running.</summary>
		public bool IsRunning
		{
			get { return _isRunning; }
			private set
			{
				if (SetProperty(ref _isRunning, value))
				{
					if (value)
					{
						_timer.Start();
					}
					else
					{
						_timer.Stop();
					}
				}
			}
		}
		private void _timer_Tick(object sender, EventArgs e)
		{
			_timer.Stop();
			OnPropertyChanged("Value");
			_timer.Interval = TimeSpan.FromMilliseconds((1000 - DateTime.Now.Millisecond) + 20);
			_timer.Start();
		}
	}
}