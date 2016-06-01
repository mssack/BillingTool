// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;
using BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase.rows

{
	partial class BelegData : IStoreComment
	{
		private BelegData[] _includedBelegDatas;
		private string _includedBelegDatasTag;


		#region Overrides/Interfaces
		/// <summary>sets the value of a column and notify property changed.</summary>
		public override bool SetDbValue<T>(T m, string columnName, [CallerMemberName] string propName = "")
		{
			if (!base.SetDbValue(m, columnName, propName))
				return false;

			if (propName == nameof(Comment))
			{
				//change last changed date on comment change.
				CommentLastChanged = DateTime.Now;
			}

			return true;
		}

		/// <summary>On row creation this method will be executed. So a new row will always have the highest reference number</summary>
		public override void ApplyExtendedDefaults()
		{
			Nummer = DataSet.Configurations.DataIntegrity.LastBelegNummer + 1;
		}

		/// <summary>Returns an identifier for the database row.</summary>
		public override string ToString()
		{
			if (Typ == BelegDataTypes.Storno)
			{
				return $"[BelegData(STORNO), Nummer={Nummer}, Date={Datum}, Beleg={StornoBeleg?.Nummer}]";
			}

			return $"[BelegData, Nummer={Nummer}, Date={Datum}]";
		}
		#endregion


		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(InvalidReason))]
		public bool IsValid => InvalidReason == BelegDataInvalidReasons.Valid;

		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(StateNumber))]
		[DependsOn(nameof(TypNumber))]
		public bool CanBeStorniert => Typ.CanBeStorniert() && !IsStorniert;

		/// <summary>returns true if the <see cref="BelegData" /> has been storniert.</summary>
		[DependsOn(nameof(StateNumber))]
		public bool IsStorniert => State == BelegDataStates.Storniert;


		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(TypNumber))]
		[DependsOn(nameof(KassenOperator))]
		[DependsOn(nameof(StornoBelegId))]
		public BelegDataInvalidReasons InvalidReason
		{
			get
			{
				if (Typ == BelegDataTypes.Unknown || Typ == BelegDataTypes.Undefined)
					return BelegDataInvalidReasons.Missing_BelegDataType;
				if (string.IsNullOrEmpty(KassenOperator))
					return BelegDataInvalidReasons.Missing_Kassenoperator;
				if (Typ == BelegDataTypes.Storno && StornoBeleg == null)
					return BelegDataInvalidReasons.Missing_StornoBeleg;
				if (!Typ.IsRecapBon() && Postens.Count == 0)
					return BelegDataInvalidReasons.Missing_BelegPosten;
				return BelegDataInvalidReasons.Valid;
			}
		}


		/// <summary>The wrapper property for column property <see cref="TypNumber" />.</summary>
		[DependsOn(nameof(TypNumber))]
		public BelegDataTypes Typ
		{
			get { return EnumWrapper.Get(TypNumber, BelegDataTypes.Unknown); }
			set { EnumWrapper.Set(() => TypNumber = (int) value); }
		}

		/// <summary>
		///     true if Bon is of <see cref="Typ" /> <see cref="BelegDataTypes.TagesBon" /> or <see cref="BelegDataTypes.MonatsBon" /> or
		///     <see cref="BelegDataTypes.JahresBon" />.
		/// </summary>
		[DependsOn(nameof(TypNumber))]
		public bool IsRecapBon => Typ.IsRecapBon();

		/// <summary>The wrapper property for column property <see cref="StateNumber" />.</summary>
		[DependsOn(nameof(StateNumber))]
		public BelegDataStates State
		{
			get { return EnumWrapper.Get(StateNumber, BelegDataStates.Unknown); }
			set { EnumWrapper.Set(() => StateNumber = (int) value); }

		}

		/// <summary>Gets the <see cref="BelegData" /> which contains information about the reason why this <see cref="BelegData" /> had been storniert.</summary>
		[DependsOn(nameof(StateNumber))]
		public BelegData StornierenderBeleg
		{
			get
			{
				if (!IsStorniert)
					return null;
				if (StornierendeBelege.Count == 0)
					return null;
				return StornierendeBelege[0];
			}
		}

		/// <summary>
		///     Only useful if <see cref="Typ" /> == <see cref="BelegDataTypes.TagesBon" /> || <see cref="BelegDataTypes.MonatsBon" /> ||
		///     <see cref="BelegDataTypes.JahresBon" />.
		/// </summary>
		[DependsOn(nameof(Typ))]
		[DependsOn(nameof(BonNummerVon))]
		[DependsOn(nameof(BonNummerBis))]
		// ReSharper disable once InconsistentNaming
		public BelegData[] VonBis_BelegData
		{
			get
			{
				if (!Typ.IsRecapBon())
					return null;
				if (BonNummerVon == null || BonNummerBis == null)
					return null;
				if (BonNummerVon > BonNummerBis)
					return null;
				if (_includedBelegDatasTag == $"{BonNummerVon.Value}.{BonNummerBis.Value}")
					return _includedBelegDatas;

				_includedBelegDatas = Table.LoadThenFind_Between(BonNummerVon.Value, BonNummerBis.Value);
				_includedBelegDatasTag = $"{BonNummerVon.Value}.{BonNummerBis.Value}";
				return _includedBelegDatas;
			}
		}

		/// <summary>Recalculates the <see cref="BetragBrutto" /> field.</summary>
		public bool Recalculate_BetragBrutto()
		{
			if (Postens == null)
			{
				var b1 = 0 != BetragBrutto;
				BetragBrutto = 0;
				return b1;
			}
			var betragBrutto = Postens.Sum(x => x.BetragBrutto);
			var b = betragBrutto != BetragBrutto;
			BetragBrutto = betragBrutto;
			return b;
		}

		/// <summary>Recalculates the <see cref="BetragNetto" /> field.</summary>
		public bool Recalculate_BetragNetto()
		{
			if (Postens == null)
			{
				var b1 = 0 != BetragNetto;
				BetragNetto = 0;
				return b1;
			}
			var betragNetto = Postens.Sum(x => x.BetragNetto);
			var b = betragNetto != BetragNetto;
			BetragNetto = betragNetto;
			return b;
		}

		/// <summary>Recalculates the <see cref="MailCount" /> field.</summary>
		public void Recalculate_MailCount()
		{
			MailCount = MailedBelege.Count;
		}

		/// <summary>Recalculates the <see cref="PrintCount" /> field.</summary>
		public void Recalculate_PrintCount()
		{
			PrintCount = PrintedBelege.Count;
		}
	}
}