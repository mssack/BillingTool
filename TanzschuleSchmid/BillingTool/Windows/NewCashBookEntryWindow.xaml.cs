// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BillingDataAccess.sqlcedatabases.billingdatabase.Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>This window is used to create a new cash book entry inside a generic table. This is the typical use case for this program.</summary>
	public partial class NewCashBookEntryWindow : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (CashBookEntry), typeof (NewCashBookEntryWindow), new FrameworkPropertyMetadata {DefaultValue = default(CashBookEntry), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((NewCashBookEntryWindow) o).ItemChanged(args.OldValue as CashBookEntry, args.NewValue as CashBookEntry); }});

#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public NewCashBookEntryWindow(CashBookEntry item)
		{
			Item = item;
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "NewCashBookEntryWindow");
			Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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
			Bt.Logging.New(LogTitels.FinanzbucheintragAbgebrochen, $"Ein neuer Finanzbucheintrag[RefNr. {Item.ReferenceNumber}, Table = '{Item.Table.TableName}'] wurde abgebrochen.");
			Item.Delete();
			Exit();
		}

		/// <summary>Accepts the <see cref="CashBookEntry" /> item and save it to the database. This method does not open an message box!!!</summary>
		public void Accept()
		{
			if (Item.RowState != DataRowState.Detached)
				throw new InvalidOperationException($"The {Item} is already added to an table. " +
													$"This is illegal might be an programming failure. " +
													$"The {nameof(NewCashBookEntryWindow)} is for validating an item not for editing an existing item");
			if (!Item.IsValid)
				throw new InvalidOperationException($"The {Item} is invalid and can not be saved.");



			Item.LastEdited = Item.Date = DateTime.Now;
			Item.Table.Add(Item);
			Item.Table.SaveChanges();
			Item.Table.AcceptChanges();

			Bt.Logging.New(LogTitels.FinanzbucheintragErstellt, $"Ein neuer Finanzbucheintrag [RefNr = '{Item.ReferenceNumber}', Table = '{Item.Table.TableName}'] wurde erstellt.");
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
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push($"Sind Sie sicher, dass Sie den Beleg [RefNr={Item.ReferenceNumber}] bonieren wollen", CsMessage.Types.Information, "Beleg eintragen?", CsMessage.MessageButtons.YesNo))
				return;
			Accept();
		}

		private void AbbrechenClick(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push($"Beleg mit der Nummer [{Item.ReferenceNumber}] verwerfen?", CsMessage.Types.Warning, "Beleg verwerfen?", CsMessage.MessageButtons.YesNo))
				return;
			Abort();
		}


		private void WindowKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.F2 && e.Key != Key.Escape)
				return;


			e.Handled = true;

			if (e.Key == Key.F2 && Item.IsValid)
				Accept();
			else if (e.Key == Key.Escape)
				Abort();
		}
	}
}