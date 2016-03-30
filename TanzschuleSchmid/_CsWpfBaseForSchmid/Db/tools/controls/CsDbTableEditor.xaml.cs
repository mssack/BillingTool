// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CsWpfBase.Db.models;






namespace CsWpfBase.Db.tools.controls
{
	/// <summary>Interaction logic for CsDbTableEditor.xaml</summary>
	public partial class CsDbTableEditor : UserControl
	{
		#region DP Keys
		public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof (CsDbTable), typeof (CsDbTableEditor), new FrameworkPropertyMetadata {DefaultValue = default(CsDbTable), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		public CsDbTableEditor()
		{
			InitializeComponent();
		}

		public CsDbTable ItemSource
		{
			get { return (CsDbTable) GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}
	}
}