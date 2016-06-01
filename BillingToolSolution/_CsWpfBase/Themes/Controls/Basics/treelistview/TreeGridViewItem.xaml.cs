// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;






namespace CsWpfBase.Themes.Controls.Basics.treelistview
{
	/// <summary>An item for the <see cref="TreeGridView" />.</summary>
	public class TreeGridViewItem : TreeViewItem
	{
		#region DP Keys
		private static readonly PropertyInfo ParentTreeViewProperty = typeof (TreeGridViewItem).GetProperty("ParentTreeViewItem", BindingFlags.NonPublic | BindingFlags.Instance);
		#endregion


		private byte? _level;

		static TreeGridViewItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TreeGridViewItem), new FrameworkPropertyMetadata(typeof (TreeGridViewItem)));
		}

		/// <summary>ctor</summary>
		public TreeGridViewItem()
		{
			KeyDown += TreeGridViewItem_KeyDown;
		}


		#region Overrides/Interfaces
		/// <summary>Creates a new <see cref="T:System.Windows.Controls.TreeViewItem" /> to use to display the object.</summary>
		/// <returns>A new <see cref="T:System.Windows.Controls.TreeViewItem" />.</returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new TreeGridViewItem();
		}

		/// <summary>Determines whether an object is a <see cref="T:System.Windows.Controls.TreeViewItem" />.</summary>
		/// <returns>true if <paramref name="item" /> is a <see cref="T:System.Windows.Controls.TreeViewItem" />; otherwise, false.</returns>
		/// <param name="item">The object to evaluate.</param>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is TreeGridViewItem;
		}
		#endregion


		/// <summary>Indentation level of the item.</summary>
		public byte Level
		{
			get
			{
				if (_level == null)
				{
					var parent = ItemsControlFromItemContainer(this) as TreeGridViewItem;
					_level = (byte) (parent?.Level + 1 ?? 0);
				}
				return _level.Value;
			}
		}

		/// <summary>Returns the owning <see cref="TreeGridViewItem" /> of the instance.</summary>
		public TreeGridViewItem GetParentTreeViewItem()
		{
			return ParentTreeViewProperty.GetValue(this, null) as TreeGridViewItem;
		}

		private void TreeGridViewItem_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Right)
			{
				if (HasItems)
				{
					var fromItem = (TreeGridViewItem) ItemContainerGenerator.ContainerFromItem(Items[0]);
					Keyboard.Focus(fromItem);
				}
				e.Handled = true;
			}
			else if (e.Key == Key.Left)
			{
				if (IsExpanded == false)
				{
					var parent = GetParentTreeViewItem();
					if (parent != null)
					{
						parent.IsExpanded = false;
						Keyboard.Focus(parent);
					}
				}
				else
				{
					IsExpanded = false;
				}
				e.Handled = true;
			}
			else if (e.Key == Key.Down)
			{
				e.Handled = true;
			}
			else if (e.Key == Key.Up)
			{
				e.Handled = true;
			}
		}
	}
}