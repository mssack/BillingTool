// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-23</date>

using System;
using System.Windows;
using System.Windows.Input;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.wpf.Dragging;





namespace CsWpfBase.Global.wpf
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgWpfDragging : Base
	{
		#region SINGLETON CLASS
		private static CsgWpfDragging _instance;
		private static readonly object SingletonLock = new object();
		private CsgWpfDragging()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		public static CsgWpfDragging I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgWpfDragging());
				}
			}
		}
		#endregion


		/// <summary>Starts an drag operation with a mouse click. The operation ends when the mouse is click ends.</summary>
		/// <typeparam name="TSource">the type of the source item which is hold by the the drag operation</typeparam>
		/// <typeparam name="TTarget">the possible target type where the source item can be dropped onto.</typeparam>
		/// <param name="container">the container around the possible targets</param>
		/// <param name="source">the source item which acts like the tag field. you will get the source back in the dropped event.</param>
		/// <param name="clickEventArgs">the click event args.</param>
		/// <param name="isDropableOn">a delegate which verify that the target of type <typeparamref name="TTarget"/> can be a drop target.</param>
		/// <param name="dropped">the callback when the source is dropped on a valid target.</param>
		public void BeginWithDefaultAdorner<TSource, TTarget>(UIElement container, TSource source, MouseButtonEventArgs clickEventArgs,  DragDropHandler<TSource, TTarget>.Del dropped,DragDropHandler<TSource, TTarget>.Del1 isDropableOn) where TTarget : UIElement
		{
			var dropHandler = new DragDropHandler<TSource, TTarget>(container, source, clickEventArgs, isDropableOn) {HoverAdornerType = typeof (DropOntoAdorner)};
			dropHandler.Dropped += dropped;
		}
	}
}