// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using BillingTool.btScope;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Global.message;
using CsWpfBase.Themes.Controls.Basics;






#pragma warning disable 1591

namespace BillingTool.Themes.Controls.belegdatacreation
{
	/// <summary>Interaction logic for NewBelegPostenControl.xaml</summary>
	public partial class NewBelegPostenControl : UserControl
	{
		#region DP Keys
		public static readonly DependencyProperty BelegPosten_AnzahlProperty = DependencyProperty.Register("BelegPosten_Anzahl", typeof(int), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(int), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty BelegPosten_PostenProperty = DependencyProperty.Register("BelegPosten_Posten", typeof(Posten), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(Posten), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty BelegPosten_SteuersatzProperty = DependencyProperty.Register("BelegPosten_Steuersatz", typeof(Steuersatz), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(Steuersatz), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SortedPostenProperty = DependencyProperty.Register("SortedPosten", typeof(ContractCollection<Posten>), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(ContractCollection<Posten>), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty Posten_NameProperty = DependencyProperty.Register("Posten_Name", typeof(string), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty Posten_PreisBruttoProperty = DependencyProperty.Register("Posten_PreisBrutto", typeof(decimal), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(decimal), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty Posten_DimensionProperty = DependencyProperty.Register("Posten_Dimension", typeof(string), typeof(NewBelegPostenControl), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private bool _wasReseted;


		/// <summary>ctor</summary>
		public NewBelegPostenControl()
		{
			InitializeComponent();
			Loaded += NewBelegPostenControl_Loaded;

		}

		public ContractCollection<Posten> SortedPosten
		{
			get { return (ContractCollection<Posten>) GetValue(SortedPostenProperty); }
			set { SetValue(SortedPostenProperty, value); }
		}
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		public int BelegPosten_Anzahl
		{
			get { return (int) GetValue(BelegPosten_AnzahlProperty); }
			set { SetValue(BelegPosten_AnzahlProperty, value); }
		}
		public Posten BelegPosten_Posten
		{
			get { return (Posten) GetValue(BelegPosten_PostenProperty); }
			set { SetValue(BelegPosten_PostenProperty, value); }
		}

		public Steuersatz BelegPosten_Steuersatz
		{
			get { return (Steuersatz) GetValue(BelegPosten_SteuersatzProperty); }
			set { SetValue(BelegPosten_SteuersatzProperty, value); }
		}

		public string Posten_Name
		{
			get { return (string) GetValue(Posten_NameProperty); }
			set { SetValue(Posten_NameProperty, value); }
		}

		public decimal Posten_PreisBrutto
		{
			get { return (decimal) GetValue(Posten_PreisBruttoProperty); }
			set { SetValue(Posten_PreisBruttoProperty, value); }
		}

		public string Posten_Dimension
		{
			get { return (string) GetValue(Posten_DimensionProperty); }
			set { SetValue(Posten_DimensionProperty, value); }
		}

		private void NewBelegPostenControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (_wasReseted == true)
				return;
			SortedPosten = Bt.Db.Billing.Postens.CreateContractCollection(posten => true);
			SortedPosten.SortDesc(posten => posten.LastUsedDate);
			_wasReseted = true;
			Reset();
		}

		public event Action<NewBelegPostenControl> Completed;

		private void Reset()
		{

			BelegPosten_Anzahl = 1;
			BelegPosten_Posten = SortedPosten.Count == 0 ? null : SortedPosten[0];
			BelegPosten_Steuersatz = Bt.Db.Billing.Steuersätze.OrderByDescending(x => x.LastUsedDate).FirstOrDefault();
		}
		

		private void Create_BelegPosten_Click(object sender, RoutedEventArgs e)
		{
			var item = Item.Postens.FirstOrDefault(x => x.Posten == BelegPosten_Posten && x.Steuersatz == BelegPosten_Steuersatz);
			if (item != null)
			{
				item.Anzahl = item.Anzahl + BelegPosten_Anzahl;
				item.Posten.AnzahlGekauft = item.Posten.AnzahlGekauft + item.Anzahl;
				item.Posten.LastUsedDate = DateTime.Now;
				item.Steuersatz.LastUsedDate = DateTime.Now;

				Bt.Data.BelegData.UpdateBetrag_Of_BelegData(Item);
			}
			else
			{
				var belegPosten = Bt.Data.BelegPosten.New(Item, BelegPosten_Anzahl, BelegPosten_Posten, BelegPosten_Steuersatz);
				Bt.Data.BelegPosten.Finalize(belegPosten);
			}
			Reset();
			Completed?.Invoke(this);
		}


		private void Cancle_BelegPosten_Click(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			Completed?.Invoke(this);
		}

		private void Change_To_PostenCreation(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			Transition_To(NewPostenBorder, 500);
		}

		private void Cancle_Posten_Clicked(object sender, RoutedEventArgs e)
		{
			Transition_To(NewBelegPostenBorder, 500);
		}

		private void Create_Posten_Clicked(object sender, RoutedEventArgs e)
		{
			var posten = Bt.Db.Billing.Postens.LoadThenFind_By_NameAndPreis(Posten_Name, Posten_PreisBrutto);
			if (posten != null)
			{
				BelegPosten_Posten = posten;
				return;
			}

			posten = Bt.Data.Posten.New();
			posten.Name = Posten_Name;
			posten.PreisBrutto = Posten_PreisBrutto;
			posten.Dimension = Posten_Dimension;
			Bt.Data.Posten.Finalize(posten);

			BelegPosten_Posten = posten;

			Transition_To(NewBelegPostenBorder, 500);
		}

		private Task Transition_To(FrameworkElement to, int duration)
		{
			var tResult = new TaskCompletionSource<Task>();
			FrameworkElement from = Equals(to, NewBelegPostenBorder) ? NewPostenBorder : NewBelegPostenBorder;
			var fromCurrentSynchronizationContext = TaskScheduler.FromCurrentSynchronizationContext();


			to.Visibility = Visibility.Collapsed;
			to.Opacity = 0;
			to.IsEnabled = true;
			@from.Visibility = Visibility.Visible;
			@from.Opacity = 1;

			var t1 = CsGlobal.Wpf.Animation.Opacity(@from, 0, new Duration(new TimeSpan(0, 0, 0, 0, duration/2)));
			t1.ContinueWith(t =>
			{
				@from.Visibility = Visibility.Collapsed;
				@from.Opacity = 0;
				from.IsEnabled = false;

				to.Visibility = Visibility.Visible;
				CsGlobal.Wpf.Animation.Opacity(to, 1, new Duration(new TimeSpan(0, 0, 0, 0, duration/2))).ContinueWith(t2 => { tResult.SetResult(t2); }, fromCurrentSynchronizationContext);
			}, fromCurrentSynchronizationContext);

			return tResult.Task;
		}
	}
}