﻿// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingOutput.Interfaces;
using CsWpfBase.Ev.Objects;






namespace BillingOutput.Controls.BonVisuals
{
	/// <summary>Interaction logic for BonV1Visual.xaml</summary>
	public partial class BonV1Visual : UserControl
	{


		/// <summary>ctor</summary>
		public BonV1Visual()
		{
			InitializeComponent();
		}

		/// <summary>ctor</summary>
		public BonV1Visual(BelegData item)
		{
			Item = item;
			InitializeComponent();
		}

		/// <summary>The item for which the <see cref="BonV1Visual" /> should be drawn.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}
		/// <summary>The summation for each <see cref="Steuersatz" />.</summary>
		public SteuerSchlüssel[] SteuerAufschlüsselung
		{
			get { return (SteuerSchlüssel[]) GetValue(SteuerAufschlüsselungProperty); }
			set { SetValue(SteuerAufschlüsselungProperty, value); }
		}
		/// <summary>Layout informations for the Bon.</summary>
		public IContainBonLayout BonLayout
		{
			get { return (IContainBonLayout) GetValue(BonLayoutProperty); }
			set { SetValue(BonLayoutProperty, value); }
		}

		private void ItemChanged(BelegData newValue)
		{
			ReloadSteuerschlüssel();
		}


		private void ReloadSteuerschlüssel()
		{
			if (Item == null)
			{
				SteuerAufschlüsselung = null;
				return;
			}

			var data = new Dictionary<Steuersatz, SteuerSchlüssel>();
			foreach (var posten in Item.Postens)
			{
				SteuerSchlüssel schlüssel;
				if (!data.TryGetValue(posten.Steuersatz, out schlüssel))
				{
					schlüssel = new SteuerSchlüssel(posten.Steuersatz);
					data.Add(posten.Steuersatz, schlüssel);
				}
				schlüssel.BetragBrutto += posten.BetragBrutto;
			}
			SteuerAufschlüsselung = data.Values.ToArray();
		}



		/// <summary>Used for the summation of taxes.</summary>
		public class SteuerSchlüssel : Base
		{
			private decimal _betragBrutto;
			private Steuersatz _steuersatz;

			/// <summary>ctor</summary>
			public SteuerSchlüssel(Steuersatz steuersatz)
			{
				Steuersatz = steuersatz;
			}

			/// <summary>Gets or sets the Steuersatz.</summary>
			public Steuersatz Steuersatz
			{
				get { return _steuersatz; }
				private set { SetProperty(ref _steuersatz, value); }
			}
			/// <summary>Brutto.</summary>
			public decimal BetragBrutto
			{
				get { return _betragBrutto; }
				set
				{
					if (!SetProperty(ref _betragBrutto, value)) return;
					OnPropertyChanged(nameof(BetragNetto));
					OnPropertyChanged(nameof(BetragDifferenz));
				}
			}
			/// <summary>Netto</summary>
			public decimal BetragNetto => BetragBrutto/(1 + Steuersatz.Percent/100);
			/// <summary>The difference between <see cref="BetragBrutto" /> and <see cref="BetragNetto" /></summary>
			public decimal BetragDifferenz => BetragBrutto - BetragNetto;
		}
#pragma warning disable 1591
		public static readonly DependencyProperty BonLayoutProperty = DependencyProperty.Register("BonLayout", typeof (IContainBonLayout), typeof (BonV1Visual), new FrameworkPropertyMetadata {DefaultValue = default(IContainBonLayout), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof (BelegData), typeof (BonV1Visual), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BonV1Visual) o).ItemChanged(args.NewValue as BelegData)});
		public static readonly DependencyProperty SteuerAufschlüsselungProperty = DependencyProperty.Register("SteuerAufschlüsselung", typeof (SteuerSchlüssel[]), typeof (BonV1Visual), new FrameworkPropertyMetadata {DefaultValue = default(SteuerSchlüssel[]), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}