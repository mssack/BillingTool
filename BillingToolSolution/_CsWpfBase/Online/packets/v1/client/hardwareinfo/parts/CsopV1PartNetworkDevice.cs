// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.computer.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo.parts
{
	/// <summary>wrapper</summary>
	[Serializable]
	public class CsopV1PartNetworkDevice : Base, CsoPacket.IPart
	{
		private UInt64 _activeMaximumTransmissionUnit;
		private string _deviceId;
		private string _driverDate;
		private string _driverDescription;
		private string _driverProvider;
		private string _driverVersion;
		private bool _fullDuplex;
		private CsgNetworkDevice.IanaTypes _ianaType;
		private UInt32 _interfaceIndex;
		private string _name;
		private CsgNetworkDevice.OperationalStates _operationalStatus;
		private string _permanentAddress;
		private UInt64 _speed;
		private bool _wdmInterface;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			DeviceId = reader.String();
			IanaType = (CsgNetworkDevice.IanaTypes) reader.UInt32();
			InterfaceIndex = reader.UInt32();
			PermanentAddress = reader.String();
			Name = reader.String();
			ActiveMaximumTransmissionUnit = reader.UInt64();
			Speed = reader.UInt64();
			DriverDate = reader.String();
			DriverVersion = reader.String();
			DriverProvider = reader.String();
			DriverDescription = reader.String();
			FullDuplex = reader.Byte() == 1;
			OperationalStatus = (CsgNetworkDevice.OperationalStates) reader.UInt32();
			WdmInterface = reader.Byte() == 1;
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.String(DeviceId);
			writer.UInt32((uint) IanaType);
			writer.UInt32(InterfaceIndex);
			writer.String(PermanentAddress);
			writer.String(Name);
			writer.UInt64(ActiveMaximumTransmissionUnit);
			writer.UInt64(Speed);
			writer.String(DriverDate);
			writer.String(DriverVersion);
			writer.String(DriverProvider);
			writer.String(DriverDescription);
			writer.Byte((byte) (FullDuplex ? 1 : 0));
			writer.UInt32((uint) OperationalStatus);
			writer.Byte((byte) (WdmInterface ? 1 : 0));
		}
		#endregion


		/// <summary>Address or other identifying information to uniquely name the logical device.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>The interface type as defined by the Internet Assigned Names Authority (IANA).</summary>
		public CsgNetworkDevice.IanaTypes IanaType
		{
			get { return _ianaType; }
			set { SetProperty(ref _ianaType, value); }
		}
		/// <summary>
		///     The index that identifies the network interface. This index value may change when a network adapter is disabled and then enabled, and should not
		///     be considered persistent.
		/// </summary>
		public UInt32 InterfaceIndex
		{
			get { return _interfaceIndex; }
			set { SetProperty(ref _interfaceIndex, value); }
		}
		/// <summary>
		///     The network address that is hardcoded into a port. This hardcoded address can be changed using a firmware upgrade or a software configuration.
		///     When this change is made, the field should be updated at the same time. PermanentAddress should be left blank if no hardcoded address exists for
		///     the network adapter.
		/// </summary>
		public string PermanentAddress
		{
			get { return _permanentAddress; }
			set { SetProperty(ref _permanentAddress, value); }
		}
		/// <summary>Label by which the object is known. </summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>The active or negotiated maximum transmission unit (MTU) that can be supported.</summary>
		public UInt64 ActiveMaximumTransmissionUnit
		{
			get { return _activeMaximumTransmissionUnit; }
			set { SetProperty(ref _activeMaximumTransmissionUnit, value); }
		}
		/// <summary>The bandwidth, in bits per second, of the port.</summary>
		public UInt64 Speed
		{
			get { return _speed; }
			set { SetProperty(ref _speed, value); }
		}
		/// <summary>The network adapter driver date in YYYY-MM-DD format.</summary>
		public string DriverDate
		{
			get { return _driverDate; }
			set { SetProperty(ref _driverDate, value); }
		}
		/// <summary>A string representing the network adapter driver version.</summary>
		public string DriverVersion
		{
			get { return _driverVersion; }
			set { SetProperty(ref _driverVersion, value); }
		}
		/// <summary>The driver provider name.</summary>
		public string DriverProvider
		{
			get { return _driverProvider; }
			set { SetProperty(ref _driverProvider, value); }
		}
		/// <summary>The description for the network adapter driver.</summary>
		public string DriverDescription
		{
			get { return _driverDescription; }
			set { SetProperty(ref _driverDescription, value); }
		}
		/// <summary>Boolean that indicates that the port is operating in full duplex mode. </summary>
		public bool FullDuplex
		{
			get { return _fullDuplex; }
			set { SetProperty(ref _fullDuplex, value); }
		}
		/// <summary>Current network interface operational status.</summary>
		public CsgNetworkDevice.OperationalStates OperationalStatus
		{
			get { return _operationalStatus; }
			set { SetProperty(ref _operationalStatus, value); }
		}
		/// <summary>The lower-level interface of the network adapter is a WDM bus driver such as USB.</summary>
		public bool WdmInterface
		{
			get { return _wdmInterface; }
			set { SetProperty(ref _wdmInterface, value); }
		}

		/// <summary>Creates an instance from an <see cref="CsgNetworkDevice" />.</summary>
		public static CsopV1PartNetworkDevice From(CsgNetworkDevice device)
		{
			var rv = new CsopV1PartNetworkDevice();

			rv.DeviceId = device.DeviceId;
			rv.IanaType = device.IanaType;
			rv.InterfaceIndex = device.InterfaceIndex;
			rv.PermanentAddress = device.PermanentAddress;
			rv.Name = device.Name;
			rv.ActiveMaximumTransmissionUnit = device.ActiveMaximumTransmissionUnit;
			rv.Speed = device.Speed;
			rv.DriverDate = device.DriverDate;
			rv.DriverVersion = device.DriverVersion;
			rv.DriverProvider = device.DriverProvider;
			rv.DriverDescription = device.DriverDescription;
			rv.FullDuplex = device.FullDuplex;
			rv.OperationalStatus = device.OperationalStatus;
			rv.WdmInterface = device.WdmInterface;

			return rv;
		}
	}
}