// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows

{
	partial class BelegData : IStoreComment
	{
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
			Nummer = DataSet.Configurations.LastBelegNummer + 1;
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
		[DependsOn(nameof(StateName))]
		[DependsOn(nameof(TypName))]
		public bool CanBeStorniert => Typ != BelegDataTypes.Storno && !IsStorniert;

		/// <summary>returns true if the <see cref="BelegData" /> has been storniert.</summary>
		[DependsOn(nameof(StateName))]
		public bool IsStorniert => State == BelegDataStates.Storniert;


		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(TypName))]
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
				if (Postens.Count == 0)
					return BelegDataInvalidReasons.Missing_BelegPosten;
				return BelegDataInvalidReasons.Valid;
			}
		}


		/// <summary>The wrapper property for column property <see cref="TypName" />.</summary>
		[DependsOn(nameof(TypName))]
		public BelegDataTypes Typ
		{
			get
			{
				BelegDataTypes val;
				if (Enum.TryParse(TypName, true, out val))
					return val;
				return BelegDataTypes.Unknown;
			}
			set { TypName = value.ToString(); }
		}

		/// <summary>The wrapper property for column property <see cref="StateName" />.</summary>
		[DependsOn(nameof(StateName))]
		public BelegDataStates State
		{
			get
			{
				BelegDataStates val;
				if (Enum.TryParse(StateName, true, out val))
					return val;
				return BelegDataStates.Unknown;
			}
			set { StateName = value.ToString(); }
		}

		/// <summary>Gets the <see cref="BelegData" /> which contains information about the reason why this <see cref="BelegData" /> had been storniert.</summary>
		[DependsOn(nameof(StateName))]
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

		/// <summary>Recalculates the <see cref="BetragBrutto" /> field.</summary>
		public void Recalculate_BetragBrutto()
		{
			BetragBrutto = Postens.Sum(x => x.BetragBrutto);
		}

		/// <summary>Recalculates the <see cref="BetragNetto" /> field.</summary>
		public void Recalculate_BetragNetto()
		{
			BetragNetto = Postens.Sum(x => x.BetragNetto);
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