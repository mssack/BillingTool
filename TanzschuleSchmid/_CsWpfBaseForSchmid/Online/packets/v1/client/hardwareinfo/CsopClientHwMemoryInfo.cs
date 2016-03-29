// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Global;
using CsWpfBase.Online.packets.v1.client.hardwareinfo.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo
{
	/// <summary>wrapper</summary>
	[Serializable]
	public sealed class CsopClientHwMemoryInfo : CsoPacket
	{
		private List<CsopV1PartMemoryDevice> _devices;
		private UInt64 _total;

		/// <summary>ctor</summary>
		public CsopClientHwMemoryInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwMemoryInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			Total = CsGlobal.Computer.Memory.Total;
			Devices = CsGlobal.Computer.Memory.Devices.Select(CsopV1PartMemoryDevice.From).ToList();
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ClientHwMemoryInfo; }
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
			Total = reader.UInt64();
			Devices = reader.ListOfParts<CsopV1PartMemoryDevice>();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.UInt64(Total);
			writer.ListOfParts(Devices);
		}
		#endregion


		/// <summary>
		///     Total size of physical memory. Be aware that, under some circumstances, this property may not return an accurate value for the physical memory.
		///     For example, it is not accurate if the BIOS is using some of the physical memory. For an accurate value, use the Capacity property in
		///     Win32_PhysicalMemory instead.
		/// </summary>
		public UInt64 Total
		{
			get { return _total; }
			set { SetProperty(ref _total, value); }
		}
		/// <summary>Gets or sets the Devices.</summary>
		public List<CsopV1PartMemoryDevice> Devices
		{
			get { return _devices ?? (_devices = new List<CsopV1PartMemoryDevice>()); }
			set { SetProperty(ref _devices, value); }
		}
	}
}