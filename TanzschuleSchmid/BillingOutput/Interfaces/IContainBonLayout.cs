// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Windows.Media.Imaging;






namespace BillingOutput.Interfaces
{
	/// <summary>Bon layout informations.</summary>
	public interface IContainBonLayout
	{
		#region Abstract
		/// <summary>The header of the Bon.</summary>
		BitmapSource KassenBonHeader { get; }
		/// <summary>The Footer of the Bon.</summary>
		BitmapSource KassenBonFooter { get; }
		/// <summary>The header of the Bon.</summary>
		string KassenBonHeaderText { get; }
		/// <summary>The header of the Bon.</summary>
		string KassenBonFooterText { get; }
		#endregion
	}
}