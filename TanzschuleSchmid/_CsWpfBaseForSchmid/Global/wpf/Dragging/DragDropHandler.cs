// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;






namespace CsWpfBase.Global.wpf.Dragging
{
	/// <summary>Handles drag and drop operations from one source item to a target item.</summary>
	public class DragDropHandler<TSource, TTarget>
		where TTarget : UIElement
	{
		private TTarget _currentHoveredElement;
		private Type _hoverAdornerType = typeof(DropOntoAdorner);

		/// <summary>
		/// Creates a new drag drop handler
		/// </summary>
		/// <param name="container">The container of the drag operation, this can be the window or the container where either the source and the target is located.</param>
		/// <param name="source">The source of the drag operation</param>
		/// <param name="e">The mouse event args of the element which got clicked</param>
		/// <param name="isADropableTarget">The delegate which is used to identify if a visual object is a dropable target.</param>
		public DragDropHandler(UIElement container, TSource source, MouseButtonEventArgs e, Del1 isADropableTarget)
		{
			Container = container;
			Source = source;
			StartPoint = e.GetPosition(Container);
			IsADropableTarget = isADropableTarget;
			CurrentAdornerLayer = AdornerLayer.GetAdornerLayer(container);
			Register();
			e.Handled = true;
		}

		/// <summary>The adorner to present on hovering over an questionable target.</summary>
		public Type HoverAdornerType
		{
			get { return _hoverAdornerType; }
			set { _hoverAdornerType = value; }
		}
		private Adorner HoverAdorner { get; set; }


		private AdornerLayer CurrentAdornerLayer { get; set; }
		private bool IsDragging { get; set; }
		private UIElement Container { get; set; }
		private TSource Source { get; set; }
		private Point StartPoint { get; set; }
		private Del1 IsADropableTarget { get; set; }

		private TTarget CurrentHoveredElement
		{
			get { return _currentHoveredElement; }
			set
			{
				if (Equals(value, _currentHoveredElement))
					return;

				if (_currentHoveredElement != null)
					CurrentAdornerLayer.Remove(HoverAdorner);
				_currentHoveredElement = value;
				if (_currentHoveredElement != null)
				{
					HoverAdorner = (Adorner) Activator.CreateInstance(HoverAdornerType, new object[] {_currentHoveredElement});
					CurrentAdornerLayer.Add(HoverAdorner);
					Mouse.OverrideCursor = Cursors.Hand;
				}
				else
				{
					Mouse.OverrideCursor = Cursors.No;
				}
			}
		}

		/// <summary>Occurs when the user releases the mouse over a possible target.</summary>
		public event Del Dropped;


		private void Container_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (IsDragging)
			{
				CurrentHoveredElement = GetElementFromMousePosition(e.GetPosition(Container));
			}
			else
			{
				if (Math.Abs(e.GetPosition(Container).Y - StartPoint.Y) > 15)
				{
					IsDragging = true;
					Mouse.OverrideCursor = Cursors.No;
				}
			}
		}

		private void Container_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (IsDragging && Dropped != null)
			{
				var dropTarget = GetElementFromMousePosition(e.GetPosition(Container));
				if (dropTarget != null)
					Dropped(Source, dropTarget);
			}
			Unregister();
		}


		private void Register()
		{
			Container.CaptureMouse();
			Container.PreviewMouseMove += Container_PreviewMouseMove;
			Container.PreviewMouseUp += Container_PreviewMouseUp;
			Container.LostMouseCapture += Container_LostMouseCapture;
		}

		private void Unregister()
		{
			Container.ReleaseMouseCapture();
		}

		private void Container_LostMouseCapture(object sender, MouseEventArgs e)
		{
			Container.PreviewMouseMove -= Container_PreviewMouseMove;
			Container.PreviewMouseUp -= Container_PreviewMouseUp;
			IsDragging = false;
			Dropped = null;
			CurrentHoveredElement = null;
			Mouse.OverrideCursor = null;
		}

		private TTarget GetElementFromMousePosition(Point relativeToContainer)
		{
			TTarget target = null;
			VisualTreeHelper.HitTest(Container, dpO =>
			{
				if (!(dpO is TTarget))
					return HitTestFilterBehavior.Continue;



				var temp = (TTarget) dpO;
				if (IsADropableTarget == null || IsADropableTarget(Source, temp))
				{
					target = temp;
					return HitTestFilterBehavior.Stop;
				}
				return HitTestFilterBehavior.Continue;
			}, result => HitTestResultBehavior.Stop, new PointHitTestParameters(relativeToContainer));
			return target;
		}




		/// <summary>Drop delegate</summary>
		public delegate void Del(TSource source, TTarget droppedOn);




		/// <summary>Questionable target delegate.</summary>
		public delegate bool Del1(TSource source, TTarget questionalTarget);
	}
}