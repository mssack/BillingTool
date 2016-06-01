// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Globalization;
using System.IO;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.debug
{
	/// <summary>Wraps informations about the a code position.</summary>
	[Serializable]
	public sealed class CodePosition : Base
	{
		private string _fileName;
		private string _filePath;
		private int _lineNumber;
		private string _methodName;
		private string _positionId;

		internal CodePosition(string methodName, string filePath, int lineNumber)
		{
			_methodName = methodName;
			_filePath = filePath;
			_lineNumber = lineNumber;
			_fileName = new FileInfo(FilePath).Name;
			PositionId = SmallHash.FromString(new[] {methodName, FilePath, lineNumber.ToString(CultureInfo.InvariantCulture)}.Join(""));
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return GetIdentifier(24).Expand(24);
		}
		#endregion


		/// <summary>combining all code relevant positioning informations.</summary>
		public string PositionId
		{
			get { return _positionId; }
			private set { SetProperty(ref _positionId, value); }
		}
		/// <summary>The method name where the log was created from</summary>
		public string MethodName
		{
			get { return _methodName; }
			private set { SetProperty(ref _methodName, value); }
		}
		/// <summary>The complete file path to the code.</summary>
		public string FilePath
		{
			get { return _filePath; }
			private set { SetProperty(ref _filePath, value); }
		}
		/// <summary>The file name extracted from <see cref="FilePath" />
		/// </summary>
		public string FileName
		{
			get { return _fileName; }
			set { SetProperty(ref _fileName, value); }
		}
		/// <summary>The line where the log was created at.</summary>
		public int LineNumber
		{
			get { return _lineNumber; }
			private set { SetProperty(ref _lineNumber, value); }
		}

		/// <summary>Returns an human readable identifier limited to <paramref name="maxCharacters" /> for the code position.</summary>
		public string GetIdentifier(int maxCharacters)
		{
			var fi = new FileInfo(FilePath);
			var filename = fi.Name.Replace(fi.Extension, "").Replace(".xaml", "");
			var linestring = LineNumber.ToString(CultureInfo.InvariantCulture);



			var maxChar = maxCharacters - linestring.Length - 2;
			var completeLength = filename.Length + MethodName.Length;

			if (completeLength <= maxChar)
				return new[] {filename, MethodName, linestring}.Join(".");


			var removeAmount = completeLength - maxChar;
			var filenameMinLength = maxChar/2;
			var methodMinLength = maxChar - filenameMinLength;



			if (filenameMinLength >= filename.Length)
				return new[] {filename, MethodName.CutMiddle(MethodName.Length - removeAmount), linestring}.Join(".");

			if (methodMinLength >= MethodName.Length)
				return new[] {filename.CutMiddle(filename.Length - removeAmount), MethodName, linestring}.Join(".");




			return new[] {filename.CutMiddle(filename.Length - (filename.Length - filenameMinLength)), MethodName.CutMiddle(MethodName.Length - (MethodName.Length - methodMinLength)), linestring}.Join(".");
		}
	}
}