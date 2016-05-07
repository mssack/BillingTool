// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using BillingTool.Themes.Controls.storno;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.Themes.Controls
{
	/// <summary>This control is used to represent a <see cref="BelegData" /> for viewing purpose. This helps to separate style changes of Bon controls.</summary>
	public class BelegDataView : Control
	{


		private Button _erneutDruckenButton;
		private Button _erneutSendenButton;
		private Button _stornoButton;


		static BelegDataView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(BelegDataView), new FrameworkPropertyMetadata(typeof(BelegDataView)));
		}


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

			StornoButton = (Button) Template.FindName("PART_StornoButton", this);
			ErneutSendenButton = (Button) Template.FindName("PART_ErneutSendenButton", this);
			ErneutDruckenButton = (Button) Template.FindName("PART_ErneutDruckenButton", this);
		}
		#endregion


		/// <summary>The item to view.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>The selected <see cref="OutputFormat" /> which will be used for the preview.</summary>
		public OutputFormat SelectedOutputFormat
		{
			get { return (OutputFormat) GetValue(SelectedOutputFormatProperty); }
			set { SetValue(SelectedOutputFormatProperty, value); }
		}
		/// <summary>Gets a list of used <see cref="OutputFormat" />'s by the <see cref="Item" />.</summary>
		public OutputFormat[] UsedOutputFormats
		{
			get { return (OutputFormat[]) GetValue(UsedOutputFormatsProperty); }
			set { SetValue(UsedOutputFormatsProperty, value); }
		}

		private Button StornoButton
		{
			get { return _stornoButton; }
			set
			{
				if (_stornoButton != null && !Equals(_stornoButton, value))
					_stornoButton.Click -= stornoButton_Click;
				_stornoButton = value;
				if (_stornoButton != null)
					_stornoButton.Click += stornoButton_Click;
			}
		}


		private Button ErneutSendenButton
		{
			get { return _erneutSendenButton; }
			set
			{
				if (_erneutSendenButton != null && !Equals(_erneutSendenButton, value))
					_erneutSendenButton.Click -= erneutSendenButton_Click;
				_erneutSendenButton = value;
				if (_erneutSendenButton != null)
					_erneutSendenButton.Click += erneutSendenButton_Click;
			}
		}


		private Button ErneutDruckenButton
		{
			get { return _erneutDruckenButton; }
			set
			{
				if (_erneutDruckenButton != null && !Equals(_erneutDruckenButton, value))
					_erneutDruckenButton.Click -= erneutDruckenButton_Click;
				_erneutDruckenButton = value;
				if (_erneutDruckenButton != null)
					_erneutDruckenButton.Click += erneutDruckenButton_Click;
			}
		}

		private void ItemChanged()
		{
			if (Item == null)
			{
				UsedOutputFormats = null;
				SelectedOutputFormat = null;
			}
			else
			{
				UsedOutputFormats = Item.PrintedBelege.Select(x => x.OutputFormat).Union(Item.MailedBelege.Select(x => x.OutputFormat)).ToArray();
				SelectedOutputFormat = UsedOutputFormats.Length == 0 ? null : UsedOutputFormats[0];
			}
		}


		private void stornoButton_Click(object sender, RoutedEventArgs e)
		{

			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var approvalData = StornoApproval.DoApprovalFor(Item);
				if (approvalData.MessageResult == CsMessage.MessageResults.No)
					return;
				var belegData = Bt.DataFunctions.New_StornoBelegData_From_BelegData(Item);
				belegData.Comment = approvalData.ReasonText;
				Bt.DataFunctions.Save_New_BelegData(belegData);
			}


		}

		private void erneutSendenButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void erneutDruckenButton_Click(object sender, RoutedEventArgs e)
		{

		}

#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BelegDataView) o).ItemChanged()});
		public static readonly DependencyProperty SelectedOutputFormatProperty = DependencyProperty.Register("SelectedOutputFormat", typeof(OutputFormat), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty UsedOutputFormatsProperty = DependencyProperty.Register("UsedOutputFormats", typeof(OutputFormat[]), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat[]), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}