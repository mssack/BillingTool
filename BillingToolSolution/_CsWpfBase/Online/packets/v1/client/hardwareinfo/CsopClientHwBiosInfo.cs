// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo
{
	/// <summary>wrapper</summary>
	[Serializable]
	public sealed class CsopClientHwBiosInfo : CsoPacket
	{
		private UInt16[] _characteristics;
		private string _currentLanguage;
		private string _manufacturer;
		private string _name;
		private DateTime _releaseDate;
		private string _serialNumber;
		private string _smBiosVersion;
		private string _version;
		private string[] _versions;

		/// <summary>ctor</summary>
		public CsopClientHwBiosInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwBiosInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			Name = CsGlobal.Computer.Bios.Name;
			Version = CsGlobal.Computer.Bios.Version;
			Characteristics = CsGlobal.Computer.Bios.Characteristics;
			Versions = CsGlobal.Computer.Bios.Versions;
			CurrentLanguage = CsGlobal.Computer.Bios.CurrentLanguage;
			Manufacturer = CsGlobal.Computer.Bios.Manufacturer;
			ReleaseDate = CsGlobal.Computer.Bios.ReleaseDate;
			SerialNumber = CsGlobal.Computer.Bios.SerialNumber;
			SmBiosVersion = CsGlobal.Computer.Bios.SmBiosVersion;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientHwBiosInfo; }
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
			Name = reader.String();
			Version = reader.String();
			Characteristics = reader.ListOfUInt16().ToArray();
			Versions = reader.ListOfString().ToArray();
			CurrentLanguage = reader.String();
			Manufacturer = reader.String();
			ReleaseDate = reader.DateTime();
			SerialNumber = reader.String();
			SmBiosVersion = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.String(Name);
			writer.String(Version);
			writer.ListOfUInt16(Characteristics);
			writer.ListOfString(Versions);
			writer.String(CurrentLanguage);
			writer.String(Manufacturer);
			writer.DateTime(ReleaseDate);
			writer.String(SerialNumber);
			writer.String(SmBiosVersion);
		}
		#endregion


		/// <summary>Name used to identify this software element.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the Version.</summary>
		public string Version
		{
			get { return _version; }
			set { SetProperty(ref _version, value); }
		}
		/// <summary>Array of BIOS characteristics supported by the system as defined by the System Management BIOS Reference Specification.</summary>
		public UInt16[] Characteristics
		{
			get { return _characteristics; }
			set { SetProperty(ref _characteristics, value); }
		}
		/// <summary>
		///     Array of the complete system BIOS information. In many computers there can be several version strings that are stored in the registry and
		///     represent the system BIOS information.
		/// </summary>
		public string[] Versions
		{
			get { return _versions; }
			set { SetProperty(ref _versions, value); }
		}
		/// <summary>Name of the current BIOS language.</summary>
		public string CurrentLanguage
		{
			get { return _currentLanguage; }
			set { SetProperty(ref _currentLanguage, value); }
		}
		/// <summary>Manufacturer of this software element.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Release date of the Windows BIOS in the Coordinated Universal Time (UTC) format </summary>
		public DateTime ReleaseDate
		{
			get { return _releaseDate; }
			set { SetProperty(ref _releaseDate, value); }
		}
		/// <summary>Assigned serial number of the software element.</summary>
		public string SerialNumber
		{
			get { return _serialNumber; }
			set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>BIOS version as reported by SMBIOS.</summary>
		public string SmBiosVersion
		{
			get { return _smBiosVersion; }
			set { SetProperty(ref _smBiosVersion, value); }
		}
	}
}