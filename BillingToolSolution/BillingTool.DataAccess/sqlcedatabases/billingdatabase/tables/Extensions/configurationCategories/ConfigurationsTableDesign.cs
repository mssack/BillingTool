// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-18</date>

using System;
using System.Runtime.CompilerServices;
using CsWpfBase.Ev.Objects;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase.tables.configurationCategories
{
	/// <summary>Collapses all design configurations.</summary>
	public sealed class ConfigurationsTableDesign : Base
	{
		private ConfigurationsTable _owner;

		internal ConfigurationsTableDesign(ConfigurationsTable owner)
		{
			_owner = owner;
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