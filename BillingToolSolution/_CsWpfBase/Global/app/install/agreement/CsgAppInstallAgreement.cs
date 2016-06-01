// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Text.RegularExpressions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.app.install.agreement
{
	/// <summary>See <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgAppInstallAgreement : Base
	{
		private static CsgAppInstallAgreement _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgAppInstallAgreement I
		{
			get
			{
				if (_instance != null)
					return _instance;

				_instance = new CsgAppInstallAgreement();
				_instance.LoadAgreementInfo();

				return _instance;
			}
		}
		private string _acceptedAgreement;
		private string _agreement;
		private bool _isLicensed;
		private string _privacyAgreement;

		private CsgAppInstallAgreement()
		{
		}

		/// <summary>Gets the agreement stored inside the <see cref="CsgAgreement" /> class.</summary>
		public string Agreement
		{
			get { return _agreement ?? (_agreement = new CsgAgreement().GetString()); }
			private set { _agreement = value; }
		}
		/// <summary>Gets the privacy agreement stored inside the <see cref="CsgAgreementPrivacyAddition" /> class.</summary>
		public string PrivacyAgreement
		{
			get { return _privacyAgreement ?? (_privacyAgreement = new CsgAgreementPrivacyAddition().GetString()); }
			private set { _privacyAgreement = value; }
		}
		/// <summary>Gets or sets the IsLicensed.</summary>
		public bool IsLicensed
		{
			get { return _isLicensed; }
			private set { SetProperty(ref _isLicensed, value); }
		}
		/// <summary>Gets or sets the AcceptedAgreement.</summary>
		public string AcceptedAgreement
		{
			get { return _acceptedAgreement; }
			private set { SetProperty(ref _acceptedAgreement, value); }
		}





		/// <summary>Ensures that the user have accepted the license.</summary>
		public void CheckAcceptance()
		{
			if (IsLicensed)
				return;


			var result = new CsgAgreementWindow {Agreement = Agreement, PrivacyAgreement = PrivacyAgreement, ApplicationName = CsGlobal.App.Info.Name}.ShowDialog();
			if (result == false)
				CsGlobal.App.Exit();

			AcceptedAgreement = Agreement + "\r\n" + PrivacyAgreement;
			IsLicensed = true;

			if (CsGlobal.IsInstalled(GlobalFunctions.Storage))
				WriteAgreementToFile();
		}

		private void LoadAgreementInfo()
		{
			if (!CsGlobal.IsInstalled(GlobalFunctions.Storage))
			{
				IsLicensed = false;
				return;
			}

			var file = CsGlobal.Storage.Private.GetFilePathByName("License");
			if (!file.Exists)
			{
				IsLicensed = false;
				return;
			}

			var agreementWithHash = file.LoadAs_UTF8String();
			string agreement;
			if (IsValidAgreement(agreementWithHash, out agreement))
			{
				AcceptedAgreement = agreement;
				IsLicensed = true;
				return;
			}

			IsLicensed = false;
		}

		private void WriteAgreementToFile()
		{
			var sha1Hash = AcceptedAgreement.Sha1Hash().Md5Hash().Sha1Hash();
			var completeLicenseString = $"<hash>{sha1Hash}</hash>\r\n{AcceptedAgreement}";
			var file = CsGlobal.Storage.Private.GetFilePathByName("License");
			file.DeleteFile_IfExists();
			file.CreateDirectory_IfNotExists();
			completeLicenseString.SaveAs_Utf8String(file);
		}

		private bool IsValidAgreement(string agreementWithHash, out string agreement)
		{
			var findHashRegex = new Regex("<hash>(.*?)</hash>\r\n");
			var hash = findHashRegex.Match(agreementWithHash).Groups[1].Value;
			if (string.IsNullOrEmpty(hash))
			{
				agreement = null;
				return false;
			}
			agreement = findHashRegex.Replace(agreementWithHash, "");
			return agreement.Sha1Hash().Md5Hash().Sha1Hash() == hash;
		}
	}
}