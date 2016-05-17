// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-08</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace BillingTool.Themes.Controls.belegview
{
	/// <summary>Interaction logic for RemailBelegControl.xaml</summary>
	public partial class RemailBelegControl : UserControl
	{
		/// <summary>ctor</summary>
		public RemailBelegControl()
		{
			InitializeComponent();
			Reset();
		}

		/// <summary>The selected format.</summary>
		public OutputFormat OutputFormat
		{
			get { return (OutputFormat) GetValue(OutputFormatProperty); }
			set { SetValue(OutputFormatProperty, value); }
		}
		/// <summary></summary>
		public string Betreff
		{
			get { return (string) GetValue(BetreffProperty); }
			set { SetValue(BetreffProperty, value); }
		}

		/// <summary></summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary></summary>
		public string TargetMailAddress
		{
			get { return (string) GetValue(TargetMailAddressProperty); }
			set { SetValue(TargetMailAddressProperty, value); }
		}

		/// <summary>The item which needs to be remailed.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>Occurs whenever a mail has been successfully sended.</summary>
		public event Action MailSended;

		private void ItemChanged()
		{
			Reset();
		}
		private void Reset()
		{
			OutputFormat = (Item != null && Item.Typ == BelegDataTypes.Storno) ? Bt.Db.Billing.OutputFormats.Default_StornoFormat : Bt.Db.Billing.OutputFormats.Default_MailFormat;
			Betreff = Bt.Db.Billing.Configurations.Default_MailBetreff;
			Text = Bt.Db.Billing.Configurations.Default_MailText;
			TargetMailAddress = Item?.MailedBelege.OrderBy(x=>x.ProcessingDate).FirstOrDefault()?.TargetMailAddress;
		}

		private void Send()
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var mailedBeleg = Bt.Data.MailedBeleg.New(Item, TargetMailAddress);
				mailedBeleg.OutputFormat = OutputFormat;
				mailedBeleg.Betreff = Betreff;
				mailedBeleg.Text = Text;
				Bt.Data.MailedBeleg.Finalize(mailedBeleg);

				Bt.Ui.ProcessNonProcessedOutputs(Item);
				Bt.Data.SyncAnabolicChanges();

				Reset();
				MailSended?.Invoke();
			}
		}

		private void SendButtonClicked(object sender, RoutedEventArgs e)
		{
			Send();
			((FrameworkElement)sender).GetParentByCondition<Popup>(ex => true).IsOpen = false;
		}

#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(RemailBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData),  DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((RemailBelegControl)o).ItemChanged()});


		public static readonly DependencyProperty OutputFormatProperty = DependencyProperty.Register("OutputFormat", typeof(OutputFormat), typeof(RemailBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty BetreffProperty = DependencyProperty.Register("Betreff", typeof(string), typeof(RemailBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(RemailBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty TargetMailAddressProperty = DependencyProperty.Register("TargetMailAddress", typeof(string), typeof(RemailBelegControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}