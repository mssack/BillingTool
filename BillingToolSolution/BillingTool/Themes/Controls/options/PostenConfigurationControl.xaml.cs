// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingTool.btScope;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace BillingTool.Themes.Controls.options
{
	/// <summary>Interaction logic for OutputFormatConfigurationControl.xaml</summary>
	public partial class PostenConfigurationControl : UserControl
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Posten), typeof(PostenConfigurationControl), new FrameworkPropertyMetadata {DefaultValue = default(Posten), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((PostenConfigurationControl) o).SelectedItemChanged()});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public PostenConfigurationControl()
		{
			InitializeComponent();
			Loaded += Control_Loaded;
		}
		private void Control_Loaded(object sender, RoutedEventArgs e)
		{
			if (!Bt.Db.Billing.Postens.HasBeenLoaded)
				Bt.Db.Billing.Postens.DownloadRows();
		}


		/// <summary>The current selected item.</summary>
		public Posten SelectedItem
		{
			get { return (Posten) GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}


		private void SelectedItemChanged()
		{

		}

		private void LöschenClicked(object sender, RoutedEventArgs e)
		{
			if (SelectedItem.HasBeenUsed)
			{
				CsGlobal.Message.Push("Sie können diesen Posten nicht löschen da er bereits benutzt wird.");
				return;
			}
			SelectedItem.Delete();
		}

		private void HinzufügenClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				if (Bt.Data.Posten.HasNonFinalizedRows)
					Bt.Data.Posten.Finalize_All();
			}
			catch (Exception exc)
			{
				CsGlobal.Message.Push("Der vorherige Posten konnte nicht mit diesem Namen in der Datenbank gespeichert werden, weil es bereits solch einen Posten gibt.", CsMessage.Types.Warning);
				return;
			}
			var format = Bt.Data.Posten.New();
			SelectedItem = format;
		}
	}
}