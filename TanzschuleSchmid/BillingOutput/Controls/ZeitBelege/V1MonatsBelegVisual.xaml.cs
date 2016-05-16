// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CsWpfBase.Ev.Objects;






namespace BillingOutput.Controls.ZeitBelege
{
	/// <summary>Interaction logic for V1MonatsBelegVisual.xaml</summary>
	public partial class V1MonatsBelegVisual : UserControl
	{
		#region DP Keys
#pragma warning disable 1591
		public static readonly DependencyProperty KopfProperty = DependencyProperty.Register("Kopf", typeof(KopfDaten), typeof(V1MonatsBelegVisual), new FrameworkPropertyMetadata {DefaultValue = default(KopfDaten), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty KategorienProperty = DependencyProperty.Register("Kategorien", typeof(Categorie), typeof(V1MonatsBelegVisual), new FrameworkPropertyMetadata {DefaultValue = default(Categorie), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
		#endregion


		/// <summary>ctor</summary>
		public V1MonatsBelegVisual()
		{
			Kopf = new KopfDaten();
			Kategorien = new Categorie();
			InitializeComponent();
		}

		/// <summary>The header of the document.</summary>
		public KopfDaten Kopf
		{
			get { return (KopfDaten) GetValue(KopfProperty); }
			set { SetValue(KopfProperty, value); }
		}

		/// <summary>The first list with the price per typ.</summary>
		public Categorie Kategorien
		{
			get { return (Categorie) GetValue(KategorienProperty); }
			set { SetValue(KategorienProperty, value); }
		}



		public class KopfDaten : Base
		{
			private DateTime _kassenEnde;
			private DateTime _kassenStart;


			/// <summary>Gets or sets the KassenStart.</summary>
			public DateTime KassenStart
			{
				get { return _kassenStart; }
				set { SetProperty(ref _kassenStart, value); }
			}
			/// <summary>Gets or sets the KassenEnde.</summary>
			public DateTime KassenEnde
			{
				get { return _kassenEnde; }
				set { SetProperty(ref _kassenEnde, value); }
			}
		}



		public class Categorie : Base
		{
			private decimal _anzahl;
			private decimal _brutto;
			private decimal _netto;
			private string _typ;
			private decimal _ust;

			/// <summary>Gets or sets the Typ.</summary>
			public string Typ
			{
				get { return _typ; }
				set { SetProperty(ref _typ, value); }
			}
			/// <summary>Gets or sets the Anzahl.</summary>
			public decimal Anzahl
			{
				get { return _anzahl; }
				set { SetProperty(ref _anzahl, value); }
			}
			/// <summary>Gets or sets the Netto.</summary>
			public decimal Netto
			{
				get { return _netto; }
				set { SetProperty(ref _netto, value); }
			}
			/// <summary>Gets or sets the Ust.</summary>
			public decimal Ust
			{
				get { return _ust; }
				set { SetProperty(ref _ust, value); }
			}
			/// <summary>Gets or sets the Brutto.</summary>
			public decimal Brutto
			{
				get { return _brutto; }
				set { SetProperty(ref _brutto, value); }
			}
		}
	}
}