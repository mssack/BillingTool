// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.DataInterfaces
{
	/// <summary>Stores a property called <see cref="Comment" />.</summary>
	public interface IStoreComment
	{
		#region Abstract
		/// <summary>a comment.</summary>
		string Comment { get; set; }
		/// <summary>should be updated whenever the changeable content changes.</summary>
		DateTime? CommentLastChanged { get; set; }
		#endregion
	}
}