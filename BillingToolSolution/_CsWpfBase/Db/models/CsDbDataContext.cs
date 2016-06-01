// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.models
{
	/// <summary>The base class for database context.</summary>
	[Serializable]
	public abstract class CsDbDataContext : CsDbContextBase
	{
		private readonly ObservableCollection<CsDbDataSet> _sets = new ObservableCollection<CsDbDataSet>();
		private ReadOnlyObservableCollection<CsDbDataSet> _readOnlySets;


		/// <summary>A list of all available data sets.</summary>
		public ReadOnlyObservableCollection<CsDbDataSet> Sets => _readOnlySets ?? (_readOnlySets = new ReadOnlyObservableCollection<CsDbDataSet>(_sets));

		/// <summary>Serializes this context into a binary.</summary>
		public byte[] SaveTo_Bytes()
		{
			DataSet[] sets = Sets.Select(x=>x.CloneTo_Native()).ToArray();
			return sets.ConvertTo_Bytes();
		}

		/// <summary>Deserializes this context from binary.</summary>
		public void LoadFrom_Bytes(byte[] data)
		{
			DataSet[] sets = data.ConvertTo_Object<DataSet[]>();
			foreach (var dataSet in sets)
			{
				GetDatabaseByName(dataSet.DataSetName).LoadFrom_Native(dataSet);
			}
		}

		/// <summary>Get the associated data set.</summary>
		protected T GetSet<T>(ref T field) where T : CsDbDataSet
		{
			if (field != null)
				return field;

			field = Activator.CreateInstance<T>();
			field.DataContext = this;
			_sets.Add(field);
			return field;
		}
	}
}