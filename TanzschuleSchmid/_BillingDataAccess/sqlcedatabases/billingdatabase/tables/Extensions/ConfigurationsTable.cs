// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-15</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using CsWpfBase.Ev.Public.Extensions;






// ReSharper disable InconsistentNaming

namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables
{
	partial class ConfigurationsTable
	{

		private BitmapSource _headerLogo;
		/// <summary>
		///     The last applied <see cref="BelegData.Nummer" />. Increment this number by one each time a new <see cref="BelegData" /> is added to the
		///     database.
		/// </summary>
		public int LastBelegNummer
		{
			get { return GetValue(0); }
			set { SetValue(value); }
		}

		/// <summary>Each time a new <see cref="BelegData" /> is added the <see cref="BelegData.BetragBrutto" /> has to be added to the
		///     <see cref="Umsatzzähler" />.</summary>
		public decimal Umsatzzähler
		{
			get { return GetValue(0); }
			set { SetValue(value); }
		}

		/// <summary>The last used <see cref="Steuersatz.Kürzel" />. Each time a new <see cref="Steuersatz" /> is added increment this value by one.</summary>
		public char LastSteuersatzKürzel
		{
			get { return GetValue((char) ('A' - 1)); }
			set { SetValue(value); }
		}

		/// <summary>The <see cref="OutputFormat" /> which should be used as default for printing.</summary>
		public Guid Default_PrintOutputFormat
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for mailing.</summary>
		public Guid Default_MailOutputFormat
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>The <see cref="OutputFormat" /> which should be used as default for mailing.</summary>
		public Guid Default_StornoOutputFormat
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}

		/// <summary>The default Betreff which should be used as default for mailing.</summary>
		public string Default_MailBetreff
		{
			get { return GetValue("Rechnungsbeleg"); }
			set { SetValue(value); }
		}

		/// <summary>The default text which should be used as default for mailing.</summary>
		public string Default_MailText
		{
			get { return GetValue("Sie finden Ihre Rechnung im Anhang."); }
			set { SetValue(value); }
		}


		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Normal.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid Default_BetragSatzNormal
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Ermäßigt-1.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid Default_BetragSatzErmäßigt1
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Ermäßigt-2.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid Default_BetragSatzErmäßigt2
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid Default_BetragSatzNull
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}
		/// <summary>
		///     The default <see cref="Steuersatz" /> which should be used as Betrag-Satz-Null.
		///     <para>siehe Literatur UStG 1994, Fassung vom 15.05.2016, §10 Steuersätze</para>
		///     <para>siehe Literatur "Detailspezifikationen-RKS_e_recht_20150820.pdf"</para>
		/// </summary>
		public Guid Default_BetragSatzBesonders
		{
			get { return GetValue(Guid.NewGuid()); }
			set { SetValue(value); }
		}



		/// <summary>The application logo for this database instance.</summary>
		public BitmapSource HeaderLogo
		{
			get
			{
				if (_headerLogo != null)
					return _headerLogo;
				_headerLogo = GetValue<string>().ConvertTo_Bytes().ConvertTo_Image();
				_headerLogo.Freeze();
				return _headerLogo;
			}
			set
			{
				_headerLogo = value.ResizeToMaximum(100, 100);
				_headerLogo.Freeze();
				SetValue(_headerLogo.ConvertTo_PngByteArray().ConvertTo_Base64());

			}
		}

		/// <summary>The application logo for this database instance.</summary>
		public double HeaderSize
		{
			get { return GetValue(30); }
			set { SetValue(value); }
		}

		private T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			if (DbProxy == null)
				return default(T);


			var config = GetRow(name, defaultValue == null ? null : defaultValue.ToString());

			if (string.IsNullOrEmpty(config.Value))
			{
				config.Value = defaultValue == null ? null : defaultValue.ToString();
				return defaultValue;
			}

			if (typeof(T) == typeof(Guid))
				return (T) (object) Guid.Parse(config.Value);

			return (T) Convert.ChangeType(config.Value, typeof(T));
		}

		private void SetValue(object value, [CallerMemberName] string name = null)
		{
			var config = GetRow(name, value?.ToString());
			config.LastChanged = DateTime.Now;
			config.Value = value?.ToString();
			OnPropertyChanged(name);
		}

		private Configuration GetRow(string name, string defaultVal)
		{
			if (!HasBeenLoaded)
				DownloadRows();

			var config = Find(name);
			if (config != null)
				return config;


			config = NewRow();
			config.Name = name;
			config.Value = defaultVal;
			config.LastChanged = DateTime.Now;
			Add(config);

			return config;
		}
	}
}