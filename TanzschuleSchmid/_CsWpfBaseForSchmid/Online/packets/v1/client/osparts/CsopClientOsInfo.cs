// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Global;
using CsWpfBase.Global.os;






namespace CsWpfBase.Online.packets.v1.client.osparts
{
	/// <summary>Contains informations about the client os.</summary>
	public class CsopClientOsInfo : CsoPacket
	{
		private string _architecture;
		private string _buildType;
		private DateTime _clientTime;
		private TimeZoneInfo _clientTimeZoneInfo;
		private string _codeSet;
		private string _computerName;
		private string _countryCode;
		private Int32 _currentUserIndex;
		private string _description;
		private DateTime _installDate;
		private string _name;
		private CsgOs.OperatingSystemSkus _operatingSystemSku;
		private CsgOs.ProductTypes _productType;
		private string _registeredUser;
		private string _serialNumber;
		private List<CsopV1PartUser> _users;
		private string _version;

		/// <summary>ctor</summary>
		public CsopClientOsInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientOsInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			ClientTime = DateTime.Now;
			ClientTimeZoneInfo = TimeZoneInfo.Local;

			ComputerName = CsGlobal.Os.ComputerName;
			Architecture = CsGlobal.Os.Architecture;
			BuildType = CsGlobal.Os.BuildType;
			CodeSet = CsGlobal.Os.CodeSet;
			CountryCode = CsGlobal.Os.CountryCode;

			Description = CsGlobal.Os.Description;
			InstallDate = CsGlobal.Os.InstallDate;
			Name = CsGlobal.Os.Name;
			OperatingSystemSku = CsGlobal.Os.OperatingSystemSku;

			ProductType = CsGlobal.Os.ProductType;
			RegisteredUser = CsGlobal.Os.RegisteredUser;
			SerialNumber = CsGlobal.Os.SerialNumber;
			Version = CsGlobal.Os.Version;
			CurrentUserIndex = Array.IndexOf(CsGlobal.Os.Users, CsGlobal.Os.CurrentUser);
			Users = CsGlobal.Os.Users.Select(CsopV1PartUser.FromCsg).ToList();
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientOsInfo; }
		}
		/// <summary>
		///     The initial Value of the <see cref="CsoPacket.PacketVersion" />, this value will be applied to the <see cref="CsoPacket.PacketVersion" />
		///     property whenever the packet is created or no version is defined.
		/// </summary>
		protected override uint InitialVersion
		{
			get { return 1; }
		}

		/// <summary>Interprets a binary into this object.</summary>
		internal override void Parse(Reader reader, int length)
		{
			ClientTime = reader.DateTime();
			ClientTimeZoneInfo = TimeZoneInfo.FromSerializedString(reader.String());

			ComputerName = reader.String();
			Architecture = reader.String();
			BuildType = reader.String();
			CodeSet = reader.String();
			CountryCode = reader.String();

			Description = reader.String();
			InstallDate = reader.DateTime();
			Name = reader.String();
			OperatingSystemSku = (CsgOs.OperatingSystemSkus) reader.UInt32();

			ProductType = (CsgOs.ProductTypes) reader.UInt32();
			RegisteredUser = reader.String();
			SerialNumber = reader.String();
			Version = reader.String();

			CurrentUserIndex = reader.Int32();
			Users = reader.ListOfParts<CsopV1PartUser>();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.DateTime(ClientTime);
			writer.String(ClientTimeZoneInfo.ToSerializedString());

			writer.String(ComputerName);
			writer.String(Architecture);
			writer.String(BuildType);
			writer.String(CodeSet);
			writer.String(CountryCode);

			writer.String(Description);
			writer.DateTime(InstallDate);
			writer.String(Name);
			writer.UInt32((uint) OperatingSystemSku);

			writer.UInt32((uint) ProductType);
			writer.String(RegisteredUser);
			writer.String(SerialNumber);
			writer.String(Version);

			writer.Int32(CurrentUserIndex);
			writer.ListOfParts(Users);
		}
		#endregion


		/// <summary>Gets or sets the ClientTime.</summary>
		public DateTime ClientTime
		{
			get { return _clientTime; }
			set { SetProperty(ref _clientTime, value); }
		}
		/// <summary>Gets or sets the ClientTimeZoneInfo.</summary>
		public TimeZoneInfo ClientTimeZoneInfo
		{
			get { return _clientTimeZoneInfo; }
			set { SetProperty(ref _clientTimeZoneInfo, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string ComputerName
		{
			get { return _computerName; }
			set { SetProperty(ref _computerName, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string Architecture
		{
			get { return _architecture; }
			set { SetProperty(ref _architecture, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string BuildType
		{
			get { return _buildType; }
			set { SetProperty(ref _buildType, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string CodeSet
		{
			get { return _codeSet; }
			set { SetProperty(ref _codeSet, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string CountryCode
		{
			get { return _countryCode; }
			set { SetProperty(ref _countryCode, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public DateTime InstallDate
		{
			get { return _installDate; }
			set { SetProperty(ref _installDate, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public CsgOs.OperatingSystemSkus OperatingSystemSku
		{
			get { return _operatingSystemSku; }
			set { SetProperty(ref _operatingSystemSku, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public CsgOs.ProductTypes ProductType
		{
			get { return _productType; }
			set { SetProperty(ref _productType, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string RegisteredUser
		{
			get { return _registeredUser; }
			set { SetProperty(ref _registeredUser, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string SerialNumber
		{
			get { return _serialNumber; }
			set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public string Version
		{
			get { return _version; }
			set { SetProperty(ref _version, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public Int32 CurrentUserIndex
		{
			get { return _currentUserIndex; }
			set { SetProperty(ref _currentUserIndex, value); }
		}
		/// <summary>look at <see cref="CsGlobal.Os" />.</summary>
		public List<CsopV1PartUser> Users
		{
			get { return _users ?? (_users = new List<CsopV1PartUser>()); }
			set { SetProperty(ref _users, value); }
		}
	}
}