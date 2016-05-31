// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-31</date>

using System;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.versioning
{
	/// <summary>Gives feedback about the current build version.</summary>
	public class BuildVersion : Base
	{

		private static Exception GetException(string value)
		{
			throw new Exception($"Der Text '{value}' kann nicht in einen typeof({nameof(BuildVersion)}) umgewandelt werden.");
		}

		/// <summary>parses from name.</summary>
		public BuildVersion(string name)
		{
			var originalValue = name;
			if (string.IsNullOrEmpty(name))
				throw GetException(originalValue);

			name = name.Trim();
			if (name.StartsWith("RC", StringComparison.OrdinalIgnoreCase))
				name = name.Substring(2);

			if (string.IsNullOrEmpty(name))
				throw GetException(originalValue);

			var values = name.Split('.');

			var activeDevelopment = 0;
			var gold = 0;

			if (!int.TryParse(values[0], out activeDevelopment))
				throw GetException(originalValue);
			if (values.Length == 2)
			{
				if (!int.TryParse(values[1], out activeDevelopment))
					throw GetException(originalValue);
			}
			else if (values.Length > 2)
				throw GetException(originalValue);

			ActiveDevelopment = activeDevelopment;
			Gold = gold;
		}

		/// <summary>from definition</summary>
		public BuildVersion(int activeDevelopment, int gold)
		{
			Gold = gold;
			ActiveDevelopment = activeDevelopment;
		}


		#region Overrides/Interfaces
		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
		public override bool Equals(object obj)
		{
			return obj is BuildVersion && Gold == ((BuildVersion) obj).Gold && ActiveDevelopment == ((BuildVersion) obj).ActiveDevelopment;
		}

		/// <summary>Serves as a hash function for a particular type. </summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Gold*397) ^ ActiveDevelopment;
			}
		}
		#endregion


		/// <summary>Gets the gold version of the release candidate. This describes the second part of the version like RC66.XXX where XXX is the gold version.</summary>
		public int Gold { get; private set; }
		/// <summary>
		///     Gets the active-development version of the release candidate. This describes the first part of the version like RCYYY.1 where YYY is the
		///     active-development version.
		/// </summary>
		public int ActiveDevelopment { get; private set; }



		/// <summary>Gets the name of the current RC.</summary>
		public string Name => $"RC{ActiveDevelopment}{(Gold == 0 ? "" : "." + Gold)}";


		/// <summary>Returns the name of the type.</summary>
		public override string ToString() => Name;

		/// <summary>compares with <paramref name="activeDevelopment" /> and <paramref name="gold" /> params.</summary>
		public bool Equals(int activeDevelopment, int gold)
		{
			return activeDevelopment == ActiveDevelopment && gold == Gold;
		}
	}
}