// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-06</date>

using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.tools.rowbundlecreators;






namespace CsWpfBase.Db.models
{
	/// <summary>The base for each row inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbTableRow : CsDbRowBase
	{
		/// <summary>ctor</summary>
		protected CsDbTableRow(DataRowBuilder builder) : base(builder)
		{
		}

		/// <summary>Gets the value from the primary key column on this row.</summary>
		public object PkValue => this[Table.PrimaryKey[0]];


		/// <summary>sets the value of a column and notify property changed.</summary>
		[DebuggerStepThrough]
		public bool SetDbValue<T>(T m, string columnName, [CallerMemberName] string propName = "")
		{
			if (this[columnName].Equals(m))
				return false;


			using (ChangeLock.Activate())
			{
				if (m == null)
					this[columnName] = DBNull.Value;
				else
					this[columnName] = m;
				OnPropertyChanged(propName);
				return true;
			}
		}

		/// <summary>
		///     Loads the complete data bundle of the current row into a target data set.
		///     <para>A data bundle is defined as a set of all rows inside a database which are connected via relations.</para>
		///     <para>The currently selected row is the root of the bundle</para>
		/// </summary>
		public void Copy_BundledData_Into_DataSet(CsDbDataSet target)
		{
			var bundleCreator = new RowBundleCreator(this, target);
			bundleCreator.StartTransfer();
		}

		/// <summary>
		///     Loads the complete data bundle of the current row into a new data set.
		///     <para>A data bundle is defined as a set of rows inside a database which are connected via relations.</para>
		///     <para>The currently selected row is the root of the bundle</para>
		/// </summary>
		public CsDbDataSet Copy_BundledData_In_New_DataSet()
		{
			var target = (CsDbDataSet) Activator.CreateInstance(DataSet.GetType());
			var bundleCreator = new RowBundleCreator(this, target);
			bundleCreator.StartTransfer();
			return target;
		}

		/// <summary>
		///     Loads the complete data bundle of the current row into the actual data set.
		///     <para>A data bundle is defined as a set of rows inside a database which are connected via relations.</para>
		///     <para>The currently selected row is the root of the bundle</para>
		/// </summary>
		public void Download_BundledData()
		{
			var bundleCreator = new RowBundleCreator(this, DataSet);
			bundleCreator.StartTransfer();
		}
	}
}