// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories
{
	/// <summary>Collapses all design configurations.</summary>
	public sealed class ConfigurationsTableDesign : Base
	{
		private BitmapSource _headerLogo;
		private ConfigurationsTable _owner;

		internal ConfigurationsTableDesign(ConfigurationsTable owner)
		{
			_owner = owner;
		}

		/// <summary>The application logo for this database instance.</summary>
		public BitmapSource HeaderLogo
		{
			get
			{
				if (_headerLogo != null)
					return _headerLogo;
				_headerLogo = GetValue<string>().ConvertTo_Bytes().ConvertTo_Image();
				_headerLogo?.Freeze();
				return _headerLogo;
			}
			set
			{
				_headerLogo = value.ResizeToMaximum(100, 100);
				_headerLogo?.Freeze();
				SetValue(_headerLogo.ConvertTo_PngByteArray().ConvertTo_Base64());
			}
		}

		/// <summary>The application logo for this database instance.</summary>
		public double HeaderSize
		{
			get { return GetValue<double>(30); }
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
			return Owner.GetValue(defaultValue, $"DESIGN_{name}");
		}

		internal void SetValue(object value, [CallerMemberName] string name = null)
		{
			Owner.SetValue(value, $"DESIGN_{name}");
			OnPropertyChanged(name);
		}
	}
}