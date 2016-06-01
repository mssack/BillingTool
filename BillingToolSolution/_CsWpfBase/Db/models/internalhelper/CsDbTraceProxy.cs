// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-27</date>

using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using CsWpfBase.Db.interfaces;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Db.models.internalhelper
{
	internal class CsDbTraceProxy : IDbProxy
	{
		public CsDbTraceProxy(IDbProxy underlayingProxy)
		{
			UnderlayingProxy = underlayingProxy;
		}


		#region Overrides/Interfaces
		public void SaveChanges(DataSet target, object tag = null)
		{
			Trace($"Save changes of DataSet (Tables: '{target.Tables.OfType<DataTable>().Select(x => x.TableName).Join("', '")}')");

			UnderlayingProxy.SaveChanges(target, tag);
		}

		public void SaveChanges(DataTable target, object tag = null)
		{
			Trace($"Save changes of DataTable (Name = {target.TableName})");

			UnderlayingProxy.SaveChanges(target, tag);
		}

		public DataTable ExecuteCommand(string command, object tag = null)
		{
			Trace($"SQL => \"{command}\"");

			return UnderlayingProxy.ExecuteCommand(command, tag);
		}

		public DataSet ExecuteDataSetCommand(string command, object tag = null)
		{
			Trace($"SQL => \"{command}\"");

			return UnderlayingProxy.ExecuteDataSetCommand(command, tag);
		}

		/// <summary>Executes a command and delivers the result.</summary>
		public int ExecuteNonQuery(string command, object tag = null)
		{
			Trace($"SQL => \"{command}\"");

			return UnderlayingProxy.ExecuteNonQuery(command, tag);
		}
		#endregion


		public IDbProxy UnderlayingProxy { get; }


		private void Trace(string message)
		{
			string method = null, type = null, parameters = null;
			for (var i = 2; i < 5; i++)
			{
				var cm = new StackFrame(i).GetMethod();
				if (cm.DeclaringType == typeof (CsDbDataSet) || cm.DeclaringType == typeof (CsDbTable<>))
					continue;

				type = cm.DeclaringType?.Name;
				method = cm.Name;
				parameters = cm.GetParameters().Select(x => x.Name).Join();
				break;
			}


			Debug.WriteLine($"DBTRACE @ {type}.{method}({parameters}) ".Expand(70) + $"!-> {message}");

		}
	}
}