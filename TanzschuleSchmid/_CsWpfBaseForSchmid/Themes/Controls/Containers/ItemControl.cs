// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace CsWpfBase.Themes.Controls.Containers
{
	/// <summary>The item control is the base for a user control which edit an item.</summary>
	/// <typeparam name="TItemType"></typeparam>
	public class ItemControl<TItemType> : UserControl
	{
		#region DP Keys
		/// <summary></summary>
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (TItemType), typeof (ItemControl<TItemType>), new FrameworkPropertyMetadata {DefaultValue = default(TItemType), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((ItemControl<TItemType>) o).ItemChanged((TItemType) args.OldValue, (TItemType) args.NewValue)});
		#endregion


		#region Abstract
		/// <summary>Occurs whenever the item changes.</summary>
		protected virtual void ItemChanged(TItemType oldItem, TItemType newItem)
		{
		}
		#endregion


		/// <summary>The item to be edited by the user control.</summary>
		public TItemType Item
		{
			get { return (TItemType) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
	}
}