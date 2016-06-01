// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-05</date>

using System;
using System.Data;






namespace CsWpfBase.Db.interfaces
{
	/// <summary>Used whenever execute command functionality is needed.</summary>
	public interface IDbProxy
	{
		#region Abstract
		/// <summary>Securely saves the changes to the database.</summary>
		void SaveChanges(DataSet target, object tag = null);


		/// <summary>Securely saves the changes to the database.</summary>
		void SaveChanges(DataTable target, object tag = null);



		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		DataTable ExecuteCommand(string command, object tag = null);


		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		DataSet ExecuteDataSetCommand(string command, object tag = null);


		/// <summary>Executes a command and delivers the result.</summary>
		int ExecuteNonQuery(string command, object tag = null);
		#endregion
	}



}