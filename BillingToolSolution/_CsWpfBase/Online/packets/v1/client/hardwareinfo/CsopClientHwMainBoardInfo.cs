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
	public sealed class CsopClientHwMainBoardInfo : CsoPacket
	{
		private string _manufacturer;
		private string _primaryBusType;
		private string _product;
		private string _secondaryBusType;
		private string _serialNumber;

		/// <summary>ctor</summary>
		public CsopClientHwMainBoardInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwMainBoardInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			Manufacturer = CsGlobal.Computer.Mainboard.Manufacturer;
			Product = CsGlobal.Computer.Mainboard.Product;
			SerialNumber = CsGlobal.Computer.Mainboard.SerialNumber;
			PrimaryBusType = CsGlobal.Computer.Mainboard.PrimaryBusType;
			SecondaryBusType = CsGlobal.Computer.Mainboard.SecondaryBusType;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientHwMainboardInfo; }
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
			Manufacturer = reader.String();
			Product = reader.String();
			SerialNumber = reader.String();
			PrimaryBusType = reader.String();
			SecondaryBusType = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.String(Manufacturer);
			writer.String(Product);
			writer.String(SerialNumber);
			writer.String(PrimaryBusType);
			writer.String(SecondaryBusType);
		}
		#endregion


		/// <summary>Name of the organization responsible for producing the physical element.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Baseboard part number defined by the manufacturer.</summary>
		public string Product
		{
			get { return _product; }
			set { SetProperty(ref _product, value); }
		}
		/// <summary>Manufacturer-allocated number used to identify the physical element.</summary>
		public string SerialNumber
		{
			get { return _serialNumber; }
			set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>Primary bus type of the motherboard.</summary>
		public string PrimaryBusType
		{
			get { return _primaryBusType; }
			set { SetProperty(ref _primaryBusType, value); }
		}
		/// <summary>Secondary bus type of the motherboard.</summary>
		public string SecondaryBusType
		{
			get { return _secondaryBusType; }
			set { SetProperty(ref _secondaryBusType, value); }
		}
	}
}