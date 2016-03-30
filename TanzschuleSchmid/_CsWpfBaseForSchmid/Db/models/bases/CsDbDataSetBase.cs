﻿// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-05</date>

using System;
using System.Collections.Generic;
using System.Data;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Db.models.internalhelper;






namespace CsWpfBase.Db.models.bases
{
	/// <summary>The base class for all data sets generated by the cs db engine</summary>
	[Serializable]
	public abstract class CsDbDataSetBase : DataSet, IContainDbProxy
	{
		[field: NonSerialized] private IDbProxy _dbProxy;


		#region Abstract
		/// <summary>Gets all table names owned by this data set.</summary>
		public abstract string[] TableNames { get; }

		/// <summary>Returns an array which contains all relations for this data set. You can also use the static property ReflectedRelations from the class.</summary>
		public abstract CsDbRelation[] CsDbRelations { get; }

		/// <summary>Returns an array which contains all relations for this data set. You can also use the static property ReflectedRelations from the class.</summary>
		public abstract Dictionary<Type, CsDbRelation[]> CsDbRelationsPerTableType { get; }

		/// <summary>First it loads the schema and after that it enforces the constraint's.</summary>
		public abstract void LoadConstraints();


		/// <summary>Saves the tables in an order which is good for creating new items.</summary>
		public abstract void SaveAnabolic(object tag = null);

		/// <summary>Saves the tables in an order which is good for deleting items.</summary>
		public abstract void SaveKatabolic(object tag = null);

		/// <summary>Used as a database template for the schema.</summary>
		public abstract DataSet SchemaSet { get; }

		/// <summary>Get the right table by its table name</summary>
		public abstract CsDbTableBase GetTableByName(string nativeName);

		/// <summary>Gets the name of the owning context. this is typically the db server name.</summary>
		public abstract string DataContextName { get; }
		#endregion


		#region Interfaces
		/// <summary>Tables inside can use this to execute commands.</summary>
		public IDbProxy DbProxy
		{
			get { return _dbProxy; }
			protected set
			{
#if DEBUG && DbTrace
				_dbProxy = new CsDbTraceProxy(value);
#else
				_dbProxy = value;
#endif
			}
		}
		#endregion


		/// <summary>Gets a copy of the dataset that contains all changes made to it since it was loaded or since <see cref="DataSet.AcceptChanges" />.</summary>
		public new CsDbDataSet GetChanges()
		{
			return (CsDbDataSet) base.GetChanges();
		}


		/// <summary>Universal save method can be used to save the data to the proxy without caring about anabolic order or katabolic order.</summary>
		public void SaveUnspecific(object tag = null)
		{
			var changes = GetChanges();
			if (changes != null)
				DbProxy.SaveChanges(changes.CloneTo_Native(), tag);
		}
	}
}