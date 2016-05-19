// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-19</date>

using System;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories
{
	/// <summary>Used for categorization.</summary>
	public sealed class ConfigurationsTableBusiness : Base
	{

		private BitmapSource _logo;
		private ConfigurationsTable _owner;

		internal ConfigurationsTableBusiness(ConfigurationsTable owner)
		{
			Owner = owner;
		}

		/// <summary>The business logo.</summary>
		public BitmapSource Logo
		{
			get
			{
				if (_logo != null)
					return _logo;
				_logo = GetValue<string>().ConvertTo_Bytes().ConvertTo_Image();
				_logo?.Freeze();
				return _logo;
			}
			set
			{
				_logo = value.ResizeToMaximum(100, 100);
				_logo?.Freeze();
				SetValue(_logo.ConvertTo_PngByteArray().ConvertTo_Base64());
			}
		}

		/// <summary>The name of the business.</summary>
		public string Name
		{
			get { return GetValue(""); }
			set { SetValue(value); }
		}

		/// <summary>The address of the business.</summary>
		public string Anschrift
		{
			get { return GetValue(""); }
			set { SetValue(value); }
		}

		/// <summary>The mail address of the business.</summary>
		public string Mail
		{
			get { return GetValue(""); }
			set { SetValue(value); }
		}

		/// <summary>The mail address of the business.</summary>
		public string Telefon
		{
			get { return GetValue(""); }
			set { SetValue(value); }
		}


		/// <summary>Gets a value indicating if current configuration is valid.</summary>
		public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Anschrift) && !string.IsNullOrEmpty(Telefon);

		/// <summary>Gets or sets the Owner.</summary>
		private ConfigurationsTable Owner
		{
			get { return _owner; }
			set { SetProperty(ref _owner, value); }
		}


		internal T GetValue<T>(T defaultValue = default(T), [CallerMemberName] string name = null)
		{
			return Owner.GetValue(defaultValue, $"BUSINESS_{name}");
		}

		internal void SetValue(object value, [CallerMemberName] string name = null)
		{
			Owner.SetValue(value, $"BUSINESS_{name}");
			OnPropertyChanged(name);
			OnPropertyChanged(nameof(IsValid));
		}
	}
}