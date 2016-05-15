﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows.tools
{
	/// <summary>Interaction logic for Window_BelegData_ProcessNonProcessedOutputs.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_BelegData_ProcessNonProcessedOutputs : CsWindow
	{


		/// <summary>ctor</summary>
		public Window_BelegData_ProcessNonProcessedOutputs(BelegData item)
		{
			InitializeComponent();
			Item = item;
			OpenedItems = new ObservableCollection<DataRow>(Item.MailedBelege.Where(x=>x.ProcessingState == ProcessingStates.NotProcessed).OfType<DataRow>().Union(Item.PrintedBelege.Where(x=>x.ProcessingState == ProcessingStates.NotProcessed)));

			Bt.Output.DoOpenedExportsAsync(Item).ContinueWith(TaskCompleted, TaskScheduler.FromCurrentSynchronizationContext());
		}

		/// <summary>The item</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>All the items which are currently being processed.</summary>
		public ObservableCollection<DataRow> OpenedItems
		{
			get { return (ObservableCollection<DataRow>) GetValue(OpenedItemsProperty); }
			set { SetValue(OpenedItemsProperty, value); }
		}
		

		private void TaskCompleted(Task<Task[]> task)
		{
			CloseButtonVisibility = Visibility.Visible;
			if (task.Result.Any(x => x.IsFaulted))
			{
				
			}
			else
			{
				Close();
			}
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (BelegData), typeof (Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty OpenedItemsProperty = DependencyProperty.Register("OpenedItems", typeof (ObservableCollection<DataRow>), typeof (Window_BelegData_ProcessNonProcessedOutputs), new FrameworkPropertyMetadata {DefaultValue = default(ObservableCollection<DataRow>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}