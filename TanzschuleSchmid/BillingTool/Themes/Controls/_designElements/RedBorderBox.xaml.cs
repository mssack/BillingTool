// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-21</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.Themes.Controls._designElements
{
	/// <summary>Used as a warning message.</summary>
	[ContentProperty("Content")]
	public class RedBorderBox : Control
	{
		#region DP Keys
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(RedBorderBox), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((RedBorderBox) o).OnContentChanged()});
		#endregion


		static RedBorderBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(RedBorderBox), new FrameworkPropertyMetadata(typeof(RedBorderBox)));
		}

		public object Content
		{
			get { return (object) GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		private void OnContentChanged()
		{
			if (Content != null && Content is DependencyObject)
				NameScope.SetNameScope((DependencyObject) Content, Parent.GetContainingNameScope());
		}
	}
}