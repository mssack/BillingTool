// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;





namespace CsWpfBase.Themes.AttachedProperties
{
#pragma warning disable 1591
	public class AGridViewColumn
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached("IsVisible", typeof (bool), typeof (AGridViewColumn), new UIPropertyMetadata(true, (o, args) => IsVisibleChanged(o as GridViewColumn, (bool) args.NewValue)));
		#endregion


		private static readonly PropertyInfo InheritanceContextProp = typeof (DependencyObject).GetProperty("InheritanceContext", BindingFlags.NonPublic | BindingFlags.Instance);
		private static readonly Dictionary<GridViewColumn, GridView> Cache = new Dictionary<GridViewColumn, GridView>();
		public static bool GetIsVisible(DependencyObject obj)
		{
			return (bool) obj.GetValue(IsVisibleProperty);
		}
		public static void SetIsVisible(DependencyObject obj, bool value)
		{
			obj.SetValue(IsVisibleProperty, value);
		}
		private static void IsVisibleChanged(GridViewColumn gridViewColumn, bool newValue)
		{
			var owningGridView = InheritanceContextProp.GetValue(gridViewColumn, null) as GridView;
			if (owningGridView == null)
			{
				if (!Cache.ContainsKey(gridViewColumn))
					return;
				if (newValue == false)
					return;
				Cache[gridViewColumn].Columns.Add(gridViewColumn);
				Cache.Remove(gridViewColumn);
				return;
			}
			if (newValue)
				return;
			Cache.Add(gridViewColumn, owningGridView);
			owningGridView.Columns.Remove(gridViewColumn);
		}
	}
}