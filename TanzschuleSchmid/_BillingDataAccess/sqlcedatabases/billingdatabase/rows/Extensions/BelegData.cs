// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-02</date>

using System;
using System.Linq;
using System.Windows.Markup;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows

{
	partial class BelegData
	{
		#region Overrides/Interfaces
		/// <summary>On row creation this method will be executed. So a new row will always have the highest reference number</summary>
		public override void ApplyExtendedDefaults()
		{
			Nummer = DataSet.Configurations.LastBelegNummer+1;
		}

		/// <summary>Returns an identifier for the database row.</summary>
		public override string ToString()
		{
			return $"[BelegData, Nummer={Nummer}, Date={Datum}]";
		}
		#endregion


		/// <summary>returns true if all needed informations are present in this row.</summary>
		[DependsOn(nameof(TypName))]
		[DependsOn(nameof(KassenOperator))]
		[DependsOn(nameof(StornoBelegId))]
		public bool IsValid => Typ != BelegDataTypes.Unknown && !string.IsNullOrEmpty(KassenOperator) && (Typ != BelegDataTypes.Storno || (Typ == BelegDataTypes.Storno && StornoBeleg != null));



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