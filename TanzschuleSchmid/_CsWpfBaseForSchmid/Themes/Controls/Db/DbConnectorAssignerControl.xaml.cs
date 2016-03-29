// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-07</date>

using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CsWpfBase.Db.models;
using CsWpfBase.Db.models.bases;






namespace CsWpfBase.Themes.Controls.Db
{
	/// <summary>Used to connect two rows by id's over an connector table. Uses the cs db base system.</summary>
	public class DbConnectorAssignmentControl : Control
	{
		#region DP Keys
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty ConnectorRowTemplateProperty = DependencyProperty.Register("ConnectorRowTemplate", typeof (DataTemplate), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty SourceRowTemplateProperty = DependencyProperty.Register("SourceRowTemplate", typeof (DataTemplate), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(DataTemplate), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty AssignmentLogicProperty = DependencyProperty.Register("AssignmentLogic", typeof (Logic), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(Logic), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty ItemsSource_SourceRowsProperty = DependencyProperty.Register("ItemsSource_SourceRows", typeof (IEnumerable), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(IEnumerable), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty TargetRowProperty = DependencyProperty.Register("TargetRow", typeof (object), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(object), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary>Dp Key</summary>
		public static readonly DependencyProperty ItemsSource_ConnectorRowsProperty = DependencyProperty.Register("ItemsSource_ConnectorRows", typeof (IEnumerable), typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata {DefaultValue = default(IEnumerable), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private ListView _connectorRowListView;
		private ListView _sourceRowListView;


		static DbConnectorAssignmentControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DbConnectorAssignmentControl), new FrameworkPropertyMetadata(typeof (DbConnectorAssignmentControl)));
		}


		#region Abstract
		/// <summary>Look at the generic definition</summary>
		public abstract class Logic
		{
			#region Abstract
			internal abstract void Assign(object target, object source);
			internal abstract void UnAssign(object connector, object target);
			#endregion
		}
		#endregion


		#region Overrides/Interfaces
		/// <summary>
		///     When overridden in a derived class, is invoked whenever application code or internal processes call
		///     <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template == null)
				return;
			SourceRowListView = (ListView) Template.FindName("SourceRowListView", this);
			ConnectorRowListView = (ListView) Template.FindName("ConnectorRowListView", this);
		}
		#endregion


		/// <summary>The target row which needs to be connected.</summary>
		public object TargetRow
		{
			get { return (object) GetValue(TargetRowProperty); }
			set { SetValue(TargetRowProperty, value); }
		}
		/// <summary>The source rows which can be attached to the target row via the connector table.</summary>
		public IEnumerable ItemsSource_SourceRows
		{
			get { return (IEnumerable) GetValue(ItemsSource_SourceRowsProperty); }
			set { SetValue(ItemsSource_SourceRowsProperty, value); }
		}
		/// <summary>The already existing connector rows which can be deleted.</summary>
		public IEnumerable ItemsSource_ConnectorRows
		{
			get { return (IEnumerable) GetValue(ItemsSource_ConnectorRowsProperty); }
			set { SetValue(ItemsSource_ConnectorRowsProperty, value); }
		}

		/// <summary>The data template for the connector rows which will be used inside the list view.</summary>
		public DataTemplate ConnectorRowTemplate
		{
			get { return (DataTemplate) GetValue(ConnectorRowTemplateProperty); }
			set { SetValue(ConnectorRowTemplateProperty, value); }
		}
		/// <summary>The data template for the source rows which will be used inside the list view.</summary>
		public DataTemplate SourceRowTemplate
		{
			get { return (DataTemplate) GetValue(SourceRowTemplateProperty); }
			set { SetValue(SourceRowTemplateProperty, value); }
		}
		/// <summary>The logic which will be used to assign source rows to the target row.</summary>
		public Logic AssignmentLogic
		{
			get { return (Logic) GetValue(AssignmentLogicProperty); }
			set { SetValue(AssignmentLogicProperty, value); }
		}



		private ListView SourceRowListView
		{
			get { return _sourceRowListView; }
			set
			{
				var old = _sourceRowListView;
				_sourceRowListView = value;
				if (old != null)
				{
					value.MouseDoubleClick -= SourceRowListView_DoubleClicked;
				}
				if (value != null)
				{
					value.MouseDoubleClick += SourceRowListView_DoubleClicked;
				}
			}
		}

		private ListView ConnectorRowListView
		{
			get { return _connectorRowListView; }
			set
			{
				var old = _connectorRowListView;
				_connectorRowListView = value;
				if (old != null)
				{
					value.MouseDoubleClick -= ConnectorRowListView_DoubleClicked;
				}
				if (value != null)
				{
					value.MouseDoubleClick += ConnectorRowListView_DoubleClicked;
				}
			}
		}

		private void SourceRowListView_DoubleClicked(object sender, MouseButtonEventArgs e)
		{
			if (SourceRowListView.SelectedItem == null)
				return;

			AssignmentLogic.Assign(SourceRowListView.SelectedItem, TargetRow);
		}

		private void ConnectorRowListView_DoubleClicked(object sender, MouseButtonEventArgs e)
		{
			if (ConnectorRowListView.SelectedItem == null)
				return;

			AssignmentLogic.UnAssign(ConnectorRowListView.SelectedItem, TargetRow);
		}

		



		/// <summary>The assignment logic for the control.</summary>
		/// <typeparam name="TTargetRow">The type of the target row where elements from the source table will be attached over an connector table.</typeparam>
		/// <typeparam name="TConnectorRow">the type of the table which stores the connecting rows between the target row and the source rows.</typeparam>
		/// <typeparam name="TSourceRow">The type of the table which stores the source rows. Multiple source rows can be attached to the target row.</typeparam>
		public class ManualLogic<TTargetRow, TConnectorRow, TSourceRow> : Logic
			where TTargetRow : CsDbTableRow
			where TConnectorRow : CsDbTableRow
			where TSourceRow : CsDbTableRow
		{
			private readonly OnAssignDelegate _assignAction;
			private readonly OnUnAssignDelegate _unassignAction;




			/// <summary>ctor</summary>
			/// <param name="assignAction">The action which is used to assign a new connector.</param>
			/// <param name="unassignAction">the unassign action which is used to delete a connector.</param>
			public ManualLogic(OnAssignDelegate assignAction, OnUnAssignDelegate unassignAction)
			{
				_assignAction = assignAction;
				_unassignAction = unassignAction;

			}


			#region Overrides/Interfaces
			internal override void Assign(object target, object source)
			{
				_assignAction((TSourceRow) source, (TTargetRow) target);
			}

			internal override void UnAssign(object connector, object target)
			{
				_unassignAction((TConnectorRow) connector, (TTargetRow) target);
			}
			#endregion


			/// <summary>The delegate which is used on assigning a new value to the target.</summary>
			/// <param name="source">The source which needs to be attached to the target.</param>
			/// <param name="target">The target row which needs the connection between source and target.</param>
			/// <returns></returns>
			public delegate TConnectorRow OnAssignDelegate(TSourceRow source, TTargetRow target);



			/// <summary>The delegate which is used to unassign a connector from the target row.</summary>
			/// <param name="connector">The connector which needs to be deleted.</param>
			/// <param name="target">the target row.</param>
			public delegate void OnUnAssignDelegate(TConnectorRow connector, TTargetRow target);
		}
	}
}