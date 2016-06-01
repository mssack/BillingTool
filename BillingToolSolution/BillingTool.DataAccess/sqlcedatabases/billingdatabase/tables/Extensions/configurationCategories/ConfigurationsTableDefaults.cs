// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-26</date>

using System;
using System.Runtime.CompilerServices;
using BillingToolDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Objects;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories
{
	/// <summary>Used for categorization.</summary>
	public sealed class ConfigurationsTableDefaults : Base
	{
		private ConfigurationsTable _owner;

		internal ConfigurationsTableDefaults(ConfigurationsTable owner)
		{
			Owner = owner;
		}


		/// <summary>The <see cref="OutputFormat" /> which should be used as default for printing.</summary>
		public Guid PrintFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_PrintFormat));
			}
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for mailing.</summary>
		public Guid MailFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_MailFormat));
			}
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for mailing.</summary>
		public Guid StornoFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_StornoFormat));
			}
		}

		/// <summary>The <see cref="OutputFormat" /> which should be used as default for the Tagesbon.</summary>
		public Guid TagesBonFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_TagesBonFormat));
			}
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for the Monatsbon.</summary>
		public Guid MonatsBonFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_MonatsBonFormat));
			}
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for the Jahresbon.</summary>
		public Guid JahresBonFormatId
		{
			get { return GetValue(Guid.NewGuid()); }
			set
			{
				SetValue(value);
				Owner.DataSet.OutputFormats.RaisePropertyChanged(nameof(OutputFormatsTable.Default_JahresBonFormat));
			}
		}


		/// <summary>The default Betreff which should be used as default for mailing.</summary>
		public string MailBetreff
		{
			get { return GetValue("Rechnungsbeleg"); }
			set { SetValue(value); }
		}

		/// <summary>The default text which should be used as default for mailing.</summary>
		public string MailText
		{
			get { return GetValue("Sie finden Ihre Rechnung im Anhang."); }
			set { SetValue(value); }
		}


		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Normal.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid BetragSatzNormal
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Ermäßigt-1.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid BetragSatzErmäßigt1
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Ermäßigt-2.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid BetragSatzErmäßigt2
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid BetragSatzNull
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid BetragSatzBesonders
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}



		/// <summary>Gets or sets the Owner.</summary>
		private ConfigurationsTable Owner
		{
			get { return _owner; }
			set { SetProperty(ref _owner, value); }
		}


		internal T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			return Owner.GetValue(defaultValue, $"DEFAULT_{name}");
		}

		internal void SetValue(object value, [CallerMemberName] string name = null)
		{
			Owner.SetValue(value, $"DEFAULT_{name}");
			OnPropertyChanged(name);
		}
	}



}