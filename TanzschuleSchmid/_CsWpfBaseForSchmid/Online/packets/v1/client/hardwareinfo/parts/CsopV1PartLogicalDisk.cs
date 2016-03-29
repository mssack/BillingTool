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
	public class CsopV1PartLogicalDisk : Base, CsoPacket.IPart
	{
		private string _deviceId;
		private CsgLogicalDisk.DriveTypes _driveType;
		private string _fileSystem;
		private UInt64 _freeSpace;
		private string _name;
		private UInt64 _size;
		private string _volumeName;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			DeviceId = reader.String();
			DriveType = (CsgLogicalDisk.DriveTypes) reader.UInt32();
			FileSystem = reader.String();
			Size = reader.UInt64();
			FreeSpace = reader.UInt64();
			Name = reader.String();
			VolumeName = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.String(DeviceId);
			writer.UInt32((uint) DriveType);
			writer.String(FileSystem);
			writer.UInt64(Size);
			writer.UInt64(FreeSpace);
			writer.String(Name);
			writer.String(VolumeName);
		}
		#endregion


		/// <summary>Unique identifier of the logical disk from other devices on the system.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>Numeric value that corresponds to the type of disk drive this logical disk represents.</summary>
		public CsgLogicalDisk.DriveTypes DriveType
		{
			get { return _driveType; }
			set { SetProperty(ref _driveType, value); }
		}
		/// <summary>File system on the logical disk. Example: NTFS</summary>
		public string FileSystem
		{
			get { return _fileSystem; }
			set { SetProperty(ref _fileSystem, value); }
		}
		/// <summary>Size of the disk drive.</summary>
		public UInt64 Size
		{
			get { return _size; }
			set { SetProperty(ref _size, value); }
		}
		/// <summary>Space, in bytes, available on the logical disk.</summary>
		public UInt64 FreeSpace
		{
			get { return _freeSpace; }
			set { SetProperty(ref _freeSpace, value); }
		}
		/// <summary>Label by which the object is known.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Volume name of the logical disk. </summary>
		public string VolumeName
		{
			get { return _volumeName; }
			set { SetProperty(ref _volumeName, value); }
		}

		/// <summary>Creates a part from the <see cref="CsGlobal" /> hardware section.</summary>
		public static CsopV1PartLogicalDisk From(CsgLogicalDisk logicalDisk)
		{
			var rv = new CsopV1PartLogicalDisk();

			rv.DeviceId = logicalDisk.DeviceId;
			rv.DriveType = logicalDisk.DriveType;
			rv.FileSystem = logicalDisk.FileSystem;
			rv.Size = logicalDisk.Size;
			rv.FreeSpace = logicalDisk.FreeSpace;
			rv.Name = logicalDisk.Name;
			rv.VolumeName = logicalDisk.VolumeName;


			return rv;
		}
	}
}