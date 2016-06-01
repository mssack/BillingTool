// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace BillingTool.Themes.Controls._designElements
{
	public class AcceptButton : Button
	{
		#region DP Keys
		public static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(AcceptButton), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static AcceptButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(AcceptButton), new FrameworkPropertyMetadata(typeof(AcceptButton)));
		}

		public bool IsHighlighted
		{
			get { return (bool) GetValue(IsHighlightedProperty); }
			set { SetValue(IsHighlightedProperty, value); }
		}
	}
}