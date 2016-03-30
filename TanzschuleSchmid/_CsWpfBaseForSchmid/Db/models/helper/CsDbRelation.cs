// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.models.helper
{
	/// <summary>Storage class for db relations. Use this object whenever you need some dynamic database operations @ runtime.</summary>
	public class CsDbRelation : Base
	{
		/// <summary>ctor</summary>
		public CsDbRelation(Type pkTableType, Type pkRowType, Type fkTableType, Type fkRowType, string pkTableName, string pkColumnName, string fkTableName, string fkColumnName, PropertyInfo pkColumnProperty, PropertyInfo fkColumnProperty, PropertyInfo pkReferenceProperty, PropertyInfo fkReferenceProperty)
		{
			PkTableType = pkTableType;
			PkRowType = pkRowType;
			FkTableType = fkTableType;
			FkRowType = fkRowType;
			PkTableName = pkTableName;
			PkColumnName = pkColumnName;
			FkTableName = fkTableName;
			FkColumnName = fkColumnName;
			PkColumnProperty = pkColumnProperty;
			FkColumnProperty = fkColumnProperty;
			PkReferenceProperty = pkReferenceProperty;
			FkReferenceProperty = fkReferenceProperty;
		}



		/// <summary>Gets the table type of the primary key table.</summary>
		public Type PkTableType { get; }
		/// <summary>Gets the row type of the primary key table.</summary>
		public Type PkRowType { get; }
		/// <summary>Gets the table type of the foreign key table.</summary>
		public Type FkTableType { get; }
		/// <summary>Gets the row type of the foreign key table.</summary>
		public Type FkRowType { get; }




		/// <summary>Gets the name of the primary key table.</summary>
		public string PkTableName { get; }
		/// <summary>Gets the name of the primary key column.</summary>
		public string PkColumnName { get; }
		/// <summary>Gets the name of the foreign key table.</summary>
		public string FkTableName { get; }
		/// <summary>Gets the name of the foreign key column.</summary>
		public string FkColumnName { get; }

		/// <summary>The reflected property info of the primary key column.</summary>
		public PropertyInfo PkColumnProperty { get; }
		/// <summary>The reflected property info for the foreign key column.</summary>
		public PropertyInfo FkColumnProperty { get; }


		/// <summary>The reflected property for the reference towards the primary key row.</summary>
		public PropertyInfo PkReferenceProperty { get; }
		/// <summary>The reflected property for the reference towards the foreign key rows.</summary>
		public PropertyInfo FkReferenceProperty { get; }


		/// <summary>Get the value of the pk column on a specific row. Returns null if <paramref name="row" /> is null.</summary>
		public object GetPkValueFromPkRow(object row)
		{
			if (row == null)
				return null;
			if (row.GetType() != PkRowType)
				throw new InvalidOperationException($"To get the value from column ([{PkTableName}].[{PkColumnName}]) you have to provide an object of type<{PkRowType.Name}>. You have provided row of type<{row.GetType().Name}>");
			return (DataRow) PkColumnProperty.GetValue(row, null);
		}

		/// <summary>Get the value of the fk column on a specific row. Returns null if <paramref name="row" /> is null.</summary>
		public object GetFkValueFromFkRow(object row)
		{
			if (row == null)
				return null;
			if (row.GetType() != FkRowType)
				throw new InvalidOperationException($"To get the value from column ([{FkTableName}].[{FkColumnName}]) you have to provide an object of type<{FkRowType.Name}>. You have provided row of type<{row.GetType().Name}>");
			return FkColumnProperty.GetValue(row, null);
		}


		/// <summary>Get the value of the pk column on a specific row. Returns null if <paramref name="row" /> is null.</summary>
		public CsDbTableRow GetPkRowFromFkRow(object row)
		{
			if (row == null)
				return null;
			if (row.GetType() != FkRowType)
				throw new InvalidOperationException($"To get the referenced row ([{PkTableName}].[{PkColumnName}]) you have to provide an object of type<{FkRowType.Name}>. You have provided row of type<{row.GetType().Name}>");

			return (CsDbTableRow) PkReferenceProperty.GetValue(row, null);
		}

		/// <summary>Get the value of the fk column on a specific row. Returns null if <paramref name="row" /> is null.</summary>
		public IEnumerable<object> GetFkRowsFromPkRow(object row)
		{
			if (row == null)
				return null;
			if (row.GetType() != PkRowType)
				throw new InvalidOperationException($"To get the referenced rows ([{FkTableName}].[{FkColumnName}]) you have to provide an object of type<{PkRowType.Name}>. You have provided row of type<{row.GetType().Name}>");

			return (IEnumerable<object>) FkReferenceProperty.GetValue(row, null);
		}
	}
}