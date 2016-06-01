// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;





namespace CsWpfBase.Themes.Controls.Basics
{
#pragma warning disable 1591

	/// <summary>Interaction logic for WaitCircle.xaml</summary>
	public partial class WaitCircle : UserControl
	{
		private readonly DispatcherTimer _animationTimer;
		public WaitCircle()
		{
			InitializeComponent();
			_animationTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher);
		}
		private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}

			var isVisible = (bool) e.NewValue;

			if (isVisible)
			{
				StartSpinning();
			}
			else
			{
				StopSpinning();
			}
		}
		private void StartSpinning()
		{
			_animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 80);
			_animationTimer.Tick += HandleAnimationTick;
			_animationTimer.Start();
			Opacity = 1;
		}
		private void StopSpinning()
		{
			_animationTimer.Stop();
			_animationTimer.Tick -= HandleAnimationTick;
			Opacity = 0;
		}
		private void HandleAnimationTick(object sender, EventArgs e)
		{
			SpinnerRotate.Angle = (SpinnerRotate.Angle + 36)%360;
		}
		private void HandleLoaded(object sender, RoutedEventArgs e)
		{
			SetPosition(C0, 0.0);
			SetPosition(C1, 1.0);
			SetPosition(C2, 2.0);
			SetPosition(C3, 3.0);
			SetPosition(C4, 4.0);
			SetPosition(C5, 5.0);
			SetPosition(C6, 6.0);
			SetPosition(C7, 7.0);
			SetPosition(C8, 8.0);
		}
		private void HandleUnloaded(object sender, RoutedEventArgs e)
		{
			StopSpinning();
		}
		private void SetPosition(Ellipse ellipse, double sequence)
		{
			var d = (Canvas.Width - ellipse.Width)/2;
			ellipse.SetValue(
				Canvas.LeftProperty,
				d + (Math.Sin(Math.PI*((0.2*sequence) + 1))*d));

			ellipse.SetValue(
				Canvas.TopProperty,
				d + (Math.Cos(Math.PI*((0.2*sequence) + 1))*d));
		}
	}
}