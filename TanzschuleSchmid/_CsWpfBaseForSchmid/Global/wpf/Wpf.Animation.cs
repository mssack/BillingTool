// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	public sealed class CsgWpfAnimation : Base
	{
		private static CsgWpfAnimation _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgWpfAnimation I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpfAnimation());
				}
			}
		}

		private CsgWpfAnimation()
		{
		}

		/// <summary>Allows to animate the opacity of an element.</summary>
		/// <param name="fe">the framework element to animate.</param>
		/// <param name="to">the target opacity.</param>
		/// <param name="duration">the animation duration.</param>
		/// <param name="beginTime">begin time of the animation.</param>
		/// <param name="finished">call back</param>
		public Task Opacity(FrameworkElement fe, double to, Duration duration, TimeSpan? beginTime = null, Action finished = null)
		{
			if (beginTime == null)
				beginTime = new TimeSpan(0);

			var tcs = new TaskCompletionSource<object>();
			var da = new DoubleAnimation(to, duration, FillBehavior.HoldEnd);
			var sb = new Storyboard {Duration = duration, BeginTime = beginTime};
			sb.Children.Add(da);
			Storyboard.SetTarget(da, fe);
			Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));

			sb.Completed += (sender, args) =>
			{
				tcs.SetResult(null);
				if (finished != null)
					finished();
			};
			sb.Begin();
			return tcs.Task;
		}
	}
}