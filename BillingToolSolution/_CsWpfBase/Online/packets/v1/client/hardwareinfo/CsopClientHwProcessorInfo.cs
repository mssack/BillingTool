// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;
using CsWpfBase.Global.computer;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo
{
	/// <summary>wrapper</summary>
	[Serializable]
	public sealed class CsopClientHwProcessorInfo : CsoPacket
	{
		private CsgComputerProcessor.Architectures _architecture;
		private CsgComputerProcessor.Familys _family;
		private string _manufacturer;
		private UInt32 _maxClockSpeed;
		private string _name;
		private UInt32 _numberOfCores;
		private UInt32 _numberOfLogicalProcessors;

		/// <summary>ctor</summary>
		public CsopClientHwProcessorInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwProcessorInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			Architecture = CsGlobal.Computer.Processor.Architecture;
			Family = CsGlobal.Computer.Processor.Family;
			Manufacturer = CsGlobal.Computer.Processor.Manufacturer;
			MaxClockSpeed = CsGlobal.Computer.Processor.MaxClockSpeed;
			Name = CsGlobal.Computer.Processor.Name;
			NumberOfCores = CsGlobal.Computer.Processor.NumberOfCores;
			NumberOfLogicalProcessors = CsGlobal.Computer.Processor.NumberOfLogicalProcessors;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ClientHwProcessorInfo; }
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
			Architecture = (CsgComputerProcessor.Architectures) reader.UInt16();
			Family = (CsgComputerProcessor.Familys) reader.UInt16();
			Manufacturer = reader.String();
			MaxClockSpeed = reader.UInt32();

			Name = reader.String();
			NumberOfCores = reader.UInt32();
			NumberOfLogicalProcessors = reader.UInt32();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.UInt16((ushort) Architecture);
			writer.UInt16((ushort) Family);
			writer.String(Manufacturer);
			writer.UInt32(MaxClockSpeed);

			writer.String(Name);
			writer.UInt32(NumberOfCores);
			writer.UInt32(NumberOfLogicalProcessors);
		}
		#endregion


		/// <summary>Processor architecture used by the platform.</summary>
		public CsgComputerProcessor.Architectures Architecture
		{
			get { return _architecture; }
			set { SetProperty(ref _architecture, value); }
		}
		/// <summary>Processor family type.</summary>
		public CsgComputerProcessor.Familys Family
		{
			get { return _family; }
			set { SetProperty(ref _family, value); }
		}
		/// <summary>Name of the processor manufacturer.</summary>
		/// <example>A. Datum Corporation</example>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Maximum speed of the processor, in MHz.</summary>
		public UInt32 MaxClockSpeed
		{
			get { return _maxClockSpeed; }
			set { SetProperty(ref _maxClockSpeed, value); }
		}
		/// <summary>Label by which the object is known. When this property is a subclass, it can be overridden to be a key property.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>
		///     Number of cores for the current instance of the processor. A core is a physical processor on the integrated circuit. For example, in a dual-core
		///     processor this property has a value of 2.
		/// </summary>
		public UInt32 NumberOfCores
		{
			get { return _numberOfCores; }
			set { SetProperty(ref _numberOfCores, value); }
		}
		/// <summary>
		///     Number of logical processors for the current instance of the processor. For processors capable of hyperthreading, this value includes    only the
		///     processors which have hyperthreading enabled.
		/// </summary>
		public UInt32 NumberOfLogicalProcessors
		{
			get { return _numberOfLogicalProcessors; }
			set { SetProperty(ref _numberOfLogicalProcessors, value); }
		}
	}
}