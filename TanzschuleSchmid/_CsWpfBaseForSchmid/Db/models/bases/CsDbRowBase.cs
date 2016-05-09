// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Db.models.bases
{
	/// <summary>The base class for all rows used by the db engine.</summary>
	[Serializable]
	public abstract class CsDbRowBase : DataRow, INotifyPropertyChanged, IContainDbProxy
	{
		[field: NonSerialized] private readonly Dictionary<string, string[]> _propertyDependencys;
		[field: NonSerialized] private ProcessLock _changeLock;
		[field: NonSerialized] private PropertyChangedEventHandler _internalPropertyChanged;
		[field: NonSerialized] private Dictionary<string, string> _nativeNameMapping;
		[field: NonSerialized] private PropertyChangedEventHandler _propertyChanged;

		/// <summary>ctor</summary>
		protected CsDbRowBase(DataRowBuilder builder) : base(builder)
		{
			_propertyDependencys = ReflectionHelper.GetPropertyDependencys(GetType());
		}


		#region Abstract
		/// <summary>Applys the database default values to the row.</summary>
		public abstract void ApplyDefaults();


		/// <summary>Applys the database extended default values, described by developer, to the row.</summary>
		public virtual void ApplyExtendedDefaults()
		{

		}


		/// <summary>occurs whenever a cell changes or a property inside this class.</summary>
		public virtual event PropertyChangedEventHandler PropertyChanged
		{
			add { _propertyChanged = (PropertyChangedEventHandler) Delegate.Combine(_propertyChanged, value); }
			remove { _propertyChanged = (PropertyChangedEventHandler) Delegate.Remove(_propertyChanged, value); }
		}

		/// <summary>The db proxy all commands should be routed through this.</summary>
		public virtual IDbProxy DbProxy
		{
			get
			{
				var dataSetProxy = (Table?.DataSet as IContainDbProxy)?.DbProxy;
				if (dataSetProxy == null)
					throw new Exception("No db proxy found on DataSet. Implement IContainDbProxy on DataSet Level and include this table in the data set.");
				return dataSetProxy;
			}
		}
		/// <summary>occurs whenever a cell changes or a property inside this class.</summary>
		internal virtual event PropertyChangedEventHandler InternalPropertyChanged
		{
			add { _internalPropertyChanged = (PropertyChangedEventHandler) Delegate.Combine(_internalPropertyChanged, value); }
			remove { _internalPropertyChanged = (PropertyChangedEventHandler) Delegate.Remove(_internalPropertyChanged, value); }
		}


		/// <summary>Get the value of a column by the native column name</summary>
		protected virtual T GetDbValue<T>(string nativeColumnName, [CallerMemberName] string propName = "")
		{
			var o = this[nativeColumnName];

			if (o == DBNull.Value)
				return default(T);

			return (T) o;
		}

		/// <summary>Sets the property and calls on property changed if anything have changed.</summary>
		protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = "")
		{
			if (Equals(field, value))
				return false;
			field = value;
			OnPropertyChanged(propName);
			return true;
		}

		/// <summary>
		///     Invokes the property changed event for a value. IMPORTENT: Also sends property changed for all depending properties. Use
		///     <see cref="DependsOnAttribute" /> to specify properties that depends on other properties.
		/// </summary>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (propertyName == null)
				return;
			if (_internalPropertyChanged == null && _propertyChanged == null)
				return;

			string[] dependingPropertys;
			_propertyDependencys.TryGetValue(propertyName, out dependingPropertys);


			if (_propertyChanged != null)
			{
				_propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
				dependingPropertys?.ForEach(OnPropertyChanged);
			}
			if (_internalPropertyChanged != null)
			{
				_internalPropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
				dependingPropertys?.ForEach(OnPropertyChanged);
			}
		}

		/// <summary>Invokes the property changed event for a property.</summary>
		public virtual void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(propertyName);
		}

		/// <summary>Used to notify about change if user changes the data base field directly.</summary>
		internal virtual void OnColumnChanged(string columnName)
		{
			if (ChangeLock.Active)
				return;
			string propertyName;
			OnPropertyChanged(NativeNameMapping.TryGetValue(columnName, out propertyName) ? propertyName : columnName);
		}

		/// <summary>Used to notify about change if user changes the data base field directly.</summary>
		internal virtual void OnRowChanged()
		{
			if (ChangeLock.Active)
				return;
			if (IsPropertyChangedHandled == false)
				return;
			foreach (var dbProp in NativeNameMapping.Values)
			{
				OnPropertyChanged(dbProp);
			}
		}
		#endregion


		/// <summary>Gets the owning table.</summary>
		public new CsDbTable Table => (CsDbTable) base.Table;
		/// <summary>Gets the owning DataSet.</summary>
		public CsDbDataSet DataSet => Table.DataSet;
		/// <summary>Gets the owning Context.</summary>
		public CsDbDataContext DataContext => Table.DataContext;

		/// <summary>Get a value which defines if the property changed event is currently used</summary>
		public bool IsPropertyChangedHandled => _propertyChanged != null;

		/// <summary>Used to prevent duplicated <see cref="OnPropertyChanged" /> invocation. While Outsie cannot invoke property changed.</summary>
		protected internal ProcessLock ChangeLock => _changeLock ?? (_changeLock = new ProcessLock());


		private Dictionary<string, string> NativeNameMapping => _nativeNameMapping ?? (_nativeNameMapping = Reflection.GetNativeNameMapping(GetType()));

		/// <summary>Gets the relations for this row type.</summary>
		public CsDbRelation[] GetRelations()
		{
			CsDbRelation[] rv;
			if (Table.DataSet.CsDbRelationsPerTableType.TryGetValue(Table.GetType(), out rv))
				return rv;
			return new CsDbRelation[0];
		}



		private static class Reflection
		{
			private static readonly Dictionary<Type, Dictionary<string, string>> ColumnToPropertyNameCache = new Dictionary<Type, Dictionary<string, string>>();

			public static Dictionary<string, string> GetNativeNameMapping(Type t)
			{
				Dictionary<string, string> mapping;
				if (ColumnToPropertyNameCache.TryGetValue(t, out mapping))
					return mapping;
				mapping = CreateMappingTable(t);
				ColumnToPropertyNameCache.Add(t, mapping);
				return mapping;
			}


			private static Dictionary<string, string> CreateMappingTable(Type t)
			{
				var mapping = new Dictionary<string, string>();
				var properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var property in properties)
				{
					var attributes = property.GetCustomAttributes(typeof(CsDbNativeDataColumnAttribute), false);
					if (attributes.Length != 1)
						continue;

					var attr = (CsDbNativeDataColumnAttribute) attributes[0];

					mapping.Add(attr.Name, property.Name);
				}
				return mapping;
			}
		}
	}
}