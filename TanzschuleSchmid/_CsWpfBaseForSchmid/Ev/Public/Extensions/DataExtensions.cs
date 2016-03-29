// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Bunch of data extension methods.</summary>
	public static class DataExtensions
	{
		private const string DeletedString = "> ";
		private const string AddedString = "+ ";




		/// <summary>Prints the content of each table in a formatted string for debug purpose.</summary>
		/// <param name="set"></param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static void PrintDiagnosticString(this DataSet set, int columnsize = 8, string separator = " | ")
		{
			Debug.WriteLine(GetDiagnosticString(set, columnsize, separator));
		}
		/// <summary>Prints the content of each table in a formatted string for debug purpose.</summary>
		/// <param name="set"></param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static string GetDiagnosticString(this DataSet set, int columnsize = 8, string separator = " | ")
		{
			StringBuilder sb = new StringBuilder();
			foreach (DataTable table in set.Tables)
			{
				sb.AppendLine(table.TableName);
				sb.AppendLine(table.GetDiagnosticString("   ", columnsize, separator));
				sb.AppendLine();
			}
			return sb.ToString();
		}

		/// <summary>Prints the content of the table in a formatted string for debug purpose.</summary>
		/// <param name="table"></param>
		/// <param name="tablePrefix">The prefix after line break in the output</param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static void PrintDiagnosticString(this DataTable table, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			Debug.WriteLine(table.GetDiagnosticString(tablePrefix, columnsize, separator));
		}
		/// <summary>Prints the content of the table in a formatted string for debug purpose.</summary>
		/// <param name="table"></param>
		/// <param name="tablePrefix">The prefix after line break in the output</param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static void PrintDiagnosticString(this DataRowCollection table, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			Debug.WriteLine(table.GetDiagnosticString(tablePrefix, columnsize, separator));
		}
		/// <summary>Prints the content of the table in a formatted string for debug purpose.</summary>
		/// <param name="rows"></param>
		/// <param name="tablePrefix">The prefix after line break in the output</param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static void PrintDiagnosticString(this IEnumerable<DataRow> rows, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			Debug.WriteLine(rows.GetDiagnosticString(tablePrefix, columnsize, separator));
		}



		/// <summary>Returns the tables content in a table formatted string for debug purpose.</summary>
		/// <param name="dataTable"></param>
		/// <param name="tablePrefix">The prefix after line break in the output</param>
		/// <param name="columnsize">
		///     The size of one column in characters. If the input is larger then the exceeding character will be removed from inside and
		///     replaced with '~' char
		/// </param>
		/// <param name="separator">The column separator</param>
		public static string GetDiagnosticString(this DataTable dataTable, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			return dataTable.Rows.GetDiagnosticString(tablePrefix, columnsize, separator);
		}

		/// <summary>Returns a rows content for debug purpose. </summary>
		public static string GetDiagnosticString(this DataRowCollection rows, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			return GetDiagnosticString(rows.OfType<DataRow>(), tablePrefix, columnsize, separator);
		}

		/// <summary>Returns a rows content for debug purpose. </summary>
		public static string GetDiagnosticString(this IEnumerable<DataRow> rows, string tablePrefix = "", int columnsize = 8, string separator = " | ")
		{
			var firstOrDefault = rows.FirstOrDefault();
			if (firstOrDefault == null)
				return "";
			var table = firstOrDefault.Table;


			var header = tablePrefix + "".Expand(DeletedString.Length) + table.Columns.OfType<DataColumn>().Select(column => column.ColumnName.CutMiddle(columnsize).Expand(columnsize)).Join(separator);
			var content = tablePrefix + rows.Select(row =>
			{
				if (row.RowState == DataRowState.Deleted)
					return DeletedString + row.Table.Columns.OfType<DataColumn>().Select(col => row[col, DataRowVersion.Original].ToString().CutMiddle(columnsize).Expand(columnsize)).Join(separator);
				if (row.RowState == DataRowState.Added)
					return AddedString + row.Table.Columns.OfType<DataColumn>().Select(col => row[col, DataRowVersion.Default].ToString().CutMiddle(columnsize).Expand(columnsize)).Join(separator);
				return "".Expand(DeletedString.Length) + row.Table.Columns.OfType<DataColumn>().Select(col => row[col, DataRowVersion.Original].ToString().CutMiddle(columnsize).Expand(columnsize)).Join(separator);
			}).Join("\r\n" + tablePrefix);

			return header + "\r\n" + content;
		}


	}
}