// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using BillingTool.Themes.Controls.belegview;
using BillingTool.Themes.Controls._shared;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.Themes.Controls
{
	/// <summary>This control is used to represent a <see cref="BelegData" /> for viewing purpose. This helps to separate style changes of Bon controls.</summary>
	public class BelegDataView : Control
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		private MailedBelegeListView _mailedBelegeListView;
		private PrintedBelegeListView _printedBelegeListView;
		private BelegPostenListView _belegPostenListView;
		private RemailBelegControl _remailBelegControl;
		private ReprintBelegControl _reprintBelegControl;
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

			PrintedBelegeListView = (PrintedBelegeListView) Template.FindName("PART_PrintedBelegeListView", this);
			ReprintBelegControl = (ReprintBelegControl) Template.FindName("PART_ReprintBelegControl", this);

			BelegPostenListView = (BelegPostenListView) Template.FindName("PART_BelegPostenListView", this);

			MailedBelegeListView = (MailedBelegeListView) Template.FindName("PART_MailedBelegeListView", this);
			RemailBelegControl = (RemailBelegControl) Template.FindName("PART_RemailBelegControl", this);

			BonPreviewControl = (BonPreviewControl) Template.FindName("PART_BonPreviewControl", this);
		}
		#endregion


		/// <summary>The item to view.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		private BonPreviewControl BonPreviewControl { get; set; }


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


		private PrintedBelegeListView PrintedBelegeListView
		{
			get { return _printedBelegeListView; }
			set
			{
				if (_printedBelegeListView != null && !Equals(_printedBelegeListView, value))
					_printedBelegeListView.OutputFormatSelected -= NewOutputFormatSelectionRequest;
				_printedBelegeListView = value;
				if (_printedBelegeListView != null)
				{
					CsGlobal.Wpf.Storage.ListView.Handle(_printedBelegeListView, $"{nameof(BelegDataView)}.{nameof(PrintedBelegeListView)}");
					_printedBelegeListView.OutputFormatSelected += NewOutputFormatSelectionRequest;
				}
			}
		}
		private MailedBelegeListView MailedBelegeListView
		{
			get { return _mailedBelegeListView; }
			set
			{
				if (_mailedBelegeListView != null && !Equals(_mailedBelegeListView, value))
					_mailedBelegeListView.OutputFormatSelected -= NewOutputFormatSelectionRequest;
				_mailedBelegeListView = value;
				if (_mailedBelegeListView != null)
				{
					CsGlobal.Wpf.Storage.ListView.Handle(_mailedBelegeListView, $"{nameof(BelegDataView)}.{nameof(MailedBelegeListView)}");
					_mailedBelegeListView.OutputFormatSelected += NewOutputFormatSelectionRequest;
				}
			}
		}
		private BelegPostenListView BelegPostenListView
		{
			get { return _belegPostenListView; }
			set
			{
				if (_belegPostenListView != null && !Equals(_belegPostenListView, value)) {}
				_belegPostenListView = value;
				if (_belegPostenListView != null)
				{
					CsGlobal.Wpf.Storage.ListView.Handle(_belegPostenListView, $"{nameof(BelegDataView)}.{nameof(BelegPostenListView)}");
				}
			}
		}
		private RemailBelegControl RemailBelegControl
		{
			get { return _remailBelegControl; }
			set
			{
				if (_remailBelegControl != null && !Equals(_remailBelegControl, value))
					_remailBelegControl.MailSended -= BonPreviewSelectableReload;
				_remailBelegControl = value;
				if (_remailBelegControl != null)
					_remailBelegControl.MailSended += BonPreviewSelectableReload;
			}
		}
		private ReprintBelegControl ReprintBelegControl
		{
			get { return _reprintBelegControl; }
			set
			{
				if (_reprintBelegControl != null && !Equals(_reprintBelegControl, value))
					_reprintBelegControl.BelegPrinted -= BonPreviewSelectableReload;
				_reprintBelegControl = value;
				if (_reprintBelegControl != null)
					_reprintBelegControl.BelegPrinted += BonPreviewSelectableReload;
			}
		}

		private void NewOutputFormatSelectionRequest(OutputFormat obj)
		{
			BonPreviewControl.SelectedPreviewFormat = obj;
		}

		private void BonPreviewSelectableReload()
		{
			BonPreviewControl.ReloadSelectablePreviewFormats();
		}




		private void stornoButton_Click(object sender, RoutedEventArgs e)
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var approvalData = StornoApprovalControl.DoApprovalFor(Item);
				if (approvalData.MessageResult == CsMessage.MessageResults.No)
					return;

				var stornoBeleg = Bt.Data.BelegData.New_Storno(Item);
				stornoBeleg.Comment = approvalData.ReasonText;
				Bt.Data.BelegData.Finalize(stornoBeleg);
				Bt.Data.SyncAnabolicChanges();
			}
		}
	}
}