// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Management;
using CsWpfBase.Ev.Attributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents a physical memory device located on a computer system and available to the operating system.</summary>
	[Serializable]
	public class CsgMemoryDevice : Base
	{
		private string _bankLabel;
		private UInt64 _capacity;
		private UInt32 _configuredClockSpeed;
		private string _deviceLocator;
		private string _manufacturer;
		private Types _memoryType;
		private UInt32 _speed;
		private string _tag;

		internal CsgMemoryDevice()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return MemoryType + " " + Manufacturer + " (" + (Capacity/1024.0/1024.0) + " MB" + ")";
		}
		#endregion


		/// <summary>Unique identifier for the physical memory device that is represented by an instance of Win32_PhysicalMemory.</summary>
		public string Tag
		{
			get { return _tag; }
			private set { SetProperty(ref _tag, value); }
		}
		/// <summary>Physically labeled bank where the memory is located.</summary>
		public string BankLabel
		{
			get { return _bankLabel; }
			private set { SetProperty(ref _bankLabel, value); }
		}
		/// <summary>Total capacity of the physical memory—in bytes.</summary>
		public UInt64 Capacity
		{
			get { return _capacity; }
			private set { SetProperty(ref _capacity, value); }
		}
		/// <summary>The configured clock speed of the memory device, in megahertz (MHz), or 0, if the speed is unknown.</summary>
		public UInt32 ConfiguredClockSpeed
		{
			get { return _configuredClockSpeed; }
			private set { SetProperty(ref _configuredClockSpeed, value); }
		}
		/// <summary>Label of the socket or circuit board that holds the memory.</summary>
		public string DeviceLocator
		{
			get { return _deviceLocator; }
			private set { SetProperty(ref _deviceLocator, value); }
		}
		/// <summary>Name of the organization responsible for producing the physical element.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>
		///     Type of physical memory. This is a CIM value that is mapped to the SMBIOS value. The SMBIOSMemoryType property contains the raw SMBIOS
		///     memory type.
		/// </summary>
		public Types MemoryType
		{
			get { return _memoryType; }
			private set { SetProperty(ref _memoryType, value); }
		}
		/// <summary>Speed of the physical memory in nanoseconds.</summary>
		public UInt32 Speed
		{
			get { return _speed; }
			private set { SetProperty(ref _speed, value); }
		}

		internal static CsgMemoryDevice FromManagementObject(ManagementObject o)
		{
			var mem = new CsgMemoryDevice();
			mem.Load(o);
			return mem;
		}

		internal void Load(ManagementObject o)
		{
			Tag = o.TryGet<string>("Tag");
			BankLabel = o.TryGet<string>("BankLabel");
			Capacity = o.TryGet<UInt64>("Capacity");
			ConfiguredClockSpeed = o.TryGet<UInt32>("ConfiguredClockSpeed");
			DeviceLocator = o.TryGet<string>("DeviceLocator");
			Manufacturer = o.TryGet<string>("Manufacturer");
			MemoryType = (Types) o.TryGet<UInt16>("MemoryType");
			Speed = o.TryGet<UInt32>("Speed");
		}



		/// <summary>possible memory types</summary>
		public enum Types : short
		{
			/// <summary>Unknown</summary>
			[EnumDescription("Unknown")] Unknown = 0,
			/// <summary>Other</summary>
			[EnumDescription("Other")] Other = 1,
			/// <summary>DRAM</summary>
			[EnumDescription("DRAM")] Dram = 2,
			/// <summary>Synchronous DRAM</summary>
			[EnumDescription("Synchronous DRAM")] SynchronousDram = 3,
			/// <summary>Cache DRAM</summary>
			[EnumDescription("Cache DRAM")] CacheDram = 4,
			/// <summary>EDO</summary>
			[EnumDescription("EDO")] Edo = 5,
			/// <summary>EDRAM</summary>
			[EnumDescription("EDRAM")] Edram = 6,
			/// <summary>VRAM</summary>
			[EnumDescription("VRAM")] Vram = 7,
			/// <summary>SRAM</summary>
			[EnumDescription("SRAM")] Sram = 8,
			/// <summary>RAM</summary>
			[EnumDescription("RAM")] Ram = 9,
			/// <summary>ROM</summary>
			[EnumDescription("ROM")] Rom = 10,
			/// <summary>Flash</summary>
			[EnumDescription("Flash")] Flash = 11,
			/// <summary>EEPROM</summary>
			[EnumDescription("EEPROM")] Eeprom = 12,
			/// <summary>FEPROM</summary>
			[EnumDescription("FEPROM")] Feprom = 13,
			/// <summary>EPROM</summary>
			[EnumDescription("EPROM")] Eprom = 14,
			/// <summary>CDRAM</summary>
			[EnumDescription("CDRAM")] Cdram = 15,
			/// <summary>3DRAM</summary>
			[EnumDescription("3DRAM")] Dram3 = 16,
			/// <summary>SDRAM</summary>
			[EnumDescription("SDRAM")] Sdram = 17,
			/// <summary>SGRAM</summary>
			[EnumDescription("SGRAM")] Sgram = 18,
			/// <summary>RDRAM</summary>
			[EnumDescription("RDRAM")] Rdram = 19,
			/// <summary>DDR</summary>
			[EnumDescription("DDR")] Ddr = 20,
			/// <summary>DDR2</summary>
			[EnumDescription("DDR2")] Ddr2 = 21,
			/// <summary>DDR2</summary>
			[EnumDescription("DDR2 FB-DIMM")] Ddr2Fbdimm = 22,
			/// <summary>DDR3</summary>
			[EnumDescription("DDR3")] Ddr3 = 24,
			/// <summary>FBD2</summary>
			[EnumDescription("FBD2")] Fbd2 = 25,
		}
	}
}