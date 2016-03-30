// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-01</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CsWpfBase.Db.models;






namespace CsWpfBase.Db.tools.controls
{
	/// <summary>Interaction logic for CsDbDataSetEditor.xaml</summary>
	public partial class CsDbDataSetEditor : UserControl
	{
		#region DP Keys
		/// <summary>The data set item source property.</summary>
		public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof (CsDbDataSet), typeof (CsDbDataSetEditor), new FrameworkPropertyMetadata {DefaultValue = default(CsDbDataSet), BindsTwoWayByDefault = false, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		/// <summary>Creates a nwe instance of the data set editor.</summary>
		public CsDbDataSetEditor()
		{
			InitializeComponent();
		}

		/// <summary>The data set which needs to be edited.</summary>
		public CsDbDataSet ItemSource
		{
			get { return (CsDbDataSet) GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}

		private void LoadDataSetSchema_Click(object sender, RoutedEventArgs e)
		{
			ItemSource.LoadSchema();
		}

		private void DownloadRowsFromTable_Click(object sender, RoutedEventArgs e)
		{
			var selectedTable = TableSelector.SelectedItem as CsDbTable;
			if (selectedTable == null)
				return;

			selectedTable.DownloadRows();
		}

		private void DownloadTop100RowsFromTable_Click(object sender, RoutedEventArgs e)
		{
			var selectedTable = TableSelector.SelectedItem as CsDbTable;
			if (selectedTable == null)
				return;

			selectedTable.DownloadRows(100);
		}
	}
}