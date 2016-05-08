// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Resources.Converters.logic;






namespace BillingTool.Themes.Controls.belegview
{
	/// <summary>Used to approve a Storno action.</summary>
	public partial class StornoApproval : UserControl
	{

		/// <summary>Opens a message window and asks for a reason.</summary>
		public static StornoApproval DoApprovalFor(BelegData target)
		{
			var stornoApproval = new StornoApproval {ItemToStorno = target};
			var window = CsGlobal.Message.GetWindow(stornoApproval, CsMessage.Types.Warning, $"Beleg {target.Nummer} stornieren?", CsMessage.MessageButtons.YesNo);
			window.SetBinding(CsMessageWindow.YesButtonEnabledProperty, new Binding("ReasonText") {Source = stornoApproval, Converter = new Conv_InvIsNullOrEmpty()});
			stornoApproval.MessageResult = window.ShowDialog();
			return stornoApproval;
		}

		/// <summary>ctor</summary>
		private StornoApproval()
		{
			InitializeComponent();
		}

		/// <summary>The text which can be used as reason why the <see cref="ItemToStorno" /> needs to be storniert.</summary>
		public string ReasonText
		{
			get { return (string) GetValue(ReasonTextProperty); }
			set { SetValue(ReasonTextProperty, value); }
		}

		/// <summary>The result of the message box.</summary>
		public CsMessage.MessageResults MessageResult
		{
			get { return (CsMessage.MessageResults) GetValue(MessageResultProperty); }
			set { SetValue(MessageResultProperty, value); }
		}

		/// <summary>The item which needs to be storniert.</summary>
		public BelegData ItemToStorno
		{
			get { return (BelegData) GetValue(ItemToStornoProperty); }
			set { SetValue(ItemToStornoProperty, value); }
		}
#pragma warning disable 1591
		public static readonly DependencyProperty ReasonTextProperty = DependencyProperty.Register("ReasonText", typeof(string), typeof(StornoApproval), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemToStornoProperty = DependencyProperty.Register("ItemToStorno", typeof(BelegData), typeof(StornoApproval), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty MessageResultProperty = DependencyProperty.Register("MessageResult", typeof(CsMessage.MessageResults), typeof(StornoApproval), new FrameworkPropertyMetadata {DefaultValue = default(CsMessage.MessageResults), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}