using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.computer.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo.parts
{
	/// <summary>wrapper</summary>
	[Serializable]
	public sealed class CsopV1PartMemoryDevice : Base, CsoPacket.IPart
	{
		private string _bankLabel;
		private UInt64 _capacity;
		private UInt32 _configuredClockSpeed;
		private string _deviceLocator;
		private string _manufacturer;
		private CsgMemoryDevice.Types _memoryType;
		private UInt32 _speed;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			BankLabel = reader.String();
			Capacity = reader.UInt64();
			ConfiguredClockSpeed = reader.UInt32();
			DeviceLocator = reader.String();
			Manufacturer = reader.String();
			MemoryType = (CsgMemoryDevice.Types) reader.UInt16();
			Speed = reader.UInt32();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.String(BankLabel);
			writer.UInt64(Capacity);
			writer.UInt32(ConfiguredClockSpeed);
			writer.String(DeviceLocator);
			writer.String(Manufacturer);
			writer.UInt16((ushort) MemoryType);
			writer.UInt32(Speed);
		}
		#endregion


		/// <summary>Physically labeled bank where the memory is located.</summary>
		public string BankLabel
		{
			get { return _bankLabel; }
			set { SetProperty(ref _bankLabel, value); }
		}
		/// <summary>Total capacity of the physical memory—in bytes.</summary>
		public UInt64 Capacity
		{
			get { return _capacity; }
			set { SetProperty(ref _capacity, value); }
		}
		/// <summary>The configured clock speed of the memory device, in megahertz (MHz), or 0, if the speed is unknown.</summary>
		public UInt32 ConfiguredClockSpeed
		{
			get { return _configuredClockSpeed; }
			set { SetProperty(ref _configuredClockSpeed, value); }
		}
		/// <summary>Label of the socket or circuit board that holds the memory.</summary>
		public string DeviceLocator
		{
			get { return _deviceLocator; }
			set { SetProperty(ref _deviceLocator, value); }
		}
		/// <summary>Name of the organization responsible for producing the physical element.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>
		///     Type of physical memory. This is a CIM value that is mapped to the SMBIOS value. The SMBIOSMemoryType property contains the raw SMBIOS
		///     memory type.
		/// </summary>
		public CsgMemoryDevice.Types MemoryType
		{
			get { return _memoryType; }
			set { SetProperty(ref _memoryType, value); }
		}
		/// <summary>Speed of the physical memory—in nanoseconds.</summary>
		public UInt32 Speed
		{
			get { return _speed; }
			set { SetProperty(ref _speed, value); }
		}

		/// <summary>Creates from memoryDevice.</summary>
		public static CsopV1PartMemoryDevice From(CsgMemoryDevice device)
		{
			var deviceWrapper = new CsopV1PartMemoryDevice();

			deviceWrapper.BankLabel = device.BankLabel;
			deviceWrapper.Capacity = device.Capacity;
			deviceWrapper.ConfiguredClockSpeed = device.ConfiguredClockSpeed;
			deviceWrapper.DeviceLocator = device.DeviceLocator;
			deviceWrapper.Manufacturer = device.Manufacturer;
			deviceWrapper.MemoryType = device.MemoryType;
			deviceWrapper.Speed = device.Speed;

			return deviceWrapper;
		}
	}
}