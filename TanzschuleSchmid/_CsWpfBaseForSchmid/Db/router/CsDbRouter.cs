// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Data;
using System.Data.Common;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.router
{
	/// <summary>
	///     Used to route database query's through a abstract database connection. This is the main operating interface of the whole <see cref="CsDb" />
	///     namespace. Use it whenever data from an generic database is required.
	/// </summary>
	public abstract class CsDbRouter : Base, IDbProxy
	{
		private DbConnection _connection;
		private CsDbRouterState _state;


		#region Abstract
		/// <summary>Creates a new <see cref="DbDataAdapter" /> of the specific type using the command. Do not change any settings of the data adapter.</summary>
		public abstract DbDataAdapter GetAdapter(DbCommand cmd);

		/// <summary>Returns a complete new instance of type <see cref="DbCommand"/>.</summary>
		public abstract DbCommand GetCommand();

		/// <summary>Returns a complete new instance of type <see cref="DbCommandBuilder"/>. Use <paramref name="adapter"/> for initialization.</summary>
		public abstract DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter);

		/// <summary>Returns a complete new instance of type <see cref="DbConnection"/>.</summary>
		public abstract DbConnection Initialize();

		/// <summary>Opens the connection. If opening does not succeed exception will be stored in state.</summary>
		public virtual void Open()
		{
			if (Connection != null && Connection.State == ConnectionState.Open)
				return;

			Close();

			try
			{
				State.SetConnecting();

				Connection = Initialize();
				Connection.Open();

				State.SetConnected();
			}
			catch (Exception exception)
			{
				State.SetDisconnected(exception);
			}
		}

		/// <summary>Closes the connection if intilized and opened.</summary>
		public virtual void Close()
		{
			if (Connection == null)
				return;
			if (Connection.State != ConnectionState.Closed)
				Connection.Close();

			Connection.Dispose();
			Connection = null;
			State.SetDisconnected();
		}

		/// <summary>Fill the target set with a table, this table is not filled but with the correct Schema. This is useful when adding new items to the table.</summary>
		public virtual void LoadSchema(DataSet target, string tableName)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{tableName}]"))
			{
				dbAdapter.FillSchema(target, SchemaType.Source);
			}
		}

		/// <summary>Fill the target table with the schema of the db, this table is not filled. This is useful when adding new items to the table.</summary>
		public virtual void LoadSchema(DataTable target, object tag = null)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{target.TableName}]"))
			{
				dbAdapter.FillSchema(target, SchemaType.Source);
			}
		}

		/// <summary>
		///     Creates a new instance of a specific table, this table is not filled but filled with the schema of the table with name DataTable.TableName. This
		///     is useful when adding new items to the table.
		/// </summary>
		public virtual T LoadSchema<T>() where T : DataTable
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dataTable = (DataTable) Activator.CreateInstance(typeof (T)))
			{
				var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{dataTable.TableName}]");
				dbAdapter.FillSchema(dataTable, SchemaType.Source);
				return (T) dataTable;
			}
		}

		/// <summary>Loads the complete db table into the target using the table name.</summary>
		public virtual void LoadTable(DataTable target)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{target.TableName}]"))
			{
				dbAdapter.Fill(target);
			}
		}

		/// <summary>Creates and load the complete db table into a new instance. Using the table name.</summary>
		public virtual T LoadTable<T>() where T : DataTable
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			var dataTable = (DataTable) Activator.CreateInstance(typeof (T));

			using (var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{dataTable.TableName}]"))
			{
				dbAdapter.Fill(dataTable);
				return (T) dataTable;
			}
		}

		/// <summary>Securely saves the changes to the database.</summary>
		public virtual void SaveChanges(DataTable target, object tag = null)
		{
			if (target == null)
				return;
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbAdapter = GetDefaultAdapter($"SELECT * FROM [{target.TableName}]"))
			{
				var added = target.GetChanges(DataRowState.Added);
				var modified = target.GetChanges(DataRowState.Modified);
				var deleted = target.GetChanges(DataRowState.Deleted);


				if (added != null)
					dbAdapter.Update(added);
				if (modified != null)
					dbAdapter.Update(modified);
				if (deleted != null)
					dbAdapter.Update(deleted);
				target.AcceptChanges();
			}
		}

		/// <summary>Securely saves the changes to the database.</summary>
		public virtual void SaveChanges(DataSet target, object tag = null)
		{
			if (target == null)
				return;
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			foreach (DataTable table in target.Tables)
			{
				SaveChanges(table, tag);
			}
		}

		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		public virtual DataTable ExecuteCommand(string command, object tag = null)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbDataAdapter = GetDefaultAdapter(command))
			{
				var target = new DataTable();
				dbDataAdapter.Fill(target);

				return target;
			}
		}

		/// <summary>Executes a command and delivers the result.</summary>
		/// <returns>
		///     The number of rows successfully added to or refreshed in the DataSet. This does not include rows affected by statements that do not return
		///     rows.
		/// </returns>
		public virtual DataSet ExecuteDataSetCommand(string command, object tag = null)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbDataAdapter = GetDefaultAdapter(command))
			{
				var target = new DataSet();
				dbDataAdapter.Fill(target);

				return target;
			}
		}

		/// <summary>Executes a command and delivers the result.</summary>
		public virtual int ExecuteNonQuery(string command, object tag = null)
		{
			Open();
			if (State.IsConnected == false)
				throw State.LastException;
			using (var dbcommand = GetDefaultCommand(command))
			{
				return dbcommand.ExecuteNonQuery();
			}
		}
		#endregion


		/// <summary>Current connection state.</summary>
		public CsDbRouterState State => _state ?? (_state = new CsDbRouterState());
		/// <summary>Gets or sets the Connection.</summary>
		public DbConnection Connection
		{
			get { return _connection; }
			private set { SetProperty(ref _connection, value); }
		}

		/// <summary>Returns a <see cref="DbDataAdapter" /> with the specified command as <see cref="DbCommand" />.</summary>
		/// <param name="command">The command which needs to be associated with the <see cref="DbDataAdapter" />.</param>
		public DbDataAdapter GetDefaultAdapter(string command)
		{
			var adapter = GetAdapter(GetDefaultCommand(command));
			adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

			var builder = GetCommandBuilder(adapter);
			builder.ConflictOption = ConflictOption.OverwriteChanges;

			return adapter;
		}

		/// <summary>
		///     Returns a <see cref="DbCommand" /> with the specified command as <paramref name="command" /> as content. Automatically sets the connection for
		///     the command.
		/// </summary>
		public DbCommand GetDefaultCommand(string command)
		{
			var cmd = GetCommand();
			cmd.Connection = Connection;
			cmd.CommandText = command;
			return cmd;
		}
	}
}