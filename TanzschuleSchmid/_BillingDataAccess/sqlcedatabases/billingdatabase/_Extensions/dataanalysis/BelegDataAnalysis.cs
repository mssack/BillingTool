// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.dataanalysis
{
	/// <summary>Collapses <see cref="Steuersatz" /> analysis for a <see cref="BelegData" /> instance.</summary>
	public sealed class BelegDataAnalysis : Base
	{
		private readonly BelegData[] _belegDatas;
		private decimal _betragBrutto;
		private decimal _betragNetto;
		private int _firstBelegDataNummer;
		private DateTime _firstBelegDataTime;
		private int _lastBelegDataNummer;
		private DateTime _lastBelegDataTime;
		private PerPostenEntry[] _perPostenEntries;

		private PerSteuersatzEntry[] _perSteuersatzEntries;
		private PerTypEntry[] _perTypEntries;

		/// <summary>ctor</summary>
		public BelegDataAnalysis(params BelegData[] belegDatas)
		{
			_belegDatas = belegDatas;
			Reload();
			AttachEventHandlers();

		}


		#region Overrides/Interfaces
		/// <summary>
		///     All references to this object are freed by setting <see cref="Base.PropertyChanged" /> to null. In inherited classes override this method to free
		///     all other events.
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
			DetachEventHandlers();
		}
		#endregion


		/// <summary>Gets or sets the PerSteuersatzEntries.</summary>
		public PerSteuersatzEntry[] PerSteuersatzEntries
		{
			get { return _perSteuersatzEntries; }
			private set { SetProperty(ref _perSteuersatzEntries, value); }
		}
		/// <summary>Gets or sets the PerTypEntries.</summary>
		public PerTypEntry[] PerTypEntries
		{
			get { return _perTypEntries; }
			private set { SetProperty(ref _perTypEntries, value); }
		}
		/// <summary>Gets or sets the PerPostenEntries.</summary>
		public PerPostenEntry[] PerPostenEntries
		{
			get { return _perPostenEntries; }
			private set { SetProperty(ref _perPostenEntries, value); }
		}
		/// <summary>Gets or sets the FirstBelegDataTime.</summary>
		public DateTime FirstBelegDataTime
		{
			get { return _firstBelegDataTime; }
			private set { SetProperty(ref _firstBelegDataTime, value); }
		}
		/// <summary>Gets or sets the LastBelegDataTime.</summary>
		public DateTime LastBelegDataTime
		{
			get { return _lastBelegDataTime; }
			private set { SetProperty(ref _lastBelegDataTime, value); }
		}
		/// <summary>Gets or sets the FirstBelegDataNummer.</summary>
		public int FirstBelegDataNummer
		{
			get { return _firstBelegDataNummer; }
			private set { SetProperty(ref _firstBelegDataNummer, value); }
		}
		/// <summary>Gets or sets the LastBelegDataNummer.</summary>
		public int LastBelegDataNummer
		{
			get { return _lastBelegDataNummer; }
			private set { SetProperty(ref _lastBelegDataNummer, value); }
		}
		private decimal _größterUmsatzZähler;
		///<summary>Gets or sets the GrößterUmsatzZähler.</summary>
		public decimal GrößterUmsatzZähler
		{
			get { return _größterUmsatzZähler; }
			set { SetProperty(ref _größterUmsatzZähler, value); }
		}

		/// <summary>Gets or sets the BetragBrutto.</summary>
		public decimal BetragBrutto
		{
			get { return _betragBrutto; }
			private set
			{
				if (SetProperty(ref _betragBrutto, value))
					OnPropertyChanged(nameof(BetragDifferenz));
			}
		}
		/// <summary>Gets or sets the BetragNetto.</summary>
		public decimal BetragNetto
		{
			get { return _betragNetto; }
			private set
			{
				if (SetProperty(ref _betragNetto, value))
					OnPropertyChanged(nameof(BetragDifferenz));
			}
		}
		/// <summary>The difference between <see cref="BetragBrutto" /> and <see cref="BetragNetto" /></summary>
		public decimal BetragDifferenz => BetragBrutto - BetragNetto;


		private void AttachEventHandlers()
		{
			_belegDatas.ForEach(bd => bd.PropertyChanged += BelegData_PropertyChanged);
		}

		private void DetachEventHandlers()
		{
			_belegDatas.ForEach(bd => bd.PropertyChanged -= BelegData_PropertyChanged);
		}

		private void Reload()
		{
			BetragBrutto = 0;
			BetragNetto = 0;
			FirstBelegDataTime = DateTime.MaxValue;
			LastBelegDataTime = DateTime.MinValue;
			FirstBelegDataNummer = int.MaxValue;
			LastBelegDataNummer = int.MinValue;
			GrößterUmsatzZähler = int.MinValue;

			var perSteuersatzEntries = new Dictionary<Guid, PerSteuersatzEntry>();
			var perPostenEntries = new Dictionary<Posten, PerPostenEntry>();
			var perTypEntries = new Dictionary<BelegDataTypes, Dictionary<Guid, PerSteuersatzEntry>>();

			foreach (var belegData in _belegDatas)
			{
				if (belegData.Nummer < FirstBelegDataNummer)
				{
					FirstBelegDataNummer = belegData.Nummer;
					FirstBelegDataTime = belegData.Datum;
				}

				if (belegData.Nummer > LastBelegDataNummer)
				{
					LastBelegDataNummer = belegData.Nummer;
					LastBelegDataTime = belegData.Datum;
					GrößterUmsatzZähler = belegData.UmsatzZähler;
				}

				Dictionary<Guid, PerSteuersatzEntry> perTypPerSteuersatzEntries;
				if (!perTypEntries.TryGetValue(belegData.Typ, out perTypPerSteuersatzEntries))
				{
					perTypPerSteuersatzEntries = new Dictionary<Guid, PerSteuersatzEntry>();
					perTypEntries.Add(belegData.Typ, perTypPerSteuersatzEntries);
				}
				foreach (var belegPosten in belegData.Postens)
				{
					BetragBrutto += belegPosten.BetragBrutto;
					BetragNetto += belegPosten.BetragNetto;

					Load_PerSteuerSatzEntry_Into(perSteuersatzEntries, belegPosten);
					Load_PerSteuerSatzEntry_Into(perTypPerSteuersatzEntries, belegPosten);
					Load_PerPostenEntry_Into(perPostenEntries, belegPosten);
				}
			}
			PerSteuersatzEntries = perSteuersatzEntries.Values.ToArray();
			PerTypEntries = perTypEntries.Select(x => new PerTypEntry
			{
				PerSteuersatzEntries = x.Value.Values.ToArray(),
				Typ = x.Key,
				GesamtSummeBrutto = x.Value.Values.Sum(y => y.BetragBrutto),
				Anzahl =  x.Value.Values.Sum(y=>y.Anzahl),
			}).ToArray();
			PerPostenEntries = perPostenEntries.Values.ToArray();
		}


		private void Load_PerSteuerSatzEntry_Into(Dictionary<Guid, PerSteuersatzEntry> data, BelegPosten posten)
		{
			PerSteuersatzEntry perSteuersatzEntry;
			if (!data.TryGetValue(posten.Steuersatz.Id, out perSteuersatzEntry))
			{
				perSteuersatzEntry = new PerSteuersatzEntry(posten.Steuersatz);
				data.Add(posten.Steuersatz.Id, perSteuersatzEntry);
			}
			perSteuersatzEntry.BetragBrutto += posten.BetragBrutto;
			perSteuersatzEntry.BetragNetto += posten.BetragNetto;
			perSteuersatzEntry.Anzahl += posten.Anzahl;
		}
		private void Load_PerPostenEntry_Into(Dictionary<Posten, PerPostenEntry> data, BelegPosten belegPosten)
		{
			PerPostenEntry perPostenEntry;
			if (!data.TryGetValue(belegPosten.Posten, out perPostenEntry))
			{
				perPostenEntry = new PerPostenEntry() { Posten = belegPosten.Posten };
				data.Add(belegPosten.Posten, perPostenEntry);
			}
			perPostenEntry.Anzahl++;
			perPostenEntry.BetragBrutto += belegPosten.BetragBrutto;
			perPostenEntry.BetragNetto += belegPosten.BetragNetto;
		}

		private void BelegData_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(BelegData.BetragBrutto) || e.PropertyName == nameof(BelegData.Typ))
				Reload();
		}



		/// <summary>one PerSteuersatzEntry</summary>
		public class PerSteuersatzEntry : Base
		{
			private int _anzahl;

			private decimal _betragBrutto;
			private decimal _betragNetto;
			private Steuersatz _steuersatz;


			internal PerSteuersatzEntry(Steuersatz steuersatz)
			{
				_steuersatz = steuersatz;
			}

			/// <summary>Gets or sets the Steuersatz.</summary>
			public Steuersatz Steuersatz
			{
				get { return _steuersatz; }
				private set { SetProperty(ref _steuersatz, value); }
			}
			/// <summary>Gets or sets the Artikel.</summary>
			public int Anzahl
			{
				get { return _anzahl; }
				set { SetProperty(ref _anzahl, value); }
			}
			/// <summary>Gets or sets the BetragBrutto.</summary>
			public decimal BetragBrutto
			{
				get { return _betragBrutto; }
				internal set
				{
					if (SetProperty(ref _betragBrutto, value))
						OnPropertyChanged(nameof(BetragDifferenz));
				}
			}
			/// <summary>Gets or sets the BetragNetto.</summary>
			public decimal BetragNetto
			{
				get { return _betragNetto; }
				internal set
				{
					if (SetProperty(ref _betragNetto, value))
						OnPropertyChanged(nameof(BetragDifferenz));
				}
			}
			/// <summary>The difference between <see cref="BetragBrutto" /> and <see cref="BetragNetto" /></summary>
			public decimal BetragDifferenz => BetragBrutto - BetragNetto;
		}



		/// <summary>PerTypEntry</summary>
		public class PerTypEntry : Base
		{
			private int _anzahl;
			private decimal _gesamtSummeBrutto;
			private PerSteuersatzEntry[] _perSteuersatzEntries;
			private BelegDataTypes _typ;


			/// <summary>Gets or sets the Typ.</summary>
			public BelegDataTypes Typ
			{
				get { return _typ; }
				set { SetProperty(ref _typ, value); }
			}
			/// <summary>Gets or sets the Anzahl.</summary>
			public int Anzahl
			{
				get { return _anzahl; }
				set { SetProperty(ref _anzahl, value); }
			}
			/// <summary>Gets or sets the GesamtSummeBrutto.</summary>
			public decimal GesamtSummeBrutto
			{
				get { return _gesamtSummeBrutto; }
				set { SetProperty(ref _gesamtSummeBrutto, value); }
			}
			/// <summary>Gets or sets the PerSteuersatzEntries.</summary>
			public PerSteuersatzEntry[] PerSteuersatzEntries
			{
				get { return _perSteuersatzEntries; }
				set { SetProperty(ref _perSteuersatzEntries, value); }
			}
		}



		/// <summary>PerSteuersatzEntry's sorted per typ</summary>
		public class PerPostenEntry : Base
		{

			private int _anzahl;
			private decimal _betragBrutto;
			private decimal _betragNetto;
			private Posten _posten;
			/// <summary>Gets or sets the Anzahl.</summary>
			public int Anzahl
			{
				get { return _anzahl; }
				set { SetProperty(ref _anzahl, value); }
			}
			/// <summary>Gets or sets the Posten.</summary>
			public Posten Posten
			{
				get { return _posten; }
				set { SetProperty(ref _posten, value); }
			}
			/// <summary>Gets or sets the BetragBrutto.</summary>
			public decimal BetragBrutto
			{
				get { return _betragBrutto; }
				set { SetProperty(ref _betragBrutto, value); }
			}
			/// <summary>Gets or sets the BetragNetto.</summary>
			public decimal BetragNetto
			{
				get { return _betragNetto; }
				set { SetProperty(ref _betragNetto, value); }
			}
		}
	}
}