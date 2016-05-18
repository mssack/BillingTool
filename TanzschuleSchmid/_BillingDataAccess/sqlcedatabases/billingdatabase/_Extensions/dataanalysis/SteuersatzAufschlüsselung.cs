// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

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
	public sealed class SteuersatzAufschlüsselung : Base
	{
		private readonly BelegData[] _belegDatas;

		private Entry[] _entries;
		private int _firstBelegDataNummer;
		private DateTime _firstBelegDataTime;
		private int _lastBelegDataNummer;
		private DateTime _lastBelegDataTime;
		private decimal _umsatzBrutto;
		private decimal _umsatzNetto;

		/// <summary>ctor</summary>
		public SteuersatzAufschlüsselung(params BelegData[] belegDatas)
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


		/// <summary>Gets or sets the Entries.</summary>
		public Entry[] Entries
		{
			get { return _entries; }
			private set { SetProperty(ref _entries, value); }
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

		/// <summary>Gets or sets the UmsatzBrutto.</summary>
		public decimal UmsatzBrutto
		{
			get { return _umsatzBrutto; }
			private set { SetProperty(ref _umsatzBrutto, value); }
		}
		/// <summary>Gets or sets the UmsatzNetto.</summary>
		public decimal UmsatzNetto
		{
			get { return _umsatzNetto; }
			private set { SetProperty(ref _umsatzNetto, value); }
		}

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
			var data = new Dictionary<string, Entry>();

			UmsatzBrutto = 0;
			UmsatzNetto = 0;
			FirstBelegDataTime = DateTime.MaxValue;
			LastBelegDataTime = DateTime.MinValue;
			FirstBelegDataNummer = int.MaxValue;
			LastBelegDataNummer = int.MinValue;

			foreach (var belegData in _belegDatas)
			{
				foreach (var posten in belegData.Postens)
				{
					Entry entry;
					if (!data.TryGetValue($"{belegData.Typ}.{posten.Steuersatz.Id}", out entry))
					{
						entry = new Entry(belegData.Typ, posten.Steuersatz);
						data.Add($"{belegData.Typ}.{posten.Steuersatz.Id}", entry);
					}
					entry.BetragBrutto += posten.BetragBrutto;
					entry.BetragNetto += posten.BetragNetto;
					UmsatzBrutto += posten.BetragBrutto;
					UmsatzNetto += posten.BetragNetto;
					entry.Anzahl += posten.Anzahl;
				}
				if (belegData.Nummer < FirstBelegDataNummer)
				{
					FirstBelegDataNummer = belegData.Nummer;
					FirstBelegDataTime = belegData.Datum;
				}

				if (belegData.Nummer > LastBelegDataNummer)
				{
					LastBelegDataNummer = belegData.Nummer;
					LastBelegDataTime = belegData.Datum;
				}
			}

			Entries = data.Values.ToArray();
		}

		private void BelegData_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(BelegData.BetragBrutto) || e.PropertyName == nameof(BelegData.Typ))
				Reload();
		}



		/// <summary>one entry</summary>
		public class Entry : Base
		{
			private int _anzahl;

			private decimal _betragBrutto;
			private decimal _betragNetto;
			private Steuersatz _steuersatz;
			private BelegDataTypes _typ;


			internal Entry(BelegDataTypes typ, Steuersatz steuersatz)
			{
				_steuersatz = steuersatz;
				_typ = typ;
			}

			/// <summary>Gets or sets the Typ.</summary>
			public BelegDataTypes Typ
			{
				get { return _typ; }
				private set { SetProperty(ref _typ, value); }
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
	}
}