// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Global.app.install.agreement
{
	/// <summary>A wrapper around the agreement file. Use <see cref="FileTemplate.GetString()" /> method to get the full agreement.</summary>
	[Serializable]
	public class CsgAgreement : FileTemplate
	{
		/// <summary>Configure configuration to adjust this</summary>
		[Key]
		public string ProductName => CsGlobal.App.Info.Name;
		/// <summary>Configure configuration to adjust this</summary>
		[Key]
		public string ProductDeveloper => CsWpfBaseConfig.I.AgreementDeveloperName;
		/// <summary>Configure configuration to adjust this</summary>
		[Key]
		public string ProductDeveloperAddress => CsWpfBaseConfig.I.AgreementDeveloperAddress;
		/// <summary>Automatically inserts the date</summary>
		[Key(StringFormat = "dd.MM.yyyy")]
		public DateTime SignatureDate => DateTime.Now;
	}
}