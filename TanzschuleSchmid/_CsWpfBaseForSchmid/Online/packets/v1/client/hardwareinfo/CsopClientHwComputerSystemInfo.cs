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
	public sealed class CsopClientHwComputerSystemInfo : CsoPacket
	{
		private string _manufacturer;
		private string _model;
		private bool _partOfDomain;
		private string _systemFamily;
		private string _systemSkuNumber;
		private string _workgroup;

		/// <summary>ctor</summary>
		public CsopClientHwComputerSystemInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwComputerSystemInfo(bool defaultData)
		{
			if (!defaultData)
				return;
			Manufacturer = CsGlobal.Computer.System.Manufacturer;
			Model = CsGlobal.Computer.System.Model;
			SystemFamily = CsGlobal.Computer.System.SystemFamily;
			SystemSkuNumber = CsGlobal.Computer.System.SystemSkuNumber;
			PartOfDomain = CsGlobal.Computer.System.PartOfDomain;
			Workgroup = CsGlobal.Computer.System.Workgroup;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ClientHwComputerSystemInfo; }
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
			Model = reader.String();
			SystemFamily = reader.String();
			SystemSkuNumber = reader.String();
			PartOfDomain = reader.Byte() == 1;
			Workgroup = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.String(Manufacturer);
			writer.String(Model);
			writer.String(SystemFamily);
			writer.String(SystemSkuNumber);
			writer.Byte((byte) (PartOfDomain ? 1 : 0));
			writer.String(Workgroup);
		}
		#endregion


		/// <summary>Gets or sets the Manufacturer.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Gets or sets the Model.</summary>
		public string Model
		{
			get { return _model; }
			set { SetProperty(ref _model, value); }
		}
		/// <summary>Gets or sets the SystemFamily.</summary>
		public string SystemFamily
		{
			get { return _systemFamily; }
			set { SetProperty(ref _systemFamily, value); }
		}
		/// <summary>Gets or sets the SystemSkuNumber.</summary>
		public string SystemSkuNumber
		{
			get { return _systemSkuNumber; }
			set { SetProperty(ref _systemSkuNumber, value); }
		}
		/// <summary>Gets or sets the PartOfDomain.</summary>
		public bool PartOfDomain
		{
			get { return _partOfDomain; }
			set { SetProperty(ref _partOfDomain, value); }
		}
		/// <summary>Gets or sets the Workgroup.</summary>
		public string Workgroup
		{
			get { return _workgroup; }
			set { SetProperty(ref _workgroup, value); }
		}
	}
}