// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Db.router;






namespace CsWpfBase.Db.models.helper
{
	/// <summary>A class used for the cs db tools to directly connect with a database.</summary>
	public class CsDbRouter_SqlDirect : CsDbRouter, IDbProxyAssociateable
	{
		private string _catalog;
		private string _dataSource;
		private string _password;
		private string _userName;


		#region Abstract
		/// <summary>Associates the <see cref="IDbProxy" /> with a database name.</summary>
		/// <param name="catalogName">the name of the target data base.</param>
		public virtual void Associate(string catalogName)
		{
			Catalog = catalogName;
		}
		#endregion


		#region Overrides/Interfaces
		/// <summary>Gets a data adapter with the specified command.</summary>
		public override DbDataAdapter GetAdapter(DbCommand cmd)
		{
			return new SqlDataAdapter((SqlCommand)cmd);
		}

		/// <summary>Gets a db command.</summary>
		public override DbCommand GetCommand()
		{
			return new SqlCommand();
		}

		/// <summary>Get command builder.</summary>
		public override DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter)
		{
			return new SqlCommandBuilder((SqlDataAdapter)adapter);
		}

		/// <summary>Creates a new instance of <see cref="DbConnection" />.</summary>
		public override DbConnection Initialize()
		{
			return new SqlConnection(ConnectionString);
		}

		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		public override DataSet ExecuteDataSetCommand(string command, object tag = null)
		{
			var resultSet = base.ExecuteDataSetCommand(command, tag);


			var commandTables = command.Split(';').Select(x => Regex.Match(x, @"from[\s\S]*?\[?([^\s]+)\]?", RegexOptions.IgnoreCase).Groups[1].Value).ToArray();



			if (resultSet.Tables.Count != commandTables.Length)
				return resultSet;


			for (var i = 0; i < commandTables.Length; i++)
			{
				resultSet.Tables[i].TableName = commandTables[i];
			}
			return resultSet;
		}
		#endregion


		/// <summary>The name of the catalog</summary>
		public string Catalog
		{
			get { return _catalog; }
			set
			{
				_catalog = value;
				ResetConnectionString();
			}
		}

		/// <summary>The name of the server which needs to be connected.</summary>
		public string DataSource
		{
			get { return _dataSource; }
			set
			{
				_dataSource = value;
				ResetConnectionString();
			}
		}
		/// <summary>The user name.</summary>
		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				ResetConnectionString();
			}
		}
		/// <summary>The password.</summary>
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				ResetConnectionString();
			}
		}
		private string ConnectionString { get; set; }

		/// <summary>Sets the connection string which is used to directly connect to the databse.</summary>
		protected void ResetConnectionString()
		{
			ConnectionString = $"Data Source={DataSource};User id={UserName};Password={Password}" + (String.IsNullOrEmpty(Catalog)? "":$";Initial Catalog={Catalog}");
		}
	}
}