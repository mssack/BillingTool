// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.ObjectModel;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary> represents a physical disk drive as seen by a computer running the Windows operating system.</summary>
	[Serializable]
	public class CsgDiskDriveDevice : Base
	{
		internal readonly ObservableCollection<CsgDiskPartition> _partitions = new ObservableCollection<CsgDiskPartition>();
		private string[] _capabilitys;
		private string _deviceId;
		private string _firmwareRevision;
		private UInt32 _index;
		private string _interfaceType;
		private string _manufacturer;
		private bool _mediaLoaded;
		private string _mediaType;
		private string _model;
		private UInt32 _partitionCount;
		private ReadOnlyObservableCollection<CsgDiskPartition> _readOnlyPartitions;
		private string _serialNumber;
		private UInt64 _size;
		private string _status;

		private CsgDiskDriveDevice()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return InterfaceType + " - " + Model + " - " + MediaType;
		}
		#endregion


		/// <summary>Unique identifier of the disk drive with other devices on the system.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			private set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>
		///     Physical drive number of the given drive. This property is filled by the GetDriveMapInfo method. A value of 0xFF indicates that the given drive
		///     does not map to a physical drive.
		/// </summary>
		public UInt32 Index
		{
			get { return _index; }
			private set { SetProperty(ref _index, value); }
		}
		/// <summary>Interface type of physical disk drive. Values can be SCSI, HDC, IDE, USB, 1394 </summary>
		public string InterfaceType
		{
			get { return _interfaceType; }
			private set { SetProperty(ref _interfaceType, value); }
		}
		/// <summary>Name of the disk drive manufacturer.</summary>
		public string Manufacturer
		{
			get { return _manufacturer; }
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Manufacturer's model number of the disk drive.</summary>
		public string Model
		{
			get { return _model; }
			private set { SetProperty(ref _model, value); }
		}
		/// <summary>Revision for the disk drive firmware that is assigned by the manufacturer.</summary>
		public string FirmwareRevision
		{
			get { return _firmwareRevision; }
			private set { SetProperty(ref _firmwareRevision, value); }
		}
		/// <summary>
		///     Array of capabilities of the media access device. For example, the device may support random access (3), removable media (7), and automatic
		///     cleaning (9).
		/// </summary>
		public string[] Capabilitys
		{
			get { return _capabilitys; }
			private set { SetProperty(ref _capabilitys, value); }
		}
		/// <summary>
		///     If True, the media for a disk drive is loaded, which means that the device has a readable file system and is accessible. For fixed disk drives,
		///     this property will always be TRUE.
		/// </summary>
		public bool MediaLoaded
		{
			get { return _mediaLoaded; }
			private set { SetProperty(ref _mediaLoaded, value); }
		}
		/// <summary>
		///     Type of media used or accessed by this device. Values: 'External hard disk media', 'Removable media other than floppy', 'Fixed hard disk media',
		///     'Format is unknown', 'Removable media ("Removable media other than floppy")', 'Fixed hard disk ("Fixed hard disk media")', 'Unknown ("Format is
		///     unknown")'
		/// </summary>
		public string MediaType
		{
			get { return _mediaType; }
			private set { SetProperty(ref _mediaType, value); }
		}
		/// <summary>Number of partitions on this physical disk drive that are recognized by the operating system.</summary>
		public UInt32 PartitionCount
		{
			get { return _partitionCount; }
			private set { SetProperty(ref _partitionCount, value); }
		}
		/// <summary>Number allocated by the manufacturer to identify the physical media. </summary>
		public string SerialNumber
		{
			get { return _serialNumber; }
			private set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>
		///     Size of the disk drive. It is calculated by multiplying the total number of cylinders, tracks in each cylinder, sectors in each track, and bytes
		///     in each sector.
		/// </summary>
		public UInt64 Size
		{
			get { return _size; }
			private set { SetProperty(ref _size, value); }
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
			private set { SetProperty(ref _status, value); }
		}
		/// <summary>Gets or sets the Partitions.</summary>
		public ReadOnlyObservableCollection<CsgDiskPartition> Partitions
		{
			get
			{
				CsGlobal.Computer.DiskDrive.CollectDiscPartitions(true);
				return _readOnlyPartitions ?? (_readOnlyPartitions = new ReadOnlyObservableCollection<CsgDiskPartition>(_partitions));
			}
		}

		internal static CsgDiskDriveDevice FromManagementObject(ManagementObject mo)
		{
			var dd = new CsgDiskDriveDevice();
			dd.Load(mo);
			return dd;
		}

		internal void Load(ManagementObject mo)
		{
			DeviceId = mo.TryGet<string>("DeviceID");
			Index = mo.TryGet<UInt32>("Index");
			InterfaceType = mo.TryGet<string>("InterfaceType");
			Manufacturer = mo.TryGet<string>("Manufacturer");
			Model = mo.TryGet<string>("Model");
			FirmwareRevision = mo.TryGet<string>("FirmwareRevision");
			Capabilitys = mo.TryGet<string[]>("CapabilityDescriptions");
			MediaLoaded = mo.TryGet<bool>("MediaLoaded");
			MediaType = mo.TryGet<string>("MediaType");
			PartitionCount = mo.TryGet<UInt32>("Partitions");
			SerialNumber = mo.TryGet<string>("SerialNumber");
			Size = mo.TryGet<UInt64>("Size");
			Status = mo.TryGet<string>("Status");
		}
	}
}