// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;





namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of WPF extensions</summary>
	public static class WpfExtensions
	{
		/// <summary>Opens the dialog to gather an image.</summary>
		public static BitmapSource GatherImage(this OpenFileDialog ofd)
		{
			ofd.Filter = "Bilder|*.png;*.jpg;*.gif;*.bmp;*.tiff;*.wmp";
			ofd.CheckFileExists = true;
			ofd.Multiselect = false;
			var result = ofd.ShowDialog(Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive));

			if (result != true)
				return null;

			var fi = new FileInfo(ofd.FileName);
			if (!fi.Exists)
				return null;
			return fi.LoadAs_Image();
		}
		/// <summary>Gets the device independent screen coordinates for a visual.</summary>
		public static Point GetAbsoluteScreenCoordinates(this FrameworkElement element)
		{
			var presentationSource = PresentationSource.FromVisual(element);
			if (presentationSource == null)
				throw new Exception("The screen coordinates couldnt be determined.");
			if (presentationSource.CompositionTarget == null)
				throw new Exception("The screen coordinates couldnt be determined.");
			return presentationSource.CompositionTarget.TransformFromDevice.Transform(element.PointToScreen(new Point(0, 0)));
		}
		/// <summary>Searches the tree up until an element is found with an NameScope.</summary>
		public static INameScope GetContainingNameScope(this DependencyObject element)
		{
			var parant = element;
			var nameScope = NameScope.GetNameScope(parant);

			while (nameScope == null && parant is FrameworkElement)
			{
				parant = ((FrameworkElement) parant).Parent;
				if (parant != null)
					nameScope = NameScope.GetNameScope(parant);
			}
			return nameScope;
		}

		/// <summary>
		/// Searches the visual childs fo an element of type <typeparamref name="T"/> where the name of the frame work element is <paramref name="name"/>.
		/// </summary>
		/// <typeparam name="T">The type to search for</typeparam>
		/// <param name="container">the containing visual</param>
		/// <param name="name">the name of the element to find inside <paramref name="container"/></param>
		/// <returns></returns>
		public static T GetVisualChildByName<T>(this Visual container, string name) where T : FrameworkElement
		{
			if (container is T && ((T)container).Name == name)
				return (T)container;
			return GetVisualChildByCondition<T>(container, t => t.Name == name);
		}

		/// <summary>
		/// Searches the visual childs fo an element of type <typeparamref name="T"/> where the name of the frame work element is <paramref name="name"/>.
		/// </summary>
		/// <typeparam name="T">The type to search for</typeparam>
		/// <param name="container">the containing visual</param>
		/// <param name="condition">The condition the object needs</param>
		/// <returns></returns>
		public static T GetVisualChildByCondition<T>(this Visual container, Func<T, bool> condition) where T : FrameworkElement
		{
			if (container is T && condition(((T)container)))
				return (T)container;
			return RecursiveGetVisualChildByCondition<T>(container, condition);
		}


		private static T RecursiveGetVisualChildByCondition<T>(Visual container, Func<T, bool> condition) where T : FrameworkElement
		{
			if (container == null) return null;
			T foundElement = null;
			if (container is FrameworkElement)
				(container as FrameworkElement).ApplyTemplate();

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(container); i++)
			{
				Visual visual = VisualTreeHelper.GetChild(container, i) as Visual;

				if (!(visual is T))
				{
					foundElement = RecursiveGetVisualChildByCondition<T>(visual, condition);

					if (foundElement != null)
						return foundElement;
					continue;
				}

				var element = (T)visual;
				if (!condition(element)) continue;

				return (T)element;
			}
			return null;
		}
	}
}