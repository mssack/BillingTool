// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-26</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace CsWpfBase.Themes.Controls.Editors.Base
{
#pragma warning disable 1591
	public class EditorBase : Control
	{
		#region DP Keys
		public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof (bool), typeof (EditorBase), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AutoSelectProperty = DependencyProperty.Register("AutoSelect", typeof (bool), typeof (EditorBase), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof (TextAlignment), typeof (EditorBase), new FrameworkPropertyMetadata {DefaultValue = default(TextAlignment), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		public bool IsReadOnly
		{
			get { return (bool) GetValue(IsReadOnlyProperty); }
			set { SetValue(IsReadOnlyProperty, value); }
		}

		public bool AutoSelect
		{
			get { return (bool) GetValue(AutoSelectProperty); }
			set { SetValue(AutoSelectProperty, value); }
		}

		public TextAlignment TextAlignment
		{
			get { return (TextAlignment) GetValue(TextAlignmentProperty); }
			set { SetValue(TextAlignmentProperty, value); }
		}
	}



	public class EditorBase<TValueType> : EditorBase
	{
		#region DP Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (TValueType), typeof (EditorBase<TValueType>), new FrameworkPropertyMetadata {DefaultValue = default(TValueType), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((EditorBase<TValueType>) o).ValueChanged((TValueType) args.OldValue, (TValueType) args.NewValue)});
		#endregion


		#region Abstract
		protected virtual void ValueChanged(TValueType oldValue, TValueType newValue)
		{
		}
		#endregion


		public TValueType Value
		{
			get { return (TValueType) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
	}
}