// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace CsWpfBase.Themes.Controls.Basics.treelistview
{
	/// <summary>A tree view with an integrated grid view layout.</summary>
	public class TreeGridView : TreeView
	{
		#region DP Keys
		/// <summary></summary>
		public static readonly DependencyProperty FirstColumnTemplateProperty = DependencyProperty.Register("FirstColumnTemplate", typeof (DataTemplate), typeof (TreeGridView), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty FirstColumnWidthProperty = DependencyProperty.Register("FirstColumnWidth", typeof (double), typeof (TreeGridView), new FrameworkPropertyMetadata {DefaultValue = default(double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty FirstColumnHeaderProperty = DependencyProperty.Register("FirstColumnHeader", typeof (object), typeof (TreeGridView), new FrameworkPropertyMetadata {DefaultValue = default(object), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty LevelMarginProperty = DependencyProperty.Register("LevelMargin", typeof (double), typeof (TreeGridView), new FrameworkPropertyMetadata {DefaultValue = default(double), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private GridViewColumnCollection _columns;

		static TreeGridView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TreeGridView), new FrameworkPropertyMetadata(typeof (TreeGridView)));
		}


		#region Overrides/Interfaces
		/// <summary>Creates a <see cref="T:System.Windows.Controls.TreeViewItem" /> to use to display content.</summary>
		/// <returns>A new <see cref="T:System.Windows.Controls.TreeViewItem" /> to use as a container for content.</returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new TreeGridViewItem();
		}

		/// <summary>Determines whether the specified item is its own container or can be its own container.</summary>
		/// <returns>true if <paramref name="item" /> is a <see cref="T:System.Windows.Controls.TreeViewItem" />; otherwise, false.</returns>
		/// <param name="item">The object to evaluate.</param>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is TreeGridViewItem;
		}
		#endregion


		/// <summary>Specifys the first column template.</summary>
		public DataTemplate FirstColumnTemplate
		{
			get { return (DataTemplate) GetValue(FirstColumnTemplateProperty); }
			set { SetValue(FirstColumnTemplateProperty, value); }
		}
		/// <summary>All columns. Be careful, first column is already included with <see cref="FirstColumnTemplate" /> as <see cref="DataTemplate" />.</summary>
		public GridViewColumnCollection Columns
		{
			get
			{
				if (_columns == null)
				{
					var gridViewColumn = new GridViewColumn();
					BindingOperations.SetBinding(gridViewColumn, GridViewColumn.CellTemplateProperty, new Binding("FirstColumnTemplate") {Source = this});
					BindingOperations.SetBinding(gridViewColumn, GridViewColumn.HeaderProperty, new Binding("FirstColumnHeader") {Source = this});
					BindingOperations.SetBinding(gridViewColumn, GridViewColumn.WidthProperty, new Binding("FirstColumnWidth") {Source = this});
					_columns = new GridViewColumnCollection {gridViewColumn};
				}

				return _columns;
			}
		}
		/// <summary>the margin which is applied by each level of indentation.</summary>
		public double LevelMargin
		{
			get { return (double) GetValue(LevelMarginProperty); }
			set { SetValue(LevelMarginProperty, value); }
		}
		/// <summary>the width of the first column.</summary>
		public double FirstColumnWidth
		{
			get { return (double) GetValue(FirstColumnWidthProperty); }
			set { SetValue(FirstColumnWidthProperty, value); }
		}
		/// <summary>The header property of the first column.</summary>
		public object FirstColumnHeader
		{
			get { return GetValue(FirstColumnHeaderProperty); }
			set { SetValue(FirstColumnHeaderProperty, value); }
		}
	}





}