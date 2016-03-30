// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using CsWpfBase.Db.router;






namespace BillingDataAccess.sqlcedatabases.Router
{
	/// <summary>Used to connect to the file, provides all connectivity functions needed for a SQLCe database.</summary>
	public class SqlCeRouter : CsDbRouter
	{
		private readonly string _databaseFilePath;

		public SqlCeRouter(string databaseFilePath)
		{
			_databaseFilePath = databaseFilePath;
		}


		#region Overrides/Interfaces
		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		public override DataSet ExecuteDataSetCommand(string command, object tag = null)
		{
			var commands = command.Split(';');
			var targetSet = new DataSet();
			foreach (var cmd in commands)
			{
				var table = ExecuteCommand(cmd);
				targetSet.Tables.Add(table);
			}
			return targetSet;
		}

		/// <summary>Creates a new <see cref="DbDataAdapter" /> of the specific type using the command. Do not change any settings of the data adapter.</summary>
		public override DbDataAdapter GetAdapter(DbCommand cmd)
		{
			return new SqlCeDataAdapter((SqlCeCommand) cmd);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommand" />.</summary>
		public override DbCommand GetCommand()
		{
			return new SqlCeCommand();
		}

		/// <summary>Returns a complete new instance of type <see cref="DbCommandBuilder" />. Use <paramref name="adapter" /> for initialization.</summary>
		public override DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter)
		{
			return new SqlCeCommandBuilder((SqlCeDataAdapter) adapter);
		}

		/// <summary>Returns a complete new instance of type <see cref="DbConnection" />.</summary>
		public override DbConnection Initialize()
		{
			return new SqlCeConnection($"data source={_databaseFilePath}");
		}
		#endregion
	}
}