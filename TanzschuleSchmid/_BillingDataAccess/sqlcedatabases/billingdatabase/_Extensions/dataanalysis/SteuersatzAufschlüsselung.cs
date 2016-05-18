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

			private decimal _betragBrutto;
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

			/// <summary>Gets or sets the BetragBrutto.</summary>
			public decimal BetragBrutto
			{
				get { return _betragBrutto; }
				internal set
				{
					if (SetProperty(ref _betragBrutto, value))
					{
						OnPropertyChanged(nameof(BetragNetto));
						OnPropertyChanged(nameof(BetragDifferenz));
					}
				}
			}
			/// <summary>Gets or sets the BetragNetto.</summary>
			public decimal BetragNetto => BetragBrutto/(1 + Steuersatz.Percent/100);
			/// <summary>The difference between <see cref="BetragBrutto" /> and <see cref="BetragNetto" /></summary>
			public decimal BetragDifferenz => BetragBrutto - BetragNetto;
		}
	}
}