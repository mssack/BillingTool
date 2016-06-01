// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents a data source that resolves to an actual local storage device on a computer system running Windows.</summary>
	[Serializable]
	public class CsgLogicalDisk : Base
	{
		private string _deviceId;
		private DriveTypes _driveType;
		private string _fileSystem;
		private UInt64 _freeSpace;
		private string _name;
		private UInt64 _size;
		private string _volumeName;

		private CsgLogicalDisk()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Name + " - " + VolumeName;
		}
		#endregion


		/// <summary>Unique identifier of the logical disk from other devices on the system.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			private set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>Numeric value that corresponds to the type of disk drive this logical disk represents.</summary>
		public DriveTypes DriveType
		{
			get { return _driveType; }
			private set { SetProperty(ref _driveType, value); }
		}
		/// <summary>File system on the logical disk. Example: NTFS</summary>
		public string FileSystem
		{
			get { return _fileSystem; }
			private set { SetProperty(ref _fileSystem, value); }
		}
		/// <summary>Size of the disk drive.</summary>
		public UInt64 Size
		{
			get { return _size; }
			private set { SetProperty(ref _size, value); }
		}
		/// <summary>Space, in bytes, available on the logical disk.</summary>
		public UInt64 FreeSpace
		{
			get { return _freeSpace; }
			private set { SetProperty(ref _freeSpace, value); }
		}
		/// <summary>Label by which the object is known.</summary>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>Volume name of the logical disk. </summary>
		public string VolumeName
		{
			get { return _volumeName; }
			private set { SetProperty(ref _volumeName, value); }
		}

		internal void Load(ManagementObject mo)
		{
			DeviceId = mo.TryGet<string>("DeviceID");
			DriveType = (DriveTypes) mo.TryGet<uint>("DriveType");
			FileSystem = mo.TryGet<string>("FileSystem");
			Size = mo.TryGet<UInt64>("Size");
			FreeSpace = mo.TryGet<UInt64>("FreeSpace");
			Name = mo.TryGet<string>("Name");
			VolumeName = mo.TryGet<string>("VolumeName");
		}

		internal static CsgLogicalDisk FromManagementObject(ManagementObject mo)
		{
			var logicalDisk = new CsgLogicalDisk();
			logicalDisk.Load(mo);
			return logicalDisk;
		}



		/// <summary>Numeric value that corresponds to the type of disk drive this logical disk represents.</summary>
		public enum DriveTypes : uint
		{
			/// <summary>Unknown</summary>
			Unknown = 0,
			/// <summary>NoRootDirectory</summary>
			NoRootDirectory = 1,
			/// <summary>RemovableDisk</summary>
			RemovableDisk = 2,
			/// <summary>LocalDisk</summary>
			LocalDisk = 3,
			/// <summary>NetworkDrive</summary>
			NetworkDrive = 4,
			/// <summary>CompactDisc</summary>
			CompactDisc = 5,
			/// <summary>RAMDisk</summary>
			RamDisk = 6,
		}
	}
}