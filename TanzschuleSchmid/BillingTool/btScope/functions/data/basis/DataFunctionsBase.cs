// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-09</date>

using System;
using System.Collections.Generic;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.functions.data.basis
{
	/// <summary>Used for ensuring.</summary>
	public abstract class DataFunctionsBase<TRowType> : Base
	{
		#region Abstract
		/// <summary>The action occurs when an <paramref name="item" /> needs finalization after checking if the item is valid. Should be recursive</summary>
		protected abstract void FinalizeAction(TRowType item);

		/// <summary>The action occurs before the item gets finalized. This action should throw exception on invalid States.</summary>
		protected abstract void ValidationAction(TRowType item);
		#endregion


		/// <summary>If there are any not finalized rows this property will return true</summary>
		public bool HasUnfinalizedRows => NotfinalizedRows.Count != 0;

		private HashSet<TRowType> NotfinalizedRows { get; } = new HashSet<TRowType>();

		/// <summary>Finalizes the <paramref name="item" />.</summary>
		public void Finalize(TRowType item)
		{
			if (!NotfinalizedRows.Contains(item))
				throw new ArgumentException($"The item has never been created through the {nameof(DataFunctions)} scope. Invalid programming behavior.");

			ValidationAction(item);
			FinalizeAction(item);

			Notfinalized_Remove(item);
		}

		/// <summary>Finalizes the <paramref name="item" /> if it is not finalized.</summary>
		public void TryFinalize(TRowType item)
		{
			if (NotfinalizedRows.Contains(item))
				Finalize(item);
		}

		/// <summary>Adds an entry to the not finalized collection.</summary>
		protected void Notfinalized_Add(TRowType item)
		{
			if (NotfinalizedRows.Contains(item))
				throw new ArgumentException($"The item is already part of the {nameof(NotfinalizedRows)} collection.");
			NotfinalizedRows.Add(item);
		}

		/// <summary>Removes an entry from the not finalized collection.</summary>
		protected void Notfinalized_Remove(TRowType item)
		{
			if (!NotfinalizedRows.Contains(item))
				throw new ArgumentException($"The item has never been created through the {nameof(DataFunctions)} scope. Invalid programming behavior.");
			NotfinalizedRows.Remove(item);
		}
		/// <summary>Removes an entry from the not finalized collection.</summary>
		protected void Notfinalized_TryRemove(TRowType item)
		{
			if (NotfinalizedRows.Contains(item))
				NotfinalizedRows.Remove(item);
		}



		/// <summary>Used whenever there is already an instance which is currently not finalized.</summary>
		public class NotfinalizedInstanceException : Exception
		{
			/// <summary>ctor with default message.</summary>
			public NotfinalizedInstanceException() : base($"There is already a not finalized instance of type {typeof(TRowType).Name}, which have to be finalized before another instance can be created.")
			{

			}
		}
	}
}