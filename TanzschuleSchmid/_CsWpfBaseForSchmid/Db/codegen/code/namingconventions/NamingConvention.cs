// Copyright (c) 2014 - 2016 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-28</date>

using System;
using System.Data;
using System.Text.RegularExpressions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Db.codegen.code.namingconventions
{
	/// <summary>Used to specialize naming of database tables.</summary>
	public class NamingConvention : Base
	{


		/// <summary>Parses a convention from a character delimited string.</summary>
		/// <param name="line">The line must match the following convention: {NativeName};{TableClassName};{TableRowName};{TablePropertyName}</param>
		/// <param name="delimiter">The delimiter between each property</param>
		public static NamingConvention ParseFromLine(string line, string delimiter)
		{
			var properties = Regex.Split(line, delimiter);
			if (properties.Length < 3)
				throw new InvalidOperationException($"The string '{line}' could not be parsed into an instance of {typeof (NamingConvention).Name}");
			var instance = new NamingConvention
			{
				NativeName = properties[0],
				Singular = properties[1],
				Plural = properties[2]
			};

			return instance;
		}

		/// <summary>Parses a convention from a row.</summary>
		public static NamingConvention ParseFromRow(DataRow row, int nativeNameColumn = 0, int singularNameColumn = 1, int pluralNameColumn = 2)
		{
			var instance = new NamingConvention
			{
				NativeName = (string) row[nativeNameColumn],
				Singular = (string) row[singularNameColumn],
				Plural = (string) row[pluralNameColumn]
			};

			return instance;
		}

		/// <summary>Parses a convention from a row with well defined column names.</summary>
		public static NamingConvention ParseFromRow(DataRow row)
		{
			var instance = new NamingConvention
			{
				NativeName = (string) row["NativeName"],
				Singular = (string) row["Singular"],
				Plural = (string) row["Plural"]
			};

			return instance;
		}

		private string _nativeName;
		private string _plural;
		private string _singular;


		private NamingConvention()
		{

		}

		/// <summary>The native name of the table, view or relation.</summary>
		public string NativeName
		{
			get { return _nativeName; }
			private set { SetProperty(ref _nativeName, value); }
		}
		/// <summary>Gets the singular name of the <see cref="NativeName" />.</summary>
		public string Singular
		{
			get { return _singular; }
			private set { SetProperty(ref _singular, value); }
		}
		/// <summary>Gets the plural name of the <see cref="NativeName" />.</summary>
		public string Plural
		{
			get { return _plural; }
			private set { SetProperty(ref _plural, value); }
		}
	}
}