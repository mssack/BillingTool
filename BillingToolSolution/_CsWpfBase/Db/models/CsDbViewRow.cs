// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Data;
using CsWpfBase.Db.models.bases;






namespace CsWpfBase.Db.models
{
	/// <summary>The base for each row inside the db engine.</summary>
	[Serializable]
	public abstract class CsDbViewRow : CsDbRowBase
	{
		/// <summary>ctor</summary>
		protected CsDbViewRow(DataRowBuilder builder) : base(builder)
		{
		}


		#region Overrides/Interfaces
		/// <summary>This method is not implemented on Views.</summary>
		[Obsolete]
		public override void ApplyDefaults()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}