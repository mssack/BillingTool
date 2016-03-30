// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-06</date>

using System;
using CsWpfBase.Db.models;
using CsWpfBase.Db.models.helper;






namespace CsWpfBase.Db.tools.rowbundlecreators
{
	/// <summary>Used to create data bundles, usually beginning with one row witch is connected through a network of relations.</summary>
	public class RowBundleCreator
	{
		/// <summary>Creates a new bundle creator instance, can be used to copy the whole network around a row into another dataset.</summary>
		public RowBundleCreator(CsDbTableRow row, CsDbDataSet target)
		{
			StartingRow = row;
			Target = target;
		}

		private CsDbTableRow StartingRow { get; }
		private CsDbDataSet Target { get; }

		/// <summary>Starts the transfer.</summary>
		public void StartTransfer()
		{
			RecTransfer(StartingRow, null);
		}

		private void RecTransfer(CsDbTableRow sourceRow, CsDbRelation sourceRelation)
		{
			if (sourceRow == null)
				return;

			var sourceTable = sourceRow.Table;
			var targetTable = Target.GetTableByName(sourceTable.TableName);

			if (targetTable == sourceTable)
			{
				RecGoOverRelations(sourceRow, sourceRelation);
				return;
			}




			targetTable.LoadSchema();
			if (targetTable.Generic_Find(sourceRow.PkValue) != null) return; // Skip if row is already in targetTable.
			targetTable.Rows.Add(sourceRow.ItemArray);
			RecGoOverRelations(sourceRow, sourceRelation);


		}

		private void RecGoOverRelations(CsDbTableRow sourceRow, CsDbRelation sourceRelation)
		{
			var relations = sourceRow.GetRelations();
			foreach (var relation in relations)
			{
				if (relation == sourceRelation)
					continue;


				if (relation.PkTableName.Equals(sourceRow.Table.TableName))
				{
					foreach (CsDbTableRow fkRow in relation.GetFkRowsFromPkRow(sourceRow))
					{
						RecTransfer(fkRow, relation);
					}
				}
				else if (relation.FkTableName.Equals(sourceRow.Table.TableName))
				{
					RecTransfer(relation.GetPkRowFromFkRow(sourceRow), relation);
				}
				else throw new InvalidOperationException("This is not an expected state");
			}
		}
	}
}