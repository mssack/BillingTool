// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerBios : Base
	{
		private static CsgComputerBios _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerBios I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerBios());
				}
			}
		}
		[field: NonSerialized] private Dictionary<UInt16, string> _biosCharacteristicsTable;
		private UInt16[] _characteristics;
		private string _currentLanguage;
		private bool _isCollected;
		private string _manufacturer;
		private string _name;
		private DateTime _releaseDate;
		private string _serialNumber;
		private string _smBiosVersion;
		private string _version;
		private string[] _versions;

		private CsgComputerBios()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Name + " (" + Version + ")";
		}
		#endregion


		/// <summary>Converts a bios characteristic to text.</summary>
		public Dictionary<UInt16, string> BiosCharacteristicsTable
		{
			get
			{
				return _biosCharacteristicsTable ?? (_biosCharacteristicsTable = new Dictionary<ushort, string>()
				{
					{0, "Reserved"},
					{1, "Reserved"},
					{2, "Unknown"},
					{3, "BIOS Characteristics Not Supported"},
					{4, "ISA is supported"},
					{5, "MCA is supported"},
					{6, "EISA is supported"},
					{7, "PCI is supported"},
					{8, "PC Card (PCMCIA) is supported"},
					{9, "Plug and Play is supported"},
					{10, "APM is supported"},
					{11, "BIOS is Upgradeable (Flash)"},
					{12, "BIOS shadowing is allowed"},
					{13, "VL-VESA is supported"},
					{14, "ESCD support is available"},
					{15, "Boot from CD is supported"},
					{16, "Selectable Boot is supported"},
					{17, "BIOS ROM is socketed"},
					{18, "Boot From PC Card (PCMCIA) is supported"},
					{19, "EDD (Enhanced Disk Drive) Specification is supported"},
					{20, "Int 13h - Japanese Floppy for NEC 9800 1.2mb (3.5\", 1k Bytes/Sector, 360 RPM) is supported"},
					{21, "Int 13h - Japanese Floppy for Toshiba 1.2mb (3.5\", 360 RPM) is supported"},
					{22, "Int 13h - 5.25\" / 360 KB Floppy Services are supported"},
					{23, "Int 13h - 5.25\" /1.2MB Floppy Services are supported"},
					{24, "Int 13h - 3.5\" / 720 KB Floppy Services are supported"},
					{25, "Int 13h - 3.5\" / 2.88 MB Floppy Services are supported"},
					{26, "Int 5h, Print Screen Service is supported"},
					{27, "Int 9h, 8042 Keyboard services are supported"},
					{28, "Int 14h, Serial Services are supported"},
					{29, "Int 17h, printer services are supported"},
					{30, "Int 10h, CGA/Mono Video Services are supported"},
					{31, "NEC PC-98"},
					{32, "ACPI supported"},
					{33, "USB Legacy is supported"},
					{34, "AGP is supported"},
					{35, "I2O boot is supported"},
					{36, "LS-120 boot is supported"},
					{37, "ATAPI ZIP Drive boot is supported"},
					{38, "1394 boot is supported"},
					{39, "Smart Battery supported"},
				});
			}
		}
		/// <summary>Name used to identify this software element.</summary>
		public string Name
		{
			get
			{
				Reload(true);
				return _name;
			}
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the Version.</summary>
		public string Version
		{
			get
			{
				Reload(true);
				return _version;
			}
			private set { SetProperty(ref _version, value); }
		}
		/// <summary>Array of BIOS characteristics supported by the system as defined by the System Management BIOS Reference Specification.</summary>
		public UInt16[] Characteristics
		{
			get
			{
				Reload(true);
				return _characteristics;
			}
			private set { SetProperty(ref _characteristics, value); }
		}
		/// <summary>
		///     Array of the complete system BIOS information. In many computers there can be several version strings that are stored in the registry and
		///     represent the system BIOS information.
		/// </summary>
		public string[] Versions
		{
			get
			{
				Reload(true);
				return _versions;
			}
			private set { SetProperty(ref _versions, value); }
		}
		/// <summary>Name of the current BIOS language.</summary>
		public string CurrentLanguage
		{
			get
			{
				Reload(true);
				return _currentLanguage;
			}
			private set { SetProperty(ref _currentLanguage, value); }
		}
		/// <summary>Manufacturer of this software element.</summary>
		public string Manufacturer
		{
			get
			{
				Reload(true);
				return _manufacturer;
			}
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Release date of the Windows BIOS in the Coordinated Universal Time (UTC) format </summary>
		public DateTime ReleaseDate
		{
			get
			{
				Reload(true);
				return _releaseDate;
			}
			private set { SetProperty(ref _releaseDate, value); }
		}
		/// <summary>Assigned serial number of the software element.</summary>
		public string SerialNumber
		{
			get
			{
				Reload(true);
				return _serialNumber;
			}
			private set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>BIOS version as reported by SMBIOS.</summary>
		public string SmBiosVersion
		{
			get
			{
				Reload(true);
				return _smBiosVersion;
			}
			private set { SetProperty(ref _smBiosVersion, value); }
		}

		/// <summary>Reloads the hardware informations.</summary>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;


			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS").Get();
				foreach (var o in moc)
				{
					var mo = (ManagementObject) o;
					Name = mo.TryGet<string>("Name");
					Version = mo.TryGet<string>("Version");
					Characteristics = mo.TryGet<UInt16[]>("BiosCharacteristics");
					Versions = mo.TryGet<string[]>("BIOSVersion");
					CurrentLanguage = mo.TryGet<string>("CurrentLanguage");
					Manufacturer = mo.TryGet<string>("Manufacturer");
					ReleaseDate = mo.TryGet<DateTime>("ReleaseDate");
					SerialNumber = mo.TryGet<string>("SerialNumber");
					SmBiosVersion = mo.TryGet<string>("SMBIOSBIOSVersion");
					break;
				}
			}
			catch (Exception)
			{
			}


			_isCollected = true;
		}
	}
}