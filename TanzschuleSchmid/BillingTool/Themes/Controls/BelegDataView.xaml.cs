// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-05</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Themes.Controls
{
	/// <summary>This control is used to represent a <see cref="BelegData" /> for viewing purpose. This helps to separate style changes of Bon controls.</summary>
	public class BelegDataView : Control
	{


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


		private void stornoButton_Click(object sender, RoutedEventArgs e)
		{
			using (CsGlobal.Wpf.Window.GrayOutAllWindows())
			{
				var result = CsGlobal.Message.Push($"Sind Sie sicher, dass Sie den Beleg mit der Nummer {Item.Nummer} stornieren wollen? Diese Aktion kann nicht rückgängig gemacht werden.", CsMessage.Types.Warning, $"Beleg {Item.Nummer} stornieren?", CsMessage.MessageButtons.YesNo);

				if (result == CsMessage.MessageResults.No)
					return;
			}


			var belegData = Bt.DataFunctions.New_Storno_From_BelegData(Item);
			Bt.DataFunctions.Save_New_BelegData(belegData);
		}

		private void erneutSendenButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void erneutDruckenButton_Click(object sender, RoutedEventArgs e)
		{

		}
#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BelegDataView), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		private Button _stornoButton;
		private Button _erneutSendenButton;
		private Button _erneutDruckenButton;
#pragma warning restore 1591
	}
}