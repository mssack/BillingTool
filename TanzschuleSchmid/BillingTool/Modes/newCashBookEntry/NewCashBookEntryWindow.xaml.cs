// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-29</date>

using System;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.Runtime;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.Modes.newCashBookEntry
{
	/// <summary>This window is used to create a new cash book entry inside the database. This is the typical use case for this program.</summary>
	public partial class NewCashBookEntryWindow : Window
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (CashBookEntry), typeof (NewCashBookEntryWindow), new FrameworkPropertyMetadata {DefaultValue = default(CashBookEntry), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((NewCashBookEntryWindow) o).ItemChanged(args.OldValue as CashBookEntry, args.NewValue as CashBookEntry); }});

#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public NewCashBookEntryWindow()
		{
			InitializeComponent();
		}

		/// <summary>The cash book entry to add. This row may not be in data table already!!</summary>
		public CashBookEntry Item
		{
			get { return (CashBookEntry) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>Aborts the <see cref="CashBookEntry" /> and does not store it to the database. This method does not open an message box!!!</summary>
		public void Abort()
		{
			Item.Delete();
			//TODO add logging
			Exit();
		}

		/// <summary>Accepts the <see cref="CashBookEntry" /> item and save it to the database. This method does not open an message box!!!</summary>
		public void Accept()
		{
			if (Item.RowState != DataRowState.Detached)
				throw new InvalidOperationException($"The specified {nameof(CashBookEntry)} [{Item.Id}] is already added to an table. " +
                                                    $"This is illegal might be an programming failure. " +
													$"The {nameof(NewCashBookEntryWindow)} is for validating an item not for editing an existing item");



			Db.EnsureConnectivity();

			Item.LastEdited = Item.Date = DateTime.Now;


			Db.Billing.CashBook.Add(Item);
			Db.Billing.SaveAnabolic();
			//TODO add logging
			Exit();
		}


		private void Exit()
		{
			Close();
		}

		/// <summary>Occurs whenever the item is changed.</summary>
		private void ItemChanged(CashBookEntry oldEntry, CashBookEntry newEntry)
		{
			if (newEntry.RowState != DataRowState.Detached)
				throw new InvalidOperationException($"The specified {nameof(CashBookEntry)} [{Item.Id}] is already added to an table. " +
													$"This is illegal might be an programming failure. " +
													$"The {nameof(NewCashBookEntryWindow)} is for validating an item not for editing an existing item");
		}

		private void BonierenClick(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No==CsGlobal.Message.Push($"Sind Sie sicher, dass Sie den Beleg mit der Nummer [{Item.ReferenceNumber}] bonieren wollen", CsMessage.Types.Information, "Beleg eintragen?", CsMessage.MessageButtons.YesNo))
				return;
			Accept();
		}

		private void AbbrechenClick(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push($"Beleg mit der Nummer [{Item.ReferenceNumber}] verwerfen?", CsMessage.Types.Warning, "Beleg verwerfen?", CsMessage.MessageButtons.YesNo))
				return;
			Abort();
		}
		

		private void WindowPreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (e.SystemKey != Key.F2 && e.SystemKey != Key.Escape)
				return;

			e.Handled = true;

			if (e.SystemKey == Key.F2)
				Accept();
			else if (e.SystemKey == Key.Escape)
				Abort();
		}
	}
}