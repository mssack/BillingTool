// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.Themes.Controls._designElements
{
	/// <summary>Interaction logic for RedBorderBox.xaml</summary>
	[ContentProperty("Content")]
	public partial class RedBorderBox
	{
		#region DP Keys
		public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(RedBorderBox), new FrameworkPropertyMetadata
		{
			DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback =  (o, args) => ((RedBorderBox)o).OnContentChanged()
		});
		#endregion


		/// <summary>ctor</summary>
		public RedBorderBox()
		{
			InitializeComponent();
		}


		#region Overrides/Interfaces
		private void OnContentChanged()
		{
			if (Content != null && Content is DependencyObject)
				NameScope.SetNameScope((DependencyObject)Content, Parent.GetContainingNameScope());
		}
		#endregion


		public object Content
		{
			get { return (object) GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
	}
}