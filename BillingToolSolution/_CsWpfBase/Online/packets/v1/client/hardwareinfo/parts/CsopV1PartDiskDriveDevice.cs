// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;
using CsWpfBase.Global.computer.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo.parts
{
	/// <summary>wrapper</summary>
	[Serializable]
	public class CsopV1PartDiskDriveDevice : Base, CsoPacket.IPart
	{


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			Index = reader.UInt32();
			InterfaceType = reader.String();
			Manufacturer = reader.String();
			Model = reader.String();
			FirmwareRevision = reader.String();
			Capabilities = reader.ListOfString().ToArray();
			MediaLoaded = reader.Byte() == 1;
			MediaType = reader.String();
			PartitionCount = reader.UInt32();
			SerialNumber = reader.String();
			Size = reader.UInt64();
			Status = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.UInt32(Index);
			writer.String(InterfaceType);
			writer.String(Manufacturer);
			writer.String(Model);
			writer.String(FirmwareRevision);
			writer.ListOfString(Capabilities);
			writer.Byte((byte) (MediaLoaded?1:0));
			writer.String(MediaType);
			writer.UInt32(PartitionCount);
			writer.String(SerialNumber);
			writer.UInt64(Size);
			writer.String(Status);
		}
		#endregion

		
		private string[] _capabilities;
		private string _firmwareRevision;
		private UInt32 _index;
		private string _interfaceType;
		private string _manufacturer;
		private bool _mediaLoaded;
		private string _mediaType;
		private string _model;
		private UInt32 _partitionCount;
		private string _serialNumber;
		private UInt64 _size;
		private string _status;


		/// <summary>
		///     Physical drive number of the given drive. This property is filled by the GetDriveMapInfo method. A value of 0xFF indicates that the given drive
		///     does not map to a physical drive.
		/// </summary>
		public UInt32 Index
		{
			get { return _index; }
			set { SetProperty(ref _index, value); }
		}
		/// <summary>Interface type of physical disk drive. Values can be SCSI, HDC, IDE, USB, 1394 </summary>
		public string InterfaceType
		{
			get { return _interfaceType; }
			set { SetProperty(ref _interfaceType, value); }
		}
		/// <summary>Name of the disk drive manufacturer.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Manufacturer's model number of the disk drive.</summary>
		public string Model
		{
			get { return _model; }
			set { SetProperty(ref _model, value); }
		}
		/// <summary>Revision for the disk drive firmware that is assigned by the manufacturer.</summary>
		public string FirmwareRevision
		{
			get { return _firmwareRevision; }
			set { SetProperty(ref _firmwareRevision, value); }
		}
		/// <summary>
		///     Array of capabilities of the media access device. For example, the device may support random access (3), removable media (7), and automatic
		///     cleaning (9). Each capability is seperated by a ;
		/// </summary>
		public string[] Capabilities
		{
			get { return _capabilities; }
			set { SetProperty(ref _capabilities, value); }
		}
		/// <summary>
		///     If True, the media for a disk drive is loaded, which means that the device has a readable file system and is accessible. For fixed disk drives,
		///     this property will always be TRUE.
		/// </summary>
		public bool MediaLoaded
		{
			get { return _mediaLoaded; }
			set { SetProperty(ref _mediaLoaded, value); }
		}
		/// <summary>
		///     Type of media used or accessed by this device. Values: 'External hard disk media', 'Removable media other than floppy', 'Fixed hard disk media',
		///     'Format is unknown', 'Removable media ("Removable media other than floppy")', 'Fixed hard disk ("Fixed hard disk media")', 'Unknown ("Format is
		///     unknown")'
		/// </summary>
		public string MediaType
		{
			get { return _mediaType; }
			set { SetProperty(ref _mediaType, value); }
		}
		/// <summary>Number of partitions on this physical disk drive that are recognized by the operating system.</summary>
		public UInt32 PartitionCount
		{
			get { return _partitionCount; }
			set { SetProperty(ref _partitionCount, value); }
		}
		/// <summary>Number allocated by the manufacturer to identify the physical media. </summary>
		public string SerialNumber
		{
			get { return _serialNumber; }
			set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>
		///     Size of the disk drive. It is calculated by multiplying the total number of cylinders, tracks in each cylinder, sectors in each track, and bytes
		///     in each sector.
		/// </summary>
		public UInt64 Size
		{
			get { return _size; }
			set { SetProperty(ref _size, value); }
		}
		/// <summary>
		///     Current status of the object. Various operational and nonoperational statuses can be defined. Operational statuses include: "OK", "Degraded", and
		///     "Pred Fail" (an element, such as a SMART-enabled hard disk drive, may be functioning properly but predicting a failure in the near future).
		///     Nonoperational statuses include: "Error", "Starting", "Stopping", and "Service". The latter, "Service", could apply during mirror-resilvering of
		///     a disk, reload of a user permissions list, or other administrative work. Not all such work is online, yet the managed element is neither "OK" nor
		///     in one of the other states.
		/// </summary>
		public string Status
		{
			get { return _status; }
			set { SetProperty(ref _status, value); }
		}

		/// <summary>Creates a part from the <see cref="CsGlobal" /> hardware section.</summary>
		public static CsopV1PartDiskDriveDevice From(CsgDiskDriveDevice device)
		{
			var rv = new CsopV1PartDiskDriveDevice();

			rv.Index = device.Index;
			rv.InterfaceType = device.InterfaceType;
			rv.Manufacturer = device.Manufacturer;
			rv.Model = device.Model;
			rv.FirmwareRevision = device.FirmwareRevision;
			rv.Capabilities = device.Capabilitys;
			rv.MediaLoaded = device.MediaLoaded;
			rv.MediaType = device.MediaType;
			rv.PartitionCount = device.PartitionCount;
			rv.SerialNumber = device.SerialNumber;
			rv.Size = device.Size;
			rv.Status = device.Status;

			return rv;
		}
	}
}