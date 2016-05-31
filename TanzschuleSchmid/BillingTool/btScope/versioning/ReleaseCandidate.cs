// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-30</date>

using System;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.versioning
{
	/// <summary>Gives feedback about the current build version.</summary>
	public class ReleaseCandidate : Base
	{
		public ReleaseCandidate(string value)
		{
			var originalValue = value;
			if (string.IsNullOrEmpty(value))
				throw GetException(originalValue);

			value = value.Trim();
			if (value.StartsWith("RC", StringComparison.OrdinalIgnoreCase))
				value = value.Substring(2);

			if (string.IsNullOrEmpty(value))
				throw GetException(originalValue);

			var values = value.Split('.');

			var activeDevelopment = 0;
			var gold = 0;

			if (!int.TryParse(values[0], out activeDevelopment))
				throw GetException(originalValue);
			if (values.Length == 2)
			{
				if (!int.TryParse(values[1], out activeDevelopment))
					throw GetException(originalValue);
			}
			else if (values.Length> 2)
				throw GetException(originalValue);

			ActiveDevelopment = activeDevelopment;
			Gold = gold;
		}

		private static Exception GetException(string value)
		{
			throw new Exception($"Der Text '{value}' kann nicht in einen typeof({nameof(ReleaseCandidate)}) umgewandelt werden.");
		}



		#region Overrides/Interfaces
		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
		public override bool Equals(object obj)
		{
			return obj is ReleaseCandidate && Gold == ((ReleaseCandidate) obj).Gold && ActiveDevelopment == ((ReleaseCandidate) obj).ActiveDevelopment;
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

		/// <summary>compares with <paramref name="activeDevelopment" /> and <paramref name="gold" /> params.</summary>
		public bool Equals(int activeDevelopment, int gold)
		{
			return activeDevelopment == ActiveDevelopment && gold == Gold;
		}
	}
}