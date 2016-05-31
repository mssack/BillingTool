// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using System.IO;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of DateTime extensions</summary>
	public static class DirectoryInfoExtensions
	{
		/// <summary>goes the directory structure up until it finds directory with sepcific name</summary>
		public static DirectoryInfo GoUpward_Until(this DirectoryInfo info, string name, bool ignoreCase = true)
		{
			while (info != null && info.Name != name)
			{
				info = info.Parent;
			}
			return info;
		}

		/// <summary>creates the directory if it does not exist already.</summary>
		public static DirectoryInfo Create_If_NotExists(this DirectoryInfo info)
		{
			if (!info.Exists)
				info.Create();
			return info;
		}
	}
}