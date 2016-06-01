using System;






namespace BillingToolDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations
{
	/// <summary>Contains enumeration extensions.</summary>
	public static class BelegDataTypesExtensions
	{
		/// <summary>returns true if this type is can be storniert.</summary>
		public static bool CanBeStorniert(this BelegDataTypes type) => type == BelegDataTypes.Bar || type == BelegDataTypes.Bankomat || type == BelegDataTypes.Kreditkarte;

		/// <summary>
		///     returns true if this type is one of the following: <see cref="BelegDataTypes.TagesBon" />, <see cref="BelegDataTypes.MonatsBon" />,
		///     <see cref="BelegDataTypes.JahresBon" />.
		/// </summary>
		public static bool IsRecapBon(this BelegDataTypes type) => type == BelegDataTypes.TagesBon || type == BelegDataTypes.MonatsBon || type == BelegDataTypes.JahresBon;
		/// <summary>
		///     returns true if this type is one of the following: <see cref="BelegDataTypes.Bar" />, <see cref="BelegDataTypes.Bankomat" />,
		///     <see cref="BelegDataTypes.Kreditkarte" />.
		/// </summary>
		public static bool IsNormalBon(this BelegDataTypes type) => type == BelegDataTypes.Bar || type == BelegDataTypes.Bankomat || type == BelegDataTypes.Kreditkarte;
		

	}
}