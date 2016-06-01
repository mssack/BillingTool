// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingTool.Themes.Controls._shared
{
	/// <summary>Interaction logic for ProcessingStateIcon.xaml</summary>
	public partial class ProcessingStateIcon : Image
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ProcessingStateProperty = DependencyProperty.Register("ProcessingState", typeof(ProcessingStates), typeof(ProcessingStateIcon), new FrameworkPropertyMetadata {DefaultValue = default(ProcessingStates), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public ProcessingStateIcon()
		{
			InitializeComponent();
		}

		/// <summary>The processing state to display.</summary>
		public ProcessingStates ProcessingState
		{
			get { return (ProcessingStates) GetValue(ProcessingStateProperty); }
			set { SetValue(ProcessingStateProperty, value); }
		}
	}
}