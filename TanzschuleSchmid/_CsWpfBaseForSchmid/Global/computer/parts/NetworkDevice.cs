// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.ComponentModel;
using System.Management;
using CsWpfBase.Ev.Attributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents the capabilities and management capacity of a partitioned area of a physical disk on a computer system running Windows.</summary>
	[Serializable]
	public sealed class CsgNetworkDevice : Base
	{
		private UInt64 _activeMaximumTransmissionUnit;
		private string _deviceId;
		private string _driverDate;
		private string _driverDescription;
		private string _driverProvider;
		private string _driverVersion;
		private bool _fullDuplex;
		private IanaTypes _ianaType;
		private UInt32 _interfaceIndex;
		private string _name;
		private OperationalStates _operationalStatus;
		private string _permanentAddress;
		private UInt64 _speed;
		private bool _wdmInterface;

		private CsgNetworkDevice()
		{

		}

		/// <summary>Address or other identifying information to uniquely name the logical device.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			private set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>The interface type as defined by the Internet Assigned Names Authority (IANA).</summary>
		public IanaTypes IanaType
		{
			get { return _ianaType; }
			private set { SetProperty(ref _ianaType, value); }
		}
		/// <summary>
		///     The index that identifies the network interface. This index value may change when a network adapter is disabled and then enabled, and should not
		///     be considered persistent.
		/// </summary>
		public UInt32 InterfaceIndex
		{
			get { return _interfaceIndex; }
			private set { SetProperty(ref _interfaceIndex, value); }
		}
		/// <summary>
		///     The network address that is hardcoded into a port. This hardcoded address can be changed using a firmware upgrade or a software configuration.
		///     When this change is made, the field should be updated at the same time. PermanentAddress should be left blank if no hardcoded address exists for
		///     the network adapter.
		/// </summary>
		public string PermanentAddress
		{
			get { return _permanentAddress; }
			private set { SetProperty(ref _permanentAddress, value); }
		}
		/// <summary>Label by which the object is known. </summary>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>The active or negotiated maximum transmission unit (MTU) that can be supported.</summary>
		public UInt64 ActiveMaximumTransmissionUnit
		{
			get { return _activeMaximumTransmissionUnit; }
			private set { SetProperty(ref _activeMaximumTransmissionUnit, value); }
		}
		/// <summary>The bandwidth, in bits per second, of the port.</summary>
		public UInt64 Speed
		{
			get { return _speed; }
			private set { SetProperty(ref _speed, value); }
		}
		/// <summary>The network adapter driver date in YYYY-MM-DD format.</summary>
		public string DriverDate
		{
			get { return _driverDate; }
			private set { SetProperty(ref _driverDate, value); }
		}
		/// <summary>A string representing the network adapter driver version.</summary>
		public string DriverVersion
		{
			get { return _driverVersion; }
			private set { SetProperty(ref _driverVersion, value); }
		}
		/// <summary>The driver provider name.</summary>
		public string DriverProvider
		{
			get { return _driverProvider; }
			private set { SetProperty(ref _driverProvider, value); }
		}
		/// <summary>The description for the network adapter driver.</summary>
		public string DriverDescription
		{
			get { return _driverDescription; }
			private set { SetProperty(ref _driverDescription, value); }
		}
		/// <summary>Boolean that indicates that the port is operating in full duplex mode. </summary>
		public bool FullDuplex
		{
			get { return _fullDuplex; }
			private set { SetProperty(ref _fullDuplex, value); }
		}
		/// <summary>Current network interface operational status.</summary>
		public OperationalStates OperationalStatus
		{
			get { return _operationalStatus; }
			private set { SetProperty(ref _operationalStatus, value); }
		}
		/// <summary>The lower-level interface of the network adapter is a WDM bus driver such as USB.</summary>
		public bool WdmInterface
		{
			get { return _wdmInterface; }
			private set { SetProperty(ref _wdmInterface, value); }
		}

		internal static CsgNetworkDevice FromManagementObject(ManagementObject mo)
		{
			var device = new CsgNetworkDevice();
			device.Load(mo);
			return device;
		}

		internal void Load(ManagementObject mo)
		{
			DeviceId = mo.TryGet<string>("DeviceID");
			IanaType = (IanaTypes) mo.TryGet<uint>("InterfaceType");
			InterfaceIndex = mo.TryGet<UInt32>("InterfaceIndex");
			PermanentAddress = mo.TryGet<string>("PermanentAddress");
			Name = mo.TryGet<string>("Name");
			ActiveMaximumTransmissionUnit = mo.TryGet<UInt64>("ActiveMaximumTransmissionUnit");
			Speed = mo.TryGet<UInt64>("Speed");
			DriverDate = mo.TryGet<string>("DriverDate");
			DriverVersion = mo.TryGet<string>("DriverVersionString");
			DriverProvider = mo.TryGet<string>("DriverProvider");
			DriverDescription = mo.TryGet<string>("DriverDescription");
			FullDuplex = mo.TryGet<bool>("FullDuplex");
			OperationalStatus = (OperationalStates) mo.TryGet<uint>("InterfaceOperationalStatus");
			WdmInterface = mo.TryGet<bool>("WdmInterface");

		}



		/// <summary>
		/// Iana types
		/// </summary>
		[Serializable]
		public enum IanaTypes : uint
		{
			/// <summary>other - none of the following"</summary>
			[EnumDescription("other", "none of the following")] Other = 1,
			/// <summary>regular1822</summary>
			[EnumDescription("regular1822")] Regular1822 = 2,
			/// <summary>hdh1822</summary>
			[EnumDescription("hdh1822")] Hdh1822 = 3,
			/// <summary>ddnX25</summary>
			[EnumDescription("ddnX25")] DdnX25 = 4,
			/// <summary>rfc877x25</summary>
			[EnumDescription("rfc877x25")] Rfc877X25 = 5,
			/// <summary>ethernetCsmacd - for all ethernet-like interfaces, regardless of speed, as per RFC3635"</summary>
			[EnumDescription("ethernetCsmacd", "for all ethernet-like interfaces, regardless of speed, as per RFC3635")] EthernetCsmacd = 6,
			/// <summary>iso88023Csmacd - Deprecated via RFC3635 ethernetCsmacd (6) should be used instead"</summary>
			[EnumDescription("iso88023Csmacd", "Deprecated via RFC3635 ethernetCsmacd (6) should be used instead")] Iso88023Csmacd = 7,
			/// <summary>iso88024TokenBus</summary>
			[EnumDescription("iso88024TokenBus")] Iso88024TokenBus = 8,
			/// <summary>iso88025TokenRing</summary>
			[EnumDescription("iso88025TokenRing")] Iso88025TokenRing = 9,
			/// <summary>iso88026Man</summary>
			[EnumDescription("iso88026Man")] Iso88026Man = 10,
			/// <summary>starLan - Deprecated via RFC3635 ethernetCsmacd (6) should be used instead"</summary>
			[EnumDescription("starLan", "Deprecated via RFC3635 ethernetCsmacd (6) should be used instead")] StarLan = 11,
			/// <summary>proteon10Mbit</summary>
			[EnumDescription("proteon10Mbit")] Proteon10Mbit = 12,
			/// <summary>proteon80Mbit</summary>
			[EnumDescription("proteon80Mbit")] Proteon80Mbit = 13,
			/// <summary>hyperchannel</summary>
			[EnumDescription("hyperchannel")] Hyperchannel = 14,
			/// <summary>fddi</summary>
			[EnumDescription("fddi")] Fddi = 15,
			/// <summary>lapb</summary>
			[EnumDescription("lapb")] Lapb = 16,
			/// <summary>sdlc</summary>
			[EnumDescription("sdlc")] Sdlc = 17,
			/// <summary>ds1 - DS1-MIB"</summary>
			[EnumDescription("ds1", "DS1-MIB")] Ds1 = 18,
			/// <summary>e1 - Obsolete see DS1-MIB"</summary>
			[EnumDescription("e1", "Obsolete see DS1-MIB")] E1 = 19,
			/// <summary>basicISDN - no longer used see also RFC2127"</summary>
			[EnumDescription("basicISDN", "no longer used see also RFC2127")] BasicIsdn = 20,
			/// <summary>primaryISDN - no longer used see also RFC2127"</summary>
			[EnumDescription("primaryISDN", "no longer used see also RFC2127")] PrimaryIsdn = 21,
			/// <summary>propPointToPointSerial - proprietary serial"</summary>
			[EnumDescription("propPointToPointSerial", "proprietary serial")] PropPointToPointSerial = 22,
			/// <summary>ppp</summary>
			[EnumDescription("ppp")] Ppp = 23,
			/// <summary>softwareLoopback</summary>
			[EnumDescription("softwareLoopback")] SoftwareLoopback = 24,
			/// <summary>eon - CLNP over IP "</summary>
			[EnumDescription("eon", "CLNP over IP ")] Eon = 25,
			/// <summary>ethernet3Mbit</summary>
			[EnumDescription("ethernet3Mbit")] Ethernet3Mbit = 26,
			/// <summary>nsip - XNS over IP"</summary>
			[EnumDescription("nsip", "XNS over IP")] Nsip = 27,
			/// <summary>slip - generic SLIP"</summary>
			[EnumDescription("slip", "generic SLIP")] Slip = 28,
			/// <summary>ultra - ULTRA technologies"</summary>
			[EnumDescription("ultra", "ULTRA technologies")] Ultra = 29,
			/// <summary>ds3 - DS3-MIB"</summary>
			[EnumDescription("ds3", "DS3-MIB")] Ds3 = 30,
			/// <summary>sip - SMDS, coffee"</summary>
			[EnumDescription("sip", "SMDS, coffee")] Sip = 31,
			/// <summary>frameRelay - DTE only. "</summary>
			[EnumDescription("frameRelay", "DTE only. ")] FrameRelay = 32,
			/// <summary>rs232</summary>
			[EnumDescription("rs232")] Rs232 = 33,
			/// <summary>para - parallel-port"</summary>
			[EnumDescription("para", "parallel-port")] Para = 34,
			/// <summary>arcnet - arcnet"</summary>
			[EnumDescription("arcnet", "arcnet")] Arcnet = 35,
			/// <summary>arcnetPlus - arcnet plus"</summary>
			[EnumDescription("arcnetPlus", "arcnet plus")] ArcnetPlus = 36,
			/// <summary>atm - ATM cells"</summary>
			[EnumDescription("atm", "ATM cells")] Atm = 37,
			/// <summary>miox25</summary>
			[EnumDescription("miox25")] Miox25 = 38,
			/// <summary>sonet - SONET or SDH "</summary>
			[EnumDescription("sonet", "SONET or SDH ")] Sonet = 39,
			/// <summary>x25ple</summary>
			[EnumDescription("x25ple")] X25Ple = 40,
			/// <summary>iso88022llc</summary>
			[EnumDescription("iso88022llc")] Iso88022Llc = 41,
			/// <summary>localTalk</summary>
			[EnumDescription("localTalk")] LocalTalk = 42,
			/// <summary>smdsDxi</summary>
			[EnumDescription("smdsDxi")] SmdsDxi = 43,
			/// <summary>frameRelayService - FRNETSERV-MIB"</summary>
			[EnumDescription("frameRelayService", "FRNETSERV-MIB")] FrameRelayService = 44,
			/// <summary>v35</summary>
			[EnumDescription("v35")] V35 = 45,
			/// <summary>hssi</summary>
			[EnumDescription("hssi")] Hssi = 46,
			/// <summary>hippi</summary>
			[EnumDescription("hippi")] Hippi = 47,
			/// <summary>modem - Generic modem"</summary>
			[EnumDescription("modem", "Generic modem")] Modem = 48,
			/// <summary>aal5 - AAL5 over ATM"</summary>
			[EnumDescription("aal5", "AAL5 over ATM")] Aal5 = 49,
			/// <summary>sonetPath</summary>
			[EnumDescription("sonetPath")] SonetPath = 50,
			/// <summary>sonetVT</summary>
			[EnumDescription("sonetVT")] SonetVt = 51,
			/// <summary>SMDS InterCarrier Interface</summary>
			[EnumDescription("smdsIcip", "SMDS InterCarrier Interface")] SmdsIcip = 52,
			/// <summary>proprietary virtual/internal</summary>
			[EnumDescription("propVirtual", "proprietary virtual/internal")] PropVirtual = 53,
			/// <summary>proprietary multiplexing</summary>
			[EnumDescription("propMultiplexor", "proprietary multiplexing")] PropMultiplexor = 54,
			/// <summary>100BaseVG</summary>
			[EnumDescription("ieee80212", "100BaseVG")] Ieee80212 = 55,
			/// <summary>Fibre Channel</summary>
			[EnumDescription("fibreChannel", "Fibre Channel")] FibreChannel = 56,
			/// <summary>HIPPI interfaces </summary>
			[EnumDescription("hippiInterface", "HIPPI interfaces ")] HippiInterface = 57,
			/// <summary>Obsolete, use either frameRelay(32) or  frameRelayService(44).</summary>
			[EnumDescription("frameRelayInterconnect", "Obsolete, use either frameRelay(32) or  frameRelayService(44).")] FrameRelayInterconnect = 58,
			/// <summary>ATM Emulated LAN for 802.3</summary>
			[EnumDescription("aflane8023", "ATM Emulated LAN for 802.3")] Aflane8023 = 59,
			/// <summary>ATM Emulated LAN for 802.5</summary>
			[EnumDescription("aflane8025", "ATM Emulated LAN for 802.5")] Aflane8025 = 60,
			/// <summary>ATM Emulated circuit</summary>
			[EnumDescription("cctEmul", "ATM Emulated circuit")] CctEmul = 61,
			/// <summary>Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead</summary>
			[EnumDescription("fastEther", "Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead")] FastEther = 62,
			/// <summary>ISDN and X.25 </summary>
			[EnumDescription("isdn", "ISDN and X.25 ")] Isdn = 63,
			/// <summary>CCITT V.11/X.21 </summary>
			[EnumDescription("v11", "CCITT V.11/X.21 ")] V11 = 64,
			/// <summary>CCITT V.36</summary>
			[EnumDescription("v36", "CCITT V.36")] V36 = 65,
			/// <summary>CCITT G703 at 64Kbps</summary>
			[EnumDescription("g703at64k", "CCITT G703 at 64Kbps")] G703At64K = 66,
			/// <summary>Obsolete see DS1-MIB</summary>
			[EnumDescription("g703at2mb", "Obsolete see DS1-MIB")] G703At2Mb = 67,
			/// <summary>SNA QLLC </summary>
			[EnumDescription("qllc", "SNA QLLC ")] Qllc = 68,
			/// <summary>Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead</summary>
			[EnumDescription("fastEtherFX", "Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead")] FastEtherFx = 69,
			/// <summary>channel</summary>
			[EnumDescription("channel", "channel")] Channel = 70,
			/// <summary>radio spread spectrum </summary>
			[EnumDescription("ieee80211", "radio spread spectrum ")] Ieee80211 = 71,
			/// <summary>IBM System 360/370 OEMI Channel</summary>
			[EnumDescription("ibm370parChan", "IBM System 360/370 OEMI Channel")] Ibm370ParChan = 72,
			/// <summary>IBM Enterprise Systems Connection</summary>
			[EnumDescription("escon", "IBM Enterprise Systems Connection")] Escon = 73,
			/// <summary>Data Link Switching</summary>
			[EnumDescription("dlsw", "Data Link Switching")] Dlsw = 74,
			/// <summary>ISDN S/T interface</summary>
			[EnumDescription("isdns", "ISDN S/T interface")] Isdns = 75,
			/// <summary>ISDN U interface</summary>
			[EnumDescription("isdnu", "ISDN U interface")] Isdnu = 76,
			/// <summary>Link Access Protocol D</summary>
			[EnumDescription("lapd", "Link Access Protocol D")] Lapd = 77,
			/// <summary>IP Switching Objects</summary>
			[EnumDescription("ipSwitch", "IP Switching Objects")] IpSwitch = 78,
			/// <summary>Remote Source Route Bridging</summary>
			[EnumDescription("rsrb", "Remote Source Route Bridging")] Rsrb = 79,
			/// <summary>ATM Logical Port</summary>
			[EnumDescription("atmLogical", "ATM Logical Port")] AtmLogical = 80,
			/// <summary>Digital Signal Level 0</summary>
			[EnumDescription("ds0", "Digital Signal Level 0")] Ds0 = 81,
			/// <summary>group of ds0s on the same ds1</summary>
			[EnumDescription("ds0Bundle", "group of ds0s on the same ds1")] Ds0Bundle = 82,
			/// <summary>Bisynchronous Protocol</summary>
			[EnumDescription("bsc", "Bisynchronous Protocol")] Bsc = 83,
			/// <summary>Asynchronous Protocol</summary>
			[EnumDescription("async", "Asynchronous Protocol")] Async = 84,
			/// <summary>Combat Net Radio</summary>
			[EnumDescription("cnr", "Combat Net Radio")] Cnr = 85,
			/// <summary>ISO 802.5r DTR</summary>
			[EnumDescription("iso88025Dtr", "ISO 802.5r DTR")] Iso88025Dtr = 86,
			/// <summary>Ext Pos Loc Report Sys</summary>
			[EnumDescription("eplrs", "Ext Pos Loc Report Sys")] Eplrs = 87,
			/// <summary>Appletalk Remote Access Protocol</summary>
			[EnumDescription("arap", "Appletalk Remote Access Protocol")] Arap = 88,
			/// <summary>Proprietary Connectionless Protocol</summary>
			[EnumDescription("propCnls", "Proprietary Connectionless Protocol")] PropCnls = 89,
			/// <summary>CCITT-ITU X.29 PAD Protocol</summary>
			[EnumDescription("hostPad", "CCITT-ITU X.29 PAD Protocol")] HostPad = 90,
			/// <summary>CCITT-ITU X.3 PAD Facility</summary>
			[EnumDescription("termPad", "CCITT-ITU X.3 PAD Facility")] TermPad = 91,
			/// <summary>Multiproto Interconnect over FR</summary>
			[EnumDescription("frameRelayMPI", "Multiproto Interconnect over FR")] FrameRelayMpi = 92,
			/// <summary>CCITT-ITU X213</summary>
			[EnumDescription("x213", "CCITT-ITU X213")] X213 = 93,
			/// <summary>Asymmetric Digital Subscriber Loop</summary>
			[EnumDescription("adsl", "Asymmetric Digital Subscriber Loop")] Adsl = 94,
			/// <summary>Rate-Adapt. Digital Subscriber Loop</summary>
			[EnumDescription("radsl", "Rate-Adapt. Digital Subscriber Loop")] Radsl = 95,
			/// <summary>Symmetric Digital Subscriber Loop</summary>
			[EnumDescription("sdsl", "Symmetric Digital Subscriber Loop")] Sdsl = 96,
			/// <summary>Very H-Speed Digital Subscrib. Loop</summary>
			[EnumDescription("vdsl", "Very H-Speed Digital Subscrib. Loop")] Vdsl = 97,
			/// <summary>ISO 802.5 CRFP</summary>
			[EnumDescription("iso88025CRFPInt", "ISO 802.5 CRFP")] Iso88025CrfpInt = 98,
			/// <summary>Myricom Myrinet</summary>
			[EnumDescription("myrinet", "Myricom Myrinet")] Myrinet = 99,
			/// <summary>voice recEive and transMit</summary>
			[EnumDescription("voiceEM", "voice recEive and transMit")] VoiceEm = 100,
			/// <summary>voice Foreign Exchange Office</summary>
			[EnumDescription("voiceFXO", "voice Foreign Exchange Office")] VoiceFxo = 101,
			/// <summary>voice Foreign Exchange Station</summary>
			[EnumDescription("voiceFXS", "voice Foreign Exchange Station")] VoiceFxs = 102,
			/// <summary>voice encapsulation</summary>
			[EnumDescription("voiceEncap", "voice encapsulation")] VoiceEncap = 103,
			/// <summary>voice over IP encapsulation</summary>
			[EnumDescription("voiceOverIp", "voice over IP encapsulation")] VoiceOverIp = 104,
			/// <summary>ATM DXI</summary>
			[EnumDescription("atmDxi", "ATM DXI")] AtmDxi = 105,
			/// <summary>ATM FUNI</summary>
			[EnumDescription("atmFuni", "ATM FUNI")] AtmFuni = 106,
			/// <summary>ATM IMA </summary>
			[EnumDescription("atmIma ", "ATM IMA ")] AtmIma = 107,
			/// <summary>PPP Multilink Bundle</summary>
			[EnumDescription("pppMultilinkBundle", "PPP Multilink Bundle")] PppMultilinkBundle = 108,
			/// <summary>IBM ipOverCdlc</summary>
			[EnumDescription("ipOverCdlc ", "IBM ipOverCdlc")] IpOverCdlc = 109,
			/// <summary>IBM Common Link Access to Workstn</summary>
			[EnumDescription("ipOverClaw ", "IBM Common Link Access to Workstn")] IpOverClaw = 110,
			/// <summary>IBM stackToStack</summary>
			[EnumDescription("stackToStack ", "IBM stackToStack")] StackToStack = 111,
			/// <summary>IBM VIPA</summary>
			[EnumDescription("virtualIpAddress ", "IBM VIPA")] VirtualIpAddress = 112,
			/// <summary>IBM multi-protocol channel support</summary>
			[EnumDescription("mpc ", "IBM multi-protocol channel support")] Mpc = 113,
			/// <summary>IBM ipOverAtm</summary>
			[EnumDescription("ipOverAtm ", "IBM ipOverAtm")] IpOverAtm = 114,
			/// <summary>ISO 802.5j Fiber Token Ring</summary>
			[EnumDescription("iso88025Fiber ", "ISO 802.5j Fiber Token Ring")] Iso88025Fiber = 115,
			/// <summary>IBM twinaxial data link control</summary>
			[EnumDescription("tdlc ", "IBM twinaxial data link control")] Tdlc = 116,
			/// <summary>Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead</summary>
			[EnumDescription("gigabitEthernet ", "Obsoleted via RFC3635 ethernetCsmacd (6) should be used instead")] GigabitEthernet = 117,
			/// <summary>HDLC</summary>
			[EnumDescription("hdlc ", "HDLC")] Hdlc = 118,
			/// <summary>LAP F</summary>
			[EnumDescription("lapf ", "LAP F")] Lapf = 119,
			/// <summary>V.37</summary>
			[EnumDescription("v37 ", "V.37")] V37 = 120,
			/// <summary>Multi-Link Protocol</summary>
			[EnumDescription("x25mlp ", "Multi-Link Protocol")] X25Mlp = 121,
			/// <summary>X25 Hunt Group</summary>
			[EnumDescription("x25huntGroup ", "X25 Hunt Group")] X25HuntGroup = 122,
			/// <summary>Transp HDLC</summary>
			[EnumDescription("transpHdlc ", "Transp HDLC")] TranspHdlc = 123,
			/// <summary>Interleave channel</summary>
			[EnumDescription("interleave ", "Interleave channel")] Interleave = 124,
			/// <summary>Fast channel</summary>
			[EnumDescription("fast ", "Fast channel")] Fast = 125,
			/// <summary>IP (for APPN HPR in IP networks)</summary>
			[EnumDescription("ip ", "IP (for APPN HPR in IP networks)")] Ip = 126,
			/// <summary>CATV Mac Layer</summary>
			[EnumDescription("docsCableMaclayer ", "CATV Mac Layer")] DocsCableMaclayer = 127,
			/// <summary>CATV Downstream interface</summary>
			[EnumDescription("docsCableDownstream ", "CATV Downstream interface")] DocsCableDownstream = 128,
			/// <summary>CATV Upstream interface</summary>
			[EnumDescription("docsCableUpstream ", "CATV Upstream interface")] DocsCableUpstream = 129,
			/// <summary>Avalon Parallel Processor</summary>
			[EnumDescription("a12MppSwitch ", "Avalon Parallel Processor")] A12MppSwitch = 130,
			/// <summary>Encapsulation interface</summary>
			[EnumDescription("tunnel ", "Encapsulation interface")] Tunnel = 131,
			/// <summary>coffee pot</summary>
			[EnumDescription("coffee ", "coffee pot")] Coffee = 132,
			/// <summary>Circuit Emulation Service</summary>
			[EnumDescription("ces ", "Circuit Emulation Service")] Ces = 133,
			/// <summary>ATM Sub Interface</summary>
			[EnumDescription("atmSubInterface ", "ATM Sub Interface")] AtmSubInterface = 134,
			/// <summary>Layer 2 Virtual LAN using 802.1Q</summary>
			[EnumDescription("l2vlan ", "Layer 2 Virtual LAN using 802.1Q")] L2Vlan = 135,
			/// <summary>Layer 3 Virtual LAN using IP</summary>
			[EnumDescription("l3ipvlan ", "Layer 3 Virtual LAN using IP")] L3Ipvlan = 136,
			/// <summary>Layer 3 Virtual LAN using IPX</summary>
			[EnumDescription("l3ipxvlan ", "Layer 3 Virtual LAN using IPX")] L3Ipxvlan = 137,
			/// <summary>IP over Power Lines</summary>
			[EnumDescription("digitalPowerline ", "IP over Power Lines")] DigitalPowerline = 138,
			/// <summary>Multimedia Mail over IP</summary>
			[EnumDescription("mediaMailOverIp ", "Multimedia Mail over IP")] MediaMailOverIp = 139,
			/// <summary>Dynamic syncronous Transfer Mode</summary>
			[EnumDescription("dtm ", "Dynamic syncronous Transfer Mode")] Dtm = 140,
			/// <summary>Data Communications Network</summary>
			[EnumDescription("dcn ", "Data Communications Network")] Dcn = 141,
			/// <summary>IP Forwarding Interface</summary>
			[EnumDescription("ipForward ", "IP Forwarding Interface")] IpForward = 142,
			/// <summary>Multi-rate Symmetric DSL</summary>
			[EnumDescription("msdsl ", "Multi-rate Symmetric DSL")] Msdsl = 143,
			/// <summary>IEEE1394 High Performance Serial Bus</summary>
			[EnumDescription("ieee1394 ", "IEEE1394 High Performance Serial Bus")] Ieee1394 = 144,
			/// <summary>HIPPI-6400 </summary>
			[EnumDescription("if-gsn ", "HIPPI-6400 ")] Ifgsn = 145,
			/// <summary>DVB-RCC MAC Layer</summary>
			[EnumDescription("dvbRccMacLayer ", "DVB-RCC MAC Layer")] DvbRccMacLayer = 146,
			/// <summary>DVB-RCC Downstream Channel</summary>
			[EnumDescription("dvbRccDownstream ", "DVB-RCC Downstream Channel")] DvbRccDownstream = 147,
			/// <summary>DVB-RCC Upstream Channel</summary>
			[EnumDescription("dvbRccUpstream ", "DVB-RCC Upstream Channel")] DvbRccUpstream = 148,
			/// <summary>ATM Virtual Interface</summary>
			[EnumDescription("atmVirtual ", "ATM Virtual Interface")] AtmVirtual = 149,
			/// <summary>MPLS Tunnel Virtual Interface</summary>
			[EnumDescription("mplsTunnel ", "MPLS Tunnel Virtual Interface")] MplsTunnel = 150,
			/// <summary>Spatial Reuse Protocol</summary>
			[EnumDescription("srp ", "Spatial Reuse Protocol")] Srp = 151,
			/// <summary>Voice Over ATM</summary>
			[EnumDescription("voiceOverAtm ", "Voice Over ATM")] VoiceOverAtm = 152,
			/// <summary>Voice Over Frame Relay </summary>
			[EnumDescription("voiceOverFrameRelay ", "Voice Over Frame Relay ")] VoiceOverFrameRelay = 153,
			/// <summary>Digital Subscriber Loop over ISDN</summary>
			[EnumDescription("idsl ", "Digital Subscriber Loop over ISDN")] Idsl = 154,
			/// <summary>Avici Composite Link Interface</summary>
			[EnumDescription("compositeLink ", "Avici Composite Link Interface")] CompositeLink = 155,
			/// <summary>SS7 Signaling Link </summary>
			[EnumDescription("ss7SigLink ", "SS7 Signaling Link ")] Ss7SigLink = 156,
			/// <summary>Prop. P2P wireless interface</summary>
			[EnumDescription("propWirelessP2P ", "Prop. P2P wireless interface")] PropWirelessP2P = 157,
			/// <summary>Frame Forward Interface</summary>
			[EnumDescription("frForward ", "Frame Forward Interface")] FrForward = 158,
			/// <summary>Multiprotocol over ATM AAL5</summary>
			[EnumDescription("rfc1483 ", "Multiprotocol over ATM AAL5")] Rfc1483 = 159,
			/// <summary>USB Interface</summary>
			[EnumDescription("usb ", "USB Interface")] Usb = 160,
			/// <summary>IEEE 802.3ad Link Aggregate</summary>
			[EnumDescription("ieee8023adLag ", "IEEE 802.3ad Link Aggregate")] Ieee8023AdLag = 161,
			/// <summary>BGP Policy Accounting</summary>
			[EnumDescription("bgppolicyaccounting ", "BGP Policy Accounting")] Bgppolicyaccounting = 162,
			/// <summary>FRF .16 Multilink Frame Relay </summary>
			[EnumDescription("frf16MfrBundle ", "FRF .16 Multilink Frame Relay ")] Frf16MfrBundle = 163,
			/// <summary>H323 Gatekeeper</summary>
			[EnumDescription("h323Gatekeeper ", "H323 Gatekeeper")] H323Gatekeeper = 164,
			/// <summary>H323 Voice and Video Proxy</summary>
			[EnumDescription("h323Proxy ", "H323 Voice and Video Proxy")] H323Proxy = 165,
			/// <summary>MPLS</summary>
			[EnumDescription("mpls ", "MPLS")] Mpls = 166,
			/// <summary>Multi-frequency signaling link</summary>
			[EnumDescription("mfSigLink ", "Multi-frequency signaling link")] MfSigLink = 167,
			/// <summary>High Bit-Rate DSL - 2nd generation</summary>
			[EnumDescription("hdsl2 ", "High Bit-Rate DSL - 2nd generation")] Hdsl2 = 168,
			/// <summary>Multirate HDSL2</summary>
			[EnumDescription("shdsl ", "Multirate HDSL2")] Shdsl = 169,
			/// <summary>Facility Data Link 4Kbps on a DS1</summary>
			[EnumDescription("ds1FDL ", "Facility Data Link 4Kbps on a DS1")] Ds1Fdl = 170,
			/// <summary>Packet over SONET/SDH Interface</summary>
			[EnumDescription("pos ", "Packet over SONET/SDH Interface")] Pos = 171,
			/// <summary>DVB-ASI Input</summary>
			[EnumDescription("dvbAsiIn ", "DVB-ASI Input")] DvbAsiIn = 172,
			/// <summary>DVB-ASI Output </summary>
			[EnumDescription("dvbAsiOut ", "DVB-ASI Output ")] DvbAsiOut = 173,
			/// <summary>Power Line Communtications</summary>
			[EnumDescription("plc ", "Power Line Communtications")] Plc = 174,
			/// <summary>Non Facility Associated Signaling</summary>
			[EnumDescription("nfas ", "Non Facility Associated Signaling")] Nfas = 175,
			/// <summary>TR008</summary>
			[EnumDescription("tr008 ", "TR008")] Tr008 = 176,
			/// <summary>Remote Digital Terminal</summary>
			[EnumDescription("gr303RDT ", "Remote Digital Terminal")] Gr303Rdt = 177,
			/// <summary>Integrated Digital Terminal</summary>
			[EnumDescription("gr303IDT ", "Integrated Digital Terminal")] Gr303Idt = 178,
			/// <summary>ISUP</summary>
			[EnumDescription("isup ", "ISUP")] Isup = 179,
			/// <summary>Cisco proprietary Maclayer</summary>
			[EnumDescription("propDocsWirelessMaclayer ", "Cisco proprietary Maclayer")] PropDocsWirelessMaclayer = 180,
			/// <summary>Cisco proprietary Downstream</summary>
			[EnumDescription("propDocsWirelessDownstream ", "Cisco proprietary Downstream")] PropDocsWirelessDownstream = 181,
			/// <summary>Cisco proprietary Upstream</summary>
			[EnumDescription("propDocsWirelessUpstream ", "Cisco proprietary Upstream")] PropDocsWirelessUpstream = 182,
			/// <summary>HIPERLAN Type 2 Radio Interface</summary>
			[EnumDescription("hiperlan2 ", "HIPERLAN Type 2 Radio Interface")] Hiperlan2 = 183,
			/// <summary>
			///     PropBroadbandWirelessAccesspt2multipt use of this iftype for IEEE 802.16 WMAN interfaces as per IEEE Std 802.16f is deprecated and ifType 237
			///     should be used instead.
			/// </summary>
			[EnumDescription("propBWAp2Mp ", "PropBroadbandWirelessAccesspt2multipt use of this iftype for IEEE 802.16 WMAN interfaces as per IEEE Std 802.16f is deprecated and ifType 237 should be used instead.")] PropBwAp2Mp = 184,
			/// <summary>SONET Overhead Channel</summary>
			[EnumDescription("sonetOverheadChannel ", "SONET Overhead Channel")] SonetOverheadChannel = 185,
			/// <summary>Digital Wrapper</summary>
			[EnumDescription("digitalWrapperOverheadChannel ", "Digital Wrapper")] DigitalWrapperOverheadChannel = 186,
			/// <summary>ATM adaptation layer 2</summary>
			[EnumDescription("aal2 ", "ATM adaptation layer 2")] Aal2 = 187,
			/// <summary>MAC layer over radio links</summary>
			[EnumDescription("radioMAC ", "MAC layer over radio links")] RadioMac = 188,
			/// <summary>ATM over radio links </summary>
			[EnumDescription("atmRadio ", "ATM over radio links ")] AtmRadio = 189,
			/// <summary>Inter Machine Trunks</summary>
			[EnumDescription("imt ", "Inter Machine Trunks")] Imt = 190,
			/// <summary>Multiple Virtual Lines DSL</summary>
			[EnumDescription("mvl ", "Multiple Virtual Lines DSL")] Mvl = 191,
			/// <summary>Long Reach DSL</summary>
			[EnumDescription("reachDSL ", "Long Reach DSL")] ReachDsl = 192,
			/// <summary>Frame Relay DLCI End Point</summary>
			[EnumDescription("frDlciEndPt ", "Frame Relay DLCI End Point")] FrDlciEndPt = 193,
			/// <summary>ATM VCI End Point</summary>
			[EnumDescription("atmVciEndPt ", "ATM VCI End Point")] AtmVciEndPt = 194,
			/// <summary>Optical Channel</summary>
			[EnumDescription("opticalChannel ", "Optical Channel")] OpticalChannel = 195,
			/// <summary>Optical Transport</summary>
			[EnumDescription("opticalTransport ", "Optical Transport")] OpticalTransport = 196,
			/// <summary>Proprietary ATM </summary>
			[EnumDescription("propAtm ", "Proprietary ATM ")] PropAtm = 197,
			/// <summary>Voice Over Cable Interface</summary>
			[EnumDescription("voiceOverCable ", "Voice Over Cable Interface")] VoiceOverCable = 198,
			/// <summary>Infiniband</summary>
			[EnumDescription("infiniband ", "Infiniband")] Infiniband = 199,
			/// <summary>TE Link</summary>
			[EnumDescription("teLink ", "TE Link")] TeLink = 200,
			/// <summary>Q.2931</summary>
			[EnumDescription("q2931 ", "Q.2931")] Q2931 = 201,
			/// <summary>Virtual Trunk Group</summary>
			[EnumDescription("virtualTg ", "Virtual Trunk Group")] VirtualTg = 202,
			/// <summary>SIP Trunk Group</summary>
			[EnumDescription("sipTg ", "SIP Trunk Group")] SipTg = 203,
			/// <summary>SIP Signaling </summary>
			[EnumDescription("sipSig ", "SIP Signaling ")] SipSig = 204,
			/// <summary>CATV Upstream Channel</summary>
			[EnumDescription("docsCableUpstreamChannel ", "CATV Upstream Channel")] DocsCableUpstreamChannel = 205,
			/// <summary>Acorn Econet</summary>
			[EnumDescription("econet ", "Acorn Econet")] Econet = 206,
			/// <summary>FSAN 155Mb Symetrical PON interface</summary>
			[EnumDescription("pon155 ", "FSAN 155Mb Symetrical PON interface")] Pon155 = 207,
			/// <summary>FSAN622Mb Symetrical PON interface</summary>
			[EnumDescription("pon622 ", "FSAN622Mb Symetrical PON interface")] Pon622 = 208,
			/// <summary>Transparent bridge interface</summary>
			[EnumDescription("bridge ", "Transparent bridge interface")] Bridge = 209,
			/// <summary>Interface common to multiple lines </summary>
			[EnumDescription("linegroup ", "Interface common to multiple lines ")] Linegroup = 210,
			/// <summary>voice EM Feature Group D</summary>
			[EnumDescription("voiceEMFGD ", "voice E&M Feature Group D")] VoiceEmfgd = 211,
			/// <summary>voice FGD Exchange Access North American</summary>
			[EnumDescription("voiceFGDEANA ", "voice FGD Exchange Access North American")] VoiceFgdeana = 212,
			/// <summary>voice Direct Inward Dialing</summary>
			[EnumDescription("voiceDID ", "voice Direct Inward Dialing")] VoiceDid = 213,
			/// <summary>MPEG transport interface</summary>
			[EnumDescription("mpegTransport ", "MPEG transport interface")] MpegTransport = 214,
			/// <summary>6to4 interface (DEPRECATED)</summary>
			[EnumDescription("sixToFour ", "6to4 interface (DEPRECATED)")] SixToFour = 215,
			/// <summary>GTP (GPRS Tunneling Protocol)</summary>
			[EnumDescription("gtp ", "GTP (GPRS Tunneling Protocol)")] Gtp = 216,
			/// <summary>Paradyne EtherLoop 1</summary>
			[EnumDescription("pdnEtherLoop1 ", "Paradyne EtherLoop 1")] PdnEtherLoop1 = 217,
			/// <summary>Paradyne EtherLoop 2</summary>
			[EnumDescription("pdnEtherLoop2 ", "Paradyne EtherLoop 2")] PdnEtherLoop2 = 218,
			/// <summary>Optical Channel Group</summary>
			[EnumDescription("opticalChannelGroup ", "Optical Channel Group")] OpticalChannelGroup = 219,
			/// <summary>HomePNA ITU-T G.989</summary>
			[EnumDescription("homepna ", "HomePNA ITU-T G.989")] Homepna = 220,
			/// <summary>Generic Framing Procedure (GFP)</summary>
			[EnumDescription("gfp ", "Generic Framing Procedure (GFP)")] Gfp = 221,
			/// <summary>Layer 2 Virtual LAN using Cisco ISL</summary>
			[EnumDescription("ciscoISLvlan ", "Layer 2 Virtual LAN using Cisco ISL")] CiscoIsLvlan = 222,
			/// <summary>Acteleis proprietary MetaLOOP High Speed Link </summary>
			[EnumDescription("actelisMetaLOOP ", "Acteleis proprietary MetaLOOP High Speed Link ")] ActelisMetaLoop = 223,
			/// <summary>FCIP Link </summary>
			[EnumDescription("fcipLink ", "FCIP Link ")] FcipLink = 224,
			/// <summary>Resilient Packet Ring Interface Type</summary>
			[EnumDescription("rpr ", "Resilient Packet Ring Interface Type")] Rpr = 225,
			/// <summary>RF Qam Interface</summary>
			[EnumDescription("qam ", "RF Qam Interface")] Qam = 226,
			/// <summary>Link Management Protocol</summary>
			[EnumDescription("lmp ", "Link Management Protocol")] Lmp = 227,
			/// <summary>Cambridge Broadband Networks Limited VectaStar</summary>
			[EnumDescription("cblVectaStar ", "Cambridge Broadband Networks Limited VectaStar")] CblVectaStar = 228,
			/// <summary>CATV Modular CMTS Downstream Interface</summary>
			[EnumDescription("docsCableMCmtsDownstream ", "CATV Modular CMTS Downstream Interface")] DocsCableMCmtsDownstream = 229,
			/// <summary>Asymmetric Digital Subscriber Loop Version 2  (DEPRECATED/OBSOLETED - please use adsl2plus 238 instead)</summary>
			[EnumDescription("adsl2 ", "Asymmetric Digital Subscriber Loop Version 2  (DEPRECATED/OBSOLETED - please use adsl2plus 238 instead)")] Adsl2 = 230,
			/// <summary>MACSecControlled </summary>
			[EnumDescription("macSecControlledIF ", "MACSecControlled ")] MacSecControlledIf = 231,
			/// <summary>MACSecUncontrolled</summary>
			[EnumDescription("macSecUncontrolledIF ", "MACSecUncontrolled")] MacSecUncontrolledIf = 232,
			/// <summary>Avici Optical Ethernet Aggregate</summary>
			[EnumDescription("aviciOpticalEther ", "Avici Optical Ethernet Aggregate")] AviciOpticalEther = 233,
			/// <summary>atmbond</summary>
			[EnumDescription("atmbond ", "atmbond")] Atmbond = 234,
			/// <summary>voice FGD Operator Services</summary>
			[EnumDescription("voiceFGDOS ", "voice FGD Operator Services")] VoiceFgdos = 235,
			/// <summary>MultiMedia over Coax Alliance (MoCA) Interface as documented in information provided privately to IANA</summary>
			[EnumDescription("mocaVersion1 ", "MultiMedia over Coax Alliance (MoCA) Interface as documented in information provided privately to IANA")] MocaVersion1 = 236,
			/// <summary>IEEE 802.16 WMAN interface</summary>
			[EnumDescription("ieee80216WMAN ", "IEEE 802.16 WMAN interface")] Ieee80216Wman = 237,
			/// <summary>Asymmetric Digital Subscriber Loop Version 2,  Version 2 Plus and all variants</summary>
			[EnumDescription("adsl2plus ", "Asymmetric Digital Subscriber Loop Version 2,  Version 2 Plus and all variants")] Adsl2Plus = 238,
			/// <summary>DVB-RCS MAC Layer</summary>
			[EnumDescription("dvbRcsMacLayer ", "DVB-RCS MAC Layer")] DvbRcsMacLayer = 239,
			/// <summary>DVB Satellite TDM</summary>
			[EnumDescription("dvbTdm ", "DVB Satellite TDM")] DvbTdm = 240,
			/// <summary>DVB-RCS TDMA</summary>
			[EnumDescription("dvbRcsTdma ", "DVB-RCS TDMA")] DvbRcsTdma = 241,
			/// <summary>LAPS based on ITU-T X.86/Y.1323</summary>
			[EnumDescription("x86Laps ", "LAPS based on ITU-T X.86/Y.1323")] X86Laps = 242,
			/// <summary>3GPP WWAN</summary>
			[EnumDescription("wwanPP ", "3GPP WWAN")] WwanPp = 243,
			/// <summary>3GPP2 WWAN</summary>
			[EnumDescription("wwanPP2 ", "3GPP2 WWAN")] WwanPp2 = 244,
			/// <summary>voice P-phone EBS physical interface</summary>
			[EnumDescription("voiceEBS ", "voice P-phone EBS physical interface")] VoiceEbs = 245,
			/// <summary>Pseudowire interface type</summary>
			[EnumDescription("ifPwType ", "Pseudowire interface type")] IfPwType = 246,
			/// <summary>Internal LAN on a bridge per IEEE 802.1ap</summary>
			[EnumDescription("ilan ", "Internal LAN on a bridge per IEEE 802.1ap")] Ilan = 247,
			/// <summary>Provider Instance Port on a bridge per IEEE 802.1ah PBB</summary>
			[EnumDescription("pip ", "Provider Instance Port on a bridge per IEEE 802.1ah PBB")] Pip = 248,
			/// <summary>Alcatel-Lucent Ethernet Link Protection</summary>
			[EnumDescription("aluELP ", "Alcatel-Lucent Ethernet Link Protection")] AluElp = 249,
			/// <summary>Gigabit-capable passive optical networks (G-PON) as per ITU-T G.948</summary>
			[EnumDescription("gpon ", "Gigabit-capable passive optical networks (G-PON) as per ITU-T G.948")] Gpon = 250,
			/// <summary>Very high speed digital subscriber line Version 2 (as per ITU-T Recommendation G.993.2)</summary>
			[EnumDescription("vdsl2 ", "Very high speed digital subscriber line Version 2 (as per ITU-T Recommendation G.993.2)")] Vdsl2 = 251,
			/// <summary>WLAN Profile Interface</summary>
			[EnumDescription("capwapDot11Profile ", "WLAN Profile Interface")] CapwapDot11Profile = 252,
			/// <summary>WLAN BSS Interface</summary>
			[EnumDescription("capwapDot11Bss ", "WLAN BSS Interface")] CapwapDot11Bss = 253,
			/// <summary>WTP Virtual Radio Interface</summary>
			[EnumDescription("capwapWtpVirtualRadio ", "WTP Virtual Radio Interface")] CapwapWtpVirtualRadio = 254,
			/// <summary>bitsport</summary>
			[EnumDescription("bits ", "bitsport")] Bits = 255,
			/// <summary>DOCSIS CATV Upstream RF Port</summary>
			[EnumDescription("docsCableUpstreamRfPort ", "DOCSIS CATV Upstream RF Port")] DocsCableUpstreamRfPort = 256,
			/// <summary>CATV downstream RF port</summary>
			[EnumDescription("cableDownstreamRfPort ", "CATV downstream RF port")] CableDownstreamRfPort = 257,
			/// <summary>VMware Virtual Network Interface</summary>
			[EnumDescription("vmwareVirtualNic ", "VMware Virtual Network Interface")] VmwareVirtualNic = 258,
			/// <summary>IEEE 802.15.4 WPAN interface</summary>
			[EnumDescription("ieee802154 ", "IEEE 802.15.4 WPAN interface")] Ieee802154 = 259,
			/// <summary>OTN Optical Data Unit</summary>
			[EnumDescription("otnOdu ", "OTN Optical Data Unit")] OtnOdu = 260,
			/// <summary>OTN Optical channel Transport Unit</summary>
			[EnumDescription("otnOtu ", "OTN Optical channel Transport Unit")] OtnOtu = 261,
			/// <summary>VPLS Forwarding Instance Interface Type</summary>
			[EnumDescription("ifVfiType ", "VPLS Forwarding Instance Interface Type")] IfVfiType = 262,
			/// <summary>G.998.1 bonded interface</summary>
			[EnumDescription("g9981 ", "G.998.1 bonded interface")] G9981 = 263,
			/// <summary>G.998.2 bonded interface</summary>
			[EnumDescription("g9982 ", "G.998.2 bonded interface")] G9982 = 264,
			/// <summary>G.998.3 bonded interface</summary>
			[EnumDescription("g9983 ", "G.998.3 bonded interface")] G9983 = 265,
			/// <summary>Ethernet Passive Optical Networks (E-PON)</summary>
			[EnumDescription("aluEpon ", "Ethernet Passive Optical Networks (E-PON)")] AluEpon = 266,
			/// <summary>EPON Optical Network Unit</summary>
			[EnumDescription("aluEponOnu ", "EPON Optical Network Unit")] AluEponOnu = 267,
			/// <summary>EPON physical User to Network interface</summary>
			[EnumDescription("aluEponPhysicalUni ", "EPON physical User to Network interface")] AluEponPhysicalUni = 268,
			/// <summary>The emulation of a point-to-point link over the EPON layer</summary>
			[EnumDescription("aluEponLogicalLink ", "The emulation of a point-to-point link over the EPON layer")] AluEponLogicalLink = 269,
			/// <summary>GPON Optical Network Unit</summary>
			[EnumDescription("aluGponOnu ", "GPON Optical Network Unit")] AluGponOnu = 270,
			/// <summary>GPON physical User to Network interface</summary>
			[EnumDescription("aluGponPhysicalUni ", "GPON physical User to Network interface")] AluGponPhysicalUni = 271,
			/// <summary>VMware NIC Team</summary>
			[EnumDescription("vmwareNicTeam ", "VMware NIC Team")] VmwareNicTeam = 272,
			/// <summary>CATV Downstream OFDM interface</summary>
			[EnumDescription("docsOfdmDownstream ", "CATV Downstream OFDM interface")] DocsOfdmDownstream = 277,
			/// <summary>CATV Upstream OFDMA interface</summary>
			[EnumDescription("docsOfdmaUpstream ", "CATV Upstream OFDMA interface")] DocsOfdmaUpstream = 278,
			/// <summary>G.fast port</summary>
			[EnumDescription("gfast ", "G.fast port")] Gfast = 279,
			/// <summary>SDCI (IO-Link)</summary>
			[EnumDescription("sdci ", "SDCI (IO-Link)")] Sdci = 280,
		}



		/// <summary>Interface operational states</summary>
		[Serializable]
		public enum OperationalStates : uint
		{
			/// <summary>Up</summary>
			[Description("Up")] Up = 1,
			/// <summary>Down</summary>
			[Description("Down")] Down = 2,
			/// <summary>Testing</summary>
			[Description("Testing")] Testing = 3,
			/// <summary>Unknown</summary>
			[Description("Unknown")] Unknown = 4,
			/// <summary>Dormant</summary>
			[Description("Dormant")] Dormant = 5,
			/// <summary>Not Present</summary>
			[Description("Not Present")] NotPresent = 6,
			/// <summary>Lower layer down </summary>
			[Description("Lower layer down ")] LowerLayerDown = 7,
		}
	}
}