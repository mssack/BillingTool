// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;






namespace CsWpfBase.Global.wpf.Dragging
{
	/// <summary>The default adorner for drag and drop.</summary>
	public class DropOntoAdorner : Adorner
	{
		private static readonly Pen DarkPen = new Pen(new SolidColorBrush(Color.FromArgb(130, 0, 0, 0)), 1);
		private static readonly Pen WhitePen = new Pen(new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)), 1);
		private static readonly FormattedText FormattedText = new FormattedText("Insert here", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 12, WhitePen.Brush);

		/// <summary>ctor</summary>
		/// <param name="adornedElement"></param>
		public DropOntoAdorner(UIElement adornedElement) : base(adornedElement)
		{
			if (!DarkPen.IsFrozen)
			{
				DarkPen.Freeze();
				WhitePen.Freeze();
			}
			IsHitTestVisible = false;
		}


		#region Overrides/Interfaces
		/// <summary>
		///     When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for
		///     this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing.
		/// </summary>
		/// <param name="drawingContext">The drawing instructions for a specific element. This context is provided to the layout system.</param>
		protected override void OnRender(DrawingContext drawingContext)
		{
			var frm = (FrameworkElement) AdornedElement;
			drawingContext.DrawRectangle(DarkPen.Brush, DarkPen, new Rect(0, 0, frm.ActualWidth, frm.ActualHeight));
			drawingContext.DrawText(FormattedText, new Point(frm.ActualWidth/2 - FormattedText.Width/2, frm.ActualHeight/2 - FormattedText.Height/2));
		}
		#endregion
	}
}