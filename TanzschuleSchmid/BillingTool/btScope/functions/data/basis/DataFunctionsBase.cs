// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions.data.basis
{

	/// <summary>Used for ensuring.</summary>
	public abstract class DataFunctionsBase : Base
	{
		/// <summary>Try to finalize each unfinalized item. If the item can not be validated it will be deleted.</summary>
		public abstract void Finalize_Or_Reject_All();
		/// <summary>Removes all unfinalized rows.</summary>
		public abstract void Delete_All();
		/// <summary>If there are any not finalized rows this property will return true</summary>
		public abstract bool HasNonFinalizedRows { get; }
	}

	/// <summary>Used for ensuring.</summary>
	public abstract class DataFunctionsBase<TRowType> : DataFunctionsBase where TRowType: CsDbRowBase 
	{
		#region Abstract
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected abstract void FinalizeAction(TRowType item);

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected abstract void ValidationAction(TRowType item);
		#endregion


		/// <summary>If there are any not finalized rows this property will return true</summary>
		public override bool HasNonFinalizedRows => NonFinalizedRows.Count != 0;

		private HashSet<TRowType> NonFinalizedRows { get; } = new HashSet<TRowType>();

		/// <summary>Finalizes the <paramref name="item" />.</summary>
		public void Finalize(TRowType item)
		{
			if (!NonFinalizedRows.Contains(item))
				throw new ArgumentException($"The item has never been created through the {nameof(DataFunctions)} scope. Invalid programming behavior.");

			InternalFinalize(item);
		}

		/// <summary>Finalizes the <paramref name="item" /> if it is not finalized.</summary>
		public void TryFinalize(TRowType item)
		{
			if (!NonFinalizedRows.Contains(item)) return;

			InternalFinalize(item);
		}

		/// <summary>Try to finalize each unfinalized item. If the item can not be validated it will be deleted.</summary>
		public override void Finalize_Or_Reject_All()
		{
			foreach (var row in NonFinalizedRows.ToArray())
			{
				try
				{
					InternalFinalize(row);
				}
				catch (Exception)
				{
					row.Delete();
					NonFinalized_Remove(row);
				}
			}
		}
		/// <summary>Removes all unfinalized rows.</summary>
		public override void Delete_All()
		{
			foreach (var row in NonFinalizedRows.ToArray())
			{
				if (row.RowState != DataRowState.Deleted)
					row.Delete();
				NonFinalizedRows.Remove(row);
			}
		}

		/// <summary>Adds an entry to the not finalized collection.</summary>
		protected void NonFinalized_Add(TRowType item)
		{
			if (NonFinalizedRows.Contains(item))
				throw new ArgumentException($"The item is already part of the {nameof(NonFinalizedRows)} collection.");
			NonFinalizedRows.Add(item);
		}

		/// <summary>Removes an entry from the not finalized collection.</summary>
		protected void NonFinalized_Remove(TRowType item)
		{
			if (!NonFinalizedRows.Contains(item))
				throw new ArgumentException($"The item has never been created through the {nameof(DataFunctions)} scope. Invalid programming behavior.");
			NonFinalizedRows.Remove(item);
		}

		/// <summary>Removes an entry from the not finalized collection.</summary>
		protected void NonFinalized_TryRemove(TRowType item)
		{
			if (NonFinalizedRows.Contains(item))
				NonFinalizedRows.Remove(item);
		}


		private void InternalFinalize(TRowType item)
		{
			ValidationAction(item);
			FinalizeAction(item);
			NonFinalized_Remove(item);
		}


		/// <summary>Used whenever there is already an instance which is currently not finalized.</summary>
		public class NotFinalizedInstanceException : Exception
		{
			/// <summary>ctor with default message.</summary>
			public NotFinalizedInstanceException() : base($"There is already a not finalized instance of type {typeof(TRowType).Name}, which have to be finalized before another instance can be created.")
			{

			}
		}
	}
}