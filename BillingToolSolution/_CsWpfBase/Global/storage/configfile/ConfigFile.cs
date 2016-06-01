// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CsWpfBase.Ev.Exceptions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.storage.configfile
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgConfigFile : Base
	{
		private static CsgConfigFile _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgConfigFile I
		{
			get
			{
				if (_instance == null)
					throw new CsGlobalFunctionNotConfiguredException(GlobalFunctions.ConfigFile);
				return _instance;
			}
		}
		private Dictionary<string, string> _params;

		private CsgConfigFile()
		{
		}

		/// <summary>Configuration file parameter.</summary>
		public Dictionary<string, string> Params
		{
			get { return _params ?? (_params = new Dictionary<string, string>()); }
		}

		internal static void Install()
		{
			_instance = new CsgConfigFile();
			_instance.InterpretConfigFile();
		}

		/// <summary>Reads the 'config.txt' in the application startup folder.</summary>
		private void InterpretConfigFile()
		{
			var fi = new FileInfo("config.txt");
			if (!fi.Exists)
				return;

			var content = "";
			using (var reader = new StreamReader(fi.FullName))
			{
				content = reader.ReadToEnd(); //Read content and remove each line starting with #
				reader.Close();
			}

			var lines = Regex.Split(content.Replace("\r\n", "\n"), "\n");
			for (var currentLineIndex = 0; currentLineIndex < lines.Length; currentLineIndex++)
			{
				var pos = 0;
				var line = lines[currentLineIndex];
				SkipWhiteSpaces(line, ref pos);
				if (pos == line.Length)
					continue;
				if (line[pos] == '#') //configuration file escaped or invalid format
					continue;
				if (line[pos] == '=')
					throw InvalidFormatException(currentLineIndex, pos, "The line cannot start with '=' character.");


				var indexOfEqual = line.IndexOf('=', pos);

				if (indexOfEqual == -1)
					throw InvalidFormatException(currentLineIndex, null, "No '=' character found please comment out line by using '#'.");

				var param = line.Substring(pos, indexOfEqual - pos).Trim(' ', '\t');
				var value = line.Substring(indexOfEqual + 1).Trim(' ', '\t');

				if (Params.ContainsKey(param))
					throw InvalidFormatException(currentLineIndex, null, "The parameter '" + param + "' is defined multiple times.");

				Params.Add(param, value);
			}
		}

		private void SkipWhiteSpaces(string line, ref int pos)
		{
			while (pos < line.Length && (line[pos] == ' ' || line[pos] == '\t')) //skip trailing
			{
				pos++;
			}
		}

		private InvalidDataException InvalidFormatException(int line, int? pos = null, string message = "")
		{
			return new InvalidDataException($"Invalid file ({"config.txt"}) format at line [{line}]{(pos == null ? "" : " character [" + pos + "]")}. {message}");
		}
	}
}