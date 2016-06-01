// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace BillingToolGitControl.NewRc
{
	/// <summary>Interaction logic for NewRcControl.xaml</summary>
	public partial class NewRcControl : UserControl
	{
		#region DP Keys
		public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(NewRcControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		public NewRcControl()
		{
			InitializeComponent();
		}

		public string Message
		{
			get { return (string) GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}
	}
}