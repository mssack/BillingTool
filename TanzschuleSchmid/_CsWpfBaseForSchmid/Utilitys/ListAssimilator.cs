// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;






namespace CsWpfBase.Utilitys
{
	/// <summary>Used to assimilate an existing list to mirror an model. Extend and withdraws items and order it like the model.</summary>
	public class ListAssimilator<TExisting, TAssimilate>
	{
		/// <summary>ctor</summary>
		/// <param name="existingList">the list to assimalte</param>
		/// <param name="toAssimilate">the target list layout</param>
		public ListAssimilator(IList<TExisting> existingList, IEnumerable<TAssimilate> toAssimilate)
		{
			Existings = existingList;
			ToAssimilate = toAssimilate;
		}

		/// <summary>This function is used to compare two objects.</summary>
		public Func<TExisting, TAssimilate, bool> EqualFunc { get; set; }
		/// <summary>Invokes when an item need to be added to the collection. Convert the item an as a example: Use this method to invoke an OnAdded event</summary>
		public Func<TAssimilate, TExisting> ConvertFunc { get; set; }
		/// <summary>Invokes when a pair is found by the EqualFunc. As a example: Use this method to update an existing item.</summary>
		public Action<TExisting, TAssimilate> OnPairFound { get; set; }
		/// <summary>Invokes when an existing item was removed. As a example: Use this method to invoke an OnRemoved event.</summary>
		public Action<TExisting> OnRemoved { get; set; }
		private IList<TExisting> Existings { get; set; }
		private IEnumerable<TAssimilate> ToAssimilate { get; set; }

		/// <summary>Assimilates the existingList</summary>
		public void Execute()
		{
			//if (typeof(T) == typeof(ValueType))
			//	throw new InvalidDataException("This method works only with reference types. Value types are not allowed");


			var startinglist = Existings.ToList();
			if (ToAssimilate == null)
			{
				if (Existings.Count == 0)
					return;
				Existings.Clear();
				if (OnRemoved != null)
					startinglist.ForEach(OnRemoved);
				return;
			}

			var externalList = ToAssimilate as TAssimilate[] ?? ToAssimilate.ToArray();


			if (!externalList.Any()) //No Items in List Clear Collection
			{
				if (startinglist.Count == 0)
					return;
				Existings.Clear();
				if (OnRemoved != null)
					startinglist.ForEach(OnRemoved);
				return;
			}

			for (var i = 0; i < externalList.Length; i++)
			{
				var externalItem = externalList[i];
				var existingItem = default(TExisting);
				int currentIndex;

				for (currentIndex = 0; currentIndex < Existings.Count; currentIndex++)
				{
					var ci = Existings[currentIndex];
					if (EqualFunc == null ? ci.Equals(externalItem) : EqualFunc(ci, externalItem))
					{
						existingItem = ci;
						break;
					}
				}

				if (existingItem != null)
				{
					startinglist.Remove(existingItem);
					if (currentIndex != i)
					{
						Existings.RemoveAt(i);
						Existings.Insert(i, existingItem);
					}

					if (OnPairFound != null)
						OnPairFound(existingItem, externalItem);
				}
				else
				{
					var newItem = ConvertFunc(externalItem);
					Existings.Insert(i, newItem);
				}
			}
			foreach (var removeItem in startinglist)
			{
				Existings.Remove(removeItem);
			}
		}
	}
}