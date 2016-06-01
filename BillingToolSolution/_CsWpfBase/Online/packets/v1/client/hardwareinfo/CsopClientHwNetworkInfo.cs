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
	public sealed class CsopClientHwNetworkInfo : CsoPacket
	{
		private List<CsopV1PartNetworkDevice> _devices;

		/// <summary>ctor</summary>
		public CsopClientHwNetworkInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwNetworkInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			Devices = CsGlobal.Computer.Network.Devices.Select(CsopV1PartNetworkDevice.From).ToList();
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientHwNetworkInfo; }
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
			Devices = reader.ListOfParts<CsopV1PartNetworkDevice>();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.ListOfParts(Devices);
		}
		#endregion


		/// <summary>Gets or sets the Devices.</summary>
		public List<CsopV1PartNetworkDevice> Devices
		{
			get { return _devices ?? (_devices = new List<CsopV1PartNetworkDevice>()); }
			set { SetProperty(ref _devices, value); }
		}
	}
}