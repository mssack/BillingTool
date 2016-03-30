// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Db.models.bases;






namespace CsWpfBase.Db.models
{
	/// <summary>The base for each row inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbView<TRow> : CsDbTable<TRow>
		where TRow : CsDbRowBase
	{


		///	<summary>This method is not supported on Views.</summary>
		[Obsolete]
		public override CsDbRowBase Generic_FindOrLoad(object primaryKeyValue)
		{
			throw new NotImplementedException("This method can not be used on a view");
		}
		///	<summary>This method is not supported on Views.</summary>
		[Obsolete]
		public override CsDbRowBase Generic_LoadThenFind(object primaryKeyValue)
		{
			throw new NotImplementedException("This method can not be used on a view");
		}
		///	<summary>This method is not supported on Views.</summary>
		[Obsolete]
		public override CsDbRowBase Generic_Find(object primaryKeyValue)
		{
			throw new NotImplementedException("This method can not be used on a view");
		}
	}



}