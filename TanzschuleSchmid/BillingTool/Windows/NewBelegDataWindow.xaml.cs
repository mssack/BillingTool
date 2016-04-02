// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>This window is used to create a new cash book entry inside a generic table. This is the typical use case for this program.</summary>
	public partial class NewBelegDataWindow : CsWindow
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (BelegData), typeof (NewBelegDataWindow), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((NewBelegDataWindow) o).ItemChanged(args.OldValue as BelegData, args.NewValue as BelegData); }});

#pragma warning restore 1591
		#endregion


		private bool _managedClosing;

		/// <summary>ctor</summary>
		public NewBelegDataWindow(BelegData item)
		{



			Item = item;
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "NewBelegDataWindow");
			Topmost = true;
			Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			Closing += NewCashBookEntryWindow_Closing;
		}



		/// <summary>The cash book entry to add. This row may not be in data table already!!</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}


		/// <summary>
		///     Accepts the <see cref="BelegData" /> item and save it to the database. This method does not open an message box!!! It saves the
		///     <see cref="BelegData" /> to the associated table instance(!).
		/// </summary>
		public void Accept()
		{
			Bt.Functions.FinalizeAndSave_NewBelegData(Item);
			Bt.Logging.New(LogTitels.BelegDatenErstellt, $"Ein neuer {Item} wurde erstellt.");
			Bt.Functions.SetExitCode(ExitCodes.BelegDataCreation_Succeeded);
			ManagedClose();
		}

		/// <summary>Aborts the <see cref="BelegData" /> and does not store it to the database. This method does not open an message box!!!</summary>
		public void Abort()
		{
			var item = Item.ToString();
			Item.Delete();
			Bt.Logging.New(LogTitels.FinanzbucheintragAbgebrochen, $"Ein neuer {item} wurde verworfen.");
			Bt.Functions.SetExitCode(ExitCodes.BelegDataCreation_Aborted);
			ManagedClose();
		}




		private void BonierenClick(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push($"Sind Sie sicher, dass Sie den {Item} bonieren wollen", CsMessage.Types.Information, "Beleg eintragen?", CsMessage.MessageButtons.YesNo))
				return;
			Accept();
		}

		private void AbbrechenClick(object sender, RoutedEventArgs e)
		{
			if (CsMessage.MessageResults.No == CsGlobal.Message.Push($"{Item} verwerfen?", CsMessage.Types.Warning, "Beleg verwerfen?", CsMessage.MessageButtons.YesNo))
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

		private void NewCashBookEntryWindow_Closing(object sender, CancelEventArgs e)
		{
			if (_managedClosing)
				return;

			Bt.Functions.SetExitCode(ExitCodes.BelegDataCreation_Aborted);
		}

		/// <summary>Occurs whenever the item is changed.</summary>
		private void ItemChanged(BelegData oldEntry, BelegData newEntry)
		{
			if (newEntry == null)
				return;
			//if (newEntry.RowState != DataRowState.Detached)
			//	throw new InvalidOperationException($"The specified {newEntry} is already added to an table. " +
			//										$"This is illegal might be an programming failure. " +
			//										$"The {nameof(NewBelegDataWindow)} is for creating an item not for editing an existing item");
		}

		private void ManagedClose()
		{
			_managedClosing = true;
			Close();
		}
	}
}